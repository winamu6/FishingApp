using FishingApp.Core.Services.FishServices.Interfaces;
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
    /// <summary>
    /// Логика взаимодействия для FishDexWindow.xaml
    /// </summary>
    public partial class FishDexWindow : Window
    {
        private readonly IFishService _fishService;
        private List<Fish> _fishList;

        public FishDexWindow(IFishService fishService)
        {
            InitializeComponent();
            _fishService = fishService;
            LoadFish();
        }

        private async void LoadFish()
        {
            _fishList = await _fishService.GetAllFishAsync();
            FishListBox.ItemsSource = _fishList;
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
