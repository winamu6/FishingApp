using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Models.Entities;
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
    public partial class FishDexWindow : Window
    {
        private readonly IFishService _fishService;
        private readonly IUserService _userService;

        public FishDexWindow(IFishService fishService, IUserService userService)
        {
            InitializeComponent();
            _fishService = fishService;
            _userService = userService;

            Loaded += FishDexWindow_Loaded;
        }

        private async void FishDexWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFish();
            await LoadUsers();
        }

        private async Task LoadFish()
        {
            var fishList = await _fishService.GetAllFishAsync();
            FishListBox.ItemsSource = fishList;
        }

        private async Task LoadUsers()
        {
            var userList = await _userService.GetUsersByScoreAsync();
            UserScoreListView.ItemsSource = userList;
        }

        private void FishListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FishListBox.SelectedItem is Fish fish && fish.Discription != null)
            {
                FishName.Text = fish.Name;
                FishReservoir.Text = $"Водоём: {fish.Discription.Resirvoir}";
                FishWeight.Text = $"Средний вес: {fish.Discription.Weight} кг";
                FishDescription.Text = $"Описание: {fish.Discription.Text}";
            }
        }
    }
}
