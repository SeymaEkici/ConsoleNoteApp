
# Application Description

This console application allows users to view their notes, add new users, search for users based on specific criteria, and delete users. Additionally, regular users can add and list their own notes.

## User Login

Users are required to enter their email and password for login. Admin users are directed to a special admin menu, while other users are directed to the normal user menu.

## Admin Menu

1. Add User:

Collects Name, Surname, Phone, Email, and IsAdmin information.
The phone number must be unique and not used before.
User information is saved to the users.txt file.

2. Search User:

Performs a user search based on Name, Surname, Mail, or Phone.
Search results are displayed on the screen.

3. Delete User:

Deletes a user with the provided phone number.
Displays a warning if the user does not exist.

## Normal User Menu

1. Add Note:

Allows users to add their own notes.
Note and date information are saved to the notices.txt file.

2. List My Notes:

Displays notes previously added by the user.