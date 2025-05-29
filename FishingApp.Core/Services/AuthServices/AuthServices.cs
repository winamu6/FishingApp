using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Core.Services.AuthServices.IAuthServices;
using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(string name, string password)
        {
            if (await _userRepository.UserExistsAsync(name))
                return false;

            var user = new User
            {
                Name = name,
                Password = HashPassword(password),
                Score = 0
            };

            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<User?> LoginAsync(string name, string password)
        {
            var user = await _userRepository.GetByNameAsync(name);
            if (user == null)
                return null;

            return VerifyPassword(password, user.Password) ? user : null;
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return HashPassword(inputPassword) == hashedPassword;
        }
    }
}
