using FishingApp.Core.Repositories.Interfaces;
using FishingApp.Data.Context;
using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishingApp.Core.Repositories.Implementations
{
    public class FishRepository : IFishRepository
    {
        private readonly FishingDbContext _context;

        public FishRepository(FishingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Fish>> GetAllWithDescriptionAsync()
        {
            return await _context.Fishes.Include(f => f.Discription).ToListAsync();
        }

        public async Task<Fish?> GetByIdAsync(int id)
        {
            return await _context.Fishes.Include(f => f.Discription).FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
