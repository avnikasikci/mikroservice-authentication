
using Core.CrossCuttingConcerns.Exceptions;

namespace IdentityService.Application.Features.Auth.Rules
{
    public class RegisterBusinessRules
    {

        public async Task UserToRegisterNotSucces(string error)
        {
            throw new BusinessException("Brand name exists.");
        }
    }
}
