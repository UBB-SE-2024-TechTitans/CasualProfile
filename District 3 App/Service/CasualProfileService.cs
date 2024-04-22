using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.Enitities.Mocks;
using District_3_App.ExtraInfo;
using District_3_App.LogIn;
using District_3_App.LogIn;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;

namespace District_3_App.Service
{
    public class CasualProfileService
    {
        private SnapshotsService SnapshotsService { get; set; }
        private ProfileInfoSettings ProfileInfoSettings { get; set; }
        // private ExtraInfoService extraInfoService { get; set; }
        public CasualProfileService()
        {
            this.SnapshotsService = new SnapshotsService(GetConnectedUserId());
            this.ProfileInfoSettings = new ProfileInfoSettings(GetConnectedUserId());
        }
        public List<MockPost> GetConnectedUserPosts1()
        {
            return null;
        }
        public Guid GetConnectedUserId()
        {
            UserManager userManager = new UserManager("./Users.xml");
            IReadOnlyList<User> users = userManager.GetUsers();
            foreach (User user in users)
            {
                if (userManager.IsUserLoggedIn())
                {
                    return user.Id;
                }
            }
            return new Guid("11111111-1111-1111-1111-111111111111");
        }
        public List<MockPhotoPost> GetConnectedUserPosts()
        {
            Guid userId = GetConnectedUserId();
            List<MockPhotoPost> posts = new List<MockPhotoPost>();
            string path1 = "/Images/snow.jpg";
            string path2 = "/Images/peeta.jpeg";
            string path3 = "/Images/katniss.jpg";
            string path4 = "/Images/poster.jpeg";

            MockPhotoPost post1 = new MockPhotoPost(userId, new Dictionary<int, List<object>>(), new List<object>(), "Title 1", "Description 1", path1);
            post1.SetPostId(new Guid("11111111-1111-1111-1111-111111111111"));
            MockPhotoPost post2 = new MockPhotoPost(userId, new Dictionary<int, List<object>>(), new List<object>(), "Title 2", "Description 2", path2);
            post2.SetPostId(new Guid("22222222-2222-2222-2222-222222222222"));
            MockPhotoPost post3 = new MockPhotoPost(userId, new Dictionary<int, List<object>>(), new List<object>(), "Title 3", "Description 3", path3);
            post3.SetPostId(new Guid("33333333-3333-3333-3333-333333333333"));
            MockPhotoPost post4 = new MockPhotoPost(userId, new Dictionary<int, List<object>>(), new List<object>(), "Title 4", "Description 4", path4);
            post4.SetPostId(new Guid("44444444-4444-4444-4444-444444444444"));

            posts.Add(post1);
            posts.Add(post2);
            posts.Add(post3);
            posts.Add(post4);

            return posts;
        }

        public SnapshotsService GetSnapshotsService()
        {
            return SnapshotsService;
        }
        public ProfileInfoSettings GetProfileInfoSettings()
        {
            return ProfileInfoSettings;
        }
    }
}
