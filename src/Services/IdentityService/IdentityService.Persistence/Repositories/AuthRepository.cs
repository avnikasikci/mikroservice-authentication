using Core.Application.Constants;
using Core.Application.Utilities;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using IdentityService.Application.Constants;
using IdentityService.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace IdentityService.Persistence.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private IUserRepository _userRepository;
        //private IRefreshTokenRepository _refreshTokenRepository;
        private ITokenHelper _tokenHelper;
        public AuthRepository(IUserRepository userRepository,/* IRefreshTokenRepository refreshTokenRepository,*/ ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            //_refreshTokenRepository = refreshTokenRepository;
            _tokenHelper = tokenHelper;
            
        }
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userRepository.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {            
            var userToCheck = _userRepository.Get(x=>x.Email==userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            
            if (_userRepository.Get(x => x.Email == email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userRepository.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task< IDataResult<RefreshToken>> CreateRefreshToken(User user, string ipAddress)
        {
            var refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress) ?? new RefreshToken();
            //try
            //{
            //    if (refreshToken != null)
            //    {
            //        RefreshToken saveRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
            //    }

            //}
            //catch (Exception ex)
            //{

            //    throw new BusinessException("Error: "+ex.Message);

            //}



            return new SuccessDataResult<RefreshToken>(refreshToken, Messages.AccessTokenCreated);
        }


    }
}

