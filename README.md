# AssetMonitorNET projects

## AspMVC_Monitor
Web client showing live data and allowing to configure application. Communicating with windows service (AssetMonitorService) thorugh gRPC.
Technologies: .NET Core 3.1, TypeScript, Bootstrap


## AssetMonitorService
Windows service which is main service of the application. Communicating with web client (AspMVC_Monitor) and remote agents (AssetMonitorAgent) thorugh gRPC.
Technologies: .NET Core 3.1

## AssetMonitorAgent
Windows service colleting. Communicating with windows service (AssetMonitorService) thorugh gRPC.
Technologies: .NET Core 3.1, TypeScript, Bootstrap.
