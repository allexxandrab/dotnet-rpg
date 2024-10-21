using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Validation.Character;
using FluentValidation.TestHelper;

namespace dotnet_rpg.Tests.Validation.Character
{
    public class AddCharacterSkillRequestDtoValidatorTests
    {
        private readonly AddCharacterSkillRequestDtoValidator validator;

        public AddCharacterSkillRequestDtoValidatorTests()
        {
            validator = new AddCharacterSkillRequestDtoValidator();
        }

        [Fact]
        public void Should_PassValidation_When_AllFieldsAreValid()
        {
            //Arrange
            var dto = new AddCharacterSkillRequestDto
            {
                CharacterId = 1,
                SkillId = 2
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        public void Should_FailValidation_When_IdsAreInvalid(int value)
        {
            //Arrange
            var dto = new AddCharacterSkillRequestDto
            {
                CharacterId = value,
                SkillId = value
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CharacterId);
            result.ShouldHaveValidationErrorFor(x => x.SkillId);
        }
    }
}
