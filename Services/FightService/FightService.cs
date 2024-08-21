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
                    .Include(c => c.Weapon)
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
    }
}