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
