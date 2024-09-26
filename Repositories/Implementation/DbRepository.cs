using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotnet_rpg.Repositories
{
    public class DbRepository : IDbRepository
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public DbRepository(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void SaveUser(User user)
        {
            context.Users.Add(user);
        }

        public void SaveCharacter(Character character)
        {
            context.Characters.Add(character);
        }

        public void SaveWeapon(Weapon weapon)
        {
            context.Weapons.Add(weapon);
        }

        public void DeleteCharacter(Character character)
        {
            context.Characters.Remove(character);
        }
        public async Task<User> GetCurrentUserAsync()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            if (user is null)
                throw new Exception($"User not found");
            else return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            if (await context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username.ToLower(System.Globalization.CultureInfo.InvariantCulture).Equals(username.ToLowerInvariant()));
            if (user is null)
                throw new Exception($"User with username '{username}' not found");
            else return user;
        }

        public async Task<Skill> GetCharacterSkillByIdAsync(int skillId)
        {
            var skill = await context.Skills.FirstOrDefaultAsync(s => s.Id == skillId);
            if (skill is null)
                throw new Exception($"Skill with Id '{skillId}' not found");
            else return skill;
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            var character = await context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            if (character is null)
                throw new Exception($"Character with Id '{id}' not found");
            else return character;
        }

        public async Task<Character> GetCharacterWithUserByCharacterIdAsync(int id)
        {
            var character = await context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (character is null || character.User!.Id != GetUserId())
                throw new Exception($"Character with Id '{id}' not found");
            else return character;
        }

        public async Task<List<GetCharacterResponseDto>> GetCharactersByCurrentUserAsync()
        {
            return await context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => mapper.Map<GetCharacterResponseDto>(c))
                    .ToListAsync();
        }

        public async Task<Character> GetCharacterWithWeaponAndSkillsAsync(int characterId)
        {
            var character = await context.Characters
                                .Include(c => c.Weapon)    
                                .Include(c => c.Skills)   
                                .FirstOrDefaultAsync(c => c.Id == characterId && c.User!.Id == GetUserId());

            if (character is null)
                throw new Exception($"Character with Id '{characterId}' not found");
            else return character;
        }

        public async Task<List<Character>> GetAllCharactersWithWeaponAndSkillsAsync()
        {
            return await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserId())
                .ToListAsync();
        }

        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
