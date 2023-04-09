using FluentValidation;
using FluentValidation.Validators;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Validater
{
    //Step - 8
    public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
