using dotnet_rpg.Dtos.Fight;
using FluentValidation;

namespace dotnet_rpg.Validation.Fight
{
    public class WeaponAttackRequestDtoValidator : AbstractValidator<WeaponAttackRequestDto>
    {
        public WeaponAttackRequestDtoValidator()
        {
            RuleFor(x => x.AttackerId)
                .GreaterThan(0).WithMessage("Attacker Id must be greater than 0.");

            RuleFor(x => x.OpponentId)
                .GreaterThan(0).WithMessage("Opponent Id must be greater than 0.");
        }
    }
}
