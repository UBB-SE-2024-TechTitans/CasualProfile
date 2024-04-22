using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using District_3_App.Enitities;
using District_3_App.Enitities.Mocks;

namespace District_3_App.Repository
{
    internal class SnapshotsRepo
    {
        private HighlightsRepo highlightsRepo = new HighlightsRepo();
        private Guid userId;
        public SnapshotsRepo(Guid userId)
        {
            this.userId = userId;
        }

        public bool AddHighlight(Highlight highlight)
        {
            return highlightsRepo.AddHighlight(userId, highlight);
        }
        public bool RemoveHighlight(Highlight highlight)
        {
            return highlightsRepo.RemoveHighlight(userId, highlight.GetHighlightId());
        }
        public bool AddPostToHighlight(Guid highlightId, Guid postId)
        {
            return highlightsRepo.AddPostToHighlight(userId, highlightId, postId);
        }
        public bool RemovePostFromHighlight(Guid highlightId, Guid postId)
        {
            return highlightsRepo.RemovePostFromHighlight(userId, highlightId, postId);
        }
        public HighlightsRepo GetHighlightsRepo()
        {
            return highlightsRepo;
        }
        public List<Highlight> GetHighlightsOfUser()
        {
            return highlightsRepo.GetHighlightsOfUser(userId);
        }
        public Highlight GetHighlight(Guid highlightId)
        {
            return highlightsRepo.GetHighlight(userId, highlightId);
        }

        public List<MockPhotoPost> GetPostsOfHighlight(Guid highlightId)
        {
            return highlightsRepo.GetPostsOfHighlight(userId, highlightId);
        }
    }
}
