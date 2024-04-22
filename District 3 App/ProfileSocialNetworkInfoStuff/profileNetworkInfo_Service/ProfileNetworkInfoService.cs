using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;
using District_3_App.ProfileSocialNetworkInfoStuff.Sorting_module;

namespace District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service
{
    public class ProfileNetworkInfoService
    {
        public ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> Repository { get; set; }
        public GroupsRepository GroupsRepository { get; set; }
        public UsersRepository UsersRepository { get; set; }

        public User CurrentConnectedUser { get; set; }

        public ProfileNetworkInfoService()
        {
            ////     HARDCODED STUFF
        }
        public ProfileNetworkInfoService(GroupsRepository groupsRepository, ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> repository, UsersRepository usersRepository)
        {
            this.GroupsRepository = groupsRepository;
            this.Repository = repository;
            this.UsersRepository = usersRepository;
        }

        // delegate types: takes 2 BlockedProfile params and returns int using CompareTo function
        public Func<BlockedProfile, BlockedProfile, int> CompareBlockedProfilesByDateValue = (profile1, profile2) => profile1.BlockDate.CompareTo(profile2.BlockDate);
        public Func<CloseFriendProfile, CloseFriendProfile, int> CompareCloseFriendsByUsernameValue = (profile1, profile2) => profile1.User.CompareTo(profile2.User);
        public Func<Group, Group, int> CompareGroupsByNameValue = (group1, group2) => group1.GroupName.CompareTo(group2.GroupName);

        public Func<User, User, int> CompareUsersByUsername = (user1, user2) => user1.Username.CompareTo(user2.Username);
        // public Func<LikedPost, LikedPost, int> compareLikedPostbyDate = (Post1, Post2) => Post1.date.CompareTo(Post2.date);
        public Func<BlockedProfile, BlockedProfile, int> CompareBlockedProfilesByDate
        {
            get { return this.CompareBlockedProfilesByDateValue; }
        }
        public Func<CloseFriendProfile, CloseFriendProfile, int> CompareCloseFriendsByUsername
        {
            get { return this.CompareCloseFriendsByUsernameValue; }
        }
        public Func<Group, Group, int> CompareGroupsByName
        {
            get { return this.CompareGroupsByNameValue; }
        }
        public Func<User, User, int> CompareRestrictedUsersByUsername
        {
            get { return this.CompareUsersByUsername; }
        }

        // public Func<LikedPost, LikedPost, int> CompareLikedPostsByDate
        // {
        //    get { return this.compareLikedPostbyDate; }
        // }
        public void QuickSortBlockedProfiles(Func<BlockedProfile, BlockedProfile, int> compareFunction)
        {
            if (this.Repository.GetProfileRepositoryList().Count > 0 && this.Repository.GetProfileRepositoryList() != null)
            {
                ISortingAlgorithms<BlockedProfile> sortingAlgorithms = new SortingAlgorithms<BlockedProfile>();

                foreach (UserProfileSocialNetworkInfo profile in this.Repository.GetProfileRepositoryList())
                {
                    sortingAlgorithms.QuickSortDescending(profile.BlockedProfiles, compareFunction);
                }
            }
        }

        public void QuickSortRestrictedPostsAudience(Func<User, User, int> compareFunction)
        {
            if (this.Repository.GetProfileRepositoryList().Count > 0 && this.Repository.GetProfileRepositoryList() != null)
            {
                ISortingAlgorithms<User> sortingAlgorithms = new SortingAlgorithms<User>();

                foreach (UserProfileSocialNetworkInfo profile in this.Repository.GetProfileRepositoryList())
                {
                    sortingAlgorithms.QuickSortAscending(profile.RestrictedPostsAudience, compareFunction);
                }
            }
        }

        public void QuickSortRestrictedStoriesAudience(Func<User, User, int> compareFunction)
        {
            if (this.Repository.GetProfileRepositoryList().Count > 0 && this.Repository.GetProfileRepositoryList() != null)
            {
                ISortingAlgorithms<User> sortingAlgorithms = new SortingAlgorithms<User>();

                foreach (UserProfileSocialNetworkInfo profile in this.Repository.GetProfileRepositoryList())
                {
                    sortingAlgorithms.QuickSortAscending(profile.RestrictedStoriesAudience, compareFunction);
                }
            }
        }

        public void QuickSortCloseFriends(Func<CloseFriendProfile, CloseFriendProfile, int> compareFunction)
        {
            if (this.Repository.GetProfileRepositoryList().Count > 0 && this.Repository.GetProfileRepositoryList() != null)
            {
                ISortingAlgorithms<CloseFriendProfile> sortingAlgorithms = new SortingAlgorithms<CloseFriendProfile>();

                foreach (UserProfileSocialNetworkInfo profile in this.Repository.GetProfileRepositoryList())
                {
                    sortingAlgorithms.QuickSortAscending(profile.CloseFriendsProfiles, compareFunction);
                }
            }
        }

        public void QuickSortGroups(Func<Group, Group, int> compareFunction)
        {
            if (this.Repository.GetProfileRepositoryList().Count > 0 && this.Repository.GetProfileRepositoryList() != null)
            {
                ISortingAlgorithms<Group> sortingAlgorithms = new SortingAlgorithms<Group>();

                foreach (UserProfileSocialNetworkInfo profile in this.Repository.GetProfileRepositoryList())
                {
                    sortingAlgorithms.QuickSortAscending(profile.Groups, compareFunction);
                }
            }
        }

        public bool CheckIfProfileExists(UserProfileSocialNetworkInfo profileToCheck)
        {
            foreach (var profile in this.GetAllUserProfileSocialNetworks())
            {
                if (profile.User.Id == profileToCheck.User.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public List<UserProfileSocialNetworkInfo> GetAllUserProfileSocialNetworks()
        {
            return this.Repository.GetProfileRepositoryList();
        }

        public UserProfileSocialNetworkInfo GetProfileSocialNetworkInfoCurrentUser(User currentUser)
        {
            foreach (var profile in this.GetAllUserProfileSocialNetworks())
            {
                if (profile.User.Username == currentUser.Username)
                {
                    return profile;
                }
            }
            return null;
        }

        public bool AddProfileSocialNetworkInfo(UserProfileSocialNetworkInfo profileToAdd)
        {
            return this.Repository.AddProfileSocialNetworkInfo(profileToAdd);
        }
        public bool RemoveProfileSocialNetworkInfo(UserProfileSocialNetworkInfo profileToRemove)
        {
            return this.Repository.RemoveProfileSocialNetworkInfo(profileToRemove);
        }

        // add / remove group
        public bool AddGroupToCurrentUser(UserProfileSocialNetworkInfo currentUser, Group groupToAdd)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var group in currentUser.Groups)
                {
                    if (group == groupToAdd)
                    {
                        return false;
                    }
                }

                currentUser.Groups.Add(groupToAdd);
                SaveDataIntoXML();
                return true;
            }

            return false;
        }
        public bool RemoveGroupFromCurrentUser(UserProfileSocialNetworkInfo currentUser, Group groupToRemove)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var group in currentUser.Groups)
                {
                    if (group.GroupName == groupToRemove.GroupName)
                    {
                        currentUser.Groups.Remove(group);
                        SaveDataIntoXML();
                        return true;
                    }
                }
            }

            return false;
        }

        // add / remove close friend
        public bool AddCloseFriendToCurrentUser(UserProfileSocialNetworkInfo currentUser, CloseFriendProfile closeFriendToAdd)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var closeFriend in currentUser.CloseFriendsProfiles)
                {
                    if (closeFriend == closeFriendToAdd)
                    {
                        return false;
                    }
                }

                currentUser.CloseFriendsProfiles.Add(closeFriendToAdd);
                SaveDataIntoXML();
                return true;
            }

            return false;
        }
        public bool RemoveCloseFriendFromCurrentUser(UserProfileSocialNetworkInfo currentUser, CloseFriendProfile closeFriendToRemove)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var closeFriend in currentUser.CloseFriendsProfiles)
                {
                    if (closeFriend.User.Id == closeFriendToRemove.User.Id)
                    {
                        currentUser.CloseFriendsProfiles.Remove(closeFriend);
                        SaveDataIntoXML();
                        return true;
                    }
                }
            }

            return false;
        }

        // add / remove blocked profile
        public bool AddBlockedProfileToCurrentUser(UserProfileSocialNetworkInfo currentUser, BlockedProfile blockedProfileToAdd)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var blockedProfile in currentUser.BlockedProfiles)
                {
                    if (blockedProfile.User.Id == blockedProfileToAdd.User.Id)
                    {
                        return false;
                    }
                }

                currentUser.BlockedProfiles.Add(blockedProfileToAdd);
                SaveDataIntoXML();
                return true;
            }

            return false;
        }
        public bool RemoveBlockedProfileFromCurrentUser(UserProfileSocialNetworkInfo currentUser, BlockedProfile blockedProfileToRemove)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var blockedProfile in currentUser.BlockedProfiles)
                {
                    if (blockedProfile.User.Id == blockedProfileToRemove.User.Id)
                    {
                        currentUser.BlockedProfiles.Remove(blockedProfile);
                        SaveDataIntoXML();
                        return true;
                    }
                }
            }

            return false;
        }

        // add / remove     Restricted posts
        public bool AddRestrictedPostsAudienceUserToCurrentUser(UserProfileSocialNetworkInfo currentUser, User userToAdd)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var restrictedUser in currentUser.RestrictedPostsAudience)
                {
                    if (restrictedUser == userToAdd)
                    {
                        return false;
                    }
                }

                currentUser.RestrictedPostsAudience.Add(userToAdd);
                SaveDataIntoXML();
                return true;
            }

            return false;
        }
        public bool RemoveRestrictedPostsAudienceUserFromCurrentUser(UserProfileSocialNetworkInfo currentUser, User userToRemove)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var restrictedUser in currentUser.RestrictedPostsAudience)
                {
                    if (restrictedUser.Username == userToRemove.Username)
                    {
                        currentUser.RestrictedPostsAudience.Remove(restrictedUser);
                        SaveDataIntoXML();

                        return true;
                    }
                }
            }

            return false;
        }

        // add / remove     Restricted stories
        public bool AddRestrictedStoriesAudienceUserToCurrentUser(UserProfileSocialNetworkInfo currentUser, User userToAdd)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var restrictedUser in currentUser.RestrictedStoriesAudience)
                {
                    if (restrictedUser == userToAdd)
                    {
                        return false;
                    }
                }

                currentUser.RestrictedStoriesAudience.Add(userToAdd);
                SaveDataIntoXML();
                return true;
            }

            return false;
        }
        public bool RemoveRestrictedStoriesAudienceUserFromCurrentUser(UserProfileSocialNetworkInfo currentUser, User userToRemove)
        {
            if (this.CheckIfProfileExists(currentUser))
            {
                foreach (var restrictedUser in currentUser.RestrictedStoriesAudience)
                {
                    if (restrictedUser.Username == userToRemove.Username)
                    {
                        currentUser.RestrictedStoriesAudience.Remove(restrictedUser);
                        SaveDataIntoXML();
                        return true;
                    }
                }
            }

            return false;
        }

        public List<Group> GetAllGroupsService()
        {
            return this.GroupsRepository.GetAllGroups();
        }

        public Group GetGroupByName(string name)
        {
            return this.GroupsRepository.GetGroupByGroupName(name);
        }

        public bool CreateGroupToRepository(string groupName, List<User> groupMembers)
        {
            Group groupToAdd = new Group(Guid.NewGuid(), groupName, groupMembers);

            return GroupsRepository.AddGroup(groupToAdd);
        }
        public bool DeleteGroupFromRepository(string groupName)
        {
            if (GroupsRepository.GetGroupByGroupName(groupName) == null)
            {
                return false;
            }

            GroupsRepository.RemoveGroup(GroupsRepository.GetGroupByGroupName(groupName));
            return true;
        }

        public bool AddMemberToGroup(string groupName, User user)
        {
            return GroupsRepository.AddMemberToGroup(groupName, user);
        }

        public bool RemoveMemberFromGroup(string groupName, User user)
        {
            return GroupsRepository.RemoveMemberFromGroup(groupName, user);
        }

        public void AddMemberToGroupProfile(UserProfileSocialNetworkInfo profile, string groupName, string username)
        {
            foreach (var gr in profile.Groups)
            {
                if (gr.GroupName == groupName)
                {
                    foreach (var user in GetAllUsers())
                    {
                        if (user.Username == username)
                        {
                            gr.GroupMembers.Add(user);
                        }
                    }
                }
            }
        }

        //// USERS REPOSITORY

        ///
        public User GetUserByName(string username)
        {
            return UsersRepository.GetUserByName(username);
        }

        public List<User> GetAllUsers()
        {
            return UsersRepository.GetAllUsers();
        }

        public UserProfileSocialNetworkInfo GetProfileSocialNetworkInfoByUser(string username)
        {
            foreach (User user in UsersRepository.GetAllUsers())
            {
                if (user.Username == username)
                {
                    foreach (var profile in Repository.GetProfileRepositoryList())
                    {
                        if (profile.User.Username == username)
                        {
                            return profile;
                        }
                    }
                }
            }
            return null;
        }

        public BlockedProfile GetBlockedProfileByName(UserProfileSocialNetworkInfo profile, string username)
        {
            foreach (var blockedProfile in profile.BlockedProfiles)
            {
                if (blockedProfile.User.Username == username)
                {
                    return blockedProfile;
                }
            }

            return null;
        }

        public CloseFriendProfile GetCloseFriendByName(UserProfileSocialNetworkInfo profile, string username)
        {
            foreach (var closeFriend in profile.CloseFriendsProfiles)
            {
                if (closeFriend.User.Username == username)
                {
                    return closeFriend;
                }
            }
            return null;
        }

        public void SwitchAccountPrivacyPublicPrivate(User currentConnectedUser)
        {
            UserProfileSocialNetworkInfo profile = GetProfileSocialNetworkInfoByUser(currentConnectedUser.Username);

            profile.IsProfilePrivate = !profile.IsProfilePrivate;

            SaveDataIntoXML();
        }

        public void SaveDataIntoXML()
        {
            this.Repository.SaveProfilesInXML();
        }
    }
}
