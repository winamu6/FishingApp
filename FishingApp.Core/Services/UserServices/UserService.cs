using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Models.Entities;

namespace FishingApp.Core.Services.UserServices.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetUsersByScoreAsync() => _userRepository.GetAllByScoreAsync();
    }
}
