using AutoFixture;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Mapping;
using dotnet_rpg.Mapping.Implementation;
using dotnet_rpg.Models;

namespace dotnet_rpg.Tests.Mapping
{
    public class CharacterMapperTests
    {
        private readonly ICharacterMapper characterMapper;
        private readonly IFixture fixture;

        public CharacterMapperTests()
        {
            characterMapper = new CharacterMapper();

            this.fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void AddCharacter_ShouldReturnListWithNewCharacter_WhenSuccessful()
        {
            // Arrange
            var character = fixture.Create<Character>();
            var updatedCharacter = fixture.Create<UpdateCharacterRequestDto>();

            // Act
            var result = characterMapper.MapUpdateCharacterRequestDto_To_Character(character, updatedCharacter);

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;
            // Assert
            result.Name.Equals(updatedCharacter.Name);
            result.HitPoints.Equals(updatedCharacter.HitPoints);
            result.Strength.Equals(updatedCharacter.Strength);
            result.Defense.Equals(updatedCharacter.Defense);
            result.Intelligence.Equals(updatedCharacter.Intelligence);
            result.Class.Equals(updatedCharacter.Class);
            result.Id.Equals(character.Id);
            result.User.Equals(character.User);
            result.Skills.Equals(character.Skills);
            result.Fights.Equals(character.Fights);
            result.Victories.Equals(character.Victories);
            result.Defeats.Equals(character.Defeats);
        }
    }
}
