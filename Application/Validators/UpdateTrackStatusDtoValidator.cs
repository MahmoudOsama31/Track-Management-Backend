using Application.DTOs.Tracks;
using FluentValidation;

namespace Application.Validators;

public class UpdateTrackStatusDtoValidator : AbstractValidator<UpdateTrackStatusDto>
{
    public UpdateTrackStatusDtoValidator()
    {
        RuleFor(x => x.Status).IsInEnum();
    }
}
