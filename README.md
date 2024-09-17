# SanaEcommerce

## Overview

SanaEcommerce is a web application designed to manage and display products and their categories. This project is built using .NET 8.0 and follows a multi-layered architecture.

## Technologies Used

- **.NET 8.0**: The core framework used for building the application.
- **Entity Framework Core 8.0.8**: Used for data access and ORM.
- **ASP.NET Core**: For building the web API.
- **Swashbuckle.AspNetCore**: For API documentation and Swagger UI.
- **Newtonsoft.Json**: For JSON serialization and deserialization.

## Project Structure

- **SanaEcommerceAPI**: Contains the web application project.
- **SEC.Business**: Contains the business logic layer.
- **SEc.Data**: Contains the data models and DTOs.
- **SEc.DataAccess**: Contains the data access layer.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

### Step 1: Clone the Repository

```sh
git clone https://github.com/yourusername/SanaEcommerce.git
cd SanaEcommerce
```
### Step 2: Set Up the Database
Create a new database in SQL Server.
Run the SQL script located at Anexos/database.sql to set up the database schema and initial data.
Step 3: Configure Connection Strings
Update the connection strings in SanaEcommerceAPI/WebApplication1/appsettings.Development.json:

```sh
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;",
    "AutherConnetion": "Server=your_server;Database=your_auther_database;User Id=your_user;Password=your_password;"
  }
}
```

### Step 4: Build the Solution
Open the solution file SanaEcommerce.sln in Visual Studio or use the .NET CLI to build the solution:
```sh
dotnet build
```

Step 5: Run the Application
Navigate to the SanaEcommerceAPI/WebApplication1 directory and run the application:
```sh
cd SanaEcommerceAPI/WebApplication1
dotnet run
```
