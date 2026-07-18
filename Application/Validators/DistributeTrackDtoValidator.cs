using Application.DTOs.Tracks;
using FluentValidation;

namespace Application.Validators;

public class DistributeTrackDtoValidator : AbstractValidator<DistributeTrackDto>
{
    public DistributeTrackDtoValidator()
    {
        RuleFor(x => x.DspIds).NotEmpty();
        RuleForEach(x => x.DspIds).GreaterThan(0);
        RuleFor(x => x.DspIds)
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Duplicate DSP ids are not allowed.")
            .When(x => x.DspIds is { Count: > 0 });
    }
}
