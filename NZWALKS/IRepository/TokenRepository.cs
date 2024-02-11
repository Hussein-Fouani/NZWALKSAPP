using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWALKS.IRepository;

public class TokenRepository : ITokenRepository
{
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(IdentityUser user, List<string> roles)
    {
        //create claims 
        var claims = new List<Claim>();
        if (user.Email != null) claims.Add(new Claim(ClaimTypes.Email, user.Email));
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration["JWT:issuer"],
            _configuration["JWT:audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}