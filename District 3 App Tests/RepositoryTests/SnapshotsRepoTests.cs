using System;
using District_3_App.Repository;
using District_3_App.Enitities;

namespace District_3_App_Tests.RepositoryTests
{
    public class SnapshotsRepoTests
    {
        [Fact]
        public void AddHighlight_WithValidHighlight_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");

            // Act
            var result = snapshotRepo.AddHighlight(highlight);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveHighlight_WithExistingHighlight_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");
            snapshotRepo.AddHighlight(highlight);

            // Act
            var result = snapshotRepo.RemoveHighlight(highlight);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddPostToHighlight_WithValidPost_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");
            snapshotRepo.AddHighlight(highlight);
            var postId = Guid.NewGuid();

            // Act
            var result = snapshotRepo.AddPostToHighlight(highlight.GetHighlightId(), postId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemovePostFromHighlight_WithExistingPost_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");
            snapshotRepo.AddHighlight(highlight);
            var postId = Guid.NewGuid();
            snapshotRepo.AddPostToHighlight(highlight.GetHighlightId(), postId);

            // Act
            var result = snapshotRepo.RemovePostFromHighlight(highlight.GetHighlightId(), postId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHighlightsRepo_ReturnsNotNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);

            // Act
            var result = snapshotRepo.GetHighlightsRepo();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetHighlightsOfUser_WithValidUser_ReturnsNotNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");
            snapshotRepo.AddHighlight(highlight);

            // Act
            var result = snapshotRepo.GetHighlightsOfUser();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetHighlight_WithExistingHighlightId_ReturnsNotNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var snapshotRepo = new SnapshotsRepo(userId);
            var highlight = new Highlight("highlight", "description");
            snapshotRepo.AddHighlight(highlight);

            // Act
            var result = snapshotRepo.GetHighlight(highlight.GetHighlightId());

            // Assert
            Assert.NotNull(result);
        }
    }
}
