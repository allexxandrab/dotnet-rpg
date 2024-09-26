using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Repositories
{
    public interface IDbRepository
    {
        public Task SaveChangesAsync();
        public Task<User> GetCurrentUserAsync();
        public Task<bool> UserExistsAsync(string username);
        public void SaveUser(User user);
        public void SaveCharacter(Character character);
        public void SaveWeapon(Weapon weapon);
        public void DeleteCharacter(Character character);
        public Task<User> GetUserByUsernameAsync(string username);
        public Task<Skill> GetCharacterSkillByIdAsync(int skillId);
        public Task<Character> GetCharacterByIdAsync(int id);
        public Task<Character> GetCharacterWithUserByCharacterIdAsync(int id);
        public Task<List<GetCharacterResponseDto>> GetCharactersByCurrentUserAsync();
        public Task<Character> GetCharacterWithWeaponAndSkillsAsync(int characterId);
        public Task<List<Character>> GetAllCharactersWithWeaponAndSkillsAsync();
    }
}
