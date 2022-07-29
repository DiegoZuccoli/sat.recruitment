using Sat.Recruitment.DTO;
using Sat.Recruitment.Entities;

namespace Sat.Recruitment.Interfaces
{
    public interface IUserManager
    {
        public Task<Result<UserDTO>> Add(UserDTO user);
        public Task<List<UserEntity>> GetAllUsers();
        public bool isDuplicated(UserDTO newUser, List<UserEntity> users);
    }
}