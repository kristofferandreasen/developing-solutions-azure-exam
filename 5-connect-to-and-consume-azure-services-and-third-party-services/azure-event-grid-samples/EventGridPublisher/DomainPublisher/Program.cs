//----------------------------------------------------------------------------------
// Microsoft Azure EventGrid Team
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace EventGridPublisher
{
    // This captures the "Data" portion of an EventGridEvent on a domain
    class ContosoItemReceivedEventData
    {
        [JsonProperty(PropertyName = "itemSku")]
        public string ItemSku { get; set; }

        // [JsonProperty(PropertyName = "color1\\.color2")]
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        [JsonProperty(PropertyName = "model")]
        public int Model { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Enter values for <domain-name> and <region>. You can find this domain endpoint value
            // in the "Overview" section in the "Event Grid Domains" blade in Azure Portal.
            string domainEndpoint = "https://<YOUR-DOMAIN-NAME>.<REGION-NAME>-1.eventgrid.azure.net/api/events";

            // TODO: Enter value for <domain-key>. You can find this in the "Access Keys" section in the
            // "Event Grid Domains" blade in Azure Portal.
            string domainKey = "<YOUR-DOMAIN-KEY>";

            string domainHostname = new Uri(domainEndpoint).Host;
            TopicCredentials domainKeyCredentials = new TopicCredentials(domainKey);
            EventGridClient client = new EventGridClient(domainKeyCredentials);

            client.PublishEventsAsync(domainHostname, GetEventsList()).GetAwaiter().GetResult();
            Console.Write("Published events to Event Grid domain.");
            Console.ReadLine();
        }

        static IList<EventGridEvent> GetEventsList()
        {
            List<EventGridEvent> eventsList = new List<EventGridEvent>();

            for (int i = 0; i < 1; i++)
            {
                eventsList.Add(new EventGridEvent()
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "Contoso.Items.ItemReceived",

                    // TODO: Specify the name of the topic (under the domain) to which this event is destined for.
                    // Currently using a topic name "domaintopic0"
                    Topic = $"domaintopic{i}",
                    Data = new ContosoItemReceivedEventData()
                    {
                        ItemSku = "Contoso Item SKU #1",
                        Color = "green",
                        Model = 11
                    },
                    EventTime = DateTime.Now,
                    Subject = "BLUE",
                    DataVersion = "2.0"
                });
            }

            return eventsList;
        }
    }
}

