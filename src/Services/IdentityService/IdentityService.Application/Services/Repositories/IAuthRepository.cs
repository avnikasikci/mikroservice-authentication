using Core.Application.Utilities;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace IdentityService.Application.Services.Repositories
{
    public interface IAuthRepository
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<IDataResult<RefreshToken>> CreateRefreshToken(User user, string ipAddress);
    }
}
