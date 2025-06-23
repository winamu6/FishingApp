using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishingApp.Data.Models.Entities;

namespace FishingApp.Core.Repoitories.UserRepositories.IUserRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByNameAsync(string name);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string name);
        Task<List<User>> GetAllByScoreAsync();
    }
}
