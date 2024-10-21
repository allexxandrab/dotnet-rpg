using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Validation.Character;
using FluentValidation.TestHelper;

namespace dotnet_rpg.Tests.Validation.Character
{
    public class AddCharacterRequestDtoValidatorTests
    {
        private readonly AddCharacterRequestDtoValidator validator;

        public AddCharacterRequestDtoValidatorTests()
        {
            validator = new AddCharacterRequestDtoValidator();
        }

        [Fact]
        public void Should_PassValidation_When_AllFieldsAreValid()
        {
            //Arrange
            var dto = new AddCharacterRequestDto
            {
                Name = "Yoda",
                HitPoints = 50,
                Strength = 5,
                Defense = 5,
                Intelligence = 5,
                Class = RpgClass.Knight
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Should_FailValidation_When_NameIsEmpty(string name)
        {
            //Arrange
            var dto = new AddCharacterRequestDto
            {
                Name = name,
                HitPoints = 50,
                Strength = 5,
                Defense = 5,
                Intelligence = 5,
                Class = RpgClass.Knight
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.HitPoints);
            result.ShouldNotHaveValidationErrorFor(x => x.Strength);
            result.ShouldNotHaveValidationErrorFor(x => x.Defense);
            result.ShouldNotHaveValidationErrorFor(x => x.Intelligence);
            result.ShouldNotHaveValidationErrorFor(x => x.Class);

        }

        [Theory]
        [InlineData(Constants.InvalidRpgClasses.Notamage)]
        [InlineData(Constants.InvalidRpgClasses.Knnight)]
        public void Should_FailValidation_When_ClassInvalid(Enum value)
        {
            //Arrange
            var dto = new AddCharacterRequestDto
            {
                Name = "Yoda",
                HitPoints = 50,
                Strength = 5,
                Defense = 5,
                Intelligence = 5,
                Class = (RpgClass)value
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Class);
            result.ShouldNotHaveValidationErrorFor(x => x.HitPoints);
            result.ShouldNotHaveValidationErrorFor(x => x.Strength);
            result.ShouldNotHaveValidationErrorFor(x => x.Defense);
            result.ShouldNotHaveValidationErrorFor(x => x.Intelligence);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void Should_FailValidation_When_IntegerProperties_AreInvalid(int value)
        {
            //Arrange
            var dto = new AddCharacterRequestDto
            {
                Name = "Yoda",
                HitPoints = value,
                Strength = value,
                Defense = value,
                Intelligence = value,
                Class = RpgClass.Knight
            };

            //Act
            var result = validator.TestValidate(dto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.HitPoints);
            result.ShouldHaveValidationErrorFor(x => x.Strength);
            result.ShouldHaveValidationErrorFor(x => x.Defense);
            result.ShouldHaveValidationErrorFor(x => x.Intelligence);
        }
    }
}
