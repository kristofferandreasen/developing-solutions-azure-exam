namespace ApplicationInsightsDataROI
{
    using System;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            // Demo1.RunAsync(token).Wait(); // default AI model with request/dependency/exception/trace and event
            // Demo2.RunAsync(token).Wait(); // price calculation and fixed & adaptive sampling
            // Demo3.RunAsync(token).Wait(); // exemplification of dependencies
            // Demo4.RunAsync(token).Wait(); // filtering of dependencies
            // Demo5.RunAsync(token).Wait(); // metrics aggregation, channeling business telemetry into a different iKey and default context settings
            // Demo6.RunAsync(token).Wait(); // LiveMetrics enablement
            // Demo7.RunAsync(token).Wait(); // aggressive sampling instead of filtering

            Console.ReadKey();
        }
    }
}
