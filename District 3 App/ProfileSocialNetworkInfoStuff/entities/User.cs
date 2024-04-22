using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Entities
{
    public class User : IComparable<User> // MODIFY UML DIAGRAM USER CLASS
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ConfirmationPassword { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int FollowingCount { get; set; }
        public int FollowersCount { get; set; }
        public TimeSpan Usersession { get; set; }

        public User()
        {
        }
        public User(Guid id, string username, string password, string email, string confirmationPassword)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.ConfirmationPassword = confirmationPassword;
        }

        public User(Guid id, string username, string password, string email, string confirmationPassword, int followingCount, int followersCount)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.ConfirmationPassword = confirmationPassword;
            this.FollowersCount = followersCount;
            this.FollowingCount = followingCount;
        }

        public User(Guid id, string username, string password, string email, string confirmationPassword, TimeSpan usersession)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.ConfirmationPassword = confirmationPassword;
            this.Usersession = usersession;
        }

        public string RegistrationDateToString()
        {
            return this.RegistrationDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public int CompareTo(User other)
        {
            return this.Username.CompareTo(other.Username);
        }
    }
}
