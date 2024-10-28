using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Validation.Fight;
using FluentValidation.TestHelper;

namespace dotnet_rpg.Tests.Validation.Fight
{
    public class WeaponAttackRequestDtoValidatorTests
    {
        private readonly WeaponAttackRequestDtoValidator validator;

        public WeaponAttackRequestDtoValidatorTests()
        {
            validator = new WeaponAttackRequestDtoValidator();
        }

        [Fact]
        public void Should_PassValidation_When_AllFieldsAreValid()
        {
            //Arrange
            var dto = new WeaponAttackRequestDto
            {
                AttackerId = 3,
                OpponentId = 2
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(2, 0)]
        [InlineData(0, 4)]

        public void Should_FailValidation_When_FieldIsInvalid(int attackerId, int opponentId)
        {
            //Arrange
            var dto = new WeaponAttackRequestDto
            {
                AttackerId = attackerId,
                OpponentId = opponentId
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
