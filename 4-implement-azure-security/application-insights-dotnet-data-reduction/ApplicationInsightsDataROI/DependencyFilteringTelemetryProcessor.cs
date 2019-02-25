namespace ApplicationInsightsDataROI
{
    using System;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    internal class DependencyFilteringTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor next;

        public DependencyFilteringTelemetryProcessor(ITelemetryProcessor next)
        {
            this.next = next;
        }

        public void Process(ITelemetry item)
        {
            // check telemetry type
            if (item is DependencyTelemetry)
            {
                var d = item as DependencyTelemetry;
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
