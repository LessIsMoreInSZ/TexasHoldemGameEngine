using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TexasHoldem.WPF.Behaviors;
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
                new() { Name = "三重刘德华", Avatar = "/Assets/ldh.jpg" },
                new() { Name = "不愿意收手的阿祖", Avatar = "/Assets/wyz.jpg"},
                new() { Name = "赌神黄四郎", Avatar = "/Assets/zrf.jpg"},
                new() { Name = "Player",Avatar="" },
                new() { Name = "Solid本体", Avatar = "/Assets/pyy.jpg" ,},
                new() { Name = "寒战总指挥刘sir", Avatar = "/Assets/gfc.jpg" },
            };
            PlayerList=new(PlayerList.Select((n,i) =>
            {
                n.Angle = 360d / App.PlayerNumber * i;
                return n;
            }));
        }

        [RelayCommand]
        void Loaded(ItemsControl itemsControl)
        {
            items = itemsControl;
        }

        [RelayCommand]
        async Task Deal()
        {
            int i = 0;
            HashSet<int> hash = new();
            while (i < App.PlayerNumber)
            {
                hash.Add(Random.Shared.Next(0, 53));
                i = hash.Count;
            }
            var ints = hash.ToArray();
            for (int j = 0; j <App.PlayerNumber; j++)
            {
                var deckToDeal = Deck[ints[j]];
                deckToDeal.IsDealed = true;
                DeckDealBehavior.Turn++;
                await Task.Delay(200);
            }
            DeckDealBehavior.IsSecond = true;
            while (i<App.PlayerNumber*2)
            {
                hash.Add(Random.Shared.Next(0, 53));
                i = hash.Count;
            }
            ints=hash.ToArray();
            for (int k = App.PlayerNumber;k < App.PlayerNumber*2; k++)
            {
                var deckToDeal = Deck[ints[k]];
                deckToDeal.IsDealed = true;
                DeckDealBehavior.Turn++;
                await Task.Delay(200);
            }
            DeckDealBehavior.IsSecond = false;
        }

    }
}
