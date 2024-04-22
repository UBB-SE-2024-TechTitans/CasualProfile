using District_3_App.Service;
using District_3_App.Enitities;

namespace District_3_App_Tests.ServiceTests
{
    public class SnapshotsServiceTests
    {
        [Fact]
        public void AddHighlight_WithValidDetails_ReturnsTrue()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());
            var newHighlightName = "Highlight_1";
            var newHighlightCover = "cover";
            var guids = new List<Guid> { Guid.NewGuid() };

            // Act
            var result = snapshotsService.AddHighlight(newHighlightName, newHighlightCover, guids);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveHighlight_WithNonExistentHighlight_ReturnsFalse()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());
            var highlight = new Highlight("Highlight_1", "cover");

            // Act
            var result = snapshotsService.RemoveHighlight(highlight);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AddPostToHighlight_WithNonExistentHighlight_ReturnsFalse()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());
            var highlightId = Guid.NewGuid();
            var postId = Guid.NewGuid();

            // Act
            var result = snapshotsService.AddPostToHighlight(highlightId, postId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void RemovePostFromHighlight_WithNonExistentHighlight_ReturnsFalse()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());
            var highlightId = Guid.NewGuid();
            var postId = Guid.NewGuid();

            // Act
            var result = snapshotsService.RemovePostFromHighlight(highlightId, postId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHighlightsOfUser_WithValidUser_ReturnsNonNull()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());

            // Act
            var result = snapshotsService.GetHighlightsOfUser();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPostsOfHighlight_WithValidHighlight_ReturnsNonNull()
        {
            // Arrange
            var snapshotsService = new SnapshotsService(Guid.NewGuid());
            var highlightId = Guid.NewGuid();

            // Act
            var result = snapshotsService.GetPostsOfHighlight(highlightId);

            // Assert
            Assert.NotNull(result);
        }
    }
}
