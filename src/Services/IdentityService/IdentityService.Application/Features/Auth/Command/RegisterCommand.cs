
using Core.Security.Dtos;
using Core.Security.JWT;
using IdentityService.Application.Features.Auth.Rules;
using IdentityService.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Features.Auth.Command
{

    public partial class RegisterCommand : IRequest<AccessToken>
    {
        public UserForRegisterDto userForRegisterDto { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AccessToken>
        {
            private readonly IAuthRepository _authRepository;
            private readonly RegisterBusinessRules _registerBusinessRules;

            public RegisterCommandHandler(IAuthRepository authRepository, RegisterBusinessRules registerBusinessRules)
            {
                _authRepository = authRepository;
                _registerBusinessRules = registerBusinessRules;
            }

            public async Task<AccessToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                }
                return result.Data;
            }
        }
    }
}
