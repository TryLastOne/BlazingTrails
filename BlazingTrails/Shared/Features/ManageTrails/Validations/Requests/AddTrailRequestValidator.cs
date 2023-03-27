using BlazingTrails.Shared.Features.ManageTrails.Requests;
using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails.Validations.Requests;

public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
    public AddTrailRequestValidator()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}