using District_3_App.Repository;
using District_3_App.Enitities;

namespace District_3_App_Tests.RepositoryTests
{
    public class HighlightsRepoTests
    {
        [Fact]
        public void AddHighlight_WithValidHighlight_ReturnsTrue()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");

            // Act
            var result = mockHighlightsRepo.AddHighlight(userId, highlight);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveHighlight_WithExistingHighlight_ReturnsTrue()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            var highlightId = highlight.GetHighlightId();
            mockHighlightsRepo.AddHighlight(userId, highlight);

            // Act
            var result = mockHighlightsRepo.RemoveHighlight(userId, highlightId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddPostToHighlight_WithValidPost_ReturnsTrue()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            var highlightId = highlight.GetHighlightId();
            var postId = new Guid("22222222-2222-2222-2222-222222222222");
            mockHighlightsRepo.AddHighlight(userId, highlight);

            // Act
            var result = mockHighlightsRepo.AddPostToHighlight(userId, postId, highlightId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void RemovePostFromHighlight_WithExistingPost_ReturnsTrue()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            var highlightId = highlight.GetHighlightId();
            var postId = new Guid("22222222-2222-2222-2222-222222222222");
            mockHighlightsRepo.AddHighlight(userId, highlight);
            mockHighlightsRepo.AddPostToHighlight(userId, postId, highlightId);

            // Act
            var result = mockHighlightsRepo.RemovePostFromHighlight(userId, postId, highlightId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetHighlightsOfUser_WithValidUser_ReturnsNotNull()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            mockHighlightsRepo.AddHighlight(userId, highlight);

            // Act
            var result = mockHighlightsRepo.GetHighlightsOfUser(userId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetHighlight_WithValidHighlightId_ReturnsNotNull()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            var highlightId = highlight.GetHighlightId();
            mockHighlightsRepo.AddHighlight(userId, highlight);

            // Act
            var result = mockHighlightsRepo.GetHighlight(userId, highlightId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetPostsOfHighlight_WithValidHighlightId_ReturnsNotNull()
        {
            // Arrange
            var mockHighlightsRepo = new HighlightsRepo();
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var highlight = new Highlight("Highlight 1", "Cover 1");
            var highlightId = highlight.GetHighlightId();
            var postId = new Guid("22222222-2222-2222-2222-222222222222");
            mockHighlightsRepo.AddHighlight(userId, highlight);
            mockHighlightsRepo.AddPostToHighlight(userId, postId, highlightId);

            // Act
            var result = mockHighlightsRepo.GetPostsOfHighlight(userId, highlightId);

            // Assert
            Assert.NotNull(result);
        }

    }
}
