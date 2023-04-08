using EFModels.Entities.Identity;

namespace Core.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
