using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;

namespace District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository
{
    public class UsersRepository
    {
        public List<User> UsersRepositoryList { get; set; }

        private string filePath;

        public UsersRepository()
        {
        }
        public UsersRepository(List<User> usersRepositoryList)
        {
            this.UsersRepositoryList = usersRepositoryList;
        }
        public UsersRepository(string filePath)
        {
            this.filePath = filePath;
            LoadUsersFromXml();
        }

        private void LoadUsersFromXml()
        {
            try
            {
                UsersRepositoryList = new List<User>();
                XDocument xDocument = XDocument.Load(filePath);
                var users = xDocument.Descendants("User");

                foreach (var user in users)
                {
                    User newUser = new User
                    {
                        Id = (Guid)user.Attribute("id"),
                        Username = (string)user.Attribute("Username"),
                        Email = (string)user.Attribute("Email"),
                        Password = (string)user.Attribute("Password"),
                        ConfirmationPassword = (string)user.Attribute("ConfirmationPassword"),
                        FollowingCount = (int)user.Element("Following"),
                        FollowersCount = (int)user.Element("Followers")
                    };

                    UsersRepositoryList.Add(newUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading users from XML: " + ex.Message);
            }
        }

        private void SaveUsersToXml()
        {
            XDocument xDocument = new XDocument(
                new XElement("UserAccounts",
                    UsersRepositoryList.Select(user =>
                        new XElement("User",
                            new XAttribute("id", user.Id),
                            new XAttribute("Username", user.Username),
                            new XAttribute("Email", user.Email),
                            new XAttribute("Password", user.Password),
                            new XAttribute("ConfirmationPassword", user.ConfirmationPassword),
                            new XElement("Following", user.FollowingCount),
                            new XElement("Followers", user.FollowersCount)))));

            xDocument.Save(filePath);
        }

        public User GetUserByName(string username)
        {
            foreach (var user in UsersRepositoryList)
            {
                if (user.Username == username)
                {
                    return user;
                }
            }

            return null;
        }

        public List<User> GetAllUsers()
        {
            return UsersRepositoryList;
        }

        public void AddUser(User user)
        {
            UsersRepositoryList.Add(user);
            SaveUsersToXml();
        }
        public int GetFollowersCount(string username)
        {
            User user = GetUserByName(username);
            if (user != null)
            {
                return user.FollowersCount;
            }
            else
            {
                // Handle user not found scenario
                return -1;
            }
        }
        public int GetFollowingCount(string username)
        {
            User user = GetUserByName(username);
            if (user != null)
            {
                return user.FollowingCount;
            }
            else
            {
                return -1;
            }
        }

        public bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                XElement root = XElement.Load(filePath);
                IEnumerable<XElement> users = from user in root.Elements("User")
                                              where (string)user.Attribute("Email") == email
                                              select user;

                if (users.Any())
                {
                    foreach (XElement user in users)
                    {
                        user.SetAttributeValue("Password", newPassword);
                    }

                    root.Save(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating password: " + ex.Message);
                return false;
            }
        }
        public User GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return UsersRepositoryList.FirstOrDefault(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail);
        }

        public bool UsernameExists(string username)
        {
            return UsersRepositoryList.Any(user => user.Username == username);
        }

        public bool EmailExists(string email)
        {
            return UsersRepositoryList.Any(user => user.Email == email);
        }
    }
}
