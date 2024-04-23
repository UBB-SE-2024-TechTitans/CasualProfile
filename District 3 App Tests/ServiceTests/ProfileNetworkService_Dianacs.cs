using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;


namespace District_3_App_Tests.ServiceTests
{
    public class ProfileNetworkInfoServiceTests
    {
        ProfileNetworkInfoService service;
        UserProfileSocialNetworkInfo profile;

        private void Initialize()
        {
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User user2 = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");
            User user3 = new User(Guid.NewGuid(), "user3", "password3", "user3@example.com", "password3Confirmation");
            User user4 = new User(Guid.NewGuid(), "black_ship", "password4", "user4@example.com", "password4Confirmation");

            // Create mock groups
            List<User> groupMembers1 = new List<User> { user1, user2 };
            List<User> groupMembers2 = new List<User> { user2, user3 };

            Group group1 = new Group(Guid.NewGuid(), "Group 1", groupMembers1);
            Group group2 = new Group(Guid.NewGuid(), "Group 2", groupMembers2);

            List<Group> groups = new List<Group> { group1, group2 };

            // Create mock profile social network info
            List<BlockedProfile> blockedProfiles1 = new List<BlockedProfile>();
            blockedProfiles1.Add(new BlockedProfile(user4, DateTime.Now));
            List<BlockedProfile> blockedProfiles2 = new List<BlockedProfile>();
            List<BlockedProfile> blockedProfiles3 = new List<BlockedProfile>();

            List<CloseFriendProfile> closeFriends1 = new List<CloseFriendProfile>();
            List<CloseFriendProfile> closeFriends2 = new List<CloseFriendProfile>();
            List<CloseFriendProfile> closeFriends3 = new List<CloseFriendProfile>();

            List<User> restrictedStoriesAudience1 = new List<User>();
            List<User> restrictedStoriesAudience2 = new List<User>();
            List<User> restrictedStoriesAudience3 = new List<User>();

            List<User> restrictedPostsAudience1 = new List<User>();
            List<User> restrictedPostsAudience2 = new List<User>();
            List<User> restrictedPostsAudience3 = new List<User>();

            UserProfileSocialNetworkInfo profile1 = new UserProfileSocialNetworkInfo(user1, blockedProfiles1, closeFriends1, new List<Group> { group1 }, restrictedStoriesAudience1, restrictedPostsAudience1);
            UserProfileSocialNetworkInfo profile2 = new UserProfileSocialNetworkInfo(user2, blockedProfiles2, closeFriends2, new List<Group> { group2 }, restrictedStoriesAudience2, restrictedPostsAudience2);
            UserProfileSocialNetworkInfo profile3 = new UserProfileSocialNetworkInfo(user3, blockedProfiles3, closeFriends3, new List<Group>(), restrictedStoriesAudience3, restrictedPostsAudience3);

            GroupsRepository groupsRepository = new GroupsRepository(groups);
            List<UserProfileSocialNetworkInfo> profiles = new List<UserProfileSocialNetworkInfo> { profile1, profile2, profile3 };

            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(profiles);
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3, user4 });
            this.service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);
            this.profile = new UserProfileSocialNetworkInfo();
        }

        [Fact]
        public void AddProfileSocialNetworkInfo_NewValidProfile_ShouldReturnTrue()
        {
            this.Initialize();
            // Act
            var result = this.service.AddProfileSocialNetworkInfo(this.profile);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddProfileSocialNetworkInfo_ProfileAlreadyInDatabase_ShouldReturnFalse()
        {
            this.Initialize();
            // Arrange
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = this.service.AddProfileSocialNetworkInfo(this.profile);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CheckIfProfileExists_ProfileExists_ShouldReturnTrue()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            var current_answer = this.service.CheckIfProfileExists(this.profile);
            var expected_answer = true;
            Assert.Equal(expected_answer, current_answer);
        }

        [Fact]
        public void RemoveProfileSocialNetworkInfo_ShouldReturnTrue()
        {
            this.Initialize();
            // First, add the profile to the service
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.RemoveProfileSocialNetworkInfo(profile);
            var expected_answer = true;

            // Assert
            Assert.Equal(expected_answer, result);
        }
        [Fact]
        public void RemoveProfileSocialNetworkInfo_ShouldReturnFalse()
        {
            this.Initialize();
            // Act
            var result = service.RemoveProfileSocialNetworkInfo(profile);
            var expected_answer = false;
            // Assert
            Assert.Equal(expected_answer, result);
        }
        [Fact]
        public void AddGroupToCurrentUser_ProfileExistsAndGroupNotAdded_ShouldReturnTrue()
        {
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToAdd = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);

            // Act
            var result = service.AddGroupToCurrentUser(currentUser, groupToAdd);

            // Assert
            Assert.True(result);
            Assert.Contains(groupToAdd, currentUser.Groups); // Ensure the group is added to the profile's groups list
        }

        [Fact]
        public void AddGroupToCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToAdd = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            // Mock repository without existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>();

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);

            // Act
            var result = service.AddGroupToCurrentUser(currentUser, groupToAdd);

            // Assert
            Assert.False(result);
            Assert.DoesNotContain(groupToAdd, currentUser.Groups); // Ensure the group is not added since the profile doesn't exist
        }
        [Fact]
        public void RemoveGroupFromCurrentUser_ProfileExistsAndGroupFound_ShouldReturnTrue()
        {
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock groups
            Group groupToRemove = new Group(Guid.NewGuid(), "Group 1", new List<User>());
            List<Group> userGroups = new List<Group> { groupToRemove };

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), userGroups, new List<User>(), new List<User>());

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);

            // Act
            var result = service.RemoveGroupFromCurrentUser(currentUser, groupToRemove);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(groupToRemove, currentUser.Groups); // Ensure the group is removed from the profile's groups list
        }

        [Fact]
        public void RemoveGroupFromCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToRemove = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            // Mock repository without existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>();

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);

            // Act
            var result = service.RemoveGroupFromCurrentUser(currentUser, groupToRemove);

            // Assert
            Assert.False(result);
            // Ensure that the group list remains empty since the profile doesn't exist
            Assert.Empty(currentUser.Groups);
        }

        [Fact]
        public void RemoveGroupFromCurrentUser_ProfileExistsButGroupNotFound_ShouldReturnFalse()
        {
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToRemove = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);

            // Act
            var result = service.RemoveGroupFromCurrentUser(currentUser, groupToRemove);

            // Assert
            Assert.False(result);
            // Ensure that the group list remains empty since the group wasn't found in the profile's groups list
            Assert.Empty(currentUser.Groups);
        }

        [Fact]
        public void AddCloseFriendToCurrentUser_NewCloseFriend_ShouldReturnTrue()
        {
            DateTime closeFriendedDate = DateTime.Now;
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToAdd = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            CloseFriendProfile closeFriendToAdd = new CloseFriendProfile(user1, closeFriendedDate);

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);
            // Act
            var result = service.AddCloseFriendToCurrentUser(currentUser, closeFriendToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddCloseFriendToCurrentUser_CloseFriendAlreadyExists_ShouldReturnFalse()
        {
            // Create mock users
            User user = new User(Guid.NewGuid(), "test_user", "password", "test@example.com", "passwordConfirmation");
            DateTime closeFriendedDate = DateTime.Now;

            // Create mock profile social network info
            List<BlockedProfile> blockedProfiles = new List<BlockedProfile>();
            CloseFriendProfile existingCloseFriend = new CloseFriendProfile(user, closeFriendedDate);
            List<CloseFriendProfile> closeFriends = new List<CloseFriendProfile> { existingCloseFriend };
            List<Group> groups = new List<Group>();
            List<User> restrictedStoriesAudience = new List<User>();
            List<User> restrictedPostsAudience = new List<User>();

            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user, blockedProfiles, closeFriends, groups, restrictedStoriesAudience, restrictedPostsAudience);

            CloseFriendProfile closeFriendToAdd = new CloseFriendProfile(user, closeFriendedDate);

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);
            service.AddCloseFriendToCurrentUser(currentUser, closeFriendToAdd);
            // Act
            var result = service.AddCloseFriendToCurrentUser(currentUser, closeFriendToAdd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveCloseFriendFromCurrentUser_CloseFriendExists_ShouldReturnTrue()
        {
            // Create mock users
            DateTime closeFriendedDate = DateTime.Now;
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToAdd = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            CloseFriendProfile closeFriendToAdd = new CloseFriendProfile(user1, closeFriendedDate);

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);
            service.AddCloseFriendToCurrentUser(currentUser, closeFriendToAdd);
            // Act
            var result = service.RemoveCloseFriendFromCurrentUser(currentUser, closeFriendToAdd);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void RemoveCloseFriendFromCurrentUser_CloseFriendDoesntExist_ShouldReturnFalse()
        {
            // Create mock users
            DateTime closeFriendedDate = DateTime.Now;
            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");

            // Create mock group
            Group groupToAdd = new Group(Guid.NewGuid(), "Group 1", new List<User>());

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user1, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());

            CloseFriendProfile closeFriendToAdd = new CloseFriendProfile(user1, closeFriendedDate);

            // Mock repository with existing profile
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });

            // Arrange
            var service = new ProfileNetworkInfoService(null, profileNetworkInfoRepository, null);
            
            // Act
            var result = service.RemoveCloseFriendFromCurrentUser(currentUser, closeFriendToAdd);

            // Assert
            Assert.False(result);
        }


    }
}