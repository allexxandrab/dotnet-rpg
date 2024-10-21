using FluentValidation;
using static dotnet_rpg.Constants;

namespace dotnet_rpg.Validation
{
    public static class ValidationHelper 
    {
        public static IRuleBuilderOptions<T, int> ValidateStat<T>(this IRuleBuilder<T, int> ruleBuilder, string statName)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage($"{statName} must be greater than 0.")
                .LessThanOrEqualTo(ValidationLimits.IntMaxValue).WithMessage($"Maximum {statName.ToLower()} value is 10.");
        }
    }
}
