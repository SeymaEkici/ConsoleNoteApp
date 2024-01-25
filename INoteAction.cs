using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NoteApp
{
    public interface INoteAction
    {
        void AddNote(User user, string note, List<User> userList);
        List<string> GetNoteList(User user);
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

            if (user.Notes == null)
            {
                user.Notes = new List<string>();
                user.Notes.Add(note);
            }

            SaveNotesToFile(user, userList);

            user.Notes.Add($"{note}.{DateTime.UtcNow:o}");
        }

        public List<string> GetNoteList(User user)
        {
            if (user == null)
            {
                Console.WriteLine("Error: User is null.");
                return new List<string>();
            }

            if (user.Notes == null)
            {
                Console.WriteLine("No notes found.");
                return new List<string>();
            }

            Console.WriteLine("Your Notes:");
            foreach (var note in user.Notes)
            {
                Console.WriteLine(note);
            }

            return user.Notes.ToList();
        }

        public void SaveNotesToFile(User user, List<User> userList)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "notes.txt");

            try
            {
                StringBuilder sb = new StringBuilder();

                foreach (var note in user.Notes)
                {
                    sb.AppendLine(note);
                }
                File.AppendAllText(filePath, sb.ToString());

                Console.WriteLine("Notes have been successfully saved to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving notes to the file: {ex.Message}");
            }
        }
    }
}
