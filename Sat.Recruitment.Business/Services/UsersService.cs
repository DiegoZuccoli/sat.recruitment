using Sat.Recruitment.DTO;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.Business;

public class UserService : IUserService
{
    private readonly IUserManager _manager;

    public UserService(IUserManager userManager)
    {
        _manager = userManager;
    }

    public async Task<Result<UserDTO>> Add(string name, string email, string address, string phone, string userType, decimal money)
    {
        try
        {
            UserDTO newUser = new UserDTO { Name = name, Email = email, Address = address, Phone = phone, UserType = userType, Money = money };

            Result<UserDTO> result = new Result<UserDTO>();

            List<UserEntity> users = new List<UserEntity>();

            users = await _manager.GetAllUsers();

            if (_manager.isDuplicated(newUser, users))
            {
                result.Errors = "The user is duplicated";
                result.IsSuccess = false;
                result.items = newUser;
            }
            else
            {
                result = await CreateUser(newUser);

                if (result.items == null)
                {
                    result.IsSuccess = false;
                    result.Errors = "Ha ocurrido un error al guardar user";
                }
            }

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    public async Task<Result<UserDTO>> CreateUser(UserDTO newUser)
    {
        Result<UserDTO> result = await this._manager.Add(newUser);

        return result;
    }


}
