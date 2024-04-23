using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;

namespace District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository
{
    public class GroupsRepository
    {
        public List<Group> GroupsRepository1 { get; set; }
        private string filePath;

        public GroupsRepository()
        {
            this.filePath = "./ProfileSocialNetworkInfoStuff/Groups.xml";
            // Construct the file path relative to the executable directory
            // string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // this.filePath = Path.Combine(baseDirectory, "ProfileSocialNetworkInfoStuff", "Groups.xml");

            // Load groups from XML
            this.GroupsRepository1 = LoadGroupsFromXML();
            // change the filePath to the correct path
            // this.filePath = "D:\\University\\OOP\\Homeworks\\CasualProfile\\District 3 App\\ProfileSocialNetworkInfoStuff\\Groups.xml";
            // this.GroupsRepository1 = LoadGroupsFromXML();
            // this.groupsRepository = new List<Group>();
        }
        public GroupsRepository(List<Group> groupsRepository)
        {
            this.GroupsRepository1 = groupsRepository;
            this.filePath = "./ProfileSocialNetworkInfoStuff/Groups.xml";

            SaveGroupsInXML();
        }

        public List<Group> LoadGroupsFromXML()
        {
            List<Group> loadedGroups = new List<Group>();

            XmlDocument xmlDoc = new XmlDocument();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = baseDirectory.Substring(0, baseDirectory.IndexOf("bin\\Debug"));

            string currfilePath = System.IO.Path.Combine(relativePath, "ProfileSocialNetworkInfoStuff");
            filePath = System.IO.Path.Combine(currfilePath, "Groups.xml");

            xmlDoc.Load(filePath);

            foreach (XmlNode groupNode in xmlDoc.SelectNodes("//group"))
            {
                Group group = new Group();
                group.Id = Guid.Parse(groupNode.Attributes["groupId"].Value);
                group.GroupName = groupNode.Attributes["groupName"].Value;
                group.GroupMembers = new List<User>();

                foreach (XmlNode userNode in groupNode.SelectNodes("members/user"))
                {
                    User user = new User();
                    user.Id = Guid.Parse(userNode.Attributes["userId"].Value);
                    user.Username = userNode.Attributes["username"].Value;
                    user.Password = userNode.Attributes["password"].Value;
                    user.Email = userNode.Attributes["email"].Value;
                    user.ConfirmationPassword = userNode.Attributes["confirmationPassword"].Value;

                    group.GroupMembers.Add(user);
                }

                loadedGroups.Add(group);
            }

            return loadedGroups;
        }

        public void SaveGroupsInXML()
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode groupsNode = xmlDocument.CreateElement("groups");
            xmlDocument.AppendChild(groupsNode);

            foreach (Group group in GroupsRepository1)
            {
                XmlNode groupNode = xmlDocument.CreateElement("group");
                groupsNode.AppendChild(groupNode);

                XmlAttribute groupIdAttribute = xmlDocument.CreateAttribute("groupId");
                groupIdAttribute.Value = group.Id.ToString();
                groupNode.Attributes.Append(groupIdAttribute);

                XmlAttribute groupNameAttribute = xmlDocument.CreateAttribute("groupName");
                groupNameAttribute.Value = group.GroupName;
                groupNode.Attributes.Append(groupNameAttribute);

                XmlNode members = xmlDocument.CreateElement("members");
                groupNode.AppendChild(members);

                foreach (var member in group.GroupMembers)
                {
                    XmlNode userNode = xmlDocument.CreateElement("user");
                    members.AppendChild(userNode);

                    XmlAttribute userIdAttribute = xmlDocument.CreateAttribute("userId");
                    userIdAttribute.Value = member.Id.ToString();
                    userNode.Attributes.Append(userIdAttribute);

                    XmlAttribute usernameAttribute = xmlDocument.CreateAttribute("username");
                    usernameAttribute.Value = member.Username;
                    userNode.Attributes.Append(usernameAttribute);

                    XmlAttribute passwordAttribute = xmlDocument.CreateAttribute("password");
                    passwordAttribute.Value = member.Password;
                    userNode.Attributes.Append(passwordAttribute);

                    XmlAttribute emailAttribute = xmlDocument.CreateAttribute("email");
                    emailAttribute.Value = member.Email;
                    userNode.Attributes.Append(emailAttribute);

                    XmlAttribute confirmationPasswordAttribute = xmlDocument.CreateAttribute("confirmationPassword");
                    confirmationPasswordAttribute.Value = member.ConfirmationPassword;
                    userNode.Attributes.Append(confirmationPasswordAttribute);
                }
            }

            xmlDocument.Save(this.filePath);
        }

        public List<Group> GetAllGroups()
        {
            return this.GroupsRepository1;
        }

        public bool AddGroup(Group groupToAdd)
        {
            foreach (var group in GroupsRepository1)
            {
                if (group.Id == groupToAdd.Id)
                {
                    return false;
                }
            }

            this.GroupsRepository1.Add(groupToAdd);
            SaveGroupsInXML();
            return true;
        }

        public bool RemoveGroup(Group groupToRemove)
        {
            foreach (var group in GroupsRepository1)
            {
                if (group.Id == groupToRemove.Id)
                {
                    GroupsRepository1.Remove(group);
                    SaveGroupsInXML();
                    return true;
                }
            }
            return false;
        }

        public Group GetGroupByGroupName(string groupName)
        {
            foreach (var group in GroupsRepository1)
            {
                if (groupName == group.GroupName)
                {
                    return group;
                }
            }
            return null;
        }

        public bool AddMemberToGroup(string groupName, User memberToAdd)
        {
            foreach (var group in GroupsRepository1)
            {
                if (group.GroupName == groupName)
                {
                    foreach (var currentMember in group.GroupMembers)
                    {
                        if (currentMember.Username == memberToAdd.Username)
                        {
                            return false;
                        }
                    }

                    group.GroupMembers.Add(memberToAdd);
                    SaveGroupsInXML();
                    return true;
                }
            }
            return false;
        }

        public bool RemoveMemberFromGroup(string groupName, User memberToRemove)
        {
            foreach (var group in GroupsRepository1)
            {
                if (group.GroupName == groupName)
                {
                    foreach (var currentMember in group.GroupMembers)
                    {
                        if (currentMember.Username == memberToRemove.Username)
                        {
                            group.GroupMembers.Remove(memberToRemove);
                            SaveGroupsInXML();
                            return true;
                        }
                    }

                    return false;
                }
            }
            return false;
        }
    }
}
