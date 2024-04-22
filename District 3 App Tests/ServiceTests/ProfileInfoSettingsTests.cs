using District_3_App.Service;

namespace District_3_App_Tests.ServiceTests
{
    public class ProfileInfoSettingsTests
    {
        [Fact]
        public void AddDailyMotto_WithValidInput_ReturnsTrue()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var newMotto = "newMotto";

            // Act
            var result = profileInfoSettings.AddDailyMotto(newMotto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddLink_WithValidUrl_ReturnsTrue()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var newLink = "http://www.newLink.com";

            // Act
            var result = profileInfoSettings.AddLink(newLink);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void SetFrameNumber_WithValidInput_ReturnsTrue()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var newFrameNumber = 1;

            // Act
            var result = profileInfoSettings.SetFrameNumber(newFrameNumber);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetFrameNumber_ReturnsNonNull()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.GetFrameNumber();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SetHashtag_WithValidInput_ReturnsTrue()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var newHashtag = "#newHashtag";

            // Act
            var result = profileInfoSettings.SetHashtag(newHashtag);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddLink_WithInvalidLink_ReturnsFalse()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var invalidLink = "invalidLink";

            // Act
            var result = profileInfoSettings.AddLink(invalidLink);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteLink_WithInvalidLink_ReturnsFalse()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);
            var linkToDelete = "invalidLink";

            // Act
            var result = profileInfoSettings.DeleteLink(linkToDelete);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteFrameNumber_WithInvalidFrameNumber_ReturnsFalse()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.DeleteFrameNumber();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteHashtag_WithInvalidHashtag_ReturnsFalse()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.DeleteHashtag();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetDailyMotto_WhenMottoNotSet_ReturnsNull()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.GetDailyMotto();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetLinks_WithNoLinks_ReturnsNull()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.GetLinks();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetHashtag_WhenNoHashtagSet_ReturnsNull()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            var profileInfoSettings = new ProfileInfoSettings(profileId);

            // Act
            var result = profileInfoSettings.GetHashtag();

            // Assert
            Assert.Null(result);
        }
    }
}
