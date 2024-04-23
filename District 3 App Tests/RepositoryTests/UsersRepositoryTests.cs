using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Repository;

namespace District_3_App_Tests.RepositoryTests
{
    public class UsersRepositoryTests
    {

        [Fact]
        public void ChangeUserPasswordTest_WithValueTrue()
        {
            // Arrange
            UsersRepository usersRepository = new UsersRepository("Users.xml");
            bool result = usersRepository.UpdatePassword("test0@yahoo.com", "test-0");

            Assert.True(result);

        }

        [Fact]
        public void ChangeUserPasswordTest_WithValueFalse()
        {
            // Arrange
            UsersRepository usersRepository = new UsersRepository("Users.xml");
            bool result = usersRepository.UpdatePassword("nuexista@yahoo.com", "newpass");
            Assert.False(result);
        }

        [Fact]
        public void UsernameExists_WithValueTrue()
        {
            // Arrange
            UsersRepository usersRepository = new UsersRepository("Users.xml");
            bool result = usersRepository.UsernameExists("test_0");

            Assert.True(result);
        }

        [Fact]
        public void UsernameExists_WithValueFalse()
        {
            // Arrange
            UsersRepository usersRepository = new UsersRepository("Users.xml");
            bool result = usersRepository.UsernameExists("test-0");

            Assert.False(result);
        }
    }
}
