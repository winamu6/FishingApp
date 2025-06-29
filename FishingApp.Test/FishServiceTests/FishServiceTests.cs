using FishingApp.Core.Repositories.Implementations;
using FishingApp.Core.Services.FishServices;
using FishingApp.Data.Context;
using FishingApp.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Test.FishServiceTests
{
    [TestClass]
    public class FishServiceTests
    {
        private FishingDbContext _context;
        private FishRepository _fishRepository;
        private FishService _fishService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<FishingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FishingDbContext(options);
            _fishRepository = new FishRepository(_context);
            _fishService = new FishService(_fishRepository);
        }


        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllFishAsync_ReturnsAllFishWithDescriptions()
        {
            var fish1 = new Fish
            {
                Name = "Щука",
                Discription = new Discription
                {
                    Text = "Крупная щука",
                    Resirvoir = "Озеро Байкал",
                    Weight = 5.5
                }
            };

            var fish2 = new Fish
            {
                Name = "Окунь",
                Discription = new Discription
                {
                    Text = "Речной окунь",
                    Resirvoir = "Волга",
                    Weight = 1.2
                }
            };

            _context.Fishes.AddRange(fish1, fish2);
            await _context.SaveChangesAsync();

            var result = await _fishService.GetAllFishAsync();

            Assert.IsNotNull(result, "Список рыб не должен быть null");
            Assert.AreEqual(2, result.Count, "Должно быть 2 рыбы в списке");

            var firstFish = result.FirstOrDefault(f => f.Name == "Щука");
            Assert.IsNotNull(firstFish, "Щука должна присутствовать в списке");
            Assert.IsNotNull(firstFish.Discription, "Описание щуки должно быть загружено");
            Assert.AreEqual("Озеро Байкал", firstFish.Discription.Resirvoir, "Водоём щуки должен совпадать");
        }

        [TestMethod]
        public async Task GetFishByIdAsync_ReturnsCorrectFish()
        {
            var fish = new Fish
            {
                Name = "Карп",
                Discription = new Discription
                {
                    Text = "Зеркальный карп",
                    Resirvoir = "Пруд в деревне",
                    Weight = 3.3
                }
            };

            _context.Fishes.Add(fish);
            await _context.SaveChangesAsync();

            var addedFishId = fish.Id;

            var result = await _fishService.GetFishByIdAsync(addedFishId);

            Assert.IsNotNull(result, "Рыба должна быть найдена по Id");
            Assert.AreEqual("Карп", result.Name, "Название рыбы должно быть 'Карп'");
            Assert.IsNotNull(result.Discription, "Описание рыбы должно быть загружено");
            Assert.AreEqual("Зеркальный карп", result.Discription.Text, "Описание текста должно совпадать");
        }

        [TestMethod]
        public async Task GetFishByIdAsync_ReturnsNull_WhenFishDoesNotExist()
        {
            var result = await _fishService.GetFishByIdAsync(999);

            Assert.IsNull(result, "Если рыба с таким Id отсутствует, должно возвращаться null");
        }
    }
}
