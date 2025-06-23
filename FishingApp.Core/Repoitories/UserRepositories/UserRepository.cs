using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Data.Context;
using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Repoitories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FishingDbContext _context;

        public UserRepository(FishingDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(string name)
        {
            return await _context.Users.AnyAsync(u => u.Name == name);
        }
        public async Task<List<User>> GetAllByScoreAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.Score)
                .ToListAsync();
        }

    }
}
