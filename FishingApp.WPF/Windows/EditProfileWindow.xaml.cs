using FishingApp.Core.Services.AuthServices;
using FishingApp.Core.Services.AuthServices.IAuthServices;
using FishingApp.Core.Services.FishingServices.IFishingServices;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FishingApp.WPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProfileWindow.xaml
    /// </summary>
    public partial class EditProfileWindow : Window
    {
        private readonly IUserService _userService;
        private readonly User _currentUser;

        public EditProfileWindow(IUserService userService, User currentUser)
        {
            InitializeComponent();
            _userService = userService;
            _currentUser = currentUser;

            RegisterUsernameBox.Text = _currentUser.Name;
            RegisterPasswordBox.Password = _currentUser.Password;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = RegisterUsernameBox.Text.Trim();
            var password = RegisterPasswordBox.Password;
            var password2 = RegisterPasswordBox2.Password;

            if (password == password2)
            {
                _currentUser.Name = username;
                _currentUser.Password = password;

                await _userService.UpdateUserAsync(_currentUser);

                MessageBox.Show("Профиль успешно обновлен!", "Успех");

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка обновления профиля");
            }
        }
    }
}
