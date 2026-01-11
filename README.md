# Payment Gateway Project

This project is a high-performance Payment Gateway API built with .NET 6 using Clean Architecture principles. It supports transaction management, automated database migrations, and is fully containerized with Docker.

### Project Details
- **Patterns**: Repository Pattern, Unit of Work, Dependency Injection
- **Database**: MS SQL Server (EF Core Code First)

---

### Prerequisites

- **.NET 6 SDK**
- **Docker Desktop**
- **SQL Server** (If running locally without Docker)
- **Git**

---

# Getting Started

## 1. Clone the Repository
```bash
git clone <repository-url>
cd PaymentGatewayApi
```
# .env File Content

Create .env file in root directory like in this example

```bash
DB_PASSWORD=Password
DB_NAME=PAYMENTDB
DB_USER=sa
```

## 2. Running the Project

### Running with Docker (Recommended)
This will set up the API and SQL Server automatically.

```bash
docker compose up --build -d
```
API URL: http://localhost:8080/swagger/index.html

Database Port: 1433

### Running Locally 
Update appsettings.json with your local SQL Connection String.

Navigate to the WebAPI directory:

```bash
cd WebAPI
dotnet run
```

## 4. Database Migrations
The project is configured to automatically migrate the database on startup. However, if you need to manage migrations manually:

Add a New Migration:

```bash
dotnet ef migrations add <MigrationName> --project DataAccessLayer --startup-project WebAPI
```
Update Database:

```bash
dotnet ef database update --project DataAccessLayer --startup-project WebAPI
```

## 5. Technologies & Packages Used
Core: .NET 6

ORM: Entity Framework Core 6

Validation: FluentValidation

Mapping: AutoMapper

Database: MS SQL Server

Containerization: Docker & Docker Compose

API Documentation: Swagger 

6. Project Structure
Core: Contains Domain Entities, Enums, and Repository Interfaces. 
DataAccessLayer: Contains DB Context, Migrations, and Repository/UnitOfWork implementations.
Business: Contains Business Logic, Services, DTOs, and FluentValidation rules.
WebAPI: API Controllers, Middleware, and Program.cs configuration.

### Manual Database Script
If migrations fail, you can generate the SQL script to run manually on your server:

```bash
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PaymentDb')
BEGIN
    CREATE DATABASE PaymentDb;
END
GO

USE PaymentDb;
GO

-- 2. Transactions 
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Transactions')
BEGIN
    CREATE TABLE Transactions (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), 
        Amount DECIMAL(18, 2) NOT NULL, 
        Currency NVARCHAR(3) NOT NULL, 
        CardHolderName NVARCHAR(100) NOT NULL,
        MaskedCardNumber NVARCHAR(20) NOT NULL, 
        Status NVARCHAR(20) NOT NULL, -- AUTHORIZED, CAPTURED, VOIDED
        OrderReference NVARCHAR(50) NOT NULL, 
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL 
    );


    CREATE INDEX IX_Transactions_OrderReference ON Transactions(OrderReference);
    CREATE INDEX IX_Transactions_CreatedAt ON Transactions(CreatedAt DESC);
END
GO

-- 3. TransactionEvents 
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TransactionEvents')
BEGIN
    CREATE TABLE TransactionEvents (
        Id INT IDENTITY(1,1) PRIMARY KEY, 
        TransactionId UNIQUEIDENTIFIER NOT NULL, 
        Type NVARCHAR(20) NOT NULL,
        Status NVARCHAR(20) NOT NULL, 
        Details NVARCHAR(MAX) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        
        CONSTRAINT FK_TransactionEvents_Transactions 
        FOREIGN KEY (TransactionId) REFERENCES Transactions(Id)
    );

    CREATE INDEX IX_TransactionEvents_TransactionId ON TransactionEvents(TransactionId);
END
GO
```
