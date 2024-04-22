using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.FriendsSettings
{
    internal class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }

        public User(string newUsername, string newPhoneNumber)
        {
            Username = newUsername;
            PhoneNumber = newPhoneNumber;
        }

        public User()
        {
        }
    }
}
