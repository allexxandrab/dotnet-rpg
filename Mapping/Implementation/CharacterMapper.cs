using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Mapping.Implementation
{
    public class CharacterMapper : ICharacterMapper
    {

        public Character MapUpdateCharacterRequestDto_To_Character(Character character, UpdateCharacterRequestDto updatedCharacter)
        {
           character.Name = updatedCharacter.Name;
           character.HitPoints = updatedCharacter.HitPoints;
           character.Strength = updatedCharacter.Strength;
           character.Defense = updatedCharacter.Defense;
           character.Intelligence = updatedCharacter.Intelligence;
           character.Class = updatedCharacter.Class;

            return character;
        }
    }
}
