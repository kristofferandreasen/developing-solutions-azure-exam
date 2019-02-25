using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace WebJobs_Quickstart
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Be sure to add your storage account connection strings in the App.config when running locally
        // Don't check in your conection strings to Git, though
        static void Main()
        {
            // This is the config object we pass to the JobHost
            JobHostConfiguration config = new JobHostConfiguration();
            
            // Have our Continuous Job host log all our traces up to Verbose
            config.Tracing.ConsoleLevel = System.Diagnostics.TraceLevel.Verbose;

            // Turn on Timer Triggers
            config.UseTimers();

            // Initialize host with config
            var host = new JobHost(config);

            // The following code ensures that the WebJob will be running continuously
            host.RunAndBlock();
        }
    }
}
