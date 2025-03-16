NoteApp
=======

Overview
--------

**NoteApp** is a simple console-based note-taking application that allows users to add, store, and retrieve notes. The application supports user management with two roles: **Admin** and **Normal User**.

Admins can:

-   Add new users
-   Search for users
-   List all users
-   Delete users

Normal users can:

-   Add notes
-   List their own notes

Project Structure
-----------------

The application consists of the following components:

### 1\. Interfaces

-   **INoteAction.cs**: Defines operations for managing notes.
-   **IUserAction.cs**: Defines operations for managing users.

### 2\. Classes

-   **NoteAction.cs**: Implements the `INoteAction` interface, handling note operations such as adding, saving, and loading notes.
-   **UserAction.cs**: Implements the `IUserAction` interface, handling user operations such as adding, deleting, and searching users.
-   **Notes.cs**: Represents a note, containing note text, the associated user, and the timestamp.
-   **User.cs** (Not provided in the original code but assumed to exist): Represents a user with attributes such as name, email, password, phone number, and user type.
-   **Program.cs**: The main entry point of the application, containing user authentication and menu logic.

Features
--------

### User Management

-   Admins can add new users by providing user details.
-   Users are stored in a `users.txt` file.
-   Users can be searched by name, surname, email, or phone number.
-   Users can be deleted from the system.

### Note Management

-   Users can add notes, which are saved to a `notes.txt` file.
-   Notes include a unique user identifier and timestamp.
-   Users can list their previously saved notes.

File Storage
------------

The application stores data using text files:

-   **users.txt**: Stores user details, separated by `###`.
-   **notes.txt**: Stores user notes, also separated by `###`.

How to Run the Application
--------------------------

1.  Compile and run the program using a C# compiler.
2.  When prompted, enter your email and password.
3.  Based on your role, interact with the menu to perform user or note operations.

Dependencies
------------

-   .NET framework (for running C# applications)
-   Basic file handling for data storage

Future Improvements
-------------------

-   Implement a GUI for better user experience.
-   Store data in a database instead of text files.
-   Improve error handling and validation.
-   Implement encryption for storing sensitive user data.

Author
------

Developed for educational purposes to demonstrate file handling and basic user management in C#.
