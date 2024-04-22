using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;

namespace District_3_App.Settings_Privacy_GUI
{
    /// <summary>
    /// Interaction logic for MantainGroups.xaml
    /// </summary>
    public partial class MantainGroups : UserControl
    {
        private ProfileNetworkInfoService profileNetworkInfoService;
        private User currentConnectedUser;

        public MantainGroups(User currentConnectedUser, ProfileNetworkInfoService profileNetworkInfoService)
        {
            InitializeComponent();

            this.currentConnectedUser = currentConnectedUser;
            this.profileNetworkInfoService = profileNetworkInfoService;

            // PopulateAllGroupsList();
            PopulateGroupsForCurrentUser();
            PopulateGroupMemberForSelectedGroup();
        }

        public void PopulateAllGroupsList()
        {
            if (searchGroupNameTextBox.Text != string.Empty && profileNetworkInfoService != null)
            {
                allGroupsListView.Items.Clear();

                foreach (var group in profileNetworkInfoService.GetAllGroupsService())
                {
                    if (group.GroupName.Contains(searchGroupNameTextBox.Text) || searchGroupNameTextBox.Text == string.Empty)
                    {
                        allGroupsListView.Items.Add(group.GroupName);
                    }
                }
            }
            else if (searchGroupNameTextBox.Text == string.Empty)
            {
                allGroupsListView.Items.Clear();
                foreach (var group in profileNetworkInfoService.GetAllGroupsService())
                {
                    allGroupsListView.Items.Add(group.GroupName);
                }
            }
        }

        public void PopulateGroupsForCurrentUser()
        {
            currentUserGroupsListView.Items.Clear();

            UserProfileSocialNetworkInfo profile = profileNetworkInfoService.GetProfileSocialNetworkInfoCurrentUser(currentConnectedUser);

            foreach (var group in profile.Groups)
            {
                currentUserGroupsListView.Items.Add(group.GroupName);
            }
        }

        public void PopulateGroupMemberForSelectedGroup()
        {
            if (currentUserGroupsListView.SelectedItems.Count > 0)
            {
                currentUserGroupMembersListView.Items.Clear();
                string selectedGroupName = currentUserGroupsListView.SelectedItem.ToString(); // get selected group members

                UserProfileSocialNetworkInfo profile = profileNetworkInfoService.GetProfileSocialNetworkInfoCurrentUser(currentConnectedUser);

                foreach (var group in profile.Groups)
                {
                    if (group.GroupName == selectedGroupName)
                    {
                        foreach (var groupMember in group.GroupMembers)
                        {
                            currentUserGroupMembersListView.Items.Add(groupMember.Username);
                        }
                    }
                }
            }
        }

        private void CurrentUserGroupsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateGroupMemberForSelectedGroup();
        }

        private void SearchGroupNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateAllGroupsList();
        }

        private void JoinGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (allGroupsListView.SelectedItems.Count > 0)
            {
                bool userAlreadyInGroup = false;
                string selectedGroupName = allGroupsListView.SelectedItem.ToString();
                UserProfileSocialNetworkInfo profile = profileNetworkInfoService.GetProfileSocialNetworkInfoCurrentUser(currentConnectedUser);

                foreach (var groupMember in profileNetworkInfoService.GetGroupByName(selectedGroupName).GroupMembers)
                {
                    if (groupMember.Username == profile.User.Username)
                    {
                        userAlreadyInGroup = true;
                    }
                }
                if (userAlreadyInGroup == false)
                {
                    profileNetworkInfoService.AddGroupToCurrentUser(profile, profileNetworkInfoService.GetGroupByName(selectedGroupName));
                    profileNetworkInfoService.GetGroupByName(selectedGroupName).GroupMembers.Add(profile.User);
                    profileNetworkInfoService.SaveDataIntoXML();

                    PopulateGroupsForCurrentUser();
                    PopulateGroupMemberForSelectedGroup();
                }
            }
        }

        private void CreateGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (newGroupNameTextBox.Text != string.Empty)
            {
                UserProfileSocialNetworkInfo profile = profileNetworkInfoService.GetProfileSocialNetworkInfoCurrentUser(currentConnectedUser);
                bool alreadyExists = false;

                foreach (var group in profileNetworkInfoService.GetAllGroupsService())
                {
                    if (group.GroupName == newGroupNameTextBox.Text)
                    {
                        alreadyExists = true;
                    }
                }

                if (alreadyExists)
                {
                    MessageBox.Show("Group with this name already exists", "Error creating new group");
                }
                else
                {
                    List<User> groupMembers = new List<User>();
                    groupMembers.Add(currentConnectedUser);

                    profileNetworkInfoService.CreateGroupToRepository(newGroupNameTextBox.Text, groupMembers);
                    profile.Groups.Add(profileNetworkInfoService.GetGroupByName(newGroupNameTextBox.Text));
                    profileNetworkInfoService.SaveDataIntoXML();

                    PopulateAllGroupsList();
                    PopulateGroupsForCurrentUser();
                    PopulateGroupMemberForSelectedGroup();
                }
            }
        }

        private void AddMemberToSpecificGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (addMemberToSelectedGroupTextBox.Text != string.Empty && currentUserGroupsListView.SelectedItems.Count > 0)
            {
                string selectedGroup = currentUserGroupsListView.SelectedItem.ToString();

                if (profileNetworkInfoService.GetUserByName(addMemberToSelectedGroupTextBox.Text) != null && profileNetworkInfoService.GetGroupByName(selectedGroup) != null)
                {
                    UserProfileSocialNetworkInfo profile = profileNetworkInfoService.GetProfileSocialNetworkInfoCurrentUser(currentConnectedUser);

                    if (profileNetworkInfoService.AddMemberToGroup(selectedGroup, profileNetworkInfoService.GetUserByName(addMemberToSelectedGroupTextBox.Text)))
                    {
                        UserProfileSocialNetworkInfo addedMemberProfile = profileNetworkInfoService.GetProfileSocialNetworkInfoByUser(addMemberToSelectedGroupTextBox.Text);
                        if (addedMemberProfile != null)
                        {
                            addedMemberProfile.Groups.Add(profileNetworkInfoService.GetGroupByName(selectedGroup));
                            profileNetworkInfoService.AddMemberToGroupProfile(profile, selectedGroup, addMemberToSelectedGroupTextBox.Text);

                            profileNetworkInfoService.SaveDataIntoXML();

                            PopulateGroupMemberForSelectedGroup();
                            PopulateGroupsForCurrentUser();
                        }
                        else
                        {
                            MessageBox.Show("Profile of give user not found, try again", "error");
                        }
                    }
                }
            }
        }
    }
}
