using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.FightService
{
    public interface IFightService
    { 
        Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request);
        Task<ServiceResponse<AttackResultResponseDto>> SkillAttack(SkillAtackRequestDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
    }
}