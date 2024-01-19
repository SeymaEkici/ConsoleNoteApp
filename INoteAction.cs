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
        void AddNote(User user, string note);
        List<string> GetNoteList(User user);
    }

    public class NoteAction : INoteAction
    {
        public List<User> users;
        public UserAction userAction;

        public NoteAction(List<User> users, UserAction userAction)
        {
            this.users = users;
            this.userAction = userAction;
        }

        public void AddNote(User user, string note)
        {
            if (user.Notes == null)
            {
                user.Notes = new List<string>();
            }

            user.Notes.Add($"{note}.{DateTime.UtcNow:o}");
            userAction.SaveUsersToFile();
            Console.WriteLine("Note added successfully.");
        }

        public List<string> GetNoteList(User user)
        {
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
    }
}
