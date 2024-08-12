using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterResponseDto>>> CreateCharacter(AddCharacterRequestDto newCharacter);
        Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id);
        Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id);
    }
}