![Alt text](exam203.png?raw=true "Exam AZ-203")

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
* Create ARM templates
  * [ARM - Structure and syntax](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-authoring-templates)
* Configure Azure Disk Encryption for VMs
  * [Azure Disk Encryption](https://docs.microsoft.com/en-us/azure/security/azure-security-disk-encryption-overview)

### Implement batch jobs by using Azure Batch Services
* Manage batch jobs by using Batch Service API
  * [Batch service](https://docs.microsoft.com/en-us/rest/api/batchservice/)
* Run a batch job by using Azure CLI, Azure portal, and other tools
  * [Run batch service with CLI](https://docs.microsoft.com/en-us/azure/batch/quick-create-cli)
* Write code to run an Azure Batch Services batch job
  * [Code a batch service](https://docs.microsoft.com/en-us/azure/batch/quick-run-dotnet)

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
  * []()
* Implement function triggers by using data operations, timers, and webhooks
  * []()
* Implement Azure Durable Functions
  * []()
* Create Azure Function apps by using Visual Studio
  * []()

## Develop for Azure storage (15-20%)

### Develop solutions that use storage tables
* Design and implement policies for tables
  * []()
* Query table storage by using code
  * []()
* Implement partitioning schemes
  * []()
### Develop solutions that use Cosmos DB storage
* Create, read, update, and delete data by using appropriate APIs
  * []()
* Implement partitioning schemes
  * []()
* Set the appropriate consistency level for operations
  * []()

### Develop solutions that use a relational database
* Provision and configure relational databases
  * []()
* Configure elastic pools for Azure SQL Database
  * []()
* Create, read, update, and delete data tables by using code
  * []()

### Develop solutions that use blob storage
* Move items in blob storage between storage accounts or containers
  * []()
* Set and retrieve properties and metadata
  * []()
* Implement blob leasing
  * []()
* Implement data archiving and retention
  * []()

## Implement Azure security (10-15%)

### Implement authentication
* Implement authentication by using certificates, forms-based authentication, or tokens
  * []()
* Implement multi-factor or Windows authentication by using Azure AD
  * []()
* Implement OAuth2 authentication
  * []()
* Implement Managed Service Identity (MSI)/Service Principal authentication
  * []()

### Implement access control
* Implement CBAC (Claims-Based Access Control) authorization
  * []()
* Implement RBAC (Role-Based Access Control) authorization
  * []()
* Create shared access signatures
  * []()

### Implement secure data solutions
* Encrypt and decrypt data at rest and in transit
  * []()
* Create, read, update, and delete keys, secrets, and certificates by using the KeyVault API
  * []()

## Monitor, troubleshoot, and optimize Azure solutions (15-20%)

### Develop code to support scalability of apps and services
* Implement autoscaling rules and patterns
  * []()
* Implement code that handles transient faults
  * []()

### Integrate caching and content delivery within solutions
* Store and retrieve data in Azure Redis cache
  * []()
* Develop code to implement CDNs in solutions
  * []()
* Invalidate cache content (CDN or Redis)
  * []()

### Instrument solutions to support monitoring and logging
* Configure instrumentation in an app or service by using Application Insights
  * []()
* Analyze and troubleshoot solutions by using Azure Monitor
  * []()
* Implement Application Insights Web Test and Alerts
  * []()

## Connect to and Consume Azure Services and Third-party Services (20-25%)

### Develop an App Service Logic App
* Create a Logic App
  * []()
* Create a custom connector for Logic Apps
  * []()
* Create a custom template for Logic Apps
  * []()

### Integrate Azure Search within solutions
* Create an Azure Search index
  * []()
* Import searchable data
  * []()
* Query the Azure Search index
  * []()

### Establish API Gateways
* Create an APIM instance
  * []()
* Configure authentication for APIs
  * []()
* Define policies for APIs
  * []()

### Develop event-based solutions
* Implement solutions that use Azure Event Grid
  * []()
* Implement solutions that use Azure Notification Hubs
  * []()
* Implement solutions that use Azure Event Hub
  * []()

### Develop message-based solutions
* Implement solutions that use Azure Service Bus
  * []()
* Implement solutions that use Azure Queue Storage queues
  * []()

## Flash Cards (Tinycards) for Practicing

I have create a set of Tinycards to help you practice for the exam. However, the exam does require you to code. The cards will therefore only be for yourself to practice knowledge regarding central concepts of the subjects.

[EXAM AZ-203: Flash Cards for Practicing](https://tiny.cards/decks/KXxGMwAe/exam-az-203-developing-solutions-for-azure)

## Contributing
This project was created to keep a centralized place for all the resources I needed to become proficient in Azure. Feel free to suggest or add other resources that might be relevant or let me know if some of the resources are unavailable.

You can read more on contributing in the [contributing.md file](https://github.com/kristofferandreasen/AwesomeAzure/blob/master/CONTRIBUTING.md).

## Going Forward
The repository will be updated with regular intervals.

## License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

See the explanation of the [MIT License](https://opensource.org/licenses/MIT) here..
