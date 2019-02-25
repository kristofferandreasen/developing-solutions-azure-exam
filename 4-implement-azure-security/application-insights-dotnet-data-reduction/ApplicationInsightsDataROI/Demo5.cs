namespace ApplicationInsightsDataROI
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.DependencyCollector;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;

    internal class Demo5
    {
        public static async Task RunAsync(CancellationToken token)
        {
            // set Instrumentation Key
            var configuration = new TelemetryConfiguration();
            configuration.InstrumentationKey = "fb8a0b03-235a-4b52-b491-307e9fd6b209";

            // automatically track dependency calls
            var dependencies = new DependencyTrackingTelemetryModule();
            dependencies.Initialize(configuration);

            // automatically correlate all telemetry data with request
            configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            // initialize state for the telemetry size calculation
            var collectedItems = new ProcessedItems();
            var sentItems = new ProcessedItems();

            // build telemetry processing pipeline
            configuration.TelemetryProcessorChainBuilder

                // this telemetry processor will be executed first for all telemetry items to calculate the size and # of items
                .Use((next) => { return new SizeCalculatorTelemetryProcessor(next, collectedItems); })

               // this is a standard fixed sampling processor that will let only 10%
               .Use((next) =>
               {
                   return new SamplingTelemetryProcessor(next)
                   {
                       IncludedTypes = "Dependency",
                       SamplingPercentage = 10,
                   };
               })

                // this is a standard adaptive sampling telemetry processor that will sample in/out any telemetry item it receives
                .Use((next) =>
                {
                    return new AdaptiveSamplingTelemetryProcessor(next)
                    {
                        ExcludedTypes = "Event", // exclude custom events from being sampled
                        MaxTelemetryItemsPerSecond = 1, // default: 5 calls/sec
                        SamplingPercentageIncreaseTimeout = TimeSpan.FromSeconds(1), // default: 2 min
                        SamplingPercentageDecreaseTimeout = TimeSpan.FromSeconds(1), // default: 30 sec
                        EvaluationInterval = TimeSpan.FromSeconds(1), // default: 15 sec
                        InitialSamplingPercentage = 25, // default: 100%
                    };
                })

                // this telemetry processor will be execuyted ONLY when telemetry is sampled in
                .Use((next) => { return new SizeCalculatorTelemetryProcessor(next, sentItems); })
                .Build();

            var client = new TelemetryClient(configuration);

            // configure metrics collection
            var reductionsize = client.GetMetric("Reduction Size");

            var iteration = 0;
            var http = new HttpClient();

            while (!token.IsCancellationRequested)
            {
                iteration++;

                using (var operation = client.StartOperation<RequestTelemetry>("Process item"))
                {
                    client.TrackEvent("test", new Dictionary<string, string>() { { "iteration", iteration.ToString() } });
                    client.TrackTrace($"Iteration {iteration} happened", SeverityLevel.Information);

                    try
                    {
                        await http.GetStringAsync("http://bing.com");
                    }
                    catch (Exception exc)
                    {
                        client.TrackException(exc);
                        operation.Telemetry.Success = false;
                    }

                    client.StopOperation(operation);
                    Console.WriteLine($"Iteration {iteration}. Elapsed time: {operation.Telemetry.Duration}. Collected Telemetry: {collectedItems.Size}/{collectedItems.Count}. Sent Telemetry: {sentItems.Size}/{sentItems.Count}. Ratio: {1.0 * collectedItems.Size / sentItems.Size}");

                    reductionsize.TrackValue(collectedItems.Size - sentItems.Size);
                    client.TrackMetric("[RAW] Reduction Size", collectedItems.Size - sentItems.Size);
                }
            }
        }
    }
}
