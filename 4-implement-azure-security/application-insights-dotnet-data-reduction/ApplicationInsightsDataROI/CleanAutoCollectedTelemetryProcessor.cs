namespace ApplicationInsightsDataROI
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    /// <summary>
    /// Telemetry processor that cleans the standard context for traces.
    /// </summary>
    internal class CleanAutoCollectedTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanAutoCollectedTelemetryProcessor"/> class.
        /// </summary>
        /// <param name="next">Next telemetry processor in the chain.</param>
        public CleanAutoCollectedTelemetryProcessor(ITelemetryProcessor next)
        {
            this.next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item is TraceTelemetry)
            {
                // no need to correlate high-volume traces with other telemetry:
                item.Context.Operation.Name = string.Empty;
                item.Context.User.Id = string.Empty;
                item.Context.Session.Id = string.Empty;
            }

            this.next.Process(item);
        }
    }
}
