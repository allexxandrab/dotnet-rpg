using dotnet_rpg.Dtos.Character;
using FluentValidation;

namespace dotnet_rpg.Validation.Character
{
    public class AddCharacterSkillRequestDtoValidator : AbstractValidator<AddCharacterSkillRequestDto>
    {
        public AddCharacterSkillRequestDtoValidator()
        {
            RuleFor(x => x.CharacterId)
                .GreaterThan(0).WithMessage("Character Id must be greater than 0.");

            RuleFor(x => x.SkillId)
                .GreaterThan(0).WithMessage("Skill id must be greater than 0.");
        }
    }
}
