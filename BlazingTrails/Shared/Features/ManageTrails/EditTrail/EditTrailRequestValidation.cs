using BlazingTrails.Shared.Features.ManageTrails.Validations;
using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail;

public class EditTrailRequestValidation: AbstractValidator<EditTrailRequest>
{
    public EditTrailRequestValidation()
    {
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}