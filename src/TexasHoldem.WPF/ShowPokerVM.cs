﻿using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.AI.DummyPlayer;
using TexasHoldem.AI.SmartPlayer;
using TexasHoldem.Logic.Cards;
using static TexasHoldem.WPF.TestPoker;
using TexasHoldem.Logic.Players;
using TexasHoldem.Logic.GameMechanics;
using TexasHoldem.WPF.Events;
using TexasHoldem.WPF.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;

namespace TexasHoldem.WPF
{
    public class ShowPokerVM : BindableBase
    {
        private int playerCnt = 0;
        private IEventAggregator aggregator;
        private static readonly List<IPlayer> Players = new List<IPlayer>();
        private PokerPlayer poker;
        public ShowPokerVM(IEventAggregator _aggregator)
        {
            this.aggregator = _aggregator;
            aggregator.GetEvent<CardEvent>().Subscribe(ShowCard);
            aggregator.GetEvent<TextChangeEvent>().Subscribe(ShowText);
            aggregator.GetEvent<CommCardEvent>().Subscribe(ShowCommCard);
            aggregator.GetEvent<ActionEvent>().Subscribe(ShowRaise);

            Players.Add(new DummyPlayer() { Name = "PlayerA" });
            Players.Add(new SmartPlayer() { Name = "PlayerB" });
            poker = new PokerPlayer(aggregator) { Name = "PlayerC" };
            Players.Add(poker);
            Players.Add(new DummyPlayer() { Name = "PlayerD" });
            Players.Add(new SmartPlayer() { Name = "PlayerE" });
            Players.Add(new DummyPlayer() { Name = "PlayerF" });

            Task.Run(() =>
            {
                var game = Game();
                game.Start();
            });
            
        }

       

        private string str6;
        public string Str6
        {
            get { return str6; }
            set { str6 = value; RaisePropertyChanged(); }
        }
        
        private ObservableCollection<CardUI> commCards = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> CommCards
        {
            get { return commCards; }
            set { commCards = value; RaisePropertyChanged(); }
        }

        private string strMainPot;
        public string StrMainPot
        {
            get { return strMainPot; }
            set { strMainPot = value; RaisePropertyChanged(); }
        }

        private string strSidePot;
        public string StrSidePot
        {
            get { return strSidePot; }
            set { strSidePot = value; RaisePropertyChanged(); }
        }

        private string raiseChipCount;
        public string RaiseChipCount
        {
            get { return raiseChipCount; }
            set { raiseChipCount = value; RaisePropertyChanged(); }
        }

        private string raiseChipHint;
        public string RaiseChipHint
        {
            get { return raiseChipHint; }
            set { raiseChipHint = value; RaisePropertyChanged(); }
        }

        private bool isRaiseCommit;
        public bool IsRaiseCommit
        {
            get { return isRaiseCommit; }
            set { isRaiseCommit = value; RaisePropertyChanged(); }
        }

        private Visibility isRaiseCommitVisibility = Visibility.Hidden;
        public Visibility IsRaiseCommitVisibility
        {
            get { return isRaiseCommitVisibility; }
            set { isRaiseCommitVisibility = value; RaisePropertyChanged(); }
        }

        #region player hand card
        private ObservableCollection<CardUI> listPlayerA = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerA
        {
            get { return listPlayerA; }
            set { listPlayerA = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardUI> listPlayerB = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerB
        {
            get { return listPlayerB; }
            set { listPlayerB = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardUI> listPlayerC = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerC
        {
            get { return listPlayerC; }
            set { listPlayerC = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardUI> listPlayerD = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerD
        {
            get { return listPlayerD; }
            set { listPlayerD = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardUI> listPlayerE = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerE
        {
            get { return listPlayerE; }
            set { listPlayerE = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<CardUI> listPlayerF = new ObservableCollection<CardUI>();
        public ObservableCollection<CardUI> ListPlayerF
        {
            get { return listPlayerF; }
            set { SetProperty(ref listPlayerF, value); }
            //set { listPlayerF = value; RaisePropertyChanged(); }
        }
        #endregion

        #region player textbox

        private string playerACurrentPot;
        public string PlayerACurrentPot
        {
            get { return playerACurrentPot; }
            set { playerACurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerAStatus;
        public string PlayerAStatus
        {
            get { return playerAStatus; }
            set { playerAStatus = value; RaisePropertyChanged(); }
        }

        private string playerAAction;
        public string PlayerAAction
        {
            get { return playerAAction; }
            set { playerAAction = value; RaisePropertyChanged(); }
        }

        //B
        private string playerBCurrentPot;
        public string PlayerBCurrentPot
        {
            get { return playerBCurrentPot; }
            set { playerBCurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerBStatus;
        public string PlayerBStatus
        {
            get { return playerBStatus; }
            set { playerBStatus = value; RaisePropertyChanged(); }
        }

        private string playerBAction;
        public string PlayerBAction
        {
            get { return playerBAction; }
            set { playerBAction = value; RaisePropertyChanged(); }
        }

        //C
        private string playerCCurrentPot;
        public string PlayerCCurrentPot
        {
            get { return playerCCurrentPot; }
            set { playerCCurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerCStatus;
        public string PlayerCStatus
        {
            get { return playerCStatus; }
            set { playerCStatus = value; RaisePropertyChanged(); }
        }

        private string playerCAction;
        public string PlayerCAction
        {
            get { return playerCAction; }
            set { playerCAction = value; RaisePropertyChanged(); }
        }

        //D
        private string playerDCurrentPot;
        public string PlayerDCurrentPot
        {
            get { return playerDCurrentPot; }
            set { playerDCurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerDStatus;
        public string PlayerDStatus
        {
            get { return playerDStatus; }
            set { playerDStatus = value; RaisePropertyChanged(); }
        }

        private string playerDAction;
        public string PlayerDAction
        {
            get { return playerDAction; }
            set { playerDAction = value; RaisePropertyChanged(); }
        }

        //E
        private string playerECurrentPot;
        public string PlayerECurrentPot
        {
            get { return playerECurrentPot; }
            set { playerECurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerEStatus;
        public string PlayerEStatus
        {
            get { return playerEStatus; }
            set { playerEStatus = value; RaisePropertyChanged(); }
        }

        private string playerEAction;
        public string PlayerEAction
        {
            get { return playerEAction; }
            set { playerEAction = value; RaisePropertyChanged(); }
        }

        //F
        private string playerFCurrentPot;
        public string PlayerFCurrentPot
        {
            get { return playerFCurrentPot; }
            set { playerFCurrentPot = value; RaisePropertyChanged(); }
        }

        private string playerFStatus;
        public string PlayerFStatus
        {
            get { return playerFStatus; }
            set { playerFStatus = value; RaisePropertyChanged(); }
        }

        private string playerFAction;
        public string PlayerFAction
        {
            get { return playerFAction; }
            set { playerFAction = value; RaisePropertyChanged(); }
        }
        #endregion

        private ITexasHoldemGame Game()
        {
            var list = new List<IPlayer>();

            for (int i = 0; i < Players.Count; i++)
            {
                list.Add(new PokerUiDecorator(aggregator, Players[i]));
            }

            return new TexasHoldemGame(list);
            
        }

        private void ShowRaise(ActionEventPara para)
        {
            RaiseChipHint = para.RaiseMoneyHint;
        }

        private void ShowCard(Card card)
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


                if (playerCnt == 0 || playerCnt == 1)
                {
                    //ListPlayerA.Clear();
                    ListPlayerA.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                else if (playerCnt == 2 || playerCnt == 3)
                {
                    //ListPlayerB.Clear();
                    ListPlayerB.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                else if (playerCnt == 4 || playerCnt == 5)
                {
                    //ListPlayerC.Clear();
                    ListPlayerC.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                else if (playerCnt == 6 || playerCnt == 7)
                {
                    //ListPlayerD.Clear();
                    ListPlayerD.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                else if (playerCnt == 8 || playerCnt == 9)
                {
                    //ListPlayerE.Clear();
                    ListPlayerE.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                else if (playerCnt == 10 || playerCnt == 11)
                {
                    //ListPlayerF.Clear();
                    ListPlayerF.Add(new() { IsShown = true, Value = EnumChangeCardType(card.Type), Suit = EnumChangeCardSuit(card.Suit) });
                }
                str6 += strSuit + card.Type.ToString() + "\r\n";
                playerCnt++;
                if (playerCnt % 2 == 0)
                    str6 += "---\r\n";

                if (playerCnt == 12)
                {
                    playerCnt = 0;
                }

            });
            

        }

        private void ShowCommCard(IReadOnlyCollection<Card> cards)
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
                    if(para.CurrentControl== CurrentControl.MainCommPot)
                    {
                        StrMainPot = para.message;
                    }
                    else if(para.CurrentControl == CurrentControl.SideCommPot)
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
                if(!IsRaiseCommit)
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
    }
}
