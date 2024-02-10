using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWALKS.Models.DTO;
using RegisterDTO = NZWALKS.Models.DTO.RegisterDTO;

namespace NZWALKS.Controllers;

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto )
    {
        var user =await _userManager.FindByEmailAsync(loginDto.UserName );
        if (user != null)
        {
            if (user.PasswordHash != null)
                await _userManager.ChangePasswordAsync(user, user.PasswordHash, loginDto.Password);
            return Ok();
        }

        return BadRequest("UserName or Password Incorrect");
    }
}