# ASP.NET Core API Practice

## 📖 About the Project

This project was created as a personal study and practice application focused on backend development using ASP.NET Core Web API and Entity Framework Core.

The main goal of this project is to explore and implement common concepts, patterns, and best practices frequently used in modern backend applications.

Although this is a study project, the architecture and structure were designed to simulate a real-world backend API environment.

---

# 🚀 Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* Swagger / OpenAPI
* Dependency Injection
* Repository Pattern
* Unit of Work Pattern
* Custom Middleware
* Custom Logging Provider
* LINQ
* Data Annotations
* REST API Concepts

---

# 🏗️ Project Structure

The project follows a layered architecture to improve organization, maintainability, and scalability.

```text
📦 Project
 ┣ 📂 Controllers
 ┣ 📂 Context
 ┣ 📂 Model
 ┣ 📂 Repositories
 ┣ 📂 UnitOfWork
 ┣ 📂 Logging
 ┣ 📂 Extension
 ┣ 📂 Migrations
 ┣ 📜 Program.cs
 ┗ 📜 appsettings.json
```

---

# 📂 Main Components

## Controllers

Responsible for handling HTTP requests and exposing API endpoints.

### Features

* CRUD operations
* Route configuration
* Request validation
* HTTP status responses

---

## Models

Contains the application's entities.

### Features

* Data annotations
* Validation rules
* Relationships between entities
* Database mapping

Example:

```csharp
[Required(ErrorMessage = "The product name is required.")]
[StringLength(100)]
public string Name { get; set; } = string.Empty;
```

---

## Repository Pattern

The repository layer abstracts database access logic from business logic.

### Benefits

* Cleaner code
* Better maintainability
* Easier testing
* Separation of concerns

Generic repository methods:

```csharp
GetAll()
Get()
Add()
Update()
Delete()
```

---

## Unit of Work Pattern

The Unit of Work coordinates repositories and manages database transactions.

### Responsibilities

* Centralize SaveChanges()
* Coordinate repositories
* Reduce duplicated code
* Improve transaction consistency

---

## Entity Framework Core

Used as the ORM responsible for communication with the MySQL database.

### Features

* Database migrations
* LINQ queries
* Relationship mapping
* Change tracking
* AsNoTracking queries

---

## Custom Logging System

The project contains a custom logging provider integrated with ASP.NET Core logging infrastructure.

### Features

* Custom ILogger implementation
* Custom ILoggerProvider
* File-based logging
* Thread-safe logger storage using ConcurrentDictionary
* Configuration-based log path

Example configuration:

```json
{
  "Logging": {
    "Path": "Logs/logs.txt"
  }
}
```

---

## Global Exception Middleware

A custom exception handling middleware was implemented to standardize API error responses.

### Features

* Global exception handling
* JSON formatted error responses
* HTTP status code standardization
* Development/production behavior handling

Example response:

```json
{
  "statusCode": 500,
  "message": "Internal server error",
  "trace": "stack trace information"
}
```

---

# 🗄️ Database

The application uses MySQL as the relational database.

### ORM

* Entity Framework Core
* Pomelo MySQL Provider

### Migrations

Database changes are managed using EF Core migrations.

Useful commands:

```bash
dotnet ef migrations add MigrationName
```

```bash
dotnet ef database update
```

---

# 📌 Main Features

## Categories

* Create category
* Update category
* Delete category
* Retrieve all categories
* Retrieve category by Id

## Products

* Create product
* Update product
* Delete product
* Retrieve products
* Category relationship
* Stock management
* Price management

---

# 🔍 Validation

The project uses Data Annotations for model validation.

### Examples

* Required fields
* String length validation
* Numeric range validation
* Precision configuration
* Foreign key validation

Example:

```csharp
[Range(0, 10000)]
[Precision(10, 2)]
public decimal Stock { get; set; }
```

---

# ⚙️ Dependency Injection

ASP.NET Core built-in dependency injection is used throughout the project.

### Registered services

* DbContext
* UnitOfWork
* Repositories
* Logging Provider
* Controllers

---

# 📖 Swagger / OpenAPI

Swagger is configured for API documentation and testing.

### Features

* Endpoint documentation
* Interactive testing
* Request/response visualization

Access:

```text
/swagger
```

---

# 🧠 Concepts Practiced

This project was created mainly to practice backend development concepts such as:

* REST APIs
* Clean code
* Layered architecture
* Dependency Injection
* Generic repositories
* Unit of Work
* Middleware
* Exception handling
* Logging
* Entity relationships
* Data validation
* LINQ
* Database migrations
* API documentation

---

# ▶️ Running the Project

## Clone the repository

```bash
git clone https://github.com/lazaroLopes67/Clean-Architecture-Api.git
```

---

## Navigate to the project folder

```bash
cd Clean-Architecture-Api
```

---

## Restore packages

```bash
dotnet restore
```

---

## Configure database connection

Update the connection string inside:

```text
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=ApiDB;user=root;password=yourpassword"
}
```

---

## Apply migrations

```bash
dotnet ef database update
```

---

## Run the application

```bash
dotnet run
```

---

# 📚 Future Improvements

Some future ideas for the project:

* Authentication and Authorization
* JWT tokens
* DTO implementation
* AutoMapper
* Pagination
* Filtering and sorting
* Async repository methods
* FluentValidation
* Unit tests
* Docker support
* Serilog integration
* Clean Architecture
* CQRS
* Redis cache
* API versioning

---

# 📌 Learning Purpose

This repository is primarily intended for:

* Backend practice
* Learning ASP.NET Core
* Experimenting with architecture patterns
* Studying Entity Framework Core
* Improving software engineering skills

---

# 🤝 Contributions

This is currently a personal study project, but suggestions and improvements are always welcome.

---

# 📄 License

This project is available for educational and learning purposes.

---

# 👨‍💻 Author

Developed by me (aka Zeza) as a backend learning project using ASP.NET Core and Entity Framework Core.