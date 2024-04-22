using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;

namespace District_3_App.LogIn
{
    public class UserManager
    {
        private List<User> users = new List<User>();
        private static User? currentUser;

        public string GetCurrentUser()
        {
            if (currentUser == null)
            {
                return string.Empty;
            }

            return currentUser.Username;
        }
        public UserManager(string filePath)
        {
            LoadUsersFromXml(filePath);
        }

        public bool AuthenticateUser(string username, string password)
        {
            User? user = users.Find(u => u.Username == username);
            if (user != null && user.Password == password)
            {
                StartOrRenewSession(user);
                return true;
            }
            return false;
        }

        public void StartOrRenewSession(User user)
        {
            UserManager.currentUser = user;
        }

        public bool IsUserLoggedIn()
        {
            return UserManager.currentUser != null;
        }

        public void LogOutUser()
        {
            UserManager.currentUser = null;
        }

        private void LoadUsersFromXml(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);
                foreach (var userElement in doc.Root.Elements("User"))
                {
                    User user = new User
                    {
                        Id = Guid.Parse(userElement.Attribute("id").Value),
                        Username = userElement.Attribute("Username").Value,
                        Email = userElement.Attribute("Email").Value,
                        Password = userElement.Attribute("Password").Value,
                        ConfirmationPassword = userElement.Attribute("ConfirmationPassword").Value,
                        Usersession = TimeSpan.FromMinutes(0)
                    };
                    users.Add(user);
                }
            }
            catch
            {
                Console.WriteLine("File not found");
            }
        }
        public IReadOnlyList<User> GetUsers()
        {
            return users.AsReadOnly();
        }
    }
}

