using FluentValidation;
using NZWeb2.Api.Models.DTO;

namespace NZWeb2.Api.Validater
{
    public class AddUpdateRegionRequestValidator:AbstractValidator<AddRegionRequestDTO>
    {
        public AddUpdateRegionRequestValidator() 
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Lat).GreaterThan(0);
            RuleFor(x => x.Long).GreaterThan(0);   
        }
    }
}
