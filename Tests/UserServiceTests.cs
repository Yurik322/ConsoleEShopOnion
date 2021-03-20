using System.Collections.Generic;
using OA_DataAccess.Repositories;
using OA_Repository.DTO;
using OA_Service.Implementation;
using Xunit;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserByIdWithId1()
        {
            var unitOfWork = new EfUnitOfWork();
            UserService userService = new UserService(unitOfWork);
            var actual = userService.GetUserById(1);
            var email = "admin@gmail.com";
            Assert.Equal(actual.Email, email);
        }

        [Fact]
        public void GetUserByLoginPassword()
        {
            var unitOfWork = new EfUnitOfWork();
            UserService userService = new UserService(unitOfWork);
            var actual = userService.Register("elon@gmail.com");
            var expectedEmail = "elon@gmail.com";
            Assert.Equal(actual.Email, expectedEmail);
        }

        [Fact]
        public void ShowAllUsersMustBeIEnumerableType()
        {
            var unitOfWork = new EfUnitOfWork();
            UserService userService = new UserService(unitOfWork);
            var actual = userService.GetAllUsers();
            Assert.IsAssignableFrom<IEnumerable<UserDTO>>(actual);
        }

        [Fact]
        public void RegisterNewUserShouldAddNewUser()
        {
            var unitOfWork = new EfUnitOfWork();
            var userService = new UserService(unitOfWork);
            var expectedUsersCount = 4;
            var newUser = ToString();
            userService.Register(newUser);
            var actual = 0;
            foreach (var el in userService.GetAllUsers())
            {
                actual++;
            }
            Assert.Equal(expectedUsersCount, actual);
        }

        [Fact]
        public void IsEmail_WithBlankString_ReturnFalse()
        {
            bool actual = UserDTO.IsValidEmail(" ");

            Assert.False(actual);
        }

        [Fact]
        public void IsEmail_WithGoodName_ReturnTrue()
        {
            bool actual = UserDTO.IsValidEmail("elon@gmail.com");

            Assert.True(actual);
        }
    }
}
