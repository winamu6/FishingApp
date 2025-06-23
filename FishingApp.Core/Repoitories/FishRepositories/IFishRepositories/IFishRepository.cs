using FishingApp.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishingApp.Core.Repositories.Interfaces
{
    public interface IFishRepository
    {
        Task<List<Fish>> GetAllWithDescriptionAsync();
        Task<Fish?> GetByIdAsync(int id);
    }
}
