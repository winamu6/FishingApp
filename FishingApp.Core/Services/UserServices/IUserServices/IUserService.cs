using FishingApp.Data.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FishingApp.Core.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersByScoreAsync();
        Task UpdateUserAsync(User user);

    }
}
