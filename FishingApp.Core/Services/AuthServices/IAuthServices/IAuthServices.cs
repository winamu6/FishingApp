using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Core.Services.AuthServices.IAuthServices
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string name, string password);
        Task<User?> LoginAsync(string name, string password);
    }
}
