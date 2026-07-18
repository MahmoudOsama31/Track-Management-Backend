using Application.DTOs.Tracks;
using FluentValidation;

namespace Application.Validators;

public class CreateTrackDtoValidator : AbstractValidator<CreateTrackDto>
{
    public CreateTrackDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
        RuleFor(x => x.ArtistId).GreaterThan(0);
        RuleFor(x => x.ISRC).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Genre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ReleaseDate).NotEmpty();
    }
}
