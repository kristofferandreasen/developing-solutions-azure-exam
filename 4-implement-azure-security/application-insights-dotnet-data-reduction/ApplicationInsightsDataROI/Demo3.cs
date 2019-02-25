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

    internal class Demo3
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

            // enable sampling
            configuration.TelemetryProcessorChainBuilder

                // this telemetry processor will be executed first for all telemetry items to calculate the size and # of items
                .Use((next) => { return new SizeCalculatorTelemetryProcessor(next, collectedItems); })

                // exemplify dependency telemetry that is faster than 100 ms
                .Use((next) => { return new DependencyExampleTelemetryProcessor(next); })

                // sample all telemetry to 10%
                .Use((next) =>
                {
                    return new SamplingTelemetryProcessor(next)
                    {
                        SamplingPercentage = 10,
                    };
                })

                // this telemetry processor will be execuyted ONLY when telemetry is sampled in
                .Use((next) => { return new SizeCalculatorTelemetryProcessor(next, sentItems); })
                .Build();

            var client = new TelemetryClient(configuration);

            var iteration = 0;
            var http = new HttpClient();

            while (!token.IsCancellationRequested)
            {
                using (var operation = client.StartOperation<RequestTelemetry>("Process item"))
                {
                    client.TrackEvent("IterationStarted", properties: new Dictionary<string, string>() { { "iteration", iteration.ToString() } });
                    client.TrackTrace($"Iteration {iteration} started", SeverityLevel.Information);

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
                    iteration++;
                }
            }
        }
    }
}
