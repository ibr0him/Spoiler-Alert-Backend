# 🧠 Spoiler Alert - Backend API

> RESTful API built with **ASP.NET Core** to support the Spoiler Alert movie review platform.

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

## 🔧 Features

- Manage movies, categories, reviews, ratings, and users
- Authentication and role-based authorization (Admin/User)
- JSON-based API consumed by web and mobile apps

## 📦 Tech Stack

- ASP.NET Core Web API (.NET 6/7/8)
- Entity Framework Core + SQL Server
- JWT Authentication
- AutoMapper
- Swagger (API Documentation)

## 🚀 Getting Started

### Prerequisites
- .NET SDK
- SQL Server (or LocalDB)

### Setup

```bash
git clone https://github.com/ibr0him/Spoiler-Alert-Backend
cd Spoiler-Alert-Backend
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at `https://localhost:5001`.

## 🔐 Authentication

- JWT tokens
- Roles: `Admin`, `User`, `Guest`
- Secure endpoints for creating/editing movies and reviews

## 🔍 API Documentation

Once the backend is running, access Swagger UI at:

```
https://localhost:5001/swagger
```

## 🧑‍💻 Authors

- [Ibrahim](https://github.com/ibr0him)
