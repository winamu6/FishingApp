using FishingApp.Core.Services.FishingServices.IFishingServices;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        private readonly IFishingService _fishingService;

        private readonly User _currentUser;
        private List<Fish> _currentCatch = new();

        public FishDexWindow(IFishService fishService, IUserService userService, IFishingService fishingService, User currentUser)
        {
            InitializeComponent();

            _fishService = fishService;
            _userService = userService;
            _fishingService = fishingService;
            _currentUser = currentUser;

            Loaded += FishDexWindow_Loaded;
        }

        private async void FishDexWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var fishList = await _fishService.GetAllFishAsync();
            if (fishList == null || fishList.Count == 0)
            {
                MessageBox.Show("Рыбы не найдены!");
            }
            FishComboBox.ItemsSource = fishList;

            await LoadFish();
            await LoadUsers();
            await LoadFishingHistory();
        }


        private void AddFishToCatch_Click(object sender, RoutedEventArgs e)
        {
            if (FishComboBox.SelectedItem is Fish fish)
            {
                _currentCatch.Add(fish);
                CatchListBox.Items.Add(fish);
            }
        }

        private async void SaveFishing_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCatch.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну рыбу.");
                return;
            }

            await _fishingService.SaveFishingAsync(_currentUser, _currentCatch);

            MessageBox.Show("Рыбалка сохранена! Очки начислены.");

            _currentCatch.Clear();
            CatchListBox.Items.Clear();
            await LoadUsers();
            await LoadFishingHistory();
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
        private async Task LoadFishingHistory()
        {
            var history = await _fishingService.GetFishingHistoryAsync(_currentUser.Id);
            FishingHistoryListView.ItemsSource = history;
        }
        private void FishListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FishListBox.SelectedItem is Fish fish && fish.Discription != null)
            {
                FishName_Border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                FishResoivoir_Border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                FishWeight_Border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
                FishDiscription_Border.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));

                FishName.Text = $"Имя: {fish.Name}";
                FishReservoir.Text = $"Водоём: {fish.Discription.Resirvoir}";
                FishWeight.Text = $"Средний вес: {fish.Discription.Weight} кг";
                FishDescription.Text = $"Описание: {fish.Discription.Text}";

                if (fish.Discription.Image != null && fish.Discription.Image.Length > 0)
                {
                    FishImage.Source = LoadImage(fish.Discription.Image);
                }
                else
                {
                    FishImage.Source = null;
                }
            }
            else
            {
                FishName_Border.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                FishResoivoir_Border.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                FishWeight_Border.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
                FishDiscription_Border.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));

                FishName.Text = "";
                FishReservoir.Text = "";
                FishWeight.Text = "";
                FishDescription.Text = "";
                FishImage.Source = null;
            }
        }

        private BitmapImage LoadImage(byte[] imageData)
        {
            using (var ms = new MemoryStream(imageData))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }

        public class StringToBackgroundConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string text = value as string;
                return string.IsNullOrEmpty(text) ? Brushes.Transparent : new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new EditProfileWindow(_userService, _currentUser);
            profileWindow.ShowDialog();
        }
    }
}
