# FitnessHub

**FitnessHub** is a web application designed to manage and facilitate dance classes, swimming lessons, and studio bookings. The platform allows users to register, book classes, and make payments securely online. Administrators can manage class schedules, instructor assignments, and user registrations through an intuitive interface.

## Table of Contents

- [Installation](#installation)
- [Project Structure](#project-structure)
- [Features](#features)
- [Usage](#usage)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)

## Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/kathanpatel29/FitnessHub.git
   cd FitnessHub

2. **Restore the NuGet packages:**
    Open the solution in Visual Studio and restore the required NuGet packages.

3. **Database Setup:**
    Update the connection string in Web.config to point to your SQL Server instance.
   ```bash
   Update-Database

4. **Run the application:**
    Press F5 in Visual Studio to build and run the application.

## Project Structure:
   \```
FitnessHub/  
│  
├── App_Start/                  # Configuration files for routing, authentication, etc.  
├── Controllers/                # MVC Controllers handling application logic  
├── Models/                     # Data models representing entities like Users, Classes, etc.  
├── Views/                      # Razor views for rendering UI  
├── Scripts/                    # JavaScript files for client-side functionality  
├── Content/                    # CSS, images, and other static assets  
├── bin/                        # Compiled assemblies and dependencies  
├── FitnessHub.sln              # Visual Studio solution file  
└── Web.config                  # Application configuration file  
\```

 ## Features
   ***User Management:*** Registration, login, and profile management. Role-based access control (Admin, Instructor, User).  
   ***Class Management:*** CRUD operations for dance classes and swimming lessons. View class schedules and availability. Booking and cancellation of classes.  
   ***Payment System:*** Secure payment processing for class bookings. Payment history and receipt generation.  
   ***Admin Dashboard:*** Manage users, classes, and instructors. View and generate reports on bookings and payments.  

## Usage
   ***Users:*** Register on the platform. Browse available dance classes and swimming lessons. Book classes and make payments online. View booking history and manage class enrollments.  
   ***Admin:*** Access the admin dashboard to manage the platform. Add, edit, or remove classes and instructors. Monitor user activity and generate reports.  

## Technologies Used
  ***Backend:*** ASP.NET MVC, Entity Framework  
  ***Frontend:*** HTML5, CSS3, Bootstrap, jQuery  
  ***Database:*** SQL Server  
  ***Authentication:*** ASP.NET Identity  

## Contributing
  Contributions are welcome! Please fork this repository and submit a pull request.  
  
