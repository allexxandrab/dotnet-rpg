using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.Dtos.Character
{
    public class GetCharacterResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int HitPoints { get; set; } 
        public int Strength { get; set; } 
        public int Defense { get; set; }
        public int Intelligence { get; set; } 
        public RpgClass Class { get; set; } 
        public GetWeaponResponseDto? Weapon { get; set; }
        public List<GetSkillResponseDto>? Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}