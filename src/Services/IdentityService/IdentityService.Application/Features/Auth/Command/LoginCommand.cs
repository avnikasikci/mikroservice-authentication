

using Core.Application.Utilities;
using Core.Security.Dtos;
using Core.Security.JWT;
using IdentityService.Application.Constants;
using IdentityService.Application.Features.Auth.Dto;
using IdentityService.Application.Features.Auth.Rules;
using IdentityService.Application.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace IdentityService.Application.Features.Auth.Command
{

    public class LoginCommand : IRequest<IDataResult< LoginDto>>
    {
     public   UserForLoginDto userForLoginDto { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, IDataResult<LoginDto>>
        {
            private readonly IAuthRepository _authRepository;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private readonly LoginBusinessRules _loginBusinessRules;
            private readonly IHttpContextAccessor _httpContextAccessor;


            public LoginCommandHandler( IAuthRepository authRepository, IRefreshTokenRepository refreshTokenRepository,LoginBusinessRules loginBusinessRules, IHttpContextAccessor httpContextAccessor)
            {
                _authRepository = authRepository;
                _refreshTokenRepository = refreshTokenRepository;
                _loginBusinessRules = loginBusinessRules;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<IDataResult<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var userToLogin = _authRepository.Login(request.userForLoginDto);
                if (!userToLogin.Success)
                {
                    _loginBusinessRules.UserToLoginAndTokenNotSucces(userToLogin.Message);
                    //return BadRequest(userToLogin.Message);
                    return new ErrorDataResult<LoginDto>(new LoginDto(), "User Not Found");
                }
                var result = _authRepository.CreateAccessToken(userToLogin.Data);
                var refreshToken = await _authRepository.CreateRefreshToken(userToLogin.Data, _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
                var saveRefreshToken=await _refreshTokenRepository.AddAsync(refreshToken.Data);
                if (!result.Success)
                {
                    _loginBusinessRules.UserToLoginAndTokenNotSucces(result.Message);
                    return new ErrorDataResult<LoginDto>(new LoginDto(), "Refresh Token Error");

                }
                var resultDTO = new LoginDto();
                resultDTO.Token = result.Data.Token;
                resultDTO.Expiration = result.Data.Expiration;

                resultDTO.UserId = refreshToken.Data.UserId;
                resultDTO.TokenR = refreshToken.Data.Token;
                resultDTO.Expires = refreshToken.Data.Expires;
                resultDTO.Created = refreshToken.Data.Created;
                resultDTO.CreatedByIp = refreshToken.Data.CreatedByIp;
                resultDTO.Revoked = refreshToken.Data.Revoked;
                resultDTO.RevokedByIp = refreshToken.Data.RevokedByIp;
                resultDTO.ReplacedByToken = refreshToken.Data.ReplacedByToken;
                resultDTO.ReasonRevoked = refreshToken.Data.ReasonRevoked;
                //resultDTO.User = refreshToken.Data.User;

                //resultDTO.accessToken = result.Data;
                //resultDTO.refreshToken =  refreshToken.Data;
                var returnResult=new SuccessDataResult<LoginDto>(resultDTO, Messages.AccessTokenCreated);

                return returnResult;
            }
        }
    }
}
