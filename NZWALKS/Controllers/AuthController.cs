using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.IRepository;
using NZWALKS.Models.DTO;
using RegisterDTO = NZWALKS.Models.DTO.RegisterDTO;

namespace NZWALKS.Controllers;

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    // GET
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        var identityuser = new IdentityUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.UserName
        };
        var identityResult = await _userManager.CreateAsync(identityuser, registerDto.Password);
        if (identityResult.Succeeded)
            if (registerDto.Roles.Any())
            {
                var identity = await _userManager.AddToRolesAsync(identityuser, registerDto.Roles);
                if (identity.Succeeded) return Ok("User was registered! Please login.!");
            }

        return BadRequest("Something went wrong");
        //Add roles to this useer
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.UserName);
        if (user != null)
        {
            var checkResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (checkResult)
            {
                var role = await _userManager.GetRolesAsync(user);
                if (role.Any())
                {
                   var jwtToken= _tokenRepository.CreateToken(user,role.ToList( ));
                   var response = new LoginResponseDto()
                   {
                       JwtToken = jwtToken
                   };
                    return Ok(response);
                }
                
            }

            return Ok();
        }

        return BadRequest("UserName or Password Incorrect");
    }
}