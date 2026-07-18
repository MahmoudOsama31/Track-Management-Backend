using Application.DTOs.Artists;
using FluentValidation;

namespace Application.Validators;

public class CreateArtistDtoValidator : AbstractValidator<CreateArtistDto>
{
    public CreateArtistDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
    }
}
