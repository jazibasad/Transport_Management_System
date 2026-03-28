Transport Management System 
A desktop-based enterprise solution built with C# WinForms and SQL Server to manage fleet operations, driver assignments, and customer bookings. This project features a robust data-driven architecture and a real-time analytics dashboard.

🚀 Key Features
1. Fleet & Personnel Management
Dynamic Inventory: Full CRUD (Create, Read, Update, Delete) operations for vehicles, including technical specs like engine type and mileage.

Driver Tracking: Managed database of drivers with performance ratings, seniority tracking, and automated assignment to vehicles.

Real-time Availability: Smart filtering that only shows "Available" cars in the booking interface, preventing double-booking errors.

2. Advanced Transactional Logic
Non-Refundable History: A custom logic implementation that preserves financial records. When a car is returned, the system renames the booking record with a Returned - prefix. This hides the entry from active grids but keeps the revenue data intact for the Dashboard.

Automated Driver Sync: Selecting a vehicle automatically retrieves and locks the assigned driver to the booking, reducing manual entry.

3. Executive Dashboard & Analytics
Live Metrics: Instant calculation of total fleet size, active users, and total revenue.

Business Intelligence: Uses optimized SQL TOP 1 and GROUP BY queries to identify the Best Driver and Highest-Spending Customer.

4. Security & Administration
Dual-Gate Authentication: Separate login modules for standard staff and System Administrators.

High-DPI Support: Integrated user32.dll API calls to ensure the UI remains crisp and properly scaled on high-resolution monitors (1080p/4K).

System Reset: A "Master Delete" feature that allows an administrator to perform a cascading wipe of the database for a complete system reset.

🛠️ Technical Stack
Language: C# (.NET Framework)

Database: Microsoft SQL Server (LocalDB)

Architecture: ADO.NET with Parameterized Queries (SQL Injection protection)

IDE: Visual Studio 2022

📂 Project Structure
File	Description
Program.cs	Entry point with High-DPI scaling logic.
Login.cs	Main authentication and session management.
Dashboard.cs	Analytics hub and financial reporting.
Vehicles.cs	Fleet management and "Return" logic.
Bookings.cs	Transactional core for rental processing.
Users.cs	Admin-only module for user control and system resets.
⚙️ Installation & Setup
Clone the Repository:

Bash
git clone https://github.com/jazib_asad/Transport-Management-System.git
Database Configuration:

Attach the TransportDb.mdf file to your SQL Server Instance.

Update the SqlConnection string in the source code to match your local path:

C#
SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\YourPath\TransportDb.mdf;Integrated Security=True");
Run: Open the .sln file in Visual Studio and press F5.

📈 Future Enhancements
[ ] Integration of PDF Invoice generation for customers.

[ ] SMS API integration for booking confirmations.

[ ] Multi-language support (English/Urdu toggle).

Developed by: Jazib Asad

Computer Science Professional & Student
