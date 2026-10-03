# MediShop

An online medicine store built with ASP.NET Core MVC. Customers browse medicines, manage a cart, and place orders. Administrators manage the catalog, suppliers, customers, order fulfillment, and sales and stock reports.

## Features

**Customer**
- Browse the medicine catalog
- Add to cart and check out
- View order confirmation and order history

**Admin**
- Medicines: add, edit, and delete catalog items
- Suppliers: manage supplier records
- Orders: complete and deliver orders (completing an order deducts stock and is blocked when stock is insufficient)
- Customers: view and manage customers
- Dashboard: overview of store activity
- Reports: sales and stock reports

**Authentication & roles**
- ASP.NET Core Identity with role-based access (Admin and Customer areas)

## Tech Stack

- C#, .NET 9, ASP.NET Core MVC (Areas)
- Entity Framework Core 9 (code-first, 4 migrations)
- SQL Server
- ASP.NET Core Identity

## Architecture

Layered solution with four projects:

| Project | Purpose |
|---|---|
| `MediShop` | Web app: controllers, views, Admin / Customer / Identity areas |
| `MediShop.Model` | Entity models |
| `MediShop.DataAccess` | `ApplicationDbContext` and EF Core migrations |
| `MediShop.Utility` | Shared helpers (e.g., email sender) |

**Database:** 12 tables: Medicines, Suppliers, CartItems, Orders, OrderDetails, plus the ASP.NET Core Identity tables.

## Getting Started

1. Clone the repository
```bash
   git clone https://github.com/inam003/MediShop.git
   cd MediShop
```
2. Set your SQL Server connection string in `MediShop/appsettings.json`
3. Apply migrations
```bash
   dotnet ef database update --project MediShop.DataAccess --startup-project MediShop
```
4. Run the app
```bash
   dotnet run --project MediShop
```

## Test Users

| Role | Email | Password |
|---|---|---|
| Admin | adminUser@gmail.com | adminUser@1 |
| Customer | test01@gmail.com | Test@123 |

## Screenshots

_Add 2 to 3 screenshots here (storefront, cart, admin dashboard)._
