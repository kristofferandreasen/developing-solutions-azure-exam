namespace ApplicationInsightsDataROI
{
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.ApplicationInsights.Extensibility.Implementation;

    internal class SizeCalculatorTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor next;
        private ProcessedItems processedItems;

        public SizeCalculatorTelemetryProcessor(ITelemetryProcessor next, ProcessedItems items)
        {
            this.next = next;
            this.processedItems = items;
        }

        public void Process(ITelemetry item)
        {
            try
            {
                item.Sanitize();

                byte[] content = JsonSerializer.Serialize(new List<ITelemetry>() { item }, false);
                int size = content.Length;
                string json = Encoding.Default.GetString(content);

                this.processedItems.Size += size;
                this.processedItems.Count += 1;
            }
            finally
            {
                this.next.Process(item);
            }
        }
    }
}
