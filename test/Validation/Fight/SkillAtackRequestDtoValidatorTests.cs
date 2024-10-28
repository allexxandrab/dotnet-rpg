using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Validation.Fight;
using FluentValidation.TestHelper;

namespace dotnet_rpg.Tests.Validation.Fight
{
    public class SkillAtackRequestDtoValidatorTests
    {
        private readonly SkillAtackRequestDtoValidator validator;

        public SkillAtackRequestDtoValidatorTests()
        {
            validator = new SkillAtackRequestDtoValidator();
        }

        [Fact]
        public void Should_PassValidation_When_AllFieldsAreValid()
        {
            //Arrange
            var dto = new SkillAtackRequestDto
            {
                AttackerId = 3,
                OpponentId = 2, 
                SkillId = 3
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(2, 0, 1)]
        [InlineData(1, 2, 0)]
        [InlineData(0, 6, 1)]

        public void Should_FailValidation_When_FieldIsInvalid(int attackerId, int opponentId, int skillId)
        {
            //Arrange
            var dto = new SkillAtackRequestDto
            {
                AttackerId = attackerId,
                OpponentId = opponentId,
                SkillId = skillId
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
