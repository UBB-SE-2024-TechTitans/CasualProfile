using District_3_App.Repository;

namespace District_3_App_Tests.RepositoryTests
{
    public class FancierProfileRepoTests
    {
        [Fact]
        public void AddLink_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string linkToAdd = "www.google.com";

            // Act
            bool result = repo.AddLink(userId, linkToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteLink_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string linkToAdd = "www.google.com";
            repo.AddLink(userId, linkToAdd);

            // Act
            bool result = repo.DeleteLink(userId, linkToAdd);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SetFrameNumber_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            int newFrameNumber = 5;

            // Act
            bool result = repo.SetFrameNumber(userId, newFrameNumber);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteFrameNumber_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            int newFrameNumber = 5;
            repo.SetFrameNumber(userId, newFrameNumber);

            // Act
            bool result = repo.DeleteFrameNumber(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SetHashtag_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newHashtag = "#test";

            // Act
            bool result = repo.SetHashtag(userId, newHashtag);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteHashtag_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newHashtag = "#test";
            repo.SetHashtag(userId, newHashtag);

            // Act
            bool result = repo.DeleteHashtag(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddDailyMotto_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newMotto = "test";
            DateTime dateToRemove = DateTime.Now;

            // Act
            bool result = repo.AddDailyMotto(userId, newMotto, dateToRemove);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteDailyMotto_WithValidParameters_ReturnsTrue()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newMotto = "test";
            DateTime dateToRemove = DateTime.Now;
            repo.AddDailyMotto(userId, newMotto, dateToRemove);

            // Act
            bool result = repo.DeleteDailyMotto(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetDailyMotto_WithValidParameters_ReturnsMotto()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newMotto = "test";
            DateTime dateToRemove = DateTime.Now;
            repo.AddDailyMotto(userId, newMotto, dateToRemove);

            // Act
            string result = repo.GetDailyMotto(userId);

            // Assert
            Assert.Equal(newMotto, result);
        }

        [Fact]
        public void GetLinks_WithValidParameters_ReturnsLinks()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string linkToAdd = "www.google.com";
            repo.AddLink(userId, linkToAdd);

            // Act
            List<string> result = repo.GetLinks(userId);

            // Assert
            Assert.Contains(linkToAdd, result);
        }

        [Fact]
        public void GetFrameNumber_WithValidParameters_ReturnsFrameNumber()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            int newFrameNumber = 5;
            repo.SetFrameNumber(userId, newFrameNumber);

            // Act
            int result = repo.GetFrameNumber(userId);

            // Assert
            Assert.Equal(newFrameNumber, result);
        }

        [Fact]
        public void GetHashtag_WithValidParameters_ReturnsHashtag()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();
            string newHashtag = "#test";
            repo.SetHashtag(userId, newHashtag);

            // Act
            string result = repo.GetHashtag(userId);

            // Assert
            Assert.Equal(newHashtag, result);
        }

        [Fact]
        public void GetDailyMotto_WithInvalidParameters_ReturnsNull()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();

            // Act
            string result = repo.GetDailyMotto(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetLinks_WithInvalidParameters_ReturnsNull()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();

            // Act
            List<string> result = repo.GetLinks(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetFrameNumber_WithInvalidParameters_ReturnsNull()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();

            // Act
            int result = repo.GetFrameNumber(userId);

            // Assert
            Assert.Equal(-1, result);
        }

        [Fact]
        public void GetHashtag_WithInvalidParameters_ReturnsNull()
        {
            // Arrange
            FancierProfileRepo repo = new FancierProfileRepo();
            Guid userId = Guid.NewGuid();

            // Act
            string result = repo.GetHashtag(userId);

            // Assert
            Assert.Null(result);
        }
    }
}