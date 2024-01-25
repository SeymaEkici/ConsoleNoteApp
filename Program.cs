
using NoteApp;

internal class Program
{
    public static List<User> users = new List<User>();
    public static UserAction userAction;
    public static NoteAction noteAction;
    public static User user;
    private static List<User> userList;

    static void Main(string[] args)
    {
        UserAction userAction = new UserAction();
        try
        {
            users = new List<User>();
            users = userAction.LoadUsersFromFile();
            noteAction = new NoteAction(users, userAction, userList);

            User identifiedUser = IdentifyUser();

            if (identifiedUser != null)
            {
                user = identifiedUser;
                ShowMenu();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void ShowMenu()
    {
        switch (user.UserType)
        {
            case UserType.Admin:
                AdminMenu(user);
                break;

            case UserType.Normal:
                NormalUserMenu(user);
                break;

            case UserType.Invalid:
                Console.WriteLine("Invalid input! Try again.");
                break;

            default:
                Console.WriteLine("Unexpected input! Try again.");
                ShowMenu();
                break;
        }
    }

    public static User IdentifyUser()
    {
        Console.Write("Enter your email address: ");
        String email = Console.ReadLine();
        Console.Write("Enter your password: ");
        String password = Console.ReadLine();
        Console.WriteLine(" ");

        User identifiedUser = users.Find(u => u.Email == email && u.Password == password);

        if (identifiedUser != null)
        {
            identifiedUser.UserType = identifiedUser.IsAdmin ? UserType.Admin : UserType.Normal;
        }

        return identifiedUser;
    }

    static void NormalUserMenu(User user)
    {
        if (user == null)
        {
            Console.WriteLine("Error: User is null.");
            return;
        }

        Console.WriteLine("What do you want?" +
                          "\n1- Add notes" +
                          "\n2- List my notes" +
                          "\n3- Exit");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Enter your note: ");
                string note = Console.ReadLine();
                noteAction.AddNote(user, note, userList);
                NormalUserMenu(user);
                break;

            case 2:
                List<string> userNotes = noteAction.GetNoteList(user);
                NormalUserMenu(user);
                break;

            case 3:
                break;

            default:
                Console.WriteLine("Invalid choice!");
                NormalUserMenu(user);
                break;
        }
    }

    static void AdminMenu(User user)
    {
        UserAction userAction = new UserAction(); 

        Console.WriteLine("Welcome Admin! What do you want to do?" +
                          "\n1- Add user" +
                          "\n2- Search user" +
                          "\n3- List users" +
                          "\n4- Delete user" +
                          "\n5- Exit");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                User newUser = GetUserInformationFromConsole();
                userAction.AddUser(newUser);
                break;

            case 2:
                Console.Write("Enter the search filter (name, surname, email, or phone): ");
                string searchFilter = Console.ReadLine();

                List<User> filteredUsers = userAction.GetUserListByFilter(searchFilter);

                if (filteredUsers.Count >= 1)
                {
                    Console.WriteLine("Matching users found:");
                    foreach (var person in filteredUsers)
                    {
                        Console.WriteLine($"Name: {person.Name}, Email: {person.Email}, Phone: {person.PhoneNumber}");
                    }
                }
                else
                {
                    Console.WriteLine("No matching users found!");
                }
                break;

            case 3:
                List<User> userList = userAction.LoadUsersFromFile();
                Console.WriteLine("List of users:");
                foreach (var u in userList)
                {
                    Console.WriteLine($"Name: {u.Name}, Email: {u.Email}, Phone: {u.PhoneNumber}");
                }
                break;

            case 4:
                Console.Write("Enter the phone number of the user to delete: ");
                string deletePhoneNumber = Console.ReadLine();
                userAction.RemoveUser(deletePhoneNumber);
                break;

            case 5:
                break;

            default:
                Console.WriteLine("Invalid choice!");
                AdminMenu(user);
                break;
        }
    }

    public static User GetUserInformationFromConsole()
    {
        Console.WriteLine("Enter the user information that you want to add:");

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Surname: ");
        string surname = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Phone Number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        Console.Write("Is Admin: ");
        bool isAdmin = Convert.ToBoolean(Console.ReadLine());

        return new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            PhoneNumber = phoneNumber,
            Password = password,
            IsAdmin = isAdmin
        };
    }
}