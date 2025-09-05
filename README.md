# 📚 BulkyWeb - ASP.NET Core 8 MVC Project

This repository contains the **Bulky Book Website** project, developed while following a **Beginner to Advanced .NET 8 ASP.NET Core MVC Course**.  
The project covers everything from the fundamentals of ASP.NET Core to advanced concepts, with practical implementation in a real-world style e-commerce application.

---

## 🎯 Features
- Structure of ASP.NET Core MVC and Razor Projects  
- Integrating **Entity Framework Core** with Code First Migrations
- Repository Pattern for Data Access  
- CRUD operations with Razor Pages  
- Sessions, TempData, ViewBag, ViewData in ASP.NET Core  
- Role Management with ASP.NET Core Identity  
- View Components & Partial Views  
- Bootstrap v5 integration for UI  
- Stripe Payment Gateway integration  
- Database seeding & automatic migrations  
- Email Notifications  
- Deploying to **Microsoft Azure** and **IIS**
---

## 🛠️ Tech Stack
- **Language:** C#  
- **Framework:** ASP.NET Core 8 MVC  
- **Database:** SQL Server (EF Core – Code First)  
- **UI:** Razor Pages + Bootstrap v5  
- **Authentication:** ASP.NET Core Identity  
- **Payment:** Stripe Integration  
- **Deployment:** Microsoft Azure 

---

## 📂 Project Structure
```

.
├── .github/workflows       # CI/CD workflows (Azure build & deployment)
├── Bulky.DataAccess        # Data Access Layer (EF Core, Repository, Unit of Work)
├── Bulky.Models            # Entity models & validation
├── Bulky.Utility           # Helper classes, session for shopping cart, constants
├── BulkyWeb                # Main ASP.NET Core MVC web application
├── BulkyWebRazor_Temp      # Razor Pages demo project (CRUD example)
├── BulkyWeb.sln            # Solution file
├── .gitignore
├── .gitattributes
└── README.md


````

## ⚙️ Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/tungcbh/BulkyWeb.git
   cd BulkyWeb
   ```

2. **Setup Database**

   * Ensure SQL Server is installed and running.
   * Update connection string in `appsettings.json`.
   * Apply migrations:

     ```bash
     dotnet ef database update
     ```

3. **Run the application**

   ```bash
   dotnet run --project BulkyWeb
   ```

   Access at: `https://localhost:7071`

---

## 📦 Deployment

* Deploy to **Azure App Service** (with SQL Azure Database).
* Or host locally on **IIS** with SQL Server.

---
