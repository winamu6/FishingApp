using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Repoitories.FishingRepositories.IFishingRepositories
{
    public interface IFishingRepository
    {
        Task AddFishingAsync(Fishing fishing);
    }

}
