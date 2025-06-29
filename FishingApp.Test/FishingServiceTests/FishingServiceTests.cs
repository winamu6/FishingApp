using FishingApp.Core.Repoitories.FishingRepositories;
using FishingApp.Core.Repoitories.UserRepositories;
using FishingApp.Core.Services.FishingServices;
using FishingApp.Data.Context;
using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Test.FishingServiceTests
{
    [TestClass]
    public class FishingServiceTests
    {
        private FishingDbContext _context;
        private UserRepository _userRepository;
        private FishingRepository _fishingRepository;
        private FishingService _fishingService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<FishingDbContext>()
                .UseInMemoryDatabase(databaseName: "FishingAppTestDb_Fishing")
                .Options;

            _context = new FishingDbContext(options);
            _userRepository = new UserRepository(_context);
            _fishingRepository = new FishingRepository(_context);
            _fishingService = new FishingService(_fishingRepository, _userRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task TC03_AddFishing_CorrectlySavesCatchAndUpdatesScore()
        {
            var user = new User
            {
                Name = "user123",
                Password = "hashedpass",
                Score = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var fish = new Fish
            {
                Name = "Щука",
                Discription = new Discription
                {
                    Text = "Крупная щука",
                    Resirvoir = "Озеро Байкал",
                    Weight = 5.5
                }
            };

            var catchList = new List<Fish> { fish };

            await _fishingService.SaveFishingAsync(user, catchList);

            var updatedUser = await _userRepository.GetByNameAsync("user123");
            Assert.IsNotNull(updatedUser, "Пользователь должен существовать");
            Assert.AreEqual(1000, updatedUser.Score, "После добавления 1 улова должно начислиться 1000 очков");

            var history = await _fishingService.GetFishingHistoryAsync(user.Id);
            Assert.IsNotNull(history, "История уловов не должна быть null");
            Assert.AreEqual(1, history.Count, "Должна быть 1 запись об улове");

            var savedFishing = history.First();
            Assert.AreEqual(user.Id, savedFishing.UserId, "UserId улова должен совпадать");
            Assert.AreEqual(1, savedFishing.Fishes.Count, "Должна быть сохранена 1 рыба");
            Assert.AreEqual("Щука", savedFishing.Fishes.First().Name, "Название рыбы должно быть 'Щука'");
        }
    }
}
