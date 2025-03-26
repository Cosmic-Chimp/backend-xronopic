using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Xronopic.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public UsersController(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
      _userManager = userManager;
      _configuration = configuration;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest model)
    {
      var user = new IdentityUser { UserName = model.Username, Email = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        return Ok(new { Message = "User created successfully" });
      }

      return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);

      if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
      {
        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
      }

      return Unauthorized();
    }

    private string GenerateJwtToken(IdentityUser user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

      var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class LoginRequest
    {
      public string Email { get; set; } = ""; //added default value
      public string Password { get; set; } = ""; //added default value
    }

    public class SignupRequest
    {
      public string Username { get; set; } = ""; //added default value
      public string Email { get; set; } = ""; //added default value
      public string Password { get; set; } = ""; //added default value
    }
  }
}