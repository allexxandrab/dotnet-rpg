using dotnet_rpg.Models;

namespace dotnet_rpg.Dtos.Character
{
    public class AddCharacterRequestDto
    {
        public required string Name { get; set; }
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } 
        public int Defense { get; set; } 
        public int Intelligence { get; set; } 
        public RpgClass Class { get; set; } 
    }
}