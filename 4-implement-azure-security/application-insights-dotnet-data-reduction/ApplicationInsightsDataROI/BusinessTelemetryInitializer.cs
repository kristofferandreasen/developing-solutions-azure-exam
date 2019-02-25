namespace ApplicationInsightsDataROI
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.ApplicationInsights.Extensibility;

    internal class BusinessTelemetryInitializer : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is EventTelemetry)
            {
                telemetry.Context.InstrumentationKey = "BUSINESS_TELEMETRY_KEY";
            }
        }
    }
}
