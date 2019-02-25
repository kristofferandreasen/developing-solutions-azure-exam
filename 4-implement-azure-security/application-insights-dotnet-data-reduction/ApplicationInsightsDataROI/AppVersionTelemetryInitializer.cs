namespace ApplicationInsightsDataROI
{
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;

    /// <summary>
    /// Telemetry initializer that sets the application version.
    /// </summary>
    internal class AppVersionTelemetryInitializer : ITelemetryInitializer
    {
        /// <summary>
        /// Initialize the telemetry item with the application version.
        /// </summary>
        /// <param name="telemetry">Telemetry item to initialize.</param>
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = "1.2.3";
        }
    }
}
