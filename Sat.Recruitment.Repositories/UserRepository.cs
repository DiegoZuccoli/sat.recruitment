
using Sat.Recruitment.Entities;
using Sat.Recruitment.Interfaces;
using Sat.Recruitment.DTO;

namespace Sat.Recruitment.Repositories
{
    public class UserRepository : IUserRepository
    {

        public UserRepository()
        {
        }

        public async Task<Result<UserEntity>> Add(UserEntity user)
        {
            Result<UserEntity> result = new Result<UserEntity>();
            result.IsSuccess = true;
            result.Errors = "User Created";
            result.items = user;

            return result;
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            List<UserEntity> _users = new List<UserEntity>();

            string path = "./Files/Users.txt";

            using StreamReader sr = File.OpenText(path);

            string line = String.Empty;

            while (sr.Peek() > 0)
            {
                var userLine = sr.ReadLineAsync().Result;
                var user = new UserEntity
                {
                    Name = userLine.Split(',')[0].ToString(),
                    Email = userLine.Split(',')[1].ToString(),
                    Phone = userLine.Split(',')[2].ToString(),
                    Address = userLine.Split(',')[3].ToString(),
                    UserType = userLine.Split(',')[4].ToString(),
                    Money = decimal.Parse(userLine.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }

            return _users;
        }

    }
}