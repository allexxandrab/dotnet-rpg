using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Validation.Fight;
using FluentValidation.TestHelper;

namespace dotnet_rpg.Tests.Validation.Fight
{
    public class FightRequestDtoValidatorTests
    {
        private readonly FightRequestDtoValidator validator;

        public FightRequestDtoValidatorTests()
        {
            validator = new FightRequestDtoValidator();
        }

        [Fact]
        public void Should_PassValidation_When_AllFieldsAreValid()
        {
            //Arrange
            var dto = new FightRequestDto
            {
                CharacterIds = { 2, 3 }
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }


        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new int[] { 0, 1})]
        [InlineData(new int[] { -6, 5 })]
        [InlineData(new int[] { 5 })]
        public void Should_FailValidation_When_NoIdsaArePassed(int[] characterIds)
        {
            //Arrange
            var dto = new FightRequestDto
            {
                CharacterIds = characterIds.ToList()
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CharacterIds);
        }
    }
}
