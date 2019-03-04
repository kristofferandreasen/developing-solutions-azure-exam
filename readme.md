![Exam AZ-203](exam203.png?raw=true "Exam AZ-203")

# Exam AZ-203: Developing Solutions for Microsoft Azure

This repository is meant to help you get certified in Microsoft Azure. It contains various resources and links to help you level up in developing applications for Microsoft Azure. Feel free to help out with new resources if you find any relevant ones.
The information in this repository is basedo on the skills measured in the exam.
You can read the details here on the Microsoft website: 
[Exam AZ-203: Developing Solutions for Microsoft Azure](https://www.microsoft.com/en-us/learning/exam-AZ-203.aspx)

## Getting Started
Azure can be quite intimidating because of the vast amount of services and options.
Therefore it would make sense to choose a specific learning path in order to learn how Azure can be used within the area you work in.
I would recommend everyone getting started with Azure to visit the [Microsoft Learn](https://docs.microsoft.com/en-us/learn/) portal. The portal is a great resource for getting familiar with services in Azure.

The exam consists of these areas:
* Develop Azure Infrastructure as a Service Compute Solutions (10-15%)
* Develop Azure Platform as a Service Compute Solutions (20-25%)
* Develop for Azure storage (15-20%)
* Implement Azure security (10-15%)
* Connect to and Consume Azure Services and Third-party Services (20-25%)

Each area will be described with links to the individual subjects.

## Develop Azure Infrastructure as a Service Compute Solutions (10-15%)

### Implement solutions that use virtual machines (VM)
* Provision VMs
  * [Create VM - Portal](https://docs.microsoft.com/en-us/azure/virtual-machines/windows/quick-create-portal)
  * [Create VM - Powershell](https://docs.microsoft.com/en-us/azure/virtual-machines/windows/quick-create-powershell)
  * [Create VM - CLI](https://docs.microsoft.com/en-us/azure/virtual-machines/windows/quick-create-cli)
  * [Pluralsight Course - Infrastructure](https://www.pluralsight.com/courses/implementing-virtual-machines-azure-infrastructure-70-533)

* Create ARM templates
  * [ARM - Structure and syntax](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-authoring-templates)
  * [ARM - Github Quickstart Repository](https://github.com/Azure/azure-quickstart-templates)
  * [ARM - Gallery](https://azure.microsoft.com/en-us/resources/templates/)
* Configure Azure Disk Encryption for VMs
  * [Azure Disk Encryption](https://docs.microsoft.com/en-us/azure/security/azure-security-disk-encryption-overview)

### Implement batch jobs by using Azure Batch Services
* Manage batch jobs by using Batch Service API
  * [Batch service](https://docs.microsoft.com/en-us/rest/api/batchservice/)
* Run a batch job by using Azure CLI, Azure portal, and other tools
  * [Run batch service with CLI](https://docs.microsoft.com/en-us/azure/batch/quick-create-cli)
* Write code to run an Azure Batch Services batch job
  * [Code a batch service](https://docs.microsoft.com/en-us/azure/batch/quick-run-dotnet)
  * [Run a parallel workload with Azure Batch using the .NET API](https://docs.microsoft.com/en-us/azure/batch/tutorial-parallel-dotnet)

### Create containerized solutions
* Create an Azure Managed Kubernetes Service (AKS) cluster
  * [Kubernetes - Quickstart - CLI](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough)
  * [Kubernetes - Quickstart - Portal](https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal)
* Create container images for solutions
  * [Container Registry - Tutorial](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-tutorial-quick-task)
  * [Automate image builds](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-tutorial-build-task)
* Publish an image to the Azure Container Registry
  * [Push Docker image](https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-docker-cli)
* Run containers by using Azure Container Instance or AKS
  * [Run container - tutorial](https://docs.microsoft.com/en-us/azure/container-instances/container-instances-quickstart-portal)

## Develop Azure Platform as a Service Compute Solutions (20-25%)

### Create Azure App Service Web Apps
* Create an Azure App Service Web App
  * [Create app service](https://docs.microsoft.com/en-us/azure/app-service/environment/app-service-web-how-to-create-a-web-app-in-an-ase)
* Create an Azure App Service background task by using WebJobs
  * [Create a webjob](https://docs.microsoft.com/en-us/azure/app-service/webjobs-create)
* Enable diagnostics logging
  * [Enable diagnostics logging](https://docs.microsoft.com/en-us/azure/app-service/troubleshoot-diagnostic-logs)

### Create Azure App Service mobile apps
* Add push notifications for mobile apps
  * [Azure Mobile Apps - Quickstart](https://github.com/Azure/azure-mobile-apps-quickstarts)
  * [Notification Hub](https://docs.microsoft.com/en-us/azure/notification-hubs/)
* Enable offline sync for mobile app
  * [Offline sync for mobile apps](https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-offline-data-sync)
* Implement a remote instrumentation strategy for mobile devices

### Create Azure App Service API apps
* Create an Azure App Service API app
  * [ASP.NET Core API Project](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-2.2&tabs=visual-studio)
* Create documentation for the API by using open source and other tools
  * [Document API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2)

### Implement Azure functions
* Implement input and output bindings for a function
  * [Triggers and bindings](https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings)
* Implement function triggers by using data operations, timers, and webhooks
  * [Timer Trigger](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-timer)
  * [HTTP and Webhooks](https://docs.microsoft.com/en-us/sandbox/functions-recipes/http-and-webhooks)
* Implement Azure Durable Functions
  * [Durable functions overview](https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-overview)
  * [Serverless - Overview](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs?toc=%2fazure%2fazure-functions%2fdurable%2ftoc.json)
* Create Azure Function apps by using Visual Studio
  * [Create Azure Function with Visual Studio](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio)

## Develop for Azure storage (15-20%)

### Develop solutions that use storage tables
* Design and implement policies for tables
  * [Stored Access Policy](https://docs.microsoft.com/en-us/rest/api/storageservices/establishing-a-stored-access-policy)
* Query table storage by using code
  * [Get started with Table Storage](https://docs.microsoft.com/en-us/azure/cosmos-db/table-storage-how-to-use-dotnet)
* Implement partitioning schemes
  * [Partitioning Strategy](https://docs.microsoft.com/en-us/rest/api/storageservices/designing-a-scalable-partitioning-strategy-for-azure-table-storage)
### Develop solutions that use Cosmos DB storage
* Create, read, update, and delete data by using appropriate APIs
  * [Cosmos DB - Get started](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-api-get-started)
* Implement partitioning schemes
  * [Partitioning and Scaling](https://docs.microsoft.com/en-us/azure/cosmos-db/partition-data)
* Set the appropriate consistency level for operations
  * [Consistency levels](https://docs.microsoft.com/en-us/azure/cosmos-db/consistency-levels-across-apis)

### Develop solutions that use a relational database
* Provision and configure relational databases
  * [Create Azure SQL Database](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-single-database-get-started)
* Configure elastic pools for Azure SQL Database
  * [Elastic pools for Azure SQL](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-elastic-pool)
* Create, read, update, and delete data tables by using code
  * [ASP.NET Core Razor Pages CRUD](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-2.2)
  * [ASP.NET Tutorial with SQL](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-dotnet-sqldatabase)

### Develop solutions that use blob storage
* Move items in blob storage between storage accounts or containers
  * [Choose Azure solution for data transfer](https://docs.microsoft.com/da-dk/azure/storage/common/storage-choose-data-transfer-solution?toc=%2Fazure%2Fstorage%2Ffiles%2Ftoc.json)
  * [Transfer data with AzCopy](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azcopy)
* Set and retrieve properties and metadata
  * [Storage properties and metadata](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-properties-metadata)
* Implement blob leasing
  * [Lease blob](https://docs.microsoft.com/en-us/rest/api/storageservices/lease-blob)
* Implement data archiving and retention
  * [Azure Blob Storage - Storage tiers](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blob-storage-tiers)

## Implement Azure security (10-15%)

### Implement authentication
* Implement authentication by using certificates, forms-based authentication, or tokens
  * [Introduction to Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-2.2&tabs=visual-studio)
  * [Authentication Samples ASP.Net Core - Github](https://github.com/aspnet/AspNetCore/tree/release/2.2/src/Security/samples)
  * [JWT Authentication - ASP.NET Core](http://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api)
* Implement multi-factor or Windows authentication by using Azure AD
  * [Multi Factor Authentication - How it works](https://docs.microsoft.com/en-us/azure/active-directory/authentication/concept-mfa-howitworks)
  * [QR Codes for Two-Factor Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-enable-qrcodes?view=aspnetcore-2.2)
* Implement OAuth2 authentication
  * [Authorize access to web applications with Oauth 2.0](https://docs.microsoft.com/en-us/azure/active-directory/develop/v1-protocols-oauth-code)
* Implement Managed Service Identity (MSI)/Service Principal authentication
  * [What is managed identities?](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)
  * [Use a Windows VM system-assigned managed identity to access Resource Manager](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/tutorial-windows-vm-access-arm)
  * [Configure managed identities for Azure resources on a VM using the Azure portal](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/qs-configure-portal-windows-vm)

### Implement access control
* Implement CBAC (Claims-Based Access Control) authorization
  * [ASP.NET Core Tutorial - User protected data](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-2.2)
  * [Claims-based authorization](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-2.2)
* Implement RBAC (Role-Based Access Control) authorization
  * [Role-based authorization](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-2.2)
* Create shared access signatures
  * [Shared access signatures - Blob Storage](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-dotnet-shared-access-signature-part-2)

### Implement secure data solutions
* Encrypt and decrypt data at rest and in transit
  * [ASP.NET Core - Data Protection](https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-2.2)
* Create, read, update, and delete keys, secrets, and certificates by using the KeyVault API
  * [Azure Key Vault API reference](https://docs.microsoft.com/en-us/rest/api/keyvault/)

## Monitor, troubleshoot, and optimize Azure solutions (15-20%)

### Develop code to support scalability of apps and services
* Implement autoscaling rules and patterns
  * [Autoscaling App Service](https://docs.microsoft.com/en-us/azure/app-service/environment/app-service-environment-auto-scale)
* Implement code that handles transient faults
  * [Resilient Applications](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/)
  * [Circuit Breaker Pattern](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-circuit-breaker-pattern)
  * [HTTP Call retries with Polly](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
  * [Resilient HTTP Calls with HttpClientFactory](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)

### Integrate caching and content delivery within solutions
* Store and retrieve data in Azure Redis cache
  * [Azure Redis cache overview](https://docs.microsoft.com/en-us/azure/azure-cache-for-redis/cache-overview)
* Develop code to implement CDNs in solutions
  * [Azure CDN Development](https://docs.microsoft.com/en-us/azure/cdn/cdn-app-dev-net)
* Invalidate cache content (CDN or Redis)
  * [Purge Azure CDN endpoint](https://docs.microsoft.com/en-us/azure/cdn/cdn-purge-endpoint)

### Instrument solutions to support monitoring and logging
* Configure instrumentation in an app or service by using Application Insights
  * [Application Insights - Overview](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview)
* Analyze and troubleshoot solutions by using Azure Monitor
  * [Azure Monitor Docs](https://docs.microsoft.com/en-us/azure/azure-monitor/)
* Implement Application Insights Web Test and Alerts
  * [Set Alerts in Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/alerts)

## Connect to and Consume Azure Services and Third-party Services (20-25%)

### Develop an App Service Logic App
* Create a Logic App
  * [Create Logic App - Quickstart](https://docs.microsoft.com/en-us/azure/logic-apps/quickstart-create-first-logic-app-workflow)
* Create a custom connector for Logic Apps
  * [Custom connectors in Logic Apps](https://docs.microsoft.com/en-us/azure/logic-apps/custom-connector-overview)
* Create a custom template for Logic Apps
  * [Logic App Templates](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-create-logic-apps-from-templates)

### Integrate Azure Search within solutions
* Create an Azure Search index
  * [Create basic index in Azure Search](https://docs.microsoft.com/en-us/azure/search/search-what-is-an-index)
* Import searchable data
  * [Import Data Wizard for Azure Search](https://docs.microsoft.com/en-us/azure/search/search-import-data-portal)
* Query the Azure Search index
  * [Query Azure Search in .NET](https://docs.microsoft.com/en-us/azure/search/search-howto-dotnet-sdk)

### Establish API Gateways
* Create an APIM instance
  * [Create Azure API Management instance](https://docs.microsoft.com/en-us/azure/api-management/get-started-create-service-instance)
* Configure authentication for APIs
  * [Authentication - Azure API Management](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-protect-backend-with-aad)
* Define policies for APIs
  * [Policies in Azure API Management](https://docs.microsoft.com/en-us/azure/api-management/api-management-howto-policies)

### Develop event-based solutions
* Implement solutions that use Azure Event Grid
  * [Route Events - Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/custom-event-quickstart-portal)
* Implement solutions that use Azure Notification Hubs
  * [Azure Notification Hubs - Docs](https://docs.microsoft.com/en-us/azure/notification-hubs/)
* Implement solutions that use Azure Event Hub
  * [Send messages to Azure Event Hub](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send)

### Develop message-based solutions
* Implement solutions that use Azure Service Bus
  * [What is Azure Service Bus](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview)
  * [Get started with Service Bus queues](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-get-started-with-queues)
* Implement solutions that use Azure Queue Storage queues
  * [Get started with Azure Queue Storage](https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues)

## Flash Cards (Tinycards) for Practicing

I have created a set of Tinycards to help you practice for the exam. However, the exam does require you to code. The cards will therefore only be for yourself to practice knowledge regarding central concepts of the subjects.

[![TinyCards Flash Cards](tinycards.PNG?raw=true)](https://tiny.cards/decks/KXxGMwAe/exam-az-203-developing-solutions-for-azure)

[EXAM AZ-203: Flash Cards for Practicing](https://tiny.cards/decks/KXxGMwAe/exam-az-203-developing-solutions-for-azure)

## Code Samples
The code samples are created by Microsoft and contain samples for each of the project areas.
Only the areas where programming is necessary is represented in the samples.
Some of the areas are primarily focused on the portal.

* Develop Azure Infrastructure as a Service Compute Solutions (10-15%)
  * [Link to samples](https://github.com/kristofferandreasen/developing-solutions-azure-exam/tree/master/1-develop-azure-infrastructure-as-a-service-compute-solutions)
* Develop Azure Platform as a Service Compute Solutions (20-25%)
  * [Link to samples](https://github.com/kristofferandreasen/developing-solutions-azure-exam/tree/master/2-develop-azure-platform-as-a-service-compute-solutions)
* Develop for Azure storage (15-20%)
  * [Link to samples](https://github.com/kristofferandreasen/developing-solutions-azure-exam/tree/master/3-develop-for-azure-storage)
* Implement Azure security (10-15%)
  * [Link to samples](https://github.com/kristofferandreasen/developing-solutions-azure-exam/tree/master/4-implement-azure-security)
* Connect to and Consume Azure Services and Third-party Services (20-25%)
  * [Link to samples](https://github.com/kristofferandreasen/developing-solutions-azure-exam/tree/master/5-connect-to-and-consume-azure-services-and-third-party-services)

## Exam Questions

It is possible to find relevant exam questions online.
Here are a few links to relevant questions to help you prepare.
These are closer than the flash cards to how your exam will actually look.
* [Cert Library - Exam Questions](https://www.certlibrary.com/exam/70-532)
* [Practice Test 70-532 - All Subjects (the previous exam name)](https://crpietschmann.github.io/Azure-70-532-Practice-Test/?1.1)
* [Practice Test 70-532 - Deploy Azure App Service Web Apps (the previous exam name)](https://crpietschmann.github.io/Azure-70-532-Practice-Test/?1.1)
* [Kaplan Learn Practice Exam - Click on right side in the Path page (Only for Pluralsight users)](https://app.pluralsight.com/paths/skills/developing-microsoft-azure-solutions-70-532)

## Courses - Exam Preparation
* [Udemy Course](https://www.udemy.com/70532-azure/)
* [Pluralsight Path](https://app.pluralsight.com/paths/skills/developing-solutions-for-microsoft-azure-az-203)

## Contributing
This project was created to keep a centralized place for all the resources I needed to become proficient in Azure. Feel free to suggest or add other resources that might be relevant or let me know if some of the resources are unavailable.

You can read more on contributing in the [contributing.md file](https://github.com/kristofferandreasen/developing-solutions-azure-exam/blob/master/contributing.md).

## Going Forward
The repository will be updated with regular intervals.

## License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

See the explanation of the [MIT License](https://opensource.org/licenses/MIT) here..
