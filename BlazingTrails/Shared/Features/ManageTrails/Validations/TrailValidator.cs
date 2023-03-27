using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails.Validations;

public class TrailValidator : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length");
        RuleFor(x => x.Routes).NotEmpty().WithMessage("Please add a route instruction");
        RuleForEach(x => x.Routes).SetValidator(new RouteInstructionValidator());
    }
}