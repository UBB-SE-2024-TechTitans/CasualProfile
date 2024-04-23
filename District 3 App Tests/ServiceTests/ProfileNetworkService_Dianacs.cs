using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;


namespace District_3_App_Tests.ServiceTests
{
    public class ProfileNetworkInfoServiceTests
    {
        [Fact]
        public void AddProfileSocialNetworkInfo_NewValidProfile_ShouldReturnTrue()
        {
            ///
            // Create mock users
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
            // Arrange
            var service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);
            var profile = new UserProfileSocialNetworkInfo();

            // Act
            var result = service.AddProfileSocialNetworkInfo(profile);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddProfileSocialNetworkInfo_ProfileAlreadyInDatabase_ShouldReturnFalse()
        {
            // Create mock users
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

            // Add a profile that already exists
            profiles.Add(profile1);

            ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo> profileNetworkInfoRepository = new ProfileNetworkInfoRepository<UserProfileSocialNetworkInfo>(profiles);
            UsersRepository usersRepository = new UsersRepository(new List<User> { user1, user2, user3, user4 });

            // Arrange
            var service = new ProfileNetworkInfoService(groupsRepository, profileNetworkInfoRepository, usersRepository);
            var profile = new UserProfileSocialNetworkInfo(user1, blockedProfiles1, closeFriends1, new List<Group> { group1 }, restrictedStoriesAudience1, restrictedPostsAudience1);

            // Act
            var result = service.AddProfileSocialNetworkInfo(profile);

            // Assert
            Assert.False(result);
        }

    }
}