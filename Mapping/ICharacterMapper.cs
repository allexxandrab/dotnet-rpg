using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.Mapping
{
    public interface ICharacterMapper
    {
        public Character MapUpdateCharacterRequestDto_To_Character(Character character, UpdateCharacterRequestDto updatedCharacter);
        public AttackResultResponseDto MapAttackResultResponseDto(Character attacker, Character opponent, int damage);
        public Weapon MapAddWeaponRequestDto_To_Weapon(Character character, AddWeaponRequestDto newWeapon);
    }
}
