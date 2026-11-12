SQL Server Management Utility
📌 About the Project

The SQL Server Management Utility is a Windows desktop application developed using .NET 8 Windows Forms (WinForms) to simplify and streamline common SQL Server database management activities.

The application allows users to perform frequently used database operations through a user-friendly interface without manually opening SQL Server Management Studio (SSMS) for every task.

Users can retrieve database objects such as tables, stored procedures, and functions, generate table creation queries through a simplified interface, and save SQL scripts directly into local folders.

The application also includes a login mechanism for administrators and colleagues, along with change tracking functionality that allows administrators to review the latest changes made within the application.

🚀 Key Features
1. User Login and Access
Login functionality for administrators and colleagues.
User-based access to application functionality.
Administrator access to review recent changes.
2. SQL Server Database Operations
Fetch existing tables from SQL Server.
Retrieve stored procedures.
Retrieve database functions.
View database objects through the application interface.
3. Automatic Table Creation Query Generation
Create new table definitions through the application interface.
Specify table columns and data types.
Automatically generate SQL CREATE TABLE statements without manually writing the complete query.
4. SQL Script Management
Generate SQL queries through the application.
Save generated SQL scripts into local folders.
Maintain scripts for future reference and reuse.
5. Change Tracking
Track the latest changes made through the application.
Allow administrators to review recent changes.
Help administrators monitor application activity.
🛠️ Technologies Used
Framework: .NET 8
Programming Language: C#
UI Technology: Windows Forms (WinForms)
Database: Microsoft SQL Server
Development Environment: Visual Studio
⚙️ Application Workflow
Launch the Windows desktop application.
Log in using administrator or colleague credentials.
Connect to the required SQL Server database.
Select the required database operation.
Fetch tables, stored procedures, or functions.
Create table definitions using the application interface.
Automatically generate the corresponding SQL query.
Save generated SQL scripts into local folders.
Administrators can review the latest changes.
📂 Project Structure
text
SQL-Server-Management-Utility/
│
├── Forms/
│   ├── LoginForm.cs
│   └── MainForm.cs
│
├── Database/
│   └── Database connection files
│
├── Scripts/
│   └── Generated SQL scripts
│
├── Resources/
│   └── Application resources
│
├── README.md
└── SQLServerManagementUtility.csproj


Note: Update this example structure to match your actual project folders.

▶️ Installation and Execution
Prerequisites
Windows operating system.
.NET 8 SDK or required .NET 8 runtime.
Access to a Microsoft SQL Server instance.
Visual Studio 2022 or compatible development environment.
Steps to Run
Clone the repository:
bash
git clone https://github.com/USERNAME/REPOSITORY.git

Navigate to the project directory:
bash
cd REPOSITORY

Open the .sln or .csproj file in Visual Studio.
Configure the SQL Server connection settings.
Restore NuGet packages.
Build and run the application.
📸 Screenshots

Add screenshots of the following application features:

Login screen.
Main dashboard.
Fetching database tables.
Fetching stored procedures and functions.
Automatic table creation query generation.
Saving SQL scripts locally.
Administrator change tracking screen.
🎯 Project Objective

The main objective of this project is to simplify repetitive SQL Server management activities by providing a centralized Windows desktop application.

It reduces manual SQL query writing, makes database object retrieval easier, provides a convenient way to generate and store SQL scripts, and allows administrators to review recent application changes.

👨‍💻 Author

Harshal Nitin Sanyashi
