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
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;

namespace EGManageDomainsAndEventSubscriptions
{
    /// <summary>
    /// Azure EventGrid Management Sample - Demonstrate how to create and manage EventGrid eventsubscriptions using EventGrid Management SDK.
    ///
    /// Documentation References:
    /// - EventGrid .NET SDK documentation - https://docs.microsoft.com/en-us/dotnet/api/overview/azure/eventgrid?view=azure-dotnet
    /// </summary>
    public class Program
    {
        // Enter the Azure subscription ID you want to use for this sample.
        const string SubscriptionId = "<Azure-Subscription-Id>";

        // Specify a resource group name of your choice. Specifying a new value will create a new resource group.
        const string ResourceGroupName = "<Resource-Group-Name>";

        // Using a random domain name. Optionally, replace this with a domain name of your choice.
        static readonly string DomainName = "domainsample" + Guid.NewGuid().ToString().Substring(0, 8);

        // This sample creates an event subscription with a storage queue as a destination.
        // Replace the items in CAPS below with the information corresponding to your storage account.
        const string StorageAccountId = "/subscriptions/<AZURE-SUBSCRIPTION-ID>/resourceGroups/<RESOURCE-GROUP>/providers/Microsoft.Storage/storageAccounts/<STORAGE-ACCOUNT-NAME/";
        const string StorageQueueName = "<STORAGE-QUEUE-NAME>";

        // To run the sample, you must first create an Azure service principal. To create the service principal, follow one of these guides:
        // Azure Portal: https://azure.microsoft.com/documentation/articles/resource-group-create-service-principal-portal/)
        // PowerShell: https://azure.microsoft.com/documentation/articles/resource-group-authenticate-service-principal/
        // Azure CLI: https://azure.microsoft.com/documentation/articles/resource-group-authenticate-service-principal-cli/
        // Creating the service principal will generate the values you need to specify for the constants below.

        // Use the values generated when you created the Azure service principal.
        const string ApplicationId = "<APPLICATION-ID>";
        const string Password = "<PASSWORD>";
        const string TenantId = "<TENANT-ID>";

        const string EventSubscriptionName = "EventSubscription1";
        const string DefaultLocation = "westus";

        //The following method will enable you to use the token to create credentials
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
            PerformDomainAndEventSubscriptionOperations().Wait();
        }

        static async Task PerformDomainAndEventSubscriptionOperations()
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

                // Create a new resource group
                await CreateResourceGroupAsync(ResourceGroupName, resourcesClient);

                // Create a new Event Grid domain in a resource group
                await CreateEventGridDomainAsync(ResourceGroupName, DomainName, eventGridManagementClient);

                // Get the keys for the domain
                DomainSharedAccessKeys domainKeys = await eventGridManagementClient.Domains.ListSharedAccessKeysAsync(ResourceGroupName, DomainName);
                Console.WriteLine($"The key1 value of domain {DomainName} is: {domainKeys.Key1}");

                // Create an event subscription at the scope of a topic under the domain
                await CreateEventGridEventSubscriptionAsync(ResourceGroupName, DomainName, "domaintopic1", EventSubscriptionName, eventGridManagementClient, StorageAccountId, StorageQueueName);

                // Delete the event subscription created above
                await DeleteEventGridEventSubscriptionAsync(ResourceGroupName, DomainName, "domaintopic1", EventSubscriptionName, eventGridManagementClient);

                // Create an event subscription at the scope of the domain
                await CreateEventGridEventSubscriptionAsync(ResourceGroupName, DomainName, null, EventSubscriptionName, eventGridManagementClient, StorageAccountId, StorageQueueName);

                // Delete the event subscription created above
                await DeleteEventGridEventSubscriptionAsync(ResourceGroupName, DomainName, null, EventSubscriptionName, eventGridManagementClient);

                // Delete the EventGrid domain with the given domain name and a resource group
                await DeleteEventGridDomainAsync(ResourceGroupName, DomainName, eventGridManagementClient);

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

        static async Task CreateResourceGroupAsync(string rgname, ResourceManagementClient resourcesClient)
        {
            Console.WriteLine("Creating a resource group...");
            var resourceGroup = await resourcesClient.ResourceGroups.CreateOrUpdateAsync(
                    rgname,
                    new ResourceGroup
                    {
                        Location = DefaultLocation
                    });
            Console.WriteLine("Resource group created with name " + resourceGroup.Name);
        }

        static async Task CreateEventGridDomainAsync(string rgname, string domainName, EventGridManagementClient EventGridMgmtClient)
        {
            Console.WriteLine("Creating an EventGrid domain...");

            Dictionary<string, string> defaultTags = new Dictionary<string, string>
            {
                {"key1","value1"},
                {"key2","value2"}
            };

            Domain domain = new Domain()
            {
                Tags = defaultTags,
                Location = DefaultLocation,
                InputSchema = InputSchema.EventGridSchema,
                InputSchemaMapping = null
            };

            Domain createdDomain = await EventGridMgmtClient.Domains.CreateOrUpdateAsync(rgname, domainName, domain);
            Console.WriteLine("EventGrid domain created with name " + createdDomain.Name);
        }

        static async Task CreateEventGridEventSubscriptionAsync(string rgname, string domainName, string domainTopicName, string eventSubscriptionName, EventGridManagementClient eventGridMgmtClient, string storageAccountId, string storageQueueName)
        {
            string eventSubscriptionScope = await BuildEventSubscriptionScopeAsync(rgname, domainName, domainTopicName, eventGridMgmtClient);
            Console.WriteLine($"Creating an event subscription at the scope of {eventSubscriptionScope}...");

            EventSubscription eventSubscription = new EventSubscription()
            {
                Destination = new StorageQueueEventSubscriptionDestination()
                {
                    ResourceId = storageAccountId,
                    QueueName = storageQueueName
                },
                // The below are all optional settings
                EventDeliverySchema = EventDeliverySchema.EventGridSchema,
                Filter = new EventSubscriptionFilter()
                {
                    // By default, "All" event types are included
                    IsSubjectCaseSensitive = false,
                    SubjectBeginsWith = "",
                    SubjectEndsWith = ""
                }
            };

            EventSubscription createdEventSubscription = await eventGridMgmtClient.EventSubscriptions.CreateOrUpdateAsync(eventSubscriptionScope, eventSubscriptionName, eventSubscription);
            Console.WriteLine("EventGrid event subscription created with name " + createdEventSubscription.Name);
        }

        static async Task DeleteEventGridDomainAsync(string rgname, string domainName, EventGridManagementClient EventGridMgmtClient)
        {
            Console.WriteLine($"Deleting EventGrid domain {domainName} in resource group {rgname}");
            await EventGridMgmtClient.Domains.DeleteAsync(rgname, domainName);
            Console.WriteLine("EventGrid domain " + domainName + " deleted");
        }

        static async Task DeleteEventGridEventSubscriptionAsync(string rgname, string domainName, string domainTopicName, string eventSubscriptionName, EventGridManagementClient eventGridMgmtClient)
        {
            string eventSubscriptionScope = await BuildEventSubscriptionScopeAsync(rgname, domainName, domainTopicName, eventGridMgmtClient);
            Console.WriteLine($"Deleting event subscription {eventSubscriptionName} created at scope {eventSubscriptionScope}...");
            await eventGridMgmtClient.EventSubscriptions.DeleteAsync(eventSubscriptionScope, eventSubscriptionName);
            Console.WriteLine("Event subcription " + eventSubscriptionName + " deleted");
        }

        static async Task<string> BuildEventSubscriptionScopeAsync(string resourceGroupName, string domainName, string domainTopicName, EventGridManagementClient eventGridMgmtClient)
        {
            Domain domain = await eventGridMgmtClient.Domains.GetAsync(resourceGroupName, domainName);
            string eventSubscriptionScope;

            if (domainTopicName != null)
            {
                // The scope is a topic under the domain
                eventSubscriptionScope = domain.Id + $"/topics/{domainTopicName}";
            }
            else
            {
                // The scope is the domain
                eventSubscriptionScope = domain.Id;
            }

            return eventSubscriptionScope;
        }
    }
}