# AssetMonitorNET

Application for monitoring windows and network assets using ICMP, SNMP or Agent service to collect data. It has historical data collection capability, alarming and e-mail notification.

Technologies: .NET Core 3.1, ASP.NET Core, EF Core, Dapper, Bootstrap, TypeScript

Reposisotry conatains following projects

## AspMVC_Monitor

Web client showing live data and allowing to configure application. Communicating with windows service (AssetMonitorService) thorugh gRPC.

## AssetMonitorService

Windows service which is main service of the application. Communicating with web client (AspMVC_Monitor) and remote agents (AssetMonitorAgent) thorugh gRPC.

## AssetMonitorDataAccess

Project for accessing database (EF Core)

## AssetMonitorHistoryDataAccess

Project for accessing database with historical data in dynamic manner (Dapper + EF Core)

## AssetMonitorAgent

Windows service providing data from remote windows assets. Communicating with main server (AssetMonitorService) thorugh gRPC.

## AssetMonitorSharedGRPC

Shared project for communication using gRPC

## WindowsDataLib

Shared project for getting windows information

## AssetMonitorWebClient

New web client using .NET 6 and Angular for UI


