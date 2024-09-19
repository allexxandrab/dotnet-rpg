using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Character, GetCharacterResponseDto>();
            CreateMap<AddCharacterRequestDto, Character>();
            CreateMap<Weapon, GetWeaponResponseDto>();
            CreateMap<Skill, GetSkillResponseDto>();
            CreateMap<Character, HighscoreDto>();
        }
    }
}