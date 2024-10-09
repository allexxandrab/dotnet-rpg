using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Mapping;
using dotnet_rpg.Models;
using dotnet_rpg.Repositories;

namespace dotnet_rpg.Services.Implementation
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper mapper;
        private readonly IDbRepository dbRepository;
        private readonly ICharacterMapper characterMapper;

        public WeaponService(IMapper mapper, IDbRepository dbRepository, ICharacterMapper characterMapper)
        {
            this.mapper = mapper;
            this.dbRepository = dbRepository;
            this.characterMapper = characterMapper;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                var character = await dbRepository.GetCharacterByCharacterAndUserIdsAsync(newWeapon.CharacterId);
                var weapon = characterMapper.MapAddWeaponRequestDto_To_Weapon(character, newWeapon);

                dbRepository.SaveWeapon(weapon);
                await dbRepository.SaveChangesAsync();

                response.Data = mapper.Map<GetCharacterResponseDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}