using dotnet_rpg.Dtos.Character;
using FluentValidation;
using static dotnet_rpg.Constants;

namespace dotnet_rpg.Validation.Character
{
    public class UpdateCharacterRequestDtoValidator : AbstractValidator<UpdateCharacterRequestDto>
    {
        public UpdateCharacterRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Character Id must be greater than 0.");

            RuleFor(x => x.Name)
              .Length(ValidationLimits.NameMinValue, ValidationLimits.NameMaxValue).WithMessage("Character name should be between 2 and 30 characters.");

            RuleFor(x => x.HitPoints)
                .GreaterThanOrEqualTo(ValidationLimits.HitPointsMinValue).WithMessage("Minimum hit points value is 1.")
                .LessThanOrEqualTo(ValidationLimits.HitPointsMaxValue).WithMessage("Maximum hit points value is 100.");

            RuleFor(x => x.Strength).ValidateStat("Strength");
            RuleFor(x => x.Defense).ValidateStat("Defense");
            RuleFor(x => x.Intelligence).ValidateStat("Intelligence");

            RuleFor(x => x.Class)
                .IsInEnum().WithMessage("There is no such class option.");
        }
    }
}
