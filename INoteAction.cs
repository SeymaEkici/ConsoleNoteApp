using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace NoteApp
{
    public interface INoteAction
    {
        void AddNote(User user, string note, List<User> userList);
        void SaveNoteToFile(User user, string note);
        List<Notes> LoadNotesFromFile(User targetUser);
        Notes CreateNoteFromValues(string[] values);

    }

    public class NoteAction : INoteAction
    {
        public List<User> users;
        public UserAction userAction;
        public List<User> userList;

        public NoteAction(List<User> users, UserAction userAction, List<User> userList)
        {
            this.users = users;
            this.userAction = userAction;
            this.userList = userList;
        }

        public void AddNote(User user, string note, List<User> userList)
        {
            if (user == null)
            {
                Console.WriteLine("Error: User is null.");
                return;
            }

            string noteWithGuidAndDateTime = $"{user.Guid}#{note}#{DateTime.UtcNow:o}";

            SaveNoteToFile(user, noteWithGuidAndDateTime);

            Console.WriteLine("Note added successfully.");
        }

        public void SaveNoteToFile(User user, string note)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "notes.txt");

            try
            {
                string noteWithDelimiter = $"{note}###";
                File.AppendAllText(filePath, noteWithDelimiter);

                Console.WriteLine("Notes have been successfully saved to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving notes to the file: {ex.Message}");
            }
        }

        public List<Notes> LoadNotesFromFile(User targetUser)
        {
            UserAction userAction = new UserAction();
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "notes.txt");

            try
            {
                if (File.Exists(filePath))
                {
                    string lineText = File.ReadAllText(filePath);
                    string[] noteLines = lineText.Split("###");

                    if (noteLines.Length == 0)
                    {
                        Console.WriteLine("No note data found in the file.");
                        return new List<Notes>();
                    }

                    List<Notes> loadedNotes = new List<Notes>();

                    foreach (string line in noteLines)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {        
                        string[] values = line.Split('#');

                        if (values.Length >= 3)
                        {
                            Notes note = CreateNoteFromValues(values);

                            if (note != null && note.User != null && note.User.Guid == targetUser.Guid)
                            {
                                Console.WriteLine($"Note: {note.Note}, DateTime: {note.DateTime:yyyy-MM-ddTHH:mm:ss.fffffffZ}");
                                loadedNotes.Add(note);
                            }
                        }

                        }
                    }
                    return loadedNotes;
                }

                Console.WriteLine($"Error: The file {filePath} not found.");
                return new List<Notes>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file {filePath} not found.");
                return new List<Notes>();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Error: Unauthorized access to the file {filePath}.");
                return new List<Notes>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Notes>();
            }
        }

        public Notes CreateNoteFromValues(string[] values)
        {
            UserAction userAction = new UserAction();

            string userGuid = values[0];
            string noteText = values[1];
            string dateTimeString = values[2];

            User user = userAction.GetUserByGuid(userGuid);

            if (user == null)
            {
               Console.WriteLine($"Error: User with GUID {userGuid} not found.");
                return null;
            }

            DateTime dateTime;

            if (!DateTime.TryParseExact(dateTimeString, "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dateTime))
            {
                Console.WriteLine($"Error: Invalid DateTime format - {dateTimeString}");
                return null;
            }

            return new Notes
            {
                User = user,
                Note = noteText,
                DateTime = dateTime
            };
        }
    }
}