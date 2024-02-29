using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;
using TexasHoldem.WPF.Behaviors;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Controls;
using TexasHoldem.WPF.Events;
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
        private IEventAggregator aggregator;
        private static readonly List<IPlayer> Players = new List<IPlayer>();
        private PokerPlayer poker;

        List<Tuple<string, string>> aiPlayers = new()
            {
               new( "Player1",  "/Assets/ldh.jpg" ),
                new( "Player2", "/Assets/wyz.jpg"),
                new( "Player3",  "/Assets/zrf.jpg"),
                new( "Player4",  "/Assets/pyy.jpg"),
                new( "Player5",  "/Assets/gfc.jpg" ),
                new( "Player6",  "/Assets/ldh.jpg" ),
                new( "Player7", "/Assets/wyz.jpg"),
                new( "Player8",  "/Assets/zrf.jpg"),
                new( "Player9",  "/Assets/pyy.jpg"),
                new( "Player10",  "/Assets/pyy.jpg"),
                new( "Player11",  "/Assets/gfc.jpg" ),
                new( "Player12",  "/Assets/ldh.jpg" ),
                new( "Player13", "/Assets/wyz.jpg"),
                new( "Player14",  "/Assets/zrf.jpg"),
                new( "Player15",  "/Assets/pyy.jpg"),
                new( "Player16",  "/Assets/gfc.jpg" ),
            };

        [ObservableProperty]
        ObservableCollection<Card> myCards = new(Enumerable.Range(0, 2).Select(n => new Card { Value = Value.LittleJoker }));

        [ObservableProperty]
        ObservableCollection<PlayerAccount> playerList;

        [ObservableProperty]
        ObservableCollection<Models.Deck> decks;

        [ObservableProperty]
        ObservableCollection<Card> publicCards;

        /// <summary>
        /// 公共牌
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<CardUI> commCards = new ObservableCollection<CardUI>();

        /// <summary>
        /// 主池
        /// </summary>
        [ObservableProperty]
        private string strMainPot;

        /// <summary>
        /// 边池
        /// </summary>
        [ObservableProperty]
        private string strSidePot;


        /// <summary>
        /// Raise数量
        /// </summary>
        [ObservableProperty]
        private string raiseChipCount;

        /// <summary>
        /// Raise数量提示
        /// </summary>
        [ObservableProperty]
        private string raiseChipHint;

        /// <summary>
        /// 是否提交raise数量
        /// </summary>
        [ObservableProperty]
        private bool isRaiseCommit;

        /// <summary>
        /// 是否显示raise
        /// </summary>
        [ObservableProperty]
        private Visibility isRaiseCommitVisibility = Visibility.Hidden;

        public GameViewModel(IEventAggregator _aggregator)
        {
            aggregator.GetEvent<CardEvent>().Subscribe(ShowCard);
            aggregator.GetEvent<TextChangeEvent>().Subscribe(ShowText);
            aggregator.GetEvent<CommCardEvent>().Subscribe(ShowCommCard);
            aggregator.GetEvent<ActionEvent>().Subscribe(ShowRaise);
        }

        private void ShowRaise(ActionEventPara para)
        {
            RaiseChipHint = para.RaiseMoneyHint;
        }

        private void ShowCard(TexasHoldem.Logic.Cards.Card card)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                string strSuit = string.Empty;
                if (card.Suit == CardSuit.Club)
                {
                    strSuit = "♣";
                }
                else if (card.Suit == CardSuit.Spade)
                {
                    strSuit = "♠";
                }
                else if (card.Suit == CardSuit.Diamond)
                {
                    strSuit = "♦";
                }
                else if (card.Suit == CardSuit.Heart)
                {
                    strSuit = "♥";
                }



                //if (playerCnt == 0 || playerCnt == 1)
                //{
                //    //ListPlayerA.Clear();
                //    ListPlayerA.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //else if (playerCnt == 2 || playerCnt == 3)
                //{
                //    //ListPlayerB.Clear();
                //    ListPlayerB.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //else if (playerCnt == 4 || playerCnt == 5)
                //{
                //    //ListPlayerC.Clear();
                //    ListPlayerC.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //else if (playerCnt == 6 || playerCnt == 7)
                //{
                //    //ListPlayerD.Clear();
                //    ListPlayerD.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //else if (playerCnt == 8 || playerCnt == 9)
                //{
                //    //ListPlayerE.Clear();
                //    ListPlayerE.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //else if (playerCnt == 10 || playerCnt == 11)
                //{
                //    //ListPlayerF.Clear();
                //    ListPlayerF.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                //}
                //str6 += strSuit + card.Type.ToString() + "\r\n";
                //playerCnt++;
                //if (playerCnt % 2 == 0)
                //    str6 += "---\r\n";

                //if (playerCnt == 12)
                //{
                //    playerCnt = 0;
                //}

            });


        }

        private void ShowCommCard(IReadOnlyCollection<TexasHoldem.Logic.Cards.Card> cards)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                CommCards.Clear();
                foreach (var card in cards)
                {
                    CardUI cardUI = new CardUI();
                    cardUI.Suit = EnumChangeCardSuit(card.Suit);
                    cardUI.Value = EnumChangeCardType(card.Type);
                    cardUI.IsShown = true;
                    CommCards.Add(cardUI);
                }
            });
        }

        private void ShowText(TextChangeEventPara para)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (para != null)
                {
                    if (para.CurrentControl == CurrentControl.MainCommPot)
                    {
                        StrMainPot = para.message;
                    }
                    else if (para.CurrentControl == CurrentControl.SideCommPot)
                    {
                        StrSidePot = para.message;
                    }
                    switch (para.playerName)
                    {
                        case "PlayerA":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerACurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerAStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerAAction = para.message;
                                break;
                            }
                        case "PlayerB":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerBCurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerBStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerBAction = para.message;
                                break;
                            }
                        case "PlayerC":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerCCurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerCStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerCAction = para.message;
                                break;
                            }
                        case "PlayerD":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerDCurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerDStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerDAction = para.message;
                                break;
                            }
                        case "PlayerE":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerECurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerEStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerEAction = para.message;
                                break;
                            }
                        case "PlayerF":
                            {
                                if (para.CurrentControl == CurrentControl.CurrentPot)
                                    PlayerFCurrentPot = para.message;
                                else if (para.CurrentControl == CurrentControl.Status)
                                    PlayerFStatus = para.message;
                                else if (para.CurrentControl == CurrentControl.Action)
                                    PlayerFAction = para.message;
                                break;
                            }
                    }
                }
            });
        }

        [RelayCommand]
        async Task Deal()
        {
            CleanTable();

            //如果不等待，第一张牌发不出去，头想烂了想不通为什么
            //await Task.Delay(10);

            HashSet<int> IndexSet = new();
            int length = 0;
            while (length < App.PlayerNumber * 2 + 5)
            {
                IndexSet.Add(Random.Shared.Next(0, 51));
                length = IndexSet.Count;
            }
            indexArray = IndexSet.ToArray();
            await DealToPlayers();
            //await DealToPublic();
            //ResetDealParams();
        }
        async Task DealToPlayers()
        {
            await Task.Delay(1);
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

        #region
        public ICommand CheckOrCallCommand
        {
            get => new DelegateCommand(() =>
            {
                //ActionEventPara para = new 
                //aggregator.GetEvent<ActionEvent>().Publish();
                poker.EnumAction = EnumAction.CheckOrCall;
            });
        }

        public ICommand RaiseCommand
        {
            get => new DelegateCommand(() =>
            {
                if (!IsRaiseCommit)
                {
                    IsRaiseCommit = true;
                    poker.EnumAction = EnumAction.Raise;
                    poker.isRaise = false;
                    IsRaiseCommitVisibility = Visibility.Visible;
                }
                else
                {
                    poker.isRaise = true;
                    poker.CurrentRoundMoney = int.Parse(RaiseChipCount);
                    poker.EnumAction = EnumAction.Raise;
                    IsRaiseCommitVisibility = Visibility.Hidden;
                }
            });
        }

        public ICommand FoldCommand
        {
            get => new DelegateCommand(() =>
            {
                poker.EnumAction = EnumAction.Fold;
            });
        }

        public ICommand AllInCommand
        {
            get => new DelegateCommand(() =>
            {
                poker.EnumAction = EnumAction.Allin;
            });
        }
        #endregion

        private Value EnumChangeCardType(CardType value)
        {
            switch (value)
            {
                case CardType.Ace: return Value.Ace;
                case CardType.Two: return Value.Two;
                case CardType.Three: return Value.Three;
                case CardType.Four: return Value.Four;
                case CardType.Five: return Value.Five;
                case CardType.Six: return Value.Six;
                case CardType.Seven: return Value.Seven;
                case CardType.Eight: return Value.Eight;
                case CardType.Nine: return Value.Nine;
                case CardType.Ten: return Value.Ten;
                case CardType.Jack: return Value.Jack;
                case CardType.Queen: return Value.Queen;
                case CardType.King: return Value.King;
                default: return Value.Ace;
            }
        }

        private Suit EnumChangeCardSuit(CardSuit value)
        {
            switch (value)
            {
                case CardSuit.Club: return Suit.Clubs;
                case CardSuit.Spade: return Suit.Spades;
                case CardSuit.Heart: return Suit.Hearts;
                case CardSuit.Diamond: return Suit.Diamonds;
                default: return Suit.Hearts;
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
            //重置Deck
            Decks = new(Enumerable.Range(0, 52).Select(n => new Deck { Offset = (n - 27) * 4 }));
            //重置PublicCards
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
