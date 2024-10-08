using dotnet_rpg.Models;

namespace dotnet_rpg.Repositories
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register (User user, string password);
        Task<ServiceResponse<string>> Login (string username, string password);
    }
}