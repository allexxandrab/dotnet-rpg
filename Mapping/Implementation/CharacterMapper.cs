using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Weapon;
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

        public AttackResultResponseDto MapAttackResultResponseDto(Character attacker, Character opponent, int damage)
        {
            return new AttackResultResponseDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }

        public Weapon MapAddWeaponRequestDto_To_Weapon(Character character, AddWeaponRequestDto newWeapon)
        {
            return new Weapon
            {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };
        }
    }
}
