using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.Enitities
{
    public class Highlight
    {
        private Guid highlightId;
        private Guid userId;
        private List<Guid> postsIds;
        private string name;
        private string coverFilePath;

        public Highlight()
        {
        }
        public Highlight(string newName, string newCover)
        {
            this.highlightId = Guid.NewGuid();
            this.postsIds = new List<Guid>();
            this.name = newName;
            this.coverFilePath = newCover;
        }
        public Guid GetHighlightId()
        {
            return highlightId;
        }
        public List<Guid> GetPosts()
        {
            return postsIds;
        }
        public string GetName()
        {
            return name;
        }
        public string GetCover()
        {
            return coverFilePath;
        }
        public bool AddPostToHighlight(Guid postId)
        {
            this.postsIds.Add(postId);
            return true;
        }

        public bool RemovePostFromHighlight(Guid postId)
        {
            this.postsIds.Remove(postId);
            return true;
        }
        public void SetName(string newName)
        {
            this.name = newName;
        }
        public void SetCover(string coverFilePath)
        {
            this.coverFilePath = coverFilePath;
        }
        public void SetListPosts(List<Guid> postsIds)
        {
            this.postsIds = postsIds;
        }
        public void SetGuid(Guid guid)
        {
            this.highlightId = guid;
        }

        public void SetListPosts(List<string> list)
        {
            List<Guid> guids = new List<Guid>();
            foreach (var post in list)
            {
                guids.Add(Guid.Parse(post));
            }
            this.SetListPosts(guids);
        }
        public void SetUserId(Guid userId)
        {
            this.userId = userId;
        }
        public Guid GetUserId()
        {
            return userId;
        }
    }
}
