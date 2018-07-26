[Back](../README.md)

> ## Integration Event 

## Usage

1. Configure
    
    Adding the below section in configuration, prefer in UserSecrets (Development) or Vault (Production)  
```
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
```

Parameters  
 * Provider: `provider: ['Azure', 'RabbitMQ']`  
 * RabbitMQ: only needed when provider is RabbitMQ

2. Copy files
    Copy PingDong.EventBus.dll and PingDong.EventBus.[provider].dll to application folder
