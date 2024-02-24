using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TexasHoldem.Logic.Players;
using TexasHoldem.WPF.Behaviors;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Controls;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class GameViewModel : ObservableObject, INavigationAware
    {
        public partial class Card : ObservableObject
        {
            [ObservableProperty]
            Value value;
            [ObservableProperty]
            Suit suit;
            [ObservableProperty]
            bool isShown;
            [ObservableProperty]
            Visibility visibility = Visibility.Hidden;
        }

        int[] indexArray;

        List<Tuple<string, string>> aiPlayers = new()
            {
               new( "三重刘德华",  "/Assets/ldh.jpg" ),
                new( "不愿意收手的阿祖", "/Assets/wyz.jpg"),
                new( "赌神黄四郎",  "/Assets/zrf.jpg"),
                new( "Solid本体",  "/Assets/pyy.jpg"),
                new( "寒战总指挥刘sir",  "/Assets/gfc.jpg" ),
            new( "三重刘德华",  "/Assets/ldh.jpg" ),
                new( "不愿意收手的阿祖", "/Assets/wyz.jpg"),
                new( "赌神黄四郎",  "/Assets/zrf.jpg"),
                new( "Solid本体",  "/Assets/pyy.jpg"),
                new( "寒战总指挥刘sir",  "/Assets/gfc.jpg" ),
                   new( "三重刘德华",  "/Assets/ldh.jpg" ),
                new( "不愿意收手的阿祖", "/Assets/wyz.jpg"),
                new( "赌神黄四郎",  "/Assets/zrf.jpg"),
                new( "Solid本体",  "/Assets/pyy.jpg"),
                new( "寒战总指挥刘sir",  "/Assets/gfc.jpg" ),
            };

        [ObservableProperty]
        ObservableCollection<Card> myCards = new(Enumerable.Range(0, 2).Select(n => new Card { Value = Value.LittleJoker }));

        [ObservableProperty]
        ObservableCollection<PlayerAccount> playerList;

        [ObservableProperty]
        ObservableCollection<Deck> decks;

        [ObservableProperty]
        ObservableCollection<Card> publicCards;

        public GameViewModel()
        {

        }

        [RelayCommand]
        async Task Deal()
        {
            //如果不判断当前Decks是不是全新的（没发牌给玩家或公共牌），直接调用CleanTable，第一张牌就发不出去，我也不知道为什么
            //如果有懂哥有自信能解决这个问题：注释掉下三行，直接使用CleanTable方法
            if (Decks.Any(n => n.IsDealed))
            {
                CleanTable();
            }
            //CleanTable();

            HashSet<int> IndexSet = new();
            int length = 0;
            while (length < App.PlayerNumber * 2 + 5)
            {
                IndexSet.Add(Random.Shared.Next(0, 53));
                length = IndexSet.Count;
            }
            indexArray = IndexSet.ToArray();
            await DealToPlayers();
            await DealToPublic();
            ResetDealParams();
        }
        async Task DealToPlayers()
        {
            for (int i = 0; i < App.PlayerNumber * 2; i++)
            {
                var index = indexArray[i];
                var deckToDeal = Decks[index];
                deckToDeal.IsDealed = true;
                DeckDealBehavior.Turn++;
                if (i == App.PlayerNumber - 1)
                    DeckDealBehavior.IsSecond = true;
                await Task.Delay(200);
            }

        }
        async Task DealToPublic()
        {
            for (int i = App.PlayerNumber * 2; i < indexArray.Length; i++)
            {
                var index = indexArray[i];
                var deckToDeal = Decks[index];
                deckToDeal.IsPublic = true;

                await Task.Delay(200);
                PublicCards[DeckDealBehavior.PublicTurn].Visibility = Visibility.Visible;
                PublicCards[DeckDealBehavior.PublicTurn].IsShown = true;
                DeckDealBehavior.PublicTurn++;
            }
        }
        void ResetDealParams()
        {
            DeckDealBehavior.Turn = 0;
            DeckDealBehavior.IsSecond = false;
            DeckDealBehavior.PublicTurn = 0;
        }
        void LoadPlayers()
        {
            PlayerList = new(Enumerable.Range(-1, App.PlayerNumber).Select(n =>
            {
                if (n == -1)
                    return App.Player;

                PlayerAccount player = new()
                {
                    Name = aiPlayers[n].Item1,
                    Avatar = aiPlayers[n].Item2,
                    Angle = 360d / App.PlayerNumber * (n + 1),
                };
                return player;

            }));
        }
        void CleanTable()
        {
            Decks = new(Enumerable.Range(0, 54).Select(n => new Deck { Offset = (n - 27) * 4 }));
            PublicCards = new(Enumerable.Range(0, 5).Select(n => new Card { Value = Value.BigJoker }));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            CleanTable();
            LoadPlayers();
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
=> true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
