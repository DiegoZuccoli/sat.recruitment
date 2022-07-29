using Sat.Recruitment.Entities;
using Sat.Recruitment.DTO;

namespace Sat.Recruitment.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> GetAllUsers();
        public Task<Result<UserEntity>> Add(UserEntity user);
    }
}