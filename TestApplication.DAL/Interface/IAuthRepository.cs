using TestApplication.Models.Entities;

namespace TestApplication.DAL.Interface
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task AddUserAsync(ApplicationUser user);
    }
}
