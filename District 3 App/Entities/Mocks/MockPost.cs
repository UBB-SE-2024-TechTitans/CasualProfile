using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.Enitities.Mocks
{
    internal class MockPost
    {
        private Guid postId;
        private object user;
        private Dictionary<int, List<object>> reactions;
        private List<object> mentionedUsers;
        private string title;

        public MockPost(object user, Dictionary<int, List<object>> reactions, List<object> mentionedUsers, string title)
        {
            this.postId = Guid.NewGuid();
            this.user = user;
            this.reactions = reactions;
            this.mentionedUsers = mentionedUsers;
            this.title = title;
        }
        public Guid GetPostId()
        {
            return postId;
        }
        public void SetPostId(Guid postId)
        {
            this.postId = postId;
        }
    }
}
