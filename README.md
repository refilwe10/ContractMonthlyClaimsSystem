# Contract Monthly Claims System

##  Overview
The **Contract Monthly Claims System** is a WPF desktop application developed in C# and .NET 8.0.  
It allows lecturers to submit monthly claims, upload supporting documents, and track claim approval statuses.  
Programme Coordinators and Academic Managers can verify and approve or reject claims.

---

## Features
- Lecturer Dashboard to view and submit claims  
- Claim submission with automatic total calculation  
- File upload (PDF, DOCX, XLSX) for supporting documents  
- Admin Dashboard for approving/rejecting claims  
- SQLite database integration  
- Error handling and validation  
- Unit testing for core logic    

- 
---

## Unit Tests
The **ContractMonthlyClaimsSystem.Tests** project includes:
- Tests for claim total calculation  
- Tests for input validation  
- Tests for data handling and error management  

To run tests:
1. Open the Test Explorer in Visual Studio (`Test` → `Run All Tests`)
2. Verify all tests pass successfully.

---

## Technologies Used
- **.NET 8.0 (Windows)**  
- **WPF (Windows Presentation Foundation)**  
- **Entity Framework Core (SQLite)**  
- **xUnit / MSTest** for unit testing  

---

## How to Run the Application
1. Open the solution in Visual Studio.  
2. Set `ContractMonthlyClaimsSystem` as the startup project.  
3. Run the project (`F5`).  
4. The application will automatically create and use `claims.db` (SQLite).  

---

## Version Control
This project includes  commits with descriptive messages such as:
- Initial project setup  
- Added claim model and database context  
- Implemented upload and approval functionality  
- Added unit tests  
- Final refinements and bug fixes  

---

##  Author
**[Refilwe Phore]**  
Student ID: [St10458818]  
Module: PROG6212  
Institution: The Rosebank College IIE  

---

