![Alt text](exam203.png?raw=true "Exam AZ-203")

# Exam AZ-203: Developing Solutions for Microsoft Azure

This repository is meant to help you get certified in Microsoft Azure. It contains various resources and links to help you level up in developing applications for Microsoft Azure. Feel free to help out with new resources if you find any relevant ones.

## Getting Started
Azure can be quite intimidating because of the vast amount of services and options.
Therefore it would make sense to choose a specific learning path in order to learn how Azure can be used within the area you work in.
I would recommend everyone getting started with Azure to visit the [Microsoft Learn](https://docs.microsoft.com/en-us/learn/) portal. The portal is a great resource for getting familiar with services in Azure.

## Develop for cloud storage

### Develop solutions that use file storage
* Implement quotas for file shares in storage account
  * [Link - Storage Files FAQ](https://docs.microsoft.com/en-us/azure/storage/files/storage-files-faq)
  * [Link - .NET - How to use files](https://docs.microsoft.com/en-us/azure/storage/files/storage-dotnet-how-to-use-files)

## Create Platform as a Service (PaaS) Solutions

### Create an app service Logic App
* Create a custom connector for Logic Apps, a custom template for a Logic App
  * [Link - Logic App Connector](https://docs.microsoft.com/en-us/connectors/custom-connectors/create-logic-apps-connector)

* Create a Logic App
  * https://docs.microsoft.com/en-us/azure/logic-apps/quickstart-create-first-logic-app-workflow
* Package an Azure App Service Logic App
  * https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-overview

### Create app or service that runs on Service Fabric
* Develop a stateful Reliable Service and a stateless Reliable Service
* https://docs.microsoft.com/en-us/azure/service-fabric/
* https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-tutorial-create-dotnet-app
* Develop an actor-based Reliable Service
* https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-reliable-actors-introduction
* Write code to consume Reliable Collections in your service
* https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-reliable-actors-get-started
### Schedule bulk operations
* Define the batch output and conditions by using Batch Service API
* https://docs.microsoft.com/en-us/rest/api/batchservice/
* write code to run a batch job
* https://docs.microsoft.com/en-us/azure/batch/quick-run-dotnet
* Run a batch job by using Azure CLI, Azure Portal, and other tools
* https://docs.microsoft.com/en-us/azure/batch/quick-create-cli
### Design and develop applications that run in containers
* Configure diagnostic settings on resources
* https://docs.microsoft.com/en-us/azure/azure-monitor/platform/diagnostic-logs-overview
* Create a container image by using a Docker file
* Create an Azure Container Service (ACS/AKS) cluster by using the Azure CLI and Azure Portal
* Publish an image to the Azure Container Registry
* Implement an application that runs on an Azure Container Instance
* Implement container instances by using Azure Container Service (ACS/AKS), Azure Service Fabric, and other tools
* Manage container settings by using code


## Secure cloud solutions

### Implement access control
* Implement Claims-Based Access Control (CBAC) and Role-Based Access Control (RBAC) authorization


## Develop for an Azure cloud model

### Develop for asynchronous processing
* Implement parallelism, multithreading, processing, durable functions, Azure logic apps, interfaces with storage, interfaces to data access, and appropriate asynchronous compute models

### Develop for autoscaling
* Implement autoscaling rules and patterns (schedule, operational/system metrics, code that addresses singleton application instances, and code that addresses transient state

### Implement distributed transactions
* Identify tools to implement distributed transactions (e.g., ADO.NET, elastic transactions, multi-database transactions)
* Manage transaction scope
* Manage transactions across multiple databases and servers


## Implement cloud integration solutions
### Configure a message-based integration architecture
* Configure an app or service to send emails, Event Grid, and the Azure Relay Service
* Create and configure a Notification Hub, an Event Hub, and a Service Bus
* Configure queries across multiple products
* Configure an app or service with Microsoft Graph

### Develop an application message model
* Create a message schema and a message exchange
* Create an event model
* Create topics and subscriptions

## Develop Azure Cognitive Services, Bot, and IoT solutions
### Integrate Azure Cognitive Services in an application
* Develop solutions by using intelligent algorithms that identify items from images and videos
* Develop solutions by using intelligent algorithms related to speech, natural language processing, Bing Search, and recommendations and decision making
### Create and integrate bots
* Create a bot by using the Bot Framework
* Create a natural language conversation flow
* Manage bots by using the Azure Portal
* Register a bot by using the Bot Framework
### Create and implement IoT solutions
* Configure Azure Time Series Insights
* Configure Stream Analytics service for inputs and outputs
* Establish bidirectional communication with IoT devices by using IoT Hub
* Register devices with IoT Hub Device Provisioning Service

## Develop Azure Infrastructure as a Service compute solutions

### Implement solutions that use virtual machines (VM)
* Provision VMs
* Create ARM templates
* Configure Azure Disk Encryption for VMs
### Implement batch jobs by using Azure Batch Services
* Manage batch jobs by using Batch Service API
* Run a batch job by using Azure CLI, Azure portal, and other tools
* Write code to run an Azure Batch Services batch job
### Create containerized solutions
* Create an Azure Managed Kubernetes Service (AKS) cluster
* Create container images for solutions
* Publish an image to the Azure Container Registry
* Run containers by using Azure Container Instance or AKS

## Develop Azure Platform as a Service compute solutions

### Create Azure App Service Web Apps
* Create an Azure App Service Web App
* Create an Azure App Service background task by using WebJobs
* Enable diagnostics logging
### Create Azure App Service mobile apps
* Add push notifications for mobile apps
* Enable offline sync for mobile app
* Implement a remote instrumentation strategy for mobile devices
### Create Azure App Service API apps
* Create an Azure App Service API app
* Create documentation for the API by using open source and other tools
### Implement Azure functions
* Implement input and output bindings for a function
* Implement function triggers by using data operations, timers, and webhooks
* Implement Azure Durable Functions
* Create Azure Function apps by using Visual Studio

## Develop for Azure storage

### Develop solutions that use storage tables
* Design and implement policies for tables
* Query table storage by using code
* Implement partitioning schemes
### Develop solutions that use Cosmos DB storage
* Create, read, update, and delete data by using appropriate APIs
* Implement partitioning schemes
* Set the appropriate consistency level for operations
### Develop solutions that use a relational database
* Provision and configure relational databases
* Configure elastic pools for Azure SQL Database
* Create, read, update, and delete data tables by using code
### Develop solutions that use blob storage
* Move items in blob storage between storage accounts or containers
* Set and retrieve properties and metadata
* Implement blob leasing
* Implement data archiving and retention


## Implement Azure security

### Implement authentication
* Implement authentication by using certificates, forms-based authentication, or tokens
* Implement multi-factor or Windows authentication by using Azure AD
* Implement OAuth2 authentication
* Implement Managed Service Identity (MSI)/Service Principal authentication
### Implement access control
* Implement CBAC (Claims-Based Access Control) authorization
* Implement RBAC (Role-Based Access Control) authorization
* Create shared access signatures
### Implement secure data solutions
* Encrypt and decrypt data at rest and in transit
* Create, read, update, and delete keys, secrets, and certificates by using the KeyVault API


## Monitor, troubleshoot, and optimize solutions

### Develop code to support scalability of apps and services
* Implement autoscaling rules and patterns (schedule, operational/system metrics, singleton applications)
* Implement code that handles transient faults
### Integrate caching and content delivery within solutions
* Store and retrieve data in Azure Redis cache
* Develop code to implement CDNs in solutions
* Invalidate cache content (CDN or Redis)
### Instrument solutions to support monitoring and logging
* Configure instrumentation in an app or service by using Application Insights
* Analyze and troubleshoot solutions by using Azure Monitor
* Implement Application Insights Web Test and Alerts

## Connect to and consume Azure services and third-party services

### Develop an App Service Logic App
* Create a Logic App
* Create a custom connector for Logic Apps
* Create a custom template for Logic Apps
### Integrate Azure Search within solutions
* Create an Azure Search index
* Import searchable data
* Query the Azure Search index
### Establish API Gateways
* Create an APIM instance
* Configure authentication for APIs
* Define policies for APIs
### Develop event-based solutions
* Implement solutions that use Azure Event Grid
* Implement solutions that use Azure Notification Hubs
* Implement solutions that use Azure Event Hub
### Develop message-based solutions
* Implement solutions that use Azure Service Bus
* Implement solutions that use Azure Queue Storage queues


## Contributing
This project was created to keep a centralized place for all the resources I needed to become proficient in Azure. Feel free to suggest or add other resources that might be relevant or let me know if some of the resources are unavailable.

You can read more on contributing in the [contributing.md file](https://github.com/kristofferandreasen/AwesomeAzure/blob/master/CONTRIBUTING.md).

## Going Forward
The repository will be updated with regular intervals.

## License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

See the explanation of the [MIT License](https://opensource.org/licenses/MIT) here..
