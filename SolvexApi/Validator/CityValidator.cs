using FluentValidation;
using SolvexApi.Models;

namespace SolvexApi.Validators
{
    public class CityValidator : AbstractValidator<CityDto>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name).NotEmpty().WithMessage("This field is required");
            RuleFor(city => city.Description)
                .MaximumLength(100).WithMessage("Description must be less than 100 characters")
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
