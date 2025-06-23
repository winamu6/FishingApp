using FishingApp.Data.Models.Entities;

namespace FishingApp.Core.Services.FishServices.Interfaces
{
    public interface IFishService
    {
        Task<List<Fish>> GetAllFishAsync();
        Task<Fish?> GetFishByIdAsync(int id);
    }
}
