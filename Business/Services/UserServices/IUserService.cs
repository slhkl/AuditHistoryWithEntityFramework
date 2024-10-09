using Data.Entities;

namespace Business.Services.UserServices
{
    public interface IUserService
    {
        Task<User?> GetAsync(long id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(long id);
        Task<bool> LoginAsync(string email, string password);
    }
}
