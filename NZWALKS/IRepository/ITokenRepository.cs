using Microsoft.AspNetCore.Identity;

namespace NZWALKS.IRepository;

public interface ITokenRepository
{
     string CreateToken(IdentityUser user, List<string> roles);
}

