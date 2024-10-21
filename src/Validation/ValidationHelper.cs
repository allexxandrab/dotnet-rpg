using dotnet_rpg.Dtos.Character;
using FluentValidation;

namespace dotnet_rpg.Validation
{
    public static class ValidationHelper 
    {
        public static IRuleBuilderOptions<T, int> ValidateStat<T>(this IRuleBuilder<T, int> ruleBuilder, string statName)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage($"{statName} must be greater than 0.")
                .LessThanOrEqualTo(10).WithMessage($"Maximum {statName.ToLower()} value is 10.");
        }
    }
}
