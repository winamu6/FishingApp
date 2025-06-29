using FishingApp.Core.Repoitories.UserRepositories;
using FishingApp.Core.Services.AuthServices;
using FishingApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingApp.Test.AuthServiceTests
{
    [TestClass]
    public class AuthServiceTests
    {
        private FishingDbContext _context;
        private UserRepository _userRepository;
        private AuthService _authService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<FishingDbContext>()
                .UseInMemoryDatabase(databaseName: "FishingAppTestDb")
                .Options;

            _context = new FishingDbContext(options);
            _userRepository = new UserRepository(_context);
            _authService = new AuthService(_userRepository);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task TC01_RegisterNewUser_Success()
        {
            var userExistsBefore = await _userRepository.UserExistsAsync("user123");
            Assert.IsFalse(userExistsBefore, "Пользователь уже существует до регистрации");

            var result = await _authService.RegisterAsync("user123", "pass123");

            Assert.IsTrue(result, "Регистрация должна пройти успешно");

            var user = await _userRepository.GetByNameAsync("user123");
            Assert.IsNotNull(user, "Пользователь должен быть добавлен в БД");
            Assert.AreEqual("user123", user.Name, "Имя пользователя должно совпадать");
        }

        [TestMethod]
        public async Task TC02_LoginRegisteredUser_Success()
        {
            var registrationResult = await _authService.RegisterAsync("user123", "pass123");
            Assert.IsTrue(registrationResult, "Регистрация пользователя для входа должна пройти успешно");

            var user = await _authService.LoginAsync("user123", "pass123");

            Assert.IsNotNull(user, "Авторизация должна пройти успешно");
            Assert.AreEqual("user123", user.Name, "Имя авторизованного пользователя должно совпадать");
        }
    }
}
