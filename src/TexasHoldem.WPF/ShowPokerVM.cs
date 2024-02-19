using Prism.Events;
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

namespace TexasHoldem.WPF
{
    public class ShowPokerVM : BindableBase
    {
        private int playerCnt = 0;
        private IEventAggregator aggregator;
        private static readonly List<IPlayer> Players = new List<IPlayer>();
        public ShowPokerVM(IEventAggregator _aggregator)
        {
            this.aggregator = _aggregator;
            aggregator.GetEvent<CardEvent>().Subscribe(ShowCard);
            aggregator.GetEvent<TextChangeEvent>().Subscribe(ShowText);

            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new PokerPlayer(aggregator));
            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new DummyPlayer());

            var game = Game();
            game.Start();
        }

        private string str6;
        public string Str6
        {
            get { return str6; }
            set { str6 = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerA = new List<CardUI>();
        public List<CardUI> ListPlayerA
        {
            get { return listPlayerA; }
            set { listPlayerA = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerB = new List<CardUI>();
        public List<CardUI> ListPlayerB
        {
            get { return listPlayerB; }
            set { listPlayerB = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerC = new List<CardUI>();
        public List<CardUI> ListPlayerC
        {
            get { return listPlayerC; }
            set { listPlayerC = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerD = new List<CardUI>();
        public List<CardUI> ListPlayerD
        {
            get { return listPlayerD; }
            set { listPlayerD = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerE = new List<CardUI>();
        public List<CardUI> ListPlayerE
        {
            get { return listPlayerE; }
            set { listPlayerE = value; RaisePropertyChanged(); }
        }

        private List<CardUI> listPlayerF = new List<CardUI>();
        public List<CardUI> ListPlayerF
        {
            get { return listPlayerF; }
            set { listPlayerF = value; RaisePropertyChanged(); }
        }

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

        private ITexasHoldemGame Game()
        {
            var list = new List<IPlayer>();

            for (int i = 0; i < Players.Count; i++)
            {
                //list.Add(new PokerUiDecorator(Players[i]));
                list.Add(new PokerUiDecorator(aggregator, Players[i]));
            }

            return new TexasHoldemGame(list);
        }

        private void ShowCard(Card card)
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


        }

        private void ShowText(TextChangeEventPara para)
        {
            if (para != null)
            {
                switch (para.playerName)
                {
                    case "PlayerA":
                        {
                            if (para.CurrentControl == CurrentControl.CurrentPot)
                                PlayerACurrentPot = para.message;
                            else if (para.CurrentControl == CurrentControl.Status)
                                playerAStatus = para.message;
                            else if (para.CurrentControl == CurrentControl.Action)
                                playerAAction = para.message;
                                break;
                        }
                    case "PlayerB":
                        {
                            //if (para.CurrentControl == CurrentControl.CurrentPot)
                            //    PlayerBCurrentPot = para.message;
                            //else if (para.CurrentControl == CurrentControl.Status)
                            //    PlayerBStatus = para.message;
                            //else if (para.CurrentControl == CurrentControl.Action)
                            //    PlayerBAction = para.message;
                            break;
                        }
                    case "PlayerC":
                        {
                            if (para.CurrentControl == CurrentControl.CurrentPot)
                                PlayerACurrentPot = para.message;
                            else if (para.CurrentControl == CurrentControl.Status)
                                playerAStatus = para.message;
                            else if (para.CurrentControl == CurrentControl.Action)
                                playerAAction = para.message;
                            break;
                        }
                    case "PlayerD":
                        {
                            if (para.CurrentControl == CurrentControl.CurrentPot)
                                PlayerACurrentPot = para.message;
                            else if (para.CurrentControl == CurrentControl.Status)
                                playerAStatus = para.message;
                            else if (para.CurrentControl == CurrentControl.Action)
                                playerAAction = para.message;
                            break;
                        }
                    case "PlayerE":
                        {
                            if (para.CurrentControl == CurrentControl.CurrentPot)
                                PlayerACurrentPot = para.message;
                            else if (para.CurrentControl == CurrentControl.Status)
                                playerAStatus = para.message;
                            else if (para.CurrentControl == CurrentControl.Action)
                                playerAAction = para.message;
                            break;
                        }
                    case "PlayerF":
                        {
                            if (para.CurrentControl == CurrentControl.CurrentPot)
                                PlayerACurrentPot = para.message;
                            else if (para.CurrentControl == CurrentControl.Status)
                                playerAStatus = para.message;
                            else if (para.CurrentControl == CurrentControl.Action)
                                playerAAction = para.message;
                            break;
                        }
                }
            }
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
    }
}
