# AspNetCoreLoggingAPI

## Overview
AspNetCoreLoggingAPI is an ASP.NET Core Web API that demonstrates how to:
- Save data to a SQL Server database,
- Retrieve data with optional filtering,
- Log HTTP request and response details using a custom action filter.

## Features
- **Data Saving:**  
  Processes a JSON array of key-value pairs, sorts by a numeric code, clears existing data, and saves new records to the `DataRecords` table.

- **Data Retrieval:**  
  Offers an endpoint to fetch all records with an optional `codeFilter` query parameter to filter by the `Code` field.

- **Logging:**  
  Employs a custom `LoggingActionFilter` to record details such as request path, method, and response status code into a `LogRecords` table.

## Prerequisites
- [.NET 6 SDK (or later)](https://dotnet.microsoft.com/download)
- SQL Server (or SQL Server Express)
- An IDE such as Visual Studio or Visual Studio Code

## Setup

1. **Clone the Repository:**
   Download or clone the repository to your local environment.

2. **Configure Connection String:**  
   Update the connection string in `appsettings.json` with your server and database details.

3. **Database Setup:**  
   Create the database schema manually by running the provided SQL script located in the `DatabaseScripts` folder via SQL Server Management Studio (SSMS). This script creates the required `DataRecords` and `LogRecords` tables.

## API Endpoints

### POST `/api/data/save`
- **Description:**  
  Saves new records from a JSON array. Sorts by `Code`, clears old data, and inserts the new data.

### GET `/api/data/data`
- **Description:**  
  Retrieves all records from the `DataRecords` table. Supports optional filtering by `codeFilter` to fetch specific records.

## Logging
The `LoggingActionFilter` automatically captures:
- **Request Details:** Path and HTTP method.
- **Response Details:** HTTP status code.

Logs are stored in the `LogRecords` table, assisting in monitoring and debugging.

## Running & Testing
- **Run the application:**  
  Use the .NET CLI or your IDE to start the application.
- Use tools like Postman or curl to test the API endpoints.