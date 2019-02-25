namespace ApplicationInsightsDataROI
{
    using System;
    using System.Diagnostics;
    using System.Security.Principal;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;

    internal class DefaultTelemetryInitializer : ITelemetryInitializer
    {
        private string sessionId = Guid.NewGuid().ToString();
        private Process currentProcess = Process.GetCurrentProcess();
        private string userid = WindowsIdentity.GetCurrent().Name;

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = "1.2.3";

            telemetry.Context.Cloud.RoleName = this.currentProcess.ProcessName;

            telemetry.Context.User.Id = this.userid;

            telemetry.Context.Session.Id = this.sessionId;
        }
    }
}
