using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using District_3_App.Enitities;
using District_3_App.Enitities.Mocks;
using District_3_App.Repository;

namespace District_3_App.Service
{
    internal class SnapshotsService
    {
        private SnapshotsRepo snapshotsRepo;
        private Guid userId;
        public SnapshotsService(Guid currentUserId)
        {
            this.snapshotsRepo = new SnapshotsRepo(currentUserId);
            this.userId = currentUserId;
        }

        public bool AddHighlight(string newHighlightName, string newHighlightCover, List<Guid> guids)
        {
            HighlightsRepo repo = snapshotsRepo.GetHighlightsRepo();
            // aici ar trebui apelat din post repository
            Highlight h = new Highlight(newHighlightName, newHighlightCover);

            if (newHighlightName == null)
            {
                newHighlightName = "Highlight_" + h.GetHighlightId().ToString().Replace("-", "_");
                h.SetName(newHighlightName);
            }

            int rnd = new Random().Next(0, guids.Count());
            if (guids.Count == 0)
            {
                newHighlightCover = "../Images/black.png";
            }
            while (newHighlightCover == null)
            {
                foreach (MockPhotoPost photoPost in repo.GetConnectedUserPosts(new Guid()))
                {
                    if (photoPost != null)
                    {
                        if (photoPost.GetPostId().Equals(guids[rnd]))
                        {
                            newHighlightCover = photoPost.GetPhoto();
                            continue;
                        }
                    }
                }
            }
            snapshotsRepo.AddHighlight(h);

            foreach (Guid postId in guids)
            {
                repo.AddPostToHighlight(this.userId, postId, h.GetHighlightId());
            }
            return true;
        }
        public bool RemoveHighlight(Highlight highlight)
        {
            return snapshotsRepo.RemoveHighlight(highlight);
        }
        public bool AddPostToHighlight(Guid highlightId, Guid postId)
        {
            return snapshotsRepo.AddPostToHighlight(postId, highlightId);
        }
        public bool RemovePostFromHighlight(Guid highlightId, Guid postId)
        {
            return snapshotsRepo.RemovePostFromHighlight(postId, highlightId);
        }
        public List<Highlight> GetHighlightsOfUser()
        {
            return snapshotsRepo.GetHighlightsOfUser();
        }
        public Highlight GetHighlight(Guid highlightId)
        {
            return snapshotsRepo.GetHighlight(highlightId);
        }

        public List<MockPhotoPost> GetPostsOfHighlight(Guid highlightId)
        {
            return snapshotsRepo.GetPostsOfHighlight(highlightId);
        }
    }
}
