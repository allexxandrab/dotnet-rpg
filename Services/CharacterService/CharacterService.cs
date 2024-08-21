using System.Security.Claims;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = mapper.Map<Character>(newCharacter);
            character.User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            context.Characters.Add(character);
            await context.SaveChangesAsync();

            serviceResponse.Data = 
                await context.Characters
                    .Where(c => c.Id == GetUserId())
                    .Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();
            return serviceResponse;        
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var dbCharacters = await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserId())
                .ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            var dbCharacter = await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try {
            var character = await context.Characters
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if (character is null || character.User!.Id != GetUserId())
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
            
            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;
            
            await context.SaveChangesAsync();
            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(character);
            }
            catch (Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

          public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

            try {
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            if (dbCharacter is null)
                throw new Exception($"Character with Id '{id}' not found");
            
            context.Characters.Remove(dbCharacter);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = 
                await context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();
            }
            catch (Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        
        public async Task<ServiceResponse<GetCharacterResponseDto>> AddCharacterSkill(AddCharacterSkillRequestDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            try 
            {
                var character = await context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                        c.User!.Id == GetUserId());
                
                if(character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var skill = await context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                character.Skills!.Add(skill);
                await context.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterResponseDto>(character);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}