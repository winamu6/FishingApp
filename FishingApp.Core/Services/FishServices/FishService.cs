using FishingApp.Core.Repositories.Interfaces;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Data.Models.Entities;

namespace FishingApp.Core.Services.FishServices
{
    public class FishService : IFishService
    {
        private readonly IFishRepository _fishRepository;

        public FishService(IFishRepository fishRepository)
        {
            _fishRepository = fishRepository;
        }

        public Task<List<Fish>> GetAllFishAsync() => _fishRepository.GetAllWithDescriptionAsync();

        public Task<Fish?> GetFishByIdAsync(int id) => _fishRepository.GetByIdAsync(id);
    }
}
