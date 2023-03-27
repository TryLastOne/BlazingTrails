using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails.Validations;

public class RouteInstructionValidator : AbstractValidator<RouteInstructionDto>
{
    public RouteInstructionValidator()
    {
        RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
    }
}