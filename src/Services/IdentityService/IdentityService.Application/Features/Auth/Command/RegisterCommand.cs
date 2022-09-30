
using Core.Application.Utilities;
using Core.Security.Dtos;
using Core.Security.JWT;
using IdentityService.Application.Constants;
using IdentityService.Application.Features.Auth.Dto;
using IdentityService.Application.Features.Auth.Rules;
using IdentityService.Application.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Features.Auth.Command
{

    public partial class RegisterCommand : IRequest<IDataResult<LoginDto>>
    {
        public UserForRegisterDto userForRegisterDto { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IDataResult<LoginDto>>
        {
            private readonly IAuthRepository _authRepository;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            private readonly RegisterBusinessRules _registerBusinessRules;

            public RegisterCommandHandler(IAuthRepository authRepository, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor, RegisterBusinessRules registerBusinessRules)
            {
                _authRepository = authRepository;
                _refreshTokenRepository = refreshTokenRepository;
                _httpContextAccessor = httpContextAccessor;
                _registerBusinessRules = registerBusinessRules;

            }

            public async Task<IDataResult<LoginDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var userExists = _authRepository.UserExists(request.userForRegisterDto.Email);
                if (!userExists.Success)
                {
                    //return BadRequest(userExists.Message);
                    _registerBusinessRules.UserToRegisterNotSucces(userExists.Message);
                }

                var registerResult = _authRepository.Register(request.userForRegisterDto, request.userForRegisterDto.Password);
                var result = _authRepository.CreateAccessToken(registerResult.Data);

                if (!result.Success)
                {
                    _registerBusinessRules.UserToRegisterNotSucces(result.Message);
                    return new ErrorDataResult<LoginDto>(new LoginDto(), "User Error Register");
                }


                var refreshToken = await _authRepository.CreateRefreshToken(registerResult.Data, _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
                var saveRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken.Data);

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
                var returnResult = new SuccessDataResult<LoginDto>(resultDTO, Messages.AccessTokenCreated);

                return returnResult;


            }
        }
    }
}
