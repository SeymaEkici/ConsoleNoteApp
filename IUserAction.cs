using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace NoteApp
{
    public interface IUserAction
    {
        void AddUser(User user);
        void RemoveUser(string PhoneNumber);
        List<User> GetUserListByFilter(string filter);
    }

    public class UserAction : IUserAction
    {
        public List<User> users;

        public UserAction()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            List<User> userList = LoadUsersFromFile();

            if (IsPhoneNumberUnique(userList, user.PhoneNumber))
            {
                user.Guid = Guid.NewGuid().ToString();
                user.Notes = new List<string>();
                users.Add(user);
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("A user with the same phone number already exists.");
            }

            SaveUsersToFile(users);
        }

        public void RemoveUser(string phoneNumber)
        {
            List<User> userList = LoadUsersFromFile();
            User userToDelete = userList.FirstOrDefault(u => u.PhoneNumber == phoneNumber);


            if (userToDelete != null)
            {
                userList.Remove(userToDelete);
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("There is no matching user number.");
            }

            SaveUsersToFile(userList);
        }

        public List<User> GetUserListByFilter(string filter)
        {


            if (string.IsNullOrEmpty(filter) || filter.Length < 3)
            {
                Console.WriteLine("Filter length should be at least 3 characters.");
                return users;
            }
            else
            {
                return LoadUsersFromFile()
                .Where(u =>
                            u.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                            u.Surname.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                            u.Email.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                            u.PhoneNumber.Contains(filter, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }
            return users;
        }

        public void SaveUsersToFile(List<User> userList)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "users.txt");

            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (var user in userList)
                {
                    string userLine = $"{user.Guid},{user.Name},{user.Surname},{user.Email},{user.Password},{user.PhoneNumber},{user.IsAdmin}###";
                    sb.Append(userLine);
                }
                File.AppendAllText(filePath, sb.ToString());

                Console.WriteLine("Users have been successfully saved to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving users to the file: {ex.Message}");
            }
        }

        public List<User> LoadUsersFromFile()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "users.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    string lineText = File.ReadAllText(filePath);
                    string[] userLines = lineText.Split("###");


                    if (userLines.Length == 0)
                    {
                        Console.WriteLine("No user data found in the file.");
                        return new List<User>();
                    }

                    List<User> loadedUsers = new List<User>();

                    foreach (string line in userLines)
                    {
                        string[] values = line.Split(',');

                        if (values.Length >= 7)
                        {
                            User user = new User
                            {
                                Guid = values[0],
                                Name = values[1],
                                Surname = values[2],
                                Email = values[3],
                                Password = values[4],
                                PhoneNumber = values[5],
                                IsAdmin = bool.Parse(values[6])
                            };
                            loadedUsers.Add(user);
                        }
                    }
                    return loadedUsers;
                }

                Console.WriteLine($"Error: The file {filePath} not found.");
                return new List<User>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file {filePath} not found.");
                return new List<User>();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Error: Unauthorized access to the file {filePath}.");
                return new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<User>();
            }
        }

        public bool IsPhoneNumberUnique(List<User> userList, string phoneNumber)
        {
            return users != null ? !users.Any(u => u.PhoneNumber == phoneNumber) : true;

        }

        public User GetUserByGuid(string userGuid)
        {
            var users = LoadUsersFromFile();
            foreach (User user in users)
            {
                if (user.Guid == userGuid) { return user; }
            }
            return null;
        }
    }
}
