using Sat.Recruitment.DTO;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.Manager;
public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    List<UserEntity> usersEntity = new List<UserEntity>();

    private List<UserEntity> entity;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserManager()
    {
    }

    public async Task<Result<UserDTO>> Add(UserDTO user)
    {
        UserEntity oUser = ToEntity(user);

        if (oUser.UserType == "Normal")
        {
            if (oUser.Money > 100)
                oUser.Money += oUser.Money * Convert.ToDecimal(0.12);

            if (oUser.Money < 100 && oUser.Money > 10)
                oUser.Money += oUser.Money * new decimal(0.8);
        }
        else
        {
            if (oUser.UserType == "SuperUser" && oUser.Money > 100)
                oUser.Money += oUser.Money * Convert.ToDecimal(0.20);

            if (oUser.UserType == "Premium" && oUser.Money > 100)
                oUser.Money += oUser.Money * 2;
        }

        oUser.Email = normalizeEmail(oUser.Email);

        Result<UserEntity> result = await this._userRepository.Add(oUser);

        Result<UserDTO> resultDTO = new Result<UserDTO>();

        resultDTO.IsSuccess = result.IsSuccess;
        resultDTO.Errors = result.Errors;
        resultDTO.items = ToDTO(result.items);

        return resultDTO;
    }

    public bool isDuplicated(UserDTO newUser, List<UserEntity> users)
    {
        bool duped = users.Any(x => x.Email == newUser.Email || x.Phone == newUser.Phone
                               || x.Name == newUser.Name || x.Address == newUser.Address);

        return duped;
    }

    private string normalizeEmail(string email)
    {
        var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

        var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

        aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

        return string.Join("@", new string[] { aux[0], aux[1] });
    }

    private UserEntity ToEntity(UserDTO user)
    {
        return new UserEntity()
        {
            Name = user.Name,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            UserType = user.UserType,
            Money = user.Money
        };
    }

    private UserDTO ToDTO(UserEntity user)
    {
        return new UserDTO()
        {
            Name = user.Name,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            UserType = user.UserType,
            Money = user.Money
        };
    }

    private List<UserDTO> ToDTO(List<UserEntity> users)
    {
        List<UserDTO> result = new List<UserDTO>();

        foreach (UserEntity user in users)
        {
            result.Add(new UserDTO()
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserType = user.UserType,
                Money = user.Money
            });
        }
        return result;
    }

    public async Task<List<UserEntity>> GetAllUsers()
    {
        List<UserEntity> usersEntity = new List<UserEntity>();

        usersEntity = await _userRepository.GetAllUsers();

        return usersEntity;
    }
}


