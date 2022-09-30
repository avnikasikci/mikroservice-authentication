using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Security.Encryption;
using Core.Security.Entities;
using Core.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security.JWT;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string? token = jwtSecurityTokenHandler.WriteToken(jwt);
        //var tester = jwtSecurityTokenHandler.ValidateToken(token, jwtSecurityTokenHandler, out var resultToken);
        return new AccessToken
        {
            Token = token,
            Expiration = _accessTokenExpiration
        };
    }

    public RefreshToken CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                                                   SigningCredentials signingCredentials,
                                                   IList<OperationClaim> operationClaims)
    {
        JwtSecurityToken jwt = new(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetClaims(user, operationClaims),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(User user, IList<OperationClaim> operationClaims)
    {
        List<Claim> claims = new();
        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddEmail(user.Email);
        claims.AddName($"{user.FirstName} {user.LastName}");
        claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
        return claims;
    }
    public IEnumerable<Claim> GetClaims(string token)
    {
        token = token?.Replace("Bearer ", "") ?? "";

        string secret = _tokenOptions.SecurityKey;
        //var key = Encoding.ASCII.GetBytes(secret);
        var key = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var handler = new JwtSecurityTokenHandler();
        //var jsonToken = handler.ReadToken(token);
        var tokenObject = handler.ReadToken(token) as JwtSecurityToken;
        var resultClaim = tokenObject.Claims;
        //var resultClaim = tokenObject.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).ToList();


        //var id = tokenS.Claims.First(claim => claim.Type == "nameid").Value;
        //var jwtSecurityToken = handler.ReadJwtToken(token);

        //var validations = new TokenValidationParameters
        //{
        //    //ValidateIssuerSigningKey = true,
        //    //IssuerSigningKey =key,
        //    //ValidateIssuer = false,
        //    //ValidateAudience = false
        //    ValidateIssuer = true,
        //    ValidateAudience = true,
        //    ValidateLifetime = true,
        //    ValidIssuer = _tokenOptions.Issuer,
        //    ValidAudience = _tokenOptions.Audience,
        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey)
        //};
        //var claims = handler.ValidateToken(token, validations, out var tokenSecure);


        return resultClaim;
    }

}