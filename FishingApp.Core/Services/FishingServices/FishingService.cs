using FishingApp.Core.Repoitories.FishingRepositories.IFishingRepositories;
using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Core.Services.FishingServices.IFishingServices;
using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Services.FishingServices
{
    public class FishingService : IFishingService
    {
        private readonly IFishingRepository _fishingRepo;
        private readonly IUserRepository _userRepo;

        public FishingService(IFishingRepository fishingRepo, IUserRepository userRepo)
        {
            _fishingRepo = fishingRepo;
            _userRepo = userRepo;
        }

        public async Task SaveFishingAsync(User user, List<Fish> catchList)
        {
            var fishing = new Fishing
            {
                UserId = user.Id,
                Fishes = catchList
            };

            await _fishingRepo.AddFishingAsync(fishing);

            user.Score += catchList.Count * 1000;
            await _userRepo.UpdateUserAsync(user);
        }

        public async Task<List<Fishing>> GetFishingHistoryAsync(int userId)
        {
            return await _fishingRepo.GetFishingHistoryAsync(userId);
        }

    }

}
