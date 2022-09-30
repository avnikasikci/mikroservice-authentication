using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Extensions;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ITokenHelper _tokenHelper;


    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor, ITokenHelper tokenHelper)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenHelper = tokenHelper;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                        RequestHandlerDelegate<TResponse> next)
    {
        List<string>? roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        //Claims are not received in the token request, it also needs to be verified.
        var AuthHeaderToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        var getClaims = _tokenHelper.GetClaims(AuthHeaderToken).ToList();
        //var userClaimsId=getClaims.Where(x=>x.Type == ClaimsIdentity.DefaultNameClaimType).ToList();
        int userClaimsId = 0;
        int.TryParse(getClaims.Where(x => x.Type == ClaimTypes.NameIdentifier).ToList().FirstOrDefault().Value, out userClaimsId);
        roleClaims = getClaims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).ToList().Select(x => x.Value).ToList();
        if (userClaimsId == 0)
            throw new AuthorizationException("User not found.");
        if (roleClaims != null && roleClaims.Count > 0)
        {
            //if (roleClaims == null) throw new AuthorizationException("Claims not found."); //in the future also userId etc. can be controlled.

            bool isNotMatchedARoleClaimWithRequestRoles =
                roleClaims.FirstOrDefault(roleClaim => request.Roles.Any(role => role == roleClaim)).IsNullOrEmpty();
            if (isNotMatchedARoleClaimWithRequestRoles) throw new AuthorizationException("You are not authorized.");
        }



        TResponse response = await next();
        return response;
    }
}