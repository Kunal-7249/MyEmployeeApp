# MyEmployeeApp (Employee Management Backend (PostgreSQL + Elasticsearch))

This is the backend system for managing employee data, built with ASP.NET Core (.NET 8).
It uses PostgreSQL as the main database and Elasticsearch for advanced and fast search capabilities.

**🔧 Tech Stack**
.NET 8 (ASP.NET Core Web API)

PostgreSQL – Main data storage

Elasticsearch – Used for search operations

Entity Framework Core – ORM for PostgreSQL

NEST – Elasticsearch .NET client

Swagger / OpenAPI – API testing and documentation

**✅ Features**
Create new employees with fields: Name, Salary, Address, BirthDate

Partial update support (update only what you need)

Delete employee records

Flexible search using Elasticsearch:

age > 30 or age = 25

salary >= 5000

name = John

**🚀 How It Works**
When a new employee is created, the data is stored in PostgreSQL and indexed in Elasticsearch for searching.

All search queries are handled only by Elasticsearch for speed and flexibility.

The system keeps both databases in sync on create, update, and delete.

**🔄 API Endpoints**
Method	Endpoint	Description
POST	/api/employee	Add new employee
PATCH	/api/employee/{id}	Update specific fields
DELETE	/api/employee/{id}	Delete employee
POST	/api/employee/search	Search employees (via Elastic)

Test and explore all APIs using Swagger UI.

**🛠 Setup & Run**
Clone the repository

Configure appsettings.json with your PostgreSQL and Elasticsearch settings

Run the application

Use Swagger (/swagger) to test the APIs
