using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Services.FishingServices.IFishingServices
{
    public interface IFishingService
    {
        Task SaveFishingAsync(User user, List<Fish> catchList);
        Task<List<Fishing>> GetFishingHistoryAsync(int userId);
    }

}
