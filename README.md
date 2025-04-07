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


# Database Schema Documentation

This document describes the table structures used in the ASP.NET Core Logging API project, specifically for Task 1. The API handles data saving and logging of HTTP requests/responses.

---

## 1. Table: DataRecords

**Purpose:**  
Stores the input data received via the API when saving the records.

**Structure:**

- **Id**  
  - *Data Type:* INT  
  - *Properties:* Auto-increment (IDENTITY(1,1)), Primary Key  
  - *Description:* A unique identifier for each record.

- **Code**  
  - *Data Type:* INT, NOT NULL  
  - *Description:* The numeric code extracted from the key of the input JSON object.

- **Value**  
  - *Data Type:* NVARCHAR(MAX), NOT NULL  
  - *Description:* The value extracted from the input JSON object's value.

**Example Create Statement:**

<pre>
CREATE TABLE DataRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Code INT NOT NULL,
    Value NVARCHAR(MAX) NOT NULL
);
</pre>

---

## 2. Table: LogRecords

**Purpose:**  
Used by the `LoggingActionFilter` to record details about each HTTP request and its corresponding response.

### Structure

- **Id**  
  - **Data Type:** INT  
  - **Properties:** Auto-increment (IDENTITY(1,1)), Primary Key  
  - **Description:** A unique identifier for each log record.

- **Request**  
  - **Data Type:** NVARCHAR(MAX)  
  - **Description:** Contains the request information (e.g., URL path and HTTP method).

- **Response**  
  - **Data Type:** NVARCHAR(MAX)  
  - **Description:** Contains the response information (e.g., HTTP status code).

- **Timestamp**  
  - **Data Type:** DATETIME  
  - **Default Value:** GETUTCDATE() (if set at the database level, or provided programmatically)  
  - **Description:** The moment when the request was processed, using UTC time.

### Example Create Statement


<pre>
CREATE TABLE LogRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Request NVARCHAR(MAX),
    Response NVARCHAR(MAX),
    Timestamp DATETIME DEFAULT GETUTCDATE()
);
</pre>

## How These Tables Are Used in the Application

### DataRecords:
When a client sends a `POST` request to `/api/data/save`, the API:
- Accepts a JSON array consisting of key-value pairs.
- Converts each pair into a record where the key becomes the **Code** and the value becomes **Value**.
- Clears the existing data in the `DataRecords` table before inserting the new records (note that this does not reset the auto-increment counter unless explicitly reset).

### LogRecords:
For every HTTP request, the `LoggingActionFilter`:
- Captures request details (such as the HTTP method and URL path) and the response's status code.
- Constructs a log record containing this information along with a timestamp.
- Inserts the log record into the `LogRecords` table, providing an audit trail for monitoring and debugging.
