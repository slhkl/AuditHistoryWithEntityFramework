using Data.Entities;
using DataAccess.Discrete;

namespace Business.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetAsync(long id)
        {
            return await _userRepository.GetAsync(u => u.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var user = await GetAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            return await _userRepository.AnyAsync(u => u.Email == email && u.Password == password);
        }
    }
}
