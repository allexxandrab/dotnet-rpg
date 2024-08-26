using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext context;
        public FightService(DataContext context)
        {
            this.context = context;
        }

        public async Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request)
        {
            var response = new ServiceResponse<AttackResultResponseDto>();
            try
            {
                var attacker = await context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId ); 

                if (attacker is null || opponent is null || attacker.Weapon is null)
                    throw new Exception("Something fishy is goin on here...");

                int damage = attacker.Weapon.Damage + new Random().Next(attacker.Strength);
                damage -= new Random().Next(opponent.Defeats);

                if(damage > 0)
                    opponent.HitPoints -= damage;

                if(opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await context.SaveChangesAsync();

                response.Data = new AttackResultResponseDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        
        public async Task<ServiceResponse<AttackResultResponseDto>> SkillAttack(SkillAtackRequestDto request)
        {
             var response = new ServiceResponse<AttackResultResponseDto>();
            try
            {
                var attacker = await context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId ); 

                if (attacker is null || opponent is null || attacker.Skills is null)
                    throw new Exception("Something fishy is goin on here...");
                
                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if(skill is null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know that skill!";
                    return response;
                }

                int damage = skill.Damage + new Random().Next(attacker.Intelligence);
                damage -= new Random().Next(opponent.Defeats);

                if(damage > 0)
                    opponent.HitPoints -= damage;

                if(opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await context.SaveChangesAsync();

                response.Data = new AttackResultResponseDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
             var response = new ServiceResponse<FightResultDto>
             {
                Data = new FightResultDto()
             };

             try
             {
                var characters = await context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharacterIds.Contains(c.Id))
                    .ToListAsync();

                bool defeated = false;
                while(!defeated)
                {
                    foreach(var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if(useWeapon && attacker.Weapon is not null)
                        {

                        }
                        else if(!useWeapon && attacker.Skills is not null)
                        {

                        }
                        else 
                        {
                            response.Data.Log
                                .Add($"{attacker.Name} wasn't able to attack!");
                        }
                    }
                }
             }
             catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}