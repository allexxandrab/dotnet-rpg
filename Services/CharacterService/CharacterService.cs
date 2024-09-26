using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Mapping;
using dotnet_rpg.Models;
using dotnet_rpg.Repositories;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly IDbRepository dbRepository;
        private readonly ICharacterMapper characterMapper;

        public CharacterService(IMapper mapper, IDbRepository dbRepository, ICharacterMapper characterMapper)
        {
            this.mapper = mapper;
            this.dbRepository = dbRepository;
            this.characterMapper = characterMapper;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = mapper.Map<Character>(newCharacter);
            character.User = await dbRepository.GetCurrentUserAsync();

            dbRepository.SaveCharacter(character);
            await dbRepository.SaveChangesAsync();

            serviceResponse.Data = await dbRepository.GetCharactersByCurrentUserAsync();
            return serviceResponse;        
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var dbCharacters = await dbRepository.GetAllCharactersWithWeaponAndSkillsAsync();
            serviceResponse.Data = dbCharacters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            var dbCharacter = await dbRepository.GetCharacterWithWeaponAndSkillsAsync(id);
            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try 
            {
                var character = await dbRepository.GetCharacterWithUserByCharacterIdAsync(updatedCharacter.Id);
                character = characterMapper.MapUpdateCharacterRequestDto_To_Character(character, updatedCharacter);
                await dbRepository.SaveChangesAsync();
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

            try 
            {
                var character = await dbRepository.GetCharacterByIdAsync(id);
                dbRepository.DeleteCharacter(character);
                await dbRepository.SaveChangesAsync();

                serviceResponse.Data = await dbRepository.GetCharactersByCurrentUserAsync();
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
                var character = await dbRepository.GetCharacterWithWeaponAndSkillsAsync(newCharacterSkill.CharacterId);
                var skill = await dbRepository.GetCharacterSkillByIdAsync(newCharacterSkill.SkillId);

                character.Skills!.Add(skill);
                await dbRepository.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterResponseDto>(character);
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