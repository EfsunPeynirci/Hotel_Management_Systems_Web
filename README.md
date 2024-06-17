# Hotel Management System

## Project Overview
This Hotel Management System is a web application developed using C# and the MVC5 framework. The backend database is managed using Microsoft SQL Server (MSSQL). The application provides functionalities for user registration, login, room management, and user logout.

## Features
- **User Registration and Login:**
  - Users can register and login to the system.
  - Upon successful login, the navigation bar updates to include 'Room' and 'Logout' options, while 'Login' and 'Sign Up' are hidden.

- **Room Management:**
  - The 'Room' section allows users to view a table of existing room records.
  - Users can add new rooms using the 'Add Room' functionality. New room details are saved in the database and displayed in the table.
  - Existing room records can be updated or deleted. 
  - Deleting a room sets its `isActive` status to 0 in the database, making it invisible on the website but retaining the record.

- **Logout:**
  - Users can log out, which will revert the navigation bar to display 'Login' and 'Sign Up' options.

## Technologies Used
- **Programming Language:** C#
- **Framework:** MVC5
- **Database:** Microsoft SQL Server (MSSQL)
- **Front-end:** HTML, CSS

## Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/hotel-management-system.git
    ```
2. Open the solution file in Visual Studio.
3. Update the database connection string in `Web.config` to match your MSSQL server settings.
4. Run the application using Visual Studio.

## Screenshots
### User Registration
![Register Page](./images/1 - register.png)

### Room Management - Add Room
![Add Room](./images/2 - add room.png)

### Add Room Form
![Add Room Form](./images/4 - add room form.png)

### Update Room
![Update Room](./images/5 - update.png)

### Database Room Table
![Database Room](./images/6 - database room.png)

### Database Diagram
![Database Diagram](./images/database_diagram.png)

## Usage
- Register a new user or log in with existing credentials.
- Navigate to the 'Room' section to manage room records.
- Add new rooms, update existing room details, or delete rooms.
- Log out when finished.

## Acknowledgements
- Thanks to the MVC5 and C# communities for their excellent documentation and support.
- Special thanks to the YouTube tutorial by Tek Tuition which guided me through this project. [YouTube Playlist](https://www.youtube.com/watch?v=u-p7V6Yc0NM&list=PL8weiNcho1j7My1wL2cZzSMaJFhqna0BA)

Feel free to contact me for any questions or contributions.




