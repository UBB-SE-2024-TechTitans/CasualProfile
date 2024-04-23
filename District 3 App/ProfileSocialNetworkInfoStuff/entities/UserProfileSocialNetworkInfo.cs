using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.Entities
{
    public class UserProfileSocialNetworkInfo
    {
        public User User { get; set; }
        public List<BlockedProfile> BlockedProfiles { get; set; }
        public List<CloseFriendProfile> CloseFriendsProfiles { get; set; }
        public List<Group> Groups { get; set; }

        public List<User> RestrictedStoriesAudience { get; set; }

        public List<User> RestrictedPostsAudience { get; set; }

        public bool IsProfilePrivate { get; set; }

        public UserProfileSocialNetworkInfo()
        {
            // Create mock data with default values
            User = new User();
            BlockedProfiles = new List<BlockedProfile>();
            CloseFriendsProfiles = new List<CloseFriendProfile>();
            Groups = new List<Group>();
            RestrictedStoriesAudience = new List<User>();
            RestrictedPostsAudience = new List<User>();
            IsProfilePrivate = false; // Assuming the default profile privacy is set to public
        }

        public UserProfileSocialNetworkInfo(User user, List<BlockedProfile> blockedProfiles, List<CloseFriendProfile> closeFriendsProfiles, List<Group> groups, List<User> restrictedStoriesAudience, List<User> restrictedPostsAudience)
        {
            this.User = user;
            this.BlockedProfiles = blockedProfiles;
            this.CloseFriendsProfiles = closeFriendsProfiles;
            this.Groups = groups;
            this.RestrictedStoriesAudience = restrictedStoriesAudience;
            this.RestrictedPostsAudience = restrictedPostsAudience;
            this.IsProfilePrivate = false;
        }
    }
}
