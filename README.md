 Project Overview

This application simulates a real-world insurance quoting system. It supports:
Rule-based dynamic pricing using the Microsoft RulesEngine
SQL Server integration with stored procedures and triggers
API endpoints for calculating quotes

Swagger UI for testing


Tech Stack

ASP.NET Core 8.0 Web API
Entity Framework Core
SQL Server (T-SQL: stored procedures, triggers)
Microsoft RulesEngine
Swagger / OpenAPI

Project structure 
InsuranceApp/
├── Controllers/
│   └── QuoteController.cs
├── Models/
│   ├── Customer.cs
│   ├── InsurancePlan.cs
│   ├── Quote.cs
│   └── QuoteResult.cs
├── Services/
│   ├── IQuoteService.cs
│   └── QuoteService.cs
├── Rules/
│   └── pricingRules.json
├── Data/
│   └── InsuranceDbContext.cs
├── appsettings.json
└── Program.cs

Features

POST /api/quote/rules — calculate quote using in-memory pricing rules
POST /api/quote/storedproc — calculate quote using SQL stored procedure
Supports individual and family plans


 SQL Server DB Entities

Tables: 
Customers
InsurancePlans
Quotes

Stored Procedure: usp_CalculateQuote
Trigger: trg_AuditQuotes

Running the App

Clone the repo
Run Update-Database (if using EF Migrations)
Launch project (F5 in Visual Studio)
Go to https://localhost:<port>/swagger to test the APIs
