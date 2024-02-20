using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Player> playerList;

        [ObservableProperty]
        ObservableCollection<Deck> deck;

        ItemsControl items;
        public GameViewModel()
        {
            Deck = new(Enumerable.Range(0, 54).Select(n => new Deck { Offset = n * 4 }));
            PlayerList = new()
            {
                new() { Name = "三重刘德华", Avatar = "/Assets/ldh.jpg" ,Angle=0},
                new() { Name = "不愿意收手的阿祖", Avatar = "/Assets/wyz.jpg" ,Angle=60},
                new() { Name = "赌神黄四郎", Avatar = "/Assets/zrf.jpg",Angle=120 },
                new() { Name = "Player",Avatar="" ,Angle=180},
                new() { Name = "Solid本体", Avatar = "/Assets/pyy.jpg" ,Angle=240},
                new() { Name = "寒战总指挥刘sir", Avatar = "/Assets/gfc.jpg" ,Angle=300},
            };
        }
        [RelayCommand]
        void Loaded(ItemsControl itemsControl)
        {
            items = itemsControl;
        }
        [RelayCommand]
        void Deal()
        {
            int i = 0;
            HashSet<int> hash = new();
            while (i < 5)
            {
                hash.Add(Random.Shared.Next(0, 53));
                i = hash.Count;
            }
            var ints = hash.ToArray();
            for (int j = 0; j < 5; j++)
            {
                //var deckToDeal = Deck[];
                var toX = PlayerListConstants.Diameter * (Math.Sin(360d / App.PlayerNumber * j));
                var toY = PlayerListConstants.Diameter * (Math.Cos(360d / App.PlayerNumber * j));
                var it = items.Items[ints[j]];
                if (items.Items[ints[j]] is Border border)
                {

                }
                //deckToDeal.IsDealed = true;  
                //   await Task.Delay(200);
            }
        }

    }
}
