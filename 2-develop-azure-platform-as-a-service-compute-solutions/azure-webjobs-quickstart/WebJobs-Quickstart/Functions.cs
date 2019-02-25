using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using WebJobs_Quickstart.Models;
using Microsoft.Azure.WebJobs.Host;

namespace WebJobs_Quickstart
{
    public class Functions
    {
        // This function will be triggered on a schedule determined by the cron expression provided. In this case it will run every minute.
        // When this trigger is invoked, it enqueues a Message on the MessageQueue.
        // To learn more about TimerTriggers, go to https://github.com/Azure/azure-webjobs-sdk-extensions#timertrigger
        public static void TimedQueueMessage(
            [TimerTrigger("0 * * * * *", RunOnStartup = true)] TimerInfo timer,
            [Queue("MessageQueue")] out Message message,
            TraceWriter log
        )
        {
            // Create a new message
            message = new Message()
            {
                message = timer.FormatNextOccurrences(1)
            };
            log.Verbose("New message enqueued");
        }

        // This function will get triggered/executed when a new message is written to the Message Queue.
        // It's not doing any real work here, but it could do some work like making a request to a service
        // like Microsoft Graph API, or doing sentiment analysis and outputting table storage.
        // To learn more about Queues, go to https://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk-storage-queues-how-to/
        public static void ProcessQueueMessage([QueueTrigger("MessageQueue")] Message message, TraceWriter log)
        {
            log.Verbose("Message Received:");
            log.Verbose(message.message);
        }
    }
}
