using dotnet_rpg.Dtos.Fight;
using FluentValidation;

namespace dotnet_rpg.Validation.Fight
{
    public class FightRequestDtoValidator : AbstractValidator<FightRequestDto>
    {
        public FightRequestDtoValidator() 
        {
            RuleFor(x => x.CharacterIds)
                .NotEmpty().WithMessage("Character Ids are required.")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Character Ids must be greater than 0.")
                .Must(ids => ids.Count >= 2).WithMessage("At least two characters are required for a fight.");
        }
    }
}
