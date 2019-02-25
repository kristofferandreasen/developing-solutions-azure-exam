// 
// Copyright (c) Microsoft.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.EventGrid;
using Microsoft.Azure.Management.EventGrid.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Rest;

namespace EGManageStorageEventSubscriptions
{
    /// <summary>
    /// Azure EventGrid Management Sample - Demonstrate how to create and manage EventGrid event subscriptions to a storage account
    /// using EventGrid Management SDK. This sample will also demonstrate using a hybridconnection as the destination of the event subscription.
    /// 
    /// Documentation References:
    /// - EventGrid .NET SDK documentation - https://docs.microsoft.com/en-us/dotnet/api/overview/azure/eventgrid?view=azure-dotnet
    /// </summary>
    public class Program
    {
        // Enter the Azure subscription ID you want to use for this sample.
        const string SubscriptionId = "replace-with-your-subscription-id";

        // In this sample, we will be creating an event subscription to this storage account.
        // Specify the Azure resource ID of an already existing storage account. The account must be 
        // a General Purpose V2 Storage account or a Blob Storage account.
        // This should be in the format /subscriptions/id/resourceGroups/rg/providers/Microsoft.Storage/storageAccounts/account1
        // More info on Storage as an event source is at https://docs.microsoft.com/en-us/azure/event-grid/event-sources#storage
        const string StorageAccountResourceId = "replace-with-your-storage-account-resource-id";

        // In this sample, we will be demonstrating using a hybridconnection as the destination for the event subscription.
        // This should be in the format /subscriptions/id/resourceGroups/rg/providers/Microsoft.Relay/namespaces/namespace1/hybridConnections/test
        // More info on HybridConnection as an event handler is at https://docs.microsoft.com/en-us/azure/event-grid/event-handlers#hybrid-connections
        const string HybridConnectionResourceId = "replace-with-your-hybrid-connection-resource-id";

        // To run the sample, you must first create an Azure service principal. To create the service principal, follow one of these guides:
        // Azure Portal: https://azure.microsoft.com/documentation/articles/resource-group-create-service-principal-portal/)
        // PowerShell: https://azure.microsoft.com/documentation/articles/resource-group-authenticate-service-principal/
        // Azure CLI: https://azure.microsoft.com/documentation/articles/resource-group-authenticate-service-principal-cli/
        // Creating the service principal will generate the values you need to specify for the constants below.

        // Use the values generated when you created the Azure service principal.
        const string ApplicationId = "replace-with-your-application-id";
        const string Password = "replace-with-your-application-password";
        const string TenantId = "replace-with-your-tenant-id";

        const string EventSubscriptionName = "EventSubscription1";

        static async Task<string> GetAuthorizationHeaderAsync()
        {
            ClientCredential cc = new ClientCredential(ApplicationId, Password);
            var context = new AuthenticationContext("https://login.windows.net/" + TenantId);
            var result = await context.AcquireTokenAsync("https://management.azure.com/", cc);

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token. Please verify the values for your applicationId, Password, and Tenant.");
            }

            string token = result.AccessToken;
            return token;
        }

        public static void Main(string[] args)
        {
            PerformEventSubscriptionOperationsForStorageEvents().Wait();
        }

        static async Task PerformEventSubscriptionOperationsForStorageEvents()
        {
            string token = await GetAuthorizationHeaderAsync();
            TokenCredentials credential = new TokenCredentials(token);
            ResourceManagementClient resourcesClient = new ResourceManagementClient(credential)
            {
                SubscriptionId = SubscriptionId
            };

            EventGridManagementClient eventGridManagementClient = new EventGridManagementClient(credential)
            {
                SubscriptionId = SubscriptionId,
                LongRunningOperationRetryTimeout = 2
            };

            try
            {
                // Register the EventGrid Resource Provider with the Subscription
                await RegisterEventGridResourceProviderAsync(resourcesClient);

                // Create an event subscription to the storage account
                await CreateEventGridEventSubscriptionAsync(EventSubscriptionName, eventGridManagementClient);

                // Delete the event subscription
                await DeleteEventGridEventSubscriptionAsync(EventSubscriptionName, eventGridManagementClient);

                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        public static async Task RegisterEventGridResourceProviderAsync(ResourceManagementClient resourcesClient)
        {
            Console.WriteLine("Registering EventGrid Resource Provider with subscription...");
            await resourcesClient.Providers.RegisterAsync("Microsoft.EventGrid");
            Console.WriteLine("EventGrid Resource Provider registered.");
        }

        static async Task CreateEventGridEventSubscriptionAsync(string eventSubscriptionName, EventGridManagementClient eventGridMgmtClient)
        {
            Console.WriteLine($"Creating an event subscription to storage account {StorageAccountResourceId} with destination as {HybridConnectionResourceId}");

            EventSubscription eventSubscription = new EventSubscription()
            {
                Destination = new HybridConnectionEventSubscriptionDestination()
                {
                    ResourceId = HybridConnectionResourceId
                },
                // The below are all optional settings
                EventDeliverySchema = EventDeliverySchema.EventGridSchema,
                Filter = new EventSubscriptionFilter()
                {
                    IncludedEventTypes = new List<string>()
                    {
                        "Microsoft.Storage.BlobCreatedEvent"
                    },
                    IsSubjectCaseSensitive = false,
                    SubjectBeginsWith = "/blobServices/default/containers/container1",
                    SubjectEndsWith = ".jpg"
                }
            };

            EventSubscription createdEventSubscription = await eventGridMgmtClient.EventSubscriptions.CreateOrUpdateAsync(StorageAccountResourceId, eventSubscriptionName, eventSubscription);
            Console.WriteLine("EventGrid event subscription created with name " + createdEventSubscription.Name);
        }

        static async Task DeleteEventGridEventSubscriptionAsync(string eventSubscriptionName, EventGridManagementClient eventGridMgmtClient)
        {
            Console.WriteLine($"Deleting event subscription {eventSubscriptionName}...");

            await eventGridMgmtClient.EventSubscriptions.DeleteAsync(StorageAccountResourceId, eventSubscriptionName);
            Console.WriteLine("Event subcription " + eventSubscriptionName + " deleted");
        }
    }
}