using FishingApp.Core.Repoitories.FishingRepositories.IFishingRepositories;
using FishingApp.Data.Context;
using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Repoitories.FishingRepositories
{
    public class FishingRepository : IFishingRepository
    {
        private readonly FishingDbContext _context;

        public FishingRepository(FishingDbContext context)
        {
            _context = context;
        }

        public async Task AddFishingAsync(Fishing fishing)
        {
            _context.Fishings.Add(fishing);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Fishing>> GetFishingHistoryAsync(int userId)
        {
            return await _context.Fishings
                .Include(f => f.Fishes)
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.Date)
                .ToListAsync();
        }

    }
}
