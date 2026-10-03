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

<img width="1897" height="917" alt="image" src="https://github.com/user-attachments/assets/3ec908e9-1a1c-4a11-8160-8c5388eec0b4" />
<img width="1917" height="916" alt="image" src="https://github.com/user-attachments/assets/ad735286-3092-4c4d-9c06-a3c5890da5d7" />
<img width="1917" height="916" alt="image" src="https://github.com/user-attachments/assets/6b78662f-dd57-444b-b8d6-f1bbe2b0f9fc" />
<img width="1917" height="921" alt="image" src="https://github.com/user-attachments/assets/60acbe18-7d9e-4d37-8c29-5f589d3f3af3" />
<img width="1917" height="912" alt="image" src="https://github.com/user-attachments/assets/ae2ca8e2-f62e-48bf-8309-606054214d28" />




