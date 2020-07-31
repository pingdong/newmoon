## **Overview**<br />
Newmoon is an evoluting project with the latest technology and best practices. The idea of the project is a simple service provides event booking.

It covers the following concept, technology and practice
1) Microservice by both Serverless and Docker
2) Test Automation
3) Infrastructure as Code
4) DevOps

---
## **Architecture**<br />
<img src="images/architecture.svg" width="500">

---
## **Repos**<br />
* ### ***Infrastructure***
    The [repo](https://github.com/pingdong/newmoon.infrastructure) contains scripts that build infrastructure resources on Azure. <br />

    **Key Technology:** <br />
    Terraform, ARM<br />
<br />

* ### ***Shared Libary***<br />
    The [repo](https://github.com/pingdong/newmoon.shared) contains sharing models, abstractions, extensions and helpers.<br />
<br />

* ### ***Authentication Service***<br />
    The [repo](https://github.com/pingdong/newmoon.authentication) provides authentication service.<br />

    **Key Technology:** <br />
    IdentityServer4<br />
<br />

* ### ***Venues Service***<br />
    The [repo](https://github.com/pingdong/newmoon.venues) provides venues management service, implemented by both docker and serverless. <br />

    **Key Technology:** <br />
    Azure Storage Blob, Azure Storage Table<br />
    Restful Api, gRpc<br />
    Azure Service Bus Queue, Azure Storage Queue<br />
<br />

* #### ***Events Service***<br />
    The [repo](https://github.com/pingdong/newmoon.events) provides events management service, implement by both docker and serverless.

    **Key Technology:**  
    Azure Sql Server, Azure Cosmos Db, Azure Redis<br />
    GraphQL, SignalR<br />
    Azure Service Bus Topic/Subscription, Azure Event Hub<br />
<br />
