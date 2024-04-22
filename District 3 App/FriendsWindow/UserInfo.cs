using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.FriendsWindow
{
    public class UserInfo
    {
        private Guid Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }

        public UserInfo(string newUsername, string newPhoneNumber)
        {
            Username = newUsername;
            PhoneNumber = newPhoneNumber;
        }
    }
}
