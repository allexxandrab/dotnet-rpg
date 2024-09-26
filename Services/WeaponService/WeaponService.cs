using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using dotnet_rpg.Repositories;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper mapper;
        private readonly IDbRepository dbRepository;

        public WeaponService(IMapper mapper, IDbRepository dbRepository)
        {
            this.mapper = mapper;
            this.dbRepository = dbRepository;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                var character = await dbRepository.GetCharacterByIdAsync(newWeapon.CharacterId);
              
                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

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