using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
/*    private readonly Item1Service _service;
    private readonly IConfiguration _config;

    public AuthorController(Item1Service service, IConfiguration config)
    {
        _service = service;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var account = _service.CheckLogin(loginRequest.UserEmail, loginRequest.UserPassword);
        if (account == null || account.Role != 2 && account.Role != 3)
            return Unauthorized();

        var tokenString = GenerateJSONWebToken(account);
        //return Ok(new { token = tokenString });
        return Ok(tokenString);
    }

    private string GenerateJSONWebToken(Item1 userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
        [
            new("UserId", userInfo.UserAccountId.ToString()),
        new(ClaimTypes.Email, userInfo.UserEmail!),
        new(ClaimTypes.Role, userInfo.Role.ToString()!)
        ], expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }*/
}
