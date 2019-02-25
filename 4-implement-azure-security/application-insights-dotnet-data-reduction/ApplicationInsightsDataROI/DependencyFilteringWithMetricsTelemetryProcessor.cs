namespace ApplicationInsightsDataROI
{
    using System;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    internal class DependencyFilteringWithMetricsTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor next;
        private readonly Metric numberOfDependencies;
        private readonly Metric dependenciesDuration;

        public DependencyFilteringWithMetricsTelemetryProcessor(ITelemetryProcessor next, TelemetryConfiguration configuration)
        {
            this.next = next;
            var client = new TelemetryClient(configuration);
            this.numberOfDependencies = client.GetMetric("# of dependencies", "type");
            this.dependenciesDuration = client.GetMetric("dependencies duration (ms)", "type");

        }

        public void Process(ITelemetry item)
        {
            // check telemetry type
            if (item is DependencyTelemetry)
            {
                var d = item as DependencyTelemetry;

                this.numberOfDependencies.TrackValue(1, d.Type);
                this.dependenciesDuration.TrackValue(d.Duration.TotalMilliseconds, d.Type);

                if (d.Duration < TimeSpan.FromMilliseconds(100))
                {
                    // if dependency duration > 100 ms then stop telemetry
                    // processing and return from the pipeline
                    return;
                }
            }

            this.next.Process(item);
        }
    }
}
