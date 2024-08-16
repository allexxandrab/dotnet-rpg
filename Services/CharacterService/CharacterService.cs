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

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = mapper.Map<Character>(newCharacter);

            context.Characters.Add(character);
            await context.SaveChangesAsync();

            serviceResponse.Data = await context.Characters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();
            return serviceResponse;        
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var dbCharacters = await context.Characters.Where(c => c.User!.Id == userId).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try {
            var character = await context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if (character is null)
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
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if (dbCharacter is null)
                throw new Exception($"Character with Id '{id}' not found");
            
            context.Characters.Remove(dbCharacter);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = 
                await context.Characters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();
            }
            catch (Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}