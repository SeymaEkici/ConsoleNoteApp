using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> Notes { get; set; }
        public UserType UserType { get; set; }
        public string Guid  { get; set; }
    }
}
