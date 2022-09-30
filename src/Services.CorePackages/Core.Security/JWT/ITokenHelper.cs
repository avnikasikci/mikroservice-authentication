using Core.Security.Entities;
using System.Security.Claims;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    RefreshToken CreateRefreshToken(User user, string ipAddress);
    IEnumerable<Claim> GetClaims(string token);

}