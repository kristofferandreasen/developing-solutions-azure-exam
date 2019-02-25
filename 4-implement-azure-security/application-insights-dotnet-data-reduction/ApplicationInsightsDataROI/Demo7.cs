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
    using Microsoft.ApplicationInsights.WindowsServer.Channel.Implementation;
    using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;

    internal class Demo7
    {
        public static async Task RunAsync(CancellationToken token)
        {
            // set Instrumentation Key
            var configuration = new TelemetryConfiguration();
            configuration.InstrumentationKey = "4282169f-e83f-46f7-b38a-436087fa856d";

            // automatically correlate all telemetry data with request
            configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());

            // initialize state for the telemetry size calculation
            var collectedItems = new ProcessedItems();
            var sentItems = new ProcessedItems();

            // build telemetry processing pipeline
            configuration.TelemetryProcessorChainBuilder

                // this telemetry processor will be executed first for all telemetry items to calculate the size and # of items
                .Use((next) => { return new SizeCalculatorTelemetryProcessor(next, collectedItems); })

               .Use((next) =>
               {
                   return new AggressivelySampleFastRequests(next)
                   {
                       InitialSamplingPercentage = 5,
                       MaxSamplingPercentage = 5,
                       MinSamplingPercentage = 5,
                   };
               })

                .Use((next) =>
                {
                    return new AggressivelySampleFastDependencies(next)
                    {
                        InitialSamplingPercentage = 5,
                        MaxSamplingPercentage = 5,
                        MinSamplingPercentage = 5,
                    };
                })

                // this is a standard adaptive sampling telemetry processor that will sample in/out any telemetry item it receives
                .Use((next) =>
                {
                    return new AdaptiveSamplingTelemetryProcessor(next)
                    {
                        InitialSamplingPercentage = 25,
                        MaxSamplingPercentage = 25,
                        MinSamplingPercentage = 25,
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
                iteration++;

                var t1 = new Task(() =>
               {
                   using (var operation = client.StartOperation<RequestTelemetry>("Slow request"))
                   {
                       client.TrackEvent("test", new Dictionary<string, string>() { { "iteration", iteration.ToString() } });
                       client.TrackTrace($"Iteration {iteration} happened", SeverityLevel.Information);

                       using (var fastDependency = client.StartOperation<DependencyTelemetry>("Fast dependency"))
                       {
                           Thread.Sleep(TimeSpan.FromMilliseconds(2));
                       }

                       using (var slowDependency = client.StartOperation<DependencyTelemetry>("slow dependency"))
                       {
                           Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                       }

                       client.StopOperation(operation);
                       Console.WriteLine($"Iteration {iteration}. Elapsed time: {operation.Telemetry.Duration}. Collected Telemetry: {collectedItems.Size}/{collectedItems.Count}. Sent Telemetry: {sentItems.Size}/{sentItems.Count}. Ratio: {1.0 * collectedItems.Size / sentItems.Size}");

                       client.TrackMetric("[RAW] Reduction Size", collectedItems.Size - sentItems.Size);
                   }
               });

                var t2 = new Task(() =>
                {
                    using (var operation = client.StartOperation<RequestTelemetry>("Fast request"))
                    {
                        client.TrackEvent("test", new Dictionary<string, string>() { { "iteration", iteration.ToString() } });
                        client.TrackTrace($"Iteration {iteration} happened", SeverityLevel.Information);

                        using (var fastDependency = client.StartOperation<DependencyTelemetry>("Fast dependency"))
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(2));
                        }

                        using (var slowDependency = client.StartOperation<DependencyTelemetry>("slow dependency"))
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(400));
                        }

                        client.StopOperation(operation);
                        Console.WriteLine($"Iteration {iteration}. Elapsed time: {operation.Telemetry.Duration}. Collected Telemetry: {collectedItems.Size}/{collectedItems.Count}. Sent Telemetry: {sentItems.Size}/{sentItems.Count}. Ratio: {1.0 * collectedItems.Size / sentItems.Size}");

                        client.TrackMetric("[RAW] Reduction Size", collectedItems.Size - sentItems.Size);
                    }
                });

                t1.Start();
                t2.Start();
                Task.WaitAll(new Task[] { t1, t2 });
            }
        }
    }
}
