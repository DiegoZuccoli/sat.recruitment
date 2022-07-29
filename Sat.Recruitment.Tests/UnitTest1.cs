using System.Collections.Generic;
using Sat.Recruitment.DTO;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Manager;
using Sat.Recruitment.Repositories;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test()
        {
            UserDTO userTest = new UserDTO()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var repo = new UserRepository();
            var manager = new UserManager();

            List<UserEntity> users = new List<UserEntity>();
            users = repo.GetAllUsers().Result;

            bool isDuplicated = manager.isDuplicated(userTest, users);

            Result<UserDTO> result = new Result<UserDTO>();

            result.IsSuccess = !isDuplicated;
            result.Errors = "User Created";

            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            UserDTO userTest = new UserDTO()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var repo = new UserRepository();
            var manager = new UserManager();

            List<UserEntity> users = new List<UserEntity>();
            users = repo.GetAllUsers().Result;

            bool isDuplicated = manager.isDuplicated(userTest, users);

            Result<UserDTO> result = new Result<UserDTO>();

            result.IsSuccess = !isDuplicated;
            result.Errors = "The user is duplicated";

            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }


    }
}