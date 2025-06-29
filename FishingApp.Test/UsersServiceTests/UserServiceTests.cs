using FishingApp.Core.Repoitories.UserRepositories;
using FishingApp.Core.Services.UserServices.Implementations;
using FishingApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Test.UsersServiceTests
{
    [TestClass]
    public class UserServiceTests
    {
        private FishingDbContext _context;
        private UserRepository _userRepository;
        private UserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<FishingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new FishingDbContext(options);
            _userRepository = new UserRepository(_context);
            _userService = new UserService(_userRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetUsersByScoreAsync_ReturnsUsersOrderedByScore()
        {
            var user1 = new User { Name = "Alice", Password = "pass1", Score = 1500 };
            var user2 = new User { Name = "Bob", Password = "pass2", Score = 3000 };
            var user3 = new User { Name = "Charlie", Password = "pass3", Score = 2000 };

            _context.Users.AddRange(user1, user2, user3);
            await _context.SaveChangesAsync();

            var result = await _userService.GetUsersByScoreAsync();

            Assert.IsNotNull(result, "Список пользователей не должен быть null");
            Assert.AreEqual(3, result.Count, "Должно быть 3 пользователя в списке");
            Assert.AreEqual("Bob", result[0].Name, "Первым должен быть Bob с наибольшим Score");
            Assert.AreEqual("Charlie", result[1].Name, "Вторым должен быть Charlie");
            Assert.AreEqual("Alice", result[2].Name, "Третьим должна быть Alice");
        }

        [TestMethod]
        public async Task UpdateUserAsync_UpdatesUserSuccessfully()
        {
            var user = new User { Name = "Diana", Password = "pass4", Score = 1000 };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user.Score = 5000;
            await _userService.UpdateUserAsync(user);

            var updatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == "Diana");
            Assert.IsNotNull(updatedUser, "Пользователь должен существовать после обновления");
            Assert.AreEqual(5000, updatedUser.Score, "Score должен быть обновлён до 5000");
        }
    }
}