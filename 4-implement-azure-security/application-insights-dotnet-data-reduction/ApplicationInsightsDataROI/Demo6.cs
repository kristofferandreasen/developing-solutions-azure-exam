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
    using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;

    internal class Demo6
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

            QuickPulseTelemetryProcessor processor = null;

            // enable Live Metrics
            configuration.TelemetryProcessorChainBuilder

                // adding LiveMetrics telemetry processor
                .Use((next) =>
                {
                    processor = new QuickPulseTelemetryProcessor(next);
                    return processor;
                })

                .Build();

            var quickPulse = new QuickPulseTelemetryModule();
            quickPulse.Initialize(configuration);
            quickPulse.RegisterTelemetryProcessor(processor);

            var client = new TelemetryClient(configuration);

            var iteration = 0;
            var http = new HttpClient();
            var rnd = new Random();

            while (!token.IsCancellationRequested)
            {
                iteration++;

                using (var operation = client.StartOperation<RequestTelemetry>("Process item"))
                {
                    client.TrackEvent("test", new Dictionary<string, string>() { { "iteration", iteration.ToString() } });
                    client.TrackTrace($"Iteration {iteration} happened", SeverityLevel.Information);

                    var status = rnd.Next() < rnd.Next();
                    try
                    {
                        if (status)
                        {
                            throw new Exception($"Failure during processing of iteration #{iteration}");
                        }

                        await http.GetStringAsync("http://bing.com");
                    }
                    catch (Exception exc)
                    {
                        client.TrackException(exc);
                    }
                    finally
                    {
                        client.StopOperation<RequestTelemetry>(operation);
                        operation.Telemetry.Success = status;

                        Console.WriteLine($"Iteration {iteration}. Elapsed time: {operation.Telemetry.Duration}. Success: {operation.Telemetry.Success}");
                    }
                }
            }
        }
    }
}
