using Sat.Recruitment.DTO;

namespace Sat.Recruitment.Interfaces
{
    public interface IUserService
    {
        public Task<Result<UserDTO>> Add(string name, string email, string address, string phone, string userType, decimal money);

        public Task<Result<UserDTO>> CreateUser(UserDTO newUser);
    }
}