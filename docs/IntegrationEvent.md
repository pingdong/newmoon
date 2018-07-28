[Back](../README.md)

> ## Integration Event 

Using my own abstraction (borrowed from eShopOnContainer) is just a proof-of-concept. If advance features are required, the API and abstractions provided by a commerical service bus should be used.

## Usage

1. Configure
    
    Adding the below section in configuration, prefer in UserSecrets (Development) or Vault (Production)  
```
  "App": {
      "EventBus": {
        "Enabled": "true",
        "Provider": "Azure",
        "ClientName": "", 
        "ConnectionString": "",
        "RabbitMQ": {
          "UserName": "",
          "Password": "",
          "RetryCount":  "" 
        }
      }
    }
```

Parameters  
 * Provider: `provider: ['Azure', 'RabbitMQ']`  
 * RabbitMQ: only needed when provider is RabbitMQ

2. Copy files
    Copy PingDong.EventBus.dll and PingDong.EventBus.[provider].dll to application folder

## Transaction

Integration Event generally emphasise on availability and tolerance, intead of strong consistency. If consistency is crucial, the following approaches could be helpful. 
* Full Event Sourcing Pattern (One of the best approaches, if not the best. But it requires significant changes on the application)
* Transaction log mining
* Outbox Pattern 

Newmoon uses the idea of transaction from eShopOnContainers for simplicity. A better approach is using a separate worker, e.g. Azure Function, reads and publish integration events.

## Idempotent and Duplication

Message could be sent multiple times, especially through an intermittent network, a deduplication approach should be implmented on both transport level and the receiver.