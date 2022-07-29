using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.DTO;
using Sat.Recruitment.Interfaces;

namespace Sat.Recruitment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private string errNameRequired() =>
        $"The name is required";
    private string errEmailRequired() =>
        $"The email is required";
    private string errAddressRequired() =>
    $"The address is required";
    private string errPhoneRequired() =>
        $"The phone is required";

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("/create-user")]
    public async Task<IActionResult> CreateUser(string name, string email, string address, string phone, string userType, string money)
    {
        if (name == null || string.IsNullOrEmpty(name))
            throw new BadHttpRequestException(errNameRequired());
        if (name == null || string.IsNullOrEmpty(email))
            throw new BadHttpRequestException(errEmailRequired());
        if (name == null || string.IsNullOrEmpty(address))
            throw new BadHttpRequestException(errAddressRequired());
        if (name == null || string.IsNullOrEmpty(phone))
            throw new BadHttpRequestException(errPhoneRequired());

        Result<UserDTO> result = new Result<UserDTO>();
        result = await _userService.Add(name, email, address, phone, userType, decimal.Parse(money));

        return Ok(result);
    }

}
