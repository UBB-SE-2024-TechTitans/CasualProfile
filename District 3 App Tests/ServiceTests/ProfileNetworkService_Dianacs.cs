using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;


namespace District_3_App_Tests.ServiceTests
{
    public class ProfileNetworkInfoServiceTests
    {
        ProfileNetworkInfoService service;
        ProfileNetworkInfoService service_no_users;
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
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository_nousers = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { });
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3, user4 });
            UsersRepository usersRepository_nousers = new UsersRepository(new List<User> {});
            this.service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);
            this.service_no_users= new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository_nousers, usersRepository_nousers);
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
        [Fact]
        public void AddBlockedProfileToCurrentUser_NewBlockedProfile_ShouldReturnTrue()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info
            UserProfileSocialNetworkInfo userProfile = this.profile;

            BlockedProfile blockedProfileToAdd = new BlockedProfile(blockedUser, DateTime.Now);

            // Arrange
            var service = this.service;
            //add the user first
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.AddBlockedProfileToCurrentUser(userProfile, blockedProfileToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddBlockedProfileToCurrentUser_BlockedProfileAlreadyExists_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info with an existing blocked profile
            BlockedProfile existingBlockedProfile = new BlockedProfile(blockedUser, DateTime.Now);
            List<BlockedProfile> blockedProfiles = new List<BlockedProfile> { existingBlockedProfile };
            UserProfileSocialNetworkInfo userProfile = this.profile;

            BlockedProfile blockedProfileToAdd = new BlockedProfile(blockedUser, DateTime.Now);

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);
            service.AddBlockedProfileToCurrentUser(userProfile, blockedProfileToAdd);

            // Act
            var result = service.AddBlockedProfileToCurrentUser(userProfile, blockedProfileToAdd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddBlockedProfileToCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing profile
            UserProfileSocialNetworkInfo userProfile = this.profile;

            BlockedProfile blockedProfileToAdd = new BlockedProfile(blockedUser, DateTime.Now);

            // Arrange
            var service = this.service;

            // Act
            var result = service.AddBlockedProfileToCurrentUser(userProfile, blockedProfileToAdd);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void RemoveBlockedProfileFromCurrentUser_BlockedProfileExists_ShouldReturnTrue()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info with an existing blocked profile
            BlockedProfile blockedProfileToRemove = new BlockedProfile(blockedUser, DateTime.Now);
            List<BlockedProfile> blockedProfiles = new List<BlockedProfile> { blockedProfileToRemove };
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);
            service.AddBlockedProfileToCurrentUser(userProfile, blockedProfileToRemove);

            // Act
            var result = service.RemoveBlockedProfileFromCurrentUser(userProfile, blockedProfileToRemove);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveBlockedProfileFromCurrentUser_BlockedProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing blocked profile
            List<BlockedProfile> blockedProfiles = new List<BlockedProfile>();
            UserProfileSocialNetworkInfo userProfile = this.profile;

            BlockedProfile blockedProfileToRemove = new BlockedProfile(blockedUser, DateTime.Now);

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.RemoveBlockedProfileFromCurrentUser(userProfile, blockedProfileToRemove);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveBlockedProfileFromCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User blockedUser = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing profile
            UserProfileSocialNetworkInfo userProfile = this.profile;

            BlockedProfile blockedProfileToRemove = new BlockedProfile(blockedUser, DateTime.Now);

            // Arrange
            var service = this.service;

            // Act
            var result = service.RemoveBlockedProfileFromCurrentUser(userProfile, blockedProfileToRemove);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddRestrictedPostsAudienceUserToCurrentUser_NewUser_ShouldReturnTrue()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.AddRestrictedPostsAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddRestrictedPostsAudienceUserToCurrentUser_UserAlreadyExists_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info with an existing restricted user
            List<User> restrictedUsers = new List<User> { userToAdd };
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);
            service.AddRestrictedPostsAudienceUserToCurrentUser(userProfile, userToAdd);
            // Act
            var result = service.AddRestrictedPostsAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddRestrictedPostsAudienceUserToCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing profile
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;

            // Act
            var result = service.AddRestrictedPostsAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void RemoveRestrictedPostsAudienceUserFromCurrentUser_UserExists_ShouldReturnTrue()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToRemove = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info with an existing restricted user
            List<User> restrictedUsers = new List<User> { userToRemove };
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);
            service.AddRestrictedPostsAudienceUserToCurrentUser(userProfile, userToRemove);

            // Act
            var result = service.RemoveRestrictedPostsAudienceUserFromCurrentUser(userProfile, userToRemove);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveRestrictedPostsAudienceUserFromCurrentUser_UserDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToRemove = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing restricted user
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.RemoveRestrictedPostsAudienceUserFromCurrentUser(userProfile, userToRemove);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveRestrictedPostsAudienceUserFromCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User userToRemove = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing profile
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;

            // Act
            var result = service.RemoveRestrictedPostsAudienceUserFromCurrentUser(userProfile, userToRemove);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void AddRestrictedStoriesAudienceUserToCurrentUser_NewUser_ShouldReturnTrue()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);

            // Act
            var result = service.AddRestrictedStoriesAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddRestrictedStoriesAudienceUserToCurrentUser_UserAlreadyExists_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User currentUser = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info with an existing restricted user
            List<User> restrictedUsers = new List<User> { userToAdd };
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;
            this.service.AddProfileSocialNetworkInfo(this.profile);
            service.AddRestrictedStoriesAudienceUserToCurrentUser(userProfile, userToAdd);

            // Act
            var result = service.AddRestrictedStoriesAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddRestrictedStoriesAudienceUserToCurrentUser_ProfileDoesNotExist_ShouldReturnFalse()
        {
            this.Initialize();

            // Create mock users
            User userToAdd = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");

            // Create mock profile social network info without existing profile
            UserProfileSocialNetworkInfo userProfile = this.profile;

            // Arrange
            var service = this.service;

            // Act
            var result = service.AddRestrictedStoriesAudienceUserToCurrentUser(userProfile, userToAdd);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void GetAllUserProfileSocialNetworks_ReturnsCorrectList()
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
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository_nousers = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { });
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3, user4 });
            UsersRepository usersRepository_nousers = new UsersRepository(new List<User> { });
            service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);
            service_no_users = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository_nousers, usersRepository_nousers);
            profile = new UserProfileSocialNetworkInfo();

            // Act
            var result = service.GetAllUserProfileSocialNetworks();

            // Assert
            Assert.Equal(profiles, result);
        }

        [Fact]
        public void GetAllUserProfileSocialNetworks_ReturnsEmptyList_WhenNoProfilesExist()
        {
            this.Initialize();

            // Arrange
            var service = this.service_no_users;

            // Act
            var result = service.GetAllUserProfileSocialNetworks();

            // Assert
            Assert.Empty(result);
        }



        [Fact]
        public void GetUserByName_UserExists_ShouldReturnUser()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            var current_answer = this.service.GetUserByName("user1");
            var expected_answer = this.service.GetProfileSocialNetworkInfoByUser(username: "user1").User;
            Assert.Equal(expected_answer, current_answer);
        }

        [Fact]
        public void GetUserByName_UserDoesntExist_ShouldReturnNull()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            var current_answer = this.service.GetUserByName("user30");
            Assert.Null(current_answer);
        }

        [Fact]
        public void GetProfileSocialNetworkInfoByUser_UserDoesntExist_ShouldReturnNull()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            var current_answer = this.service.GetProfileSocialNetworkInfoByUser(username: "user30");
            Assert.Null(current_answer);
        }

        [Fact]
        public void GetBlockedProfileByName_UserExists_ShouldReturnBlockedProfiles()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            DateTime now = DateTime.Now;
            User user = new User(Guid.NewGuid(), "blocked", "password4", "uss@yahoo.com", "password4Confirmation");
            this.profile.BlockedProfiles.Add(new BlockedProfile(user, now));

            var current_answer = this.service.GetBlockedProfileByName(this.profile, "blocked");
            var expected_answer = this.profile.BlockedProfiles[0];
            Assert.Equal(expected_answer, current_answer);
        }

        [Fact]
        public void GetBlockedProfileByName_UserDoesntExist_ShouldReturnNull()
        {
            this.Initialize();
            this.service.AddProfileSocialNetworkInfo(this.profile);
            var current_answer = this.service.GetBlockedProfileByName(this.profile, "blocked");
            Assert.Null(current_answer);
        }

        [Fact]
        public void AddMemberToGroupProfile_GroupExistsInUserProfile_ShouldReturnTrue()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment

            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User user2 = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");
            User user3 = new User(Guid.NewGuid(), "user3", "password3", "user3@example.com", "password3Confirmation");

            // Create mock group
            Group groupToAddMember = new Group(Guid.NewGuid(), "GroupToAddMember", new List<User> { user1, user2 });

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user3, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());
            currentUser.Groups.Add(groupToAddMember);
            // Mock repository with existing profile and group
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });
            GroupsRepository groupsRepository = new GroupsRepository(new List<Group> { groupToAddMember });
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3 });

            this.service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);

            // Act
            this.service.AddMemberToGroupProfile(currentUser, "GroupToAddMember", "user3");

            //var result = this.service.GetAllGroupsService().Contains(groupToAddMember);
            //var result = groupToAddMember.GroupMembers.Contains(user3);
            var result = this.service.GetGroupByName("GroupToAddMember").GroupMembers.Contains(user3);
            Assert.Equal(this.service.GetGroupByName("GroupToAddMember").GroupMembers, new List<User> { user1, user2, user3 });
        }

        [Fact]
        public void AddMemberToGroupProfile_GroupNotExistsInUserProfile_ShouldReturnFalse()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment

            // Create mock users
            User user1 = new User(Guid.NewGuid(), "user1", "password1", "user1@example.com", "password1Confirmation");
            User user2 = new User(Guid.NewGuid(), "user2", "password2", "user2@example.com", "password2Confirmation");
            User user3 = new User(Guid.NewGuid(), "user3", "password3", "user3@example.com", "password3Confirmation");

            // Create mock group
            Group groupToAddMember = new Group(Guid.NewGuid(), "GroupToAddMember", new List<User> { user1, user2 });

            // Create mock profile social network info
            UserProfileSocialNetworkInfo currentUser = new UserProfileSocialNetworkInfo(user3, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());
            //currentUser.Groups.Add(groupToAddMember);
            // Mock repository with existing profile and group
            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(new List<UserProfileSocialNetworkInfo> { currentUser });
            GroupsRepository groupsRepository = new GroupsRepository(new List<Group> { groupToAddMember });
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3 });

            this.service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);

            // Act
            this.service.AddMemberToGroupProfile(currentUser, "GroupToAddMember", "user3");

            //var result = this.service.GetAllGroupsService().Contains(groupToAddMember);
            //var result = groupToAddMember.GroupMembers.Contains(user3);
            var result = this.service.GetGroupByName("GroupToAddMember").GroupMembers.Contains(user3);
            var expected_answer = false;
            Assert.Equal(expected_answer, result);
        }

        [Fact]
        public void DeleteGroupToRepository_ExitingGroup_ShouldReturnTrue()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Group 1", new List<User>());
            var current_value = this.service.DeleteGroupFromRepository("Group 1");
            var expected_value = true;
            Assert.Equal(expected_value, current_value);
            
        }

        [Fact]
        public void DeleteGroupToRepository_NewGroup_ShouldReturnFalse()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Group 1", new List<User>());
            var current_value = this.service.DeleteGroupFromRepository("Group that does not exist");
            var expected_value = false;
            Assert.Equal(expected_value, current_value);

        }

        [Fact]
        public void GetGroupByName_GroupExists_ShouldReturnGroup()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Group 1", new List<User>());
            var current_value = this.service.GetGroupByName("Group 1");
            var expected_value = new Group(Guid.NewGuid(), "Group 1", new List<User>());
            Assert.Equal(expected_value, current_value);
        }

        [Fact]
        public void GetGroupByName_GroupDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Group 1", new List<User>());
            var current_value = this.service.GetGroupByName("Group that does not exist");
            Assert.Null(current_value);
        }

        [Fact]
        public void GetAllGroupsService_ShouldReturnAllGroups()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            //this.service.CreateGroupToRepository("Group 1", new List<User>());
            //this.service.CreateGroupToRepository("Group 2", new List<User>());
            //this.service.CreateGroupToRepository("Group 3", new List<User>());
            this.service = new ProfileNetworkInfoService(new GroupsRepository(new List<Group>()), new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(), new UsersRepository(new List<User>()));
            Group group1 = new Group(Guid.NewGuid(), "Group 1", new List<User>());
            Group group2 = new Group(Guid.NewGuid(), "Group 2", new List<User>());
            Group group3 = new Group(Guid.NewGuid(), "Group 3", new List<User>());
            this.service.GetAllGroupsService().Insert(0, group1);
            this.service.GetAllGroupsService().Insert(1, group2);
            this.service.GetAllGroupsService().Insert(2, group3);
            var current_value = this.service.GetAllGroupsService();
            var expected_value = new List<Group> { group1, group2, group3 };
            Assert.Equal(expected_value, current_value);
        }

        [Fact]
        public void GetProfileSocialNetworkInfoByUser_UserNotExists_ShouldReturnFalse()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            UserProfileSocialNetworkInfo profile = new UserProfileSocialNetworkInfo(new User(Guid.NewGuid(), "userTest", "password1", "user@yahoo.com", "password1Confirmation"), new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>());  
            var value = this.service.AddProfileSocialNetworkInfo(profile);
            var current_value = this.service.GetProfileSocialNetworkInfoByUser("user not found");
            var expected_value = profile;
            Assert.True(value);
            Assert.NotEqual(expected_value, current_value);
        }

        [Fact]
        public void AddMemberToGroup_GroupExistsAndUserExists_ShouldReturnTrue()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Existent Group", new List<User>());
            User user = new User(Guid.NewGuid(), "user existent", "password1", "user@email", "password1Confirmation");
            this.service.AddProfileSocialNetworkInfo(new UserProfileSocialNetworkInfo(user, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>()));
            var current_value = this.service.AddMemberToGroup("Existent Group", user);
            var expected_value = true;
            Assert.Equal(expected_value, current_value);

        }

        [Fact]
        public void AddMemberToGroup_UserAlreadyInGroup_ShouldReturnFalse()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Existent Group", new List<User>());
            User user = new User(Guid.NewGuid(), "user existent", "password1", "user@email", "password1Confirmation");
            this.service.AddProfileSocialNetworkInfo(new UserProfileSocialNetworkInfo(user, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>()));
            this.service.AddMemberToGroup("Existent Group", user);
            var current_value = this.service.AddMemberToGroup("Existent Group", user);
            var expected_value = false;
            Assert.Equal(expected_value, current_value);
        }

        [Fact]
        public void RemoveMemberFromGroup_GroupExistsAndUserExists_ShouldReturnTrue()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Existent Group", new List<User>());
            User user = new User(Guid.NewGuid(), "user existent", "password1", "@email", "password1Confirmation");
            this.service.AddProfileSocialNetworkInfo(new UserProfileSocialNetworkInfo(user, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>()));
            this.service.AddMemberToGroup("Existent Group", user);
            var current_value = this.service.RemoveMemberFromGroup("Existent Group", user);
            var expected_value = true;
            Assert.Equal(expected_value, current_value);
        }

        [Fact]
        public void RemoveMemberFromGroup_UserNotInGroup_ShouldReturnFalse()
        {
            // Arrange
            this.Initialize(); // Initialize the test environment
            this.service.CreateGroupToRepository("Existent Group", new List<User>());
            User user = new User(Guid.NewGuid(), "user existent", "password1", "@email", "password1Confirmation");
            this.service.AddProfileSocialNetworkInfo(new UserProfileSocialNetworkInfo(user, new List<BlockedProfile>(), new List<CloseFriendProfile>(), new List<Group>(), new List<User>(), new List<User>()));
            var current_value = this.service.RemoveMemberFromGroup("Existent Group", user);
            var expected_value = false;
            Assert.Equal(expected_value, current_value);
        }
    }
}