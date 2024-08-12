using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        
        private static readonly List<Character> characters = [
            new Character(),
            new Character { Id = 1, Name = "Gollum" }
        ];

        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> CreateCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;        
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

            try {
            var character = characters.First(c=> c.Id == id);
            if (character is null)
                throw new Exception($"Character with Id '{id}' not found");
            
            characters.Remove(character);

            serviceResponse.Data = characters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            }
            catch (Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            serviceResponse.Data = characters.Select(c => mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try {
            var character = characters.FirstOrDefault(c=> c.Id == updatedCharacter.Id);
            if (character is null)
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
            
            mapper.Map<Character>(updatedCharacter);

            serviceResponse.Data = mapper.Map<GetCharacterResponseDto>(character);
            }
            catch (Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}