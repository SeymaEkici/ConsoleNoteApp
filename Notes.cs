using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public class Notes
    {
        public string Note { get; set; }
        public User User { get; set; }
        public DateTime DateTime { get; set; }

        public string NoteWithDateTime => $"{Note}.{DateTime:o}";

        public Notes(string note, User user)
        {
            Note = note;
            User = user;
            DateTime = DateTime.UtcNow;
        }
    }
}