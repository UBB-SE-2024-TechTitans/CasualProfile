using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.LogIn;
using Xunit;

namespace District_3_App_Tests.UserManagerTests
{
    public class UserManagerTests
    {
 
        [Fact]
        public void AuthenticateUser_WithValidParameters_ReturnsTrue()
        {
            UserManager userManager = new UserManager("Users.xml");
            string username=userManager.GetCurrentUser();
            Assert.Equal("",username);
            Assert.False(userManager.IsUserLoggedIn());
            bool result=userManager.AuthenticateUser("test_1", "Test-1");
            Assert.True(result);
            Assert.True(userManager.IsUserLoggedIn());
        }

        [Fact]
        public void AuthnticateUser_WithInvalidParameters_ReturnsFalse()
        {
            UserManager userManager = new UserManager("Users.xml");
            string username=userManager.GetCurrentUser();
            Assert.Equal("",username);
            Assert.False(userManager.IsUserLoggedIn());
            bool result=userManager.AuthenticateUser("test-1", "Test-2");
            Assert.False(result);
            Assert.False(userManager.IsUserLoggedIn());
        }

        [Fact]
        public void LogOutUser_WithValidParameters_ReturnsTrue()
        {
            UserManager userManager = new UserManager("Users.xml");
            string username=userManager.GetCurrentUser();
            Assert.Equal("",username);
            Assert.False(userManager.IsUserLoggedIn());
            bool result=userManager.AuthenticateUser("test_1", "Test-1");
            Assert.True(result);
            Assert.True(userManager.IsUserLoggedIn());
            userManager.LogOutUser();
            Assert.False(userManager.IsUserLoggedIn());
        }
    }
}
