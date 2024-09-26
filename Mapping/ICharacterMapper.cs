using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Mapping
{
    public interface ICharacterMapper
    {
        public Character MapUpdateCharacterRequestDto_To_Character(Character character, UpdateCharacterRequestDto updatedCharacter);
    }
}
