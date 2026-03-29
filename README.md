# 🚗 Transport Management System (TMS)

A desktop-based enterprise solution built with **C# WinForms** and **SQL Server** to manage fleet operations, driver assignments, and customer bookings.  
This project features a robust data-driven architecture and a real-time analytics dashboard.

---

## 🚀 Key Features

### 1. Fleet & Personnel Management
- **Dynamic Inventory:** Full CRUD (Create, Read, Update, Delete) operations for vehicles, including technical specs like engine type and mileage.  
- **Driver Tracking:** Managed database of drivers with performance ratings, seniority tracking, and automated assignment to vehicles.  
- **Real-time Availability:** Smart filtering that only shows **"Available"** cars in the booking interface, preventing double-booking errors.  

---

### 2. Advanced Transactional Logic
- **Non-Refundable History:** When a car is returned, the system renames the booking record with a `Returned -` prefix. This hides the entry from active grids but keeps revenue data intact for the dashboard.  
- **Automated Driver Sync:** Selecting a vehicle automatically retrieves and locks the assigned driver to the booking, reducing manual entry.  

---

### 3. Executive Dashboard & Analytics
- **Live Metrics:** Instant calculation of total fleet size, active users, and total revenue.  
- **Business Intelligence:** Uses optimized SQL queries (`TOP 1`, `GROUP BY`) to identify:
  - Best Driver  
  - Highest-Spending Customer  

---

### 4. Security & Administration
- **Dual-Gate Authentication:** Separate login modules for standard staff and system administrators.  
- **High-DPI Support:** Integrated `user32.dll` API calls to ensure proper UI scaling on high-resolution monitors (1080p/4K).  
- **System Reset:** A "Master Delete" feature that allows administrators to reset the entire database.  

---

## 🛠️ Technical Stack

- **Language:** C# (.NET Framework)  
- **Database:** Microsoft SQL Server (LocalDB)  
- **Architecture:** ADO.NET with Parameterized Queries (SQL Injection protection)  
- **IDE:** Visual Studio 2022  

---

## 📂 Project Structure

| File           | Description                                      |
|----------------|--------------------------------------------------|
| `Program.cs`   | Entry point with High-DPI scaling logic          |
| `Login.cs`     | Authentication and session management            |
| `Dashboard.cs` | Analytics hub and financial reporting            |
| `Vehicles.cs`  | Fleet management and return logic                |
| `Bookings.cs`  | Core rental transaction processing               |
| `Users.cs`     | Admin-only module for user control and reset     |

---

## ⚙️ Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/jazib_asad/Transport-Management-System.git
