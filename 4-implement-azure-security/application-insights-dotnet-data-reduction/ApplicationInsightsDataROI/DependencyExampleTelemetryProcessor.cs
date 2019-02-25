namespace ApplicationInsightsDataROI
{
    using System;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    internal class DependencyExampleTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor next;

        public DependencyExampleTelemetryProcessor(ITelemetryProcessor next)
        {
            this.next = next;
        }

        public void Process(ITelemetry item)
        {
            // check telemetry type
            if (item is DependencyTelemetry)
            {
                var r = item as DependencyTelemetry;
                if (r.Duration > TimeSpan.FromMilliseconds(100))
                {
                    // if dependency duration > 100 ms then "sample in"
                    // this telemetry by setting sampling percentage to 100
                    ((ISupportSampling)item).SamplingPercentage = 100;
                }
            }

            // continue with the next telemetry processor
            this.next.Process(item);
        }
    }
}
