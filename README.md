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
    git clone https://github.com/EfsunPeynirci/WebAppHotelManagement
    ```
2. Open the solution file in Visual Studio.
3. Create a `Web.config` file in the root directory of the project with the following content:
    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
      <system.web>
        <compilation debug="true" targetFramework="4.7.2" />
        <httpRuntime targetFramework="4.7.2" />
        <authentication mode="Forms">
          <forms loginUrl="~/Account/Login" timeout="2880" />
        </authentication>
        <authorization>
          <allow users="*" />
        </authorization>
      </system.web>
    </configuration>
    ```
4. Update the database connection string in `Web.config` to match your MSSQL server settings.
5. Run the application using Visual Studio.

## Screenshots
### Home Page
![home](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/d953596a-dabc-4599-8b68-a5bb5c816c44)

### User Log In
![login](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/26bf6aec-58c1-4ccf-b1cf-8330173e6de1)

### User Registration
![1 - register](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/729398f8-223f-46c6-a1ed-a30f853088bc)

### Room Management - Add Room
![2 - add room](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/63f1d807-53cb-4e6a-b0ff-5d132e01d4db)

### Add Room Form
![4 - add room form](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/120ecf48-93bd-4262-a750-ff6d1b762366)

### Update Room Form
![5 - update](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/3f2a8e3d-0004-4a44-a4ab-32517ec0d011)

### Database Room Table
![6 - database room](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/5581dbc7-654c-489e-b524-b0f9914ca882)

### Database Diagram
![database_diagram](https://github.com/EfsunPeynirci/WebAppHotelManagement/assets/100719856/4b7ae8f4-bdc4-4976-81c2-b0ed935bab39)

## Usage
- Register a new user or log in with existing credentials.
- Navigate to the 'Room' section to manage room records.
- Add new rooms, update existing room details, or delete rooms.
- Log out when finished.

## Acknowledgements
- Thanks to the MVC5 and C# communities for their excellent documentation and support.
- Special thanks to the YouTube tutorial by Tek Tuition which guided me through this project. [YouTube Playlist](https://www.youtube.com/watch?v=u-p7V6Yc0NM&list=PL8weiNcho1j7My1wL2cZzSMaJFhqna0BA)

Feel free to contact me for any questions or contributions.




