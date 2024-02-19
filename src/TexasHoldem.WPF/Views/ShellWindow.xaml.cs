using MahApps.Metro.Controls;
using Prism.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Controls;
using TexasHoldem.WPF.Interfaces;

namespace TexasHoldem.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager,IMetroDialogService metroDialogService)
        {
            InitializeComponent();
            metroDialogService.Register(this);
            RegionManager.SetRegionName(mainRegion, Regions.Main);
            RegionManager.SetRegionName(left, Regions.Split);
            RegionManager.SetRegionManager(mainRegion, regionManager);
            RegionManager.SetRegionManager(left, regionManager);

      
        }
        private async void playBt_Click(object sender, RoutedEventArgs e)
        {
            //myCards.ItemsSource = null;
            //await Task.Delay(500);
            //List<CardUI> cards = new()
            //{
            //    new(){Value=Value.BigJoker},
            //    new(){Value=Value.BigJoker},
            //    new(){Value=Value.BigJoker},
            //    new(){Value=Value.BigJoker},
            //    new(){Value=Value.BigJoker}
            //};
            //myCards.ItemsSource = cards;
            //foreach (CardUI card in cards)
            //{
            //    await Task.Delay(50);
            //    card.IsShown = true;
            //}
        }
    }
}
