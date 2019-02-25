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

    internal class Demo4
    {
        public static async Task RunAsync(CancellationToken token)
        {
            // set Instrumentation Key
            var configuration = new TelemetryConfiguration();
            configuration.InstrumentationKey = "421095c4-8c1f-4a78-a5a3-58808b5fb0c5";

            var telemetryChannel = new ServerTelemetryChannel();
            telemetryChannel.Initialize(configuration);
            configuration.TelemetryChannel = telemetryChannel;

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

                // filter telemetry that is faster than 100 ms
                // .Use((next) => { return new DependencyFilteringTelemetryProcessor(next); })

                // filter telemetry that is faster than 100 ms
                // extract metrics
                .Use((next) => { return new DependencyFilteringWithMetricsTelemetryProcessor(next, configuration); })

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
