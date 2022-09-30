

using Core.CrossCuttingConcerns.Exceptions;

namespace IdentityService.Application.Features.Auth.Rules
{
    public class LoginBusinessRules
    {

        public async Task UserToLoginAndTokenNotSucces(string error)
        {
            throw new BusinessException("Brand name exists.");
        }
    }
}
