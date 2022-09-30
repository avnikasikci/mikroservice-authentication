

using FluentValidation;

namespace IdentityService.Application.Features.Auth.Command
{

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.userForLoginDto.Email).NotEmpty();
            RuleFor(l => l.userForLoginDto.Email).EmailAddress();
            RuleFor(l => l.userForLoginDto.Password).NotEmpty();
        }
    }
}
