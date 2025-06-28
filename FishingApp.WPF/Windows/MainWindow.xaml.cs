using FishingApp.Core.Services.AuthServices.IAuthServices;
using FishingApp.Core.Services.FishingServices;
using FishingApp.Core.Services.FishingServices.IFishingServices;
using FishingApp.Core.Services.FishServices;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.UserServices.Implementations;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Models.Entities;
using FishingApp.WPF.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FishingApp.WPF
{
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;

        public MainWindow(IAuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = LoginUsernameBox.Text.Trim();
            var password = LoginPasswordBox.Password;

            var user = await _authService.LoginAsync(username, password);
            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.Name}!\nВаш счёт: {user.Score}", "Успешный вход");

                var fishService = App.Services.GetRequiredService<IFishService>();
                var userService = App.Services.GetRequiredService<IUserService>();
                var fishingService = App.Services.GetRequiredService<IFishingService>();

                var fishDexWindow = new FishDexWindow(fishService, userService, fishingService, user);
                fishDexWindow.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка входа");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = RegisterUsernameBox.Text.Trim();
            var password = RegisterPasswordBox.Password;
            var user = await _authService.LoginAsync(username, password);

            var success = await _authService.RegisterAsync(username, password);
            if (success)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех");

                var fishService = App.Services.GetRequiredService<IFishService>();
                var userService = App.Services.GetRequiredService<IUserService>();
                var fishingService = App.Services.GetRequiredService<IFishingService>();

                var fishDexWindow = new FishDexWindow(fishService, userService, fishingService, user);
                fishDexWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует.", "Ошибка регистрации");
            }
        }
    }
}