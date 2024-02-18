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
using TexasHoldem.AI.DummyPlayer;
using TexasHoldem.AI.SmartPlayer;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.GameMechanics;
using TexasHoldem.Logic.Players;
using TexasHoldem.Logic.Extensions;
using System.Configuration;
using TexasHoldem.UI.Console;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

namespace TexasHoldem.WPF
{
    public enum UIAction
    {
        None=-1,
        CheckOrCall,
        Raise,
        Fold,
        AllIn
    }
    public static class CurrentActionKey
    {
        public static UIAction CurrentUIAction;
    }
    /// <summary>
    /// TestPoker.xaml 的交互逻辑
    /// </summary>
    public partial class TestPoker : Window
    {
        public class TestPokerUiDecorator: PlayerDecorator
        {
            private Card firstCard;

            private Card secondCard;

            private IReadOnlyCollection<Card> CommunityCards { get; set; }

            public TestPokerUiDecorator(IPlayer player)
                : base(player)
            {
                //this.DrawGameBox();
            }

            public override void StartHand(IStartHandContext context)
            {
                var dealerSymbol = context.FirstPlayerName == this.Player.Name ? "D" : " ";

                this.firstCard = context.FirstCard.DeepClone();
                this.secondCard = context.SecondCard.DeepClone();
                //this.DrawSingleCard(this.row + 1, 10, this.firstCard);
                //this.DrawSingleCard(this.row + 1, 14, this.secondCard);

                base.StartHand(context);
            }

            public override void StartRound(IStartRoundContext context)
            {
                this.CommunityCards = context.CommunityCards;
                this.UpdateCommonRows(
                    context.CurrentPot,
                    context.CurrentMainPot.AmountOfMoney,
                    context.CurrentSidePots.Select(s => s.AmountOfMoney));

                ////ConsoleHelper.WriteOnConsole(this.row + 1, this.width - 11, context.RoundType + "   ");
                ////ConsoleHelper.WriteOnConsole(this.row + 3, 2, new string(' ', this.width - 3));
                base.StartRound(context);
            }

            public override PlayerAction PostingBlind(IPostingBlindContext context)
            {
                this.UpdateCommonRows(context.CurrentPot, context.CurrentPot, new int[] { });

                var action = base.PostingBlind(context);

                //ConsoleHelper.WriteOnConsole(this.row + 2, 2, new string(' ', this.width - 3));
                //ConsoleHelper.WriteOnConsole(this.row + 3, 2, "Last action: " + action.Type + "(" + action.Money + ")");

                var moneyAfterAction = context.CurrentStackSize;

                //ConsoleHelper.WriteOnConsole(this.row + 1, 2, moneyAfterAction + "   ");

                return action;
            }

            public override PlayerAction GetTurn(IGetTurnContext context)
            {
                this.UpdateCommonRows(
                    context.CurrentPot,
                    context.MainPot.AmountOfMoney,
                    context.SidePots.Select(s => s.AmountOfMoney));

                //ConsoleHelper.WriteOnConsole(this.row + 1, 2, context.MoneyLeft + "   ");

                var action = base.GetTurn(context);

                if (action.Type == PlayerActionType.Fold)
                {
                    this.Muck(context.MoneyLeft);
                }

                //ConsoleHelper.WriteOnConsole(this.row + 2, 2, new string(' ', this.width - 3));

                var lastAction = action.Type.ToString();

                if (action.Type == PlayerActionType.CheckCall)
                {
                    lastAction += $"({context.MoneyToCall})";
                }
                else if (action.Type == PlayerActionType.Raise)
                {
                    lastAction += $"({action.Money + context.MyMoneyInTheRound + context.MoneyToCall})";
                }

                //ConsoleHelper.WriteOnConsole(this.row + 3, 2, new string(' ', this.width - 3));
                //ConsoleHelper.WriteOnConsole(this.row + 3, 2, "Last action: " + lastAction);

                var moneyAfterAction = action.Type == PlayerActionType.Fold
                    ? context.MoneyLeft
                    : context.MoneyLeft - action.Money - context.MoneyToCall;

                //ConsoleHelper.WriteOnConsole(this.row + 1, 2, moneyAfterAction + "   ");

                return action;
            }

            private void Muck(int moneyLeft)
            {
                //this.DrawMuckedSingleCard(this.row + 1, 10, this.firstCard);
                //this.DrawMuckedSingleCard(this.row + 1, 14, this.secondCard);
            }

            private void UpdateCommonRows(int pot, int mainPot, IEnumerable<int> sidePots)
            {
                // Clear the first common row
                //ConsoleHelper.WriteOnConsole(this.commonRow, 0, new string(' ', this.width - 1));

                this.DrawCommunityCards();

                var potAsString = "Pot: " + pot;
                //ConsoleHelper.WriteOnConsole(this.commonRow, this.width - potAsString.Length - 2, potAsString);

                if (sidePots.Count() == 0)
                {
                    // Clear the side pots
                    //ConsoleHelper.WriteOnConsole(this.commonRow + 1, 0, new string(' ', this.width - 1));
                }
                else
                {
                    var mainPotAsString = "Main Pot: " + mainPot;
                    //ConsoleHelper.WriteOnConsole(this.commonRow, 2, mainPotAsString);

                    var sidePotsAsString = "Side Pots: ";
                    foreach (var item in sidePots)
                    {
                        sidePotsAsString += item + " | ";
                    }

                    //ConsoleHelper.WriteOnConsole(this.commonRow + 1, 2, sidePotsAsString.Remove(sidePotsAsString.Length - 2, 2));
                }
            }


            private void DrawCommunityCards()
            {
                if (this.CommunityCards != null)
                {
                    var cardsAsString = this.CommunityCards.CardsToString();
                    //var cardsLength = cardsAsString.Length / 2;
                    //var cardsStartCol = (this.width / 2) - (cardsLength / 2);
                    //var cardIndex = 0;
                    //var spacing = 0;

                    //foreach (var communityCard in this.CommunityCards)
                    //{
                    //    this.DrawSingleCard(this.commonRow, cardsStartCol + (cardIndex * 4) + spacing, communityCard);
                    //    cardIndex++;

                    //    spacing += communityCard.Type == CardType.Ten ? 1 : 0;
                    //}
                }
            }

            private void DrawSingleCard(int row, int col, Card card)
            {
                var cardColor = this.GetCardColor(card);
                //ConsoleHelper.WriteOnConsole(row, col, " " + card + " ", cardColor, ConsoleColor.White);
                //ConsoleHelper.WriteOnConsole(row, col + 2 + card.ToString().Length, " ");
            }

            private void DrawMuckedSingleCard(int row, int col, Card card)
            {
                //ConsoleHelper.WriteOnConsole(row, col, " " + card + " ", ConsoleColor.Gray, ConsoleColor.DarkGray);
            }

            private ConsoleColor GetCardColor(Card card)
            {
                switch (card.Suit)
                {
                    case CardSuit.Club: return ConsoleColor.DarkGreen;
                    case CardSuit.Diamond: return ConsoleColor.Blue;
                    case CardSuit.Heart: return ConsoleColor.Red;
                    case CardSuit.Spade: return ConsoleColor.Black;
                    default: throw new ArgumentException("card.Suit");
                }
            }
        }

        public class TestPokerPlayer : BasePlayer
        {
            public TestPokerPlayer(int buyIn = -1)
            {
                this.BuyIn = buyIn;
            }
            public override string Name => "";

            public override int BuyIn { get; }

            public override PlayerAction GetTurn(IGetTurnContext context)
            {
                //if (!context.CanRaise)
                //{
                //    this.DrawRestrictedPlayerOptions(context.MoneyToCall);
                //}
                //else
                //{
                //    this.DrawPlayerOptions(context.MoneyToCall);
                //}

                //Task.Run(() =>
                {
                    while (true)
                    {
                        PlayerAction action = null;
                        switch (CurrentActionKey.CurrentUIAction)
                        {
                            case UIAction.CheckOrCall:
                                action = PlayerAction.CheckOrCall();
                                break;
                            case UIAction.Raise:
                                if (!context.CanRaise)
                                {
                                    continue;
                                }

                                action = PlayerAction.Raise(
                                    this.RaiseAmount(context.MoneyLeft, context.MinRaise, context.MoneyToCall, context.MyMoneyInTheRound));
                                break;
                            case UIAction.Fold:
                                action = PlayerAction.Fold();
                                break;
                            case UIAction.AllIn:
                                if (!context.CanRaise)
                                {
                                    continue;
                                }

                                action = context.MoneyLeft > 0
                                             ? PlayerAction.Raise(context.MoneyLeft - context.MoneyToCall)
                                             : PlayerAction.CheckOrCall();
                                break;
                        }

                        if (action != null)
                        {
                            return action;
                        }
                        Thread.Sleep(1000);
                    }
                }//);
            }

            private int RaiseAmount(int moneyLeft, int minRaise, int moneyToCall, int myMoneyInTheRound)
            {
                var wholeMinRaise = minRaise + myMoneyInTheRound + moneyToCall;
                if (wholeMinRaise >= moneyLeft + myMoneyInTheRound)
                {
                    // Instant All-In
                    return moneyLeft - moneyToCall;
                }

                var perfix = $"Raise amount [{wholeMinRaise}-{moneyLeft + myMoneyInTheRound}]:";

                //do
                //{
                //    //ConsoleHelper.WriteOnConsole(this.row + 2, 2, new string(' ', Console.WindowWidth - 3));
                //    //ConsoleHelper.WriteOnConsole(this.row + 2, 2, perfix);

                //    var text = ConsoleHelper.UserInput(this.row + 2, perfix.Length + 3);
                //    int result;

                //    if (int.TryParse(text, out result))
                //    {
                //        if (result < wholeMinRaise)
                //        {
                //            return minRaise;
                //        }
                //        else if (result >= moneyLeft + myMoneyInTheRound)
                //        {
                //            // Raise All-in
                //            return moneyLeft - moneyToCall;
                //        }

                //        return result - (myMoneyInTheRound + moneyToCall);
                //    }
                //}
                //while (true);

                //var text = ConsoleHelper.UserInput(this.row + 2, perfix.Length + 3);
                int result;

                return result = 0;
            }


            public override PlayerAction PostingBlind(IPostingBlindContext context)
            {
                return context.BlindAction;
            }
        }

        private static readonly List<IPlayer> Players = new List<IPlayer>();

        public TestPoker()
        {
            InitializeComponent();

            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new TestPokerPlayer());
            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new DummyPlayer());

            var game = Game();
            game.Start();
        }

        private static ITexasHoldemGame Game()
        {
            var list = new List<IPlayer>();

            for (int i = 0; i < Players.Count; i++)
            {
                //list.Add(new PokerUiDecorator(Players[i]));
                list.Add(new TestPokerUiDecorator(Players[i]));
            }

            return new TexasHoldemGame(list);
        }

        private void btnCheckOrCall_Click(object sender, RoutedEventArgs e)
        {
            CurrentActionKey.CurrentUIAction = UIAction.CheckOrCall;
            PlayerAction action = PlayerAction.CheckOrCall();
        }

        private void btnRaise_Click(object sender, RoutedEventArgs e)
        {
            CurrentActionKey.CurrentUIAction = UIAction.Raise;
        }

        private void btnFold_Click(object sender, RoutedEventArgs e)
        {
            CurrentActionKey.CurrentUIAction = UIAction.Fold;
        }

        private void btnAllIn_Click(object sender, RoutedEventArgs e)
        {
            CurrentActionKey.CurrentUIAction = UIAction.AllIn;
        }
    }
}
