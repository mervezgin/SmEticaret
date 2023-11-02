using SmEticaret.Data.Entities;
using SmEticaret.Shared.ServiceResult;

namespace SmEticaret.Api.Services.TokenService
{
    public interface ITokenService
    {
        IServiceResult<string> CreateToken(UserEntity user);
    }
}
