using District_3_App.Service;
using District_3_App.Enitities.Mocks;

namespace District_3_App_Tests.ServiceTests
{
    public class CasualProfileServiceTests
    {
        [Fact]
        public void GetConnectedUserId_ReturnsGuid()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Act
            var result = casualProfileService.GetConnectedUserId();

            // Assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public void GetConnectedUserPosts_ReturnsListOfMockPhotoPosts()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Act
            var result = casualProfileService.GetConnectedUserPosts();

            // Assert
            Assert.IsType<List<MockPhotoPost>>(result);
        }

        [Fact]
        public void GetSnapshotsService_ReturnsSnapshotsService()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Act
            var result = casualProfileService.GetSnapshotsService();

            // Assert
            Assert.IsType<SnapshotsService>(result);
        }

        [Fact]
        public void GetProfileInfoSettings_ReturnsProfileInfoSettings()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Act
            var result = casualProfileService.GetProfileInfoSettings();

            // Assert
            Assert.IsType<ProfileInfoSettings>(result);
        }

        [Fact]
        public void GetConnectedUserPosts_WhenNoPosts_ReturnsNull()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Act
            var result = casualProfileService.GetConnectedUserPosts1();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CasualProfileService_CanBeInstantiated()
        {
            // Arrange
            var casualProfileService = new CasualProfileService();

            // Assert
            Assert.NotNull(casualProfileService);
        }
    }
}
