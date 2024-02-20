using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TexasHoldem.Logic.Players;
using TexasHoldem.UI.Console;
using TexasHoldem.WPF.Events;

namespace TexasHoldem.WPF
{
    public class PokerPlayer : BasePlayer
    {
        
        private readonly int row;
        private readonly IEventAggregator _aggregator;
        public bool isRaise { get; set; }
        public EnumAction EnumAction { get; set; }
        public int CurrentRoundMoney { get; set; }

        public PokerPlayer(IEventAggregator aggregator, int buyIn = -1)
        {
            _aggregator = aggregator;
            this.BuyIn = buyIn;
        }

        public override string Name { get; set; } = "ConsolePlayer_" + Guid.NewGuid();

        public override int BuyIn { get; }

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            return context.BlindAction;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            if (!context.CanRaise)
            {
                //this.DrawRestrictedPlayerOptions(context.MoneyToCall);
            }
            else
            {
                //this.DrawPlayerOptions(context.MoneyToCall);
            }

            while (true)
            {
                PlayerAction action = null;
                switch (EnumAction)
                {
                    case EnumAction.CheckOrCall:
                        action = PlayerAction.CheckOrCall();
                        break;
                    case EnumAction.Raise:
                        if (!context.CanRaise)
                        {
                            continue;
                        }

                        int result = this.RaiseAmount(context.MoneyLeft, context.MinRaise, context.MoneyToCall, context.MyMoneyInTheRound);
                        if(result != 0)
                        {
                            action = PlayerAction.Raise(result);
                        }
                        
                        break;
                    case EnumAction.Fold:
                        action = PlayerAction.Fold();
                        break;
                    case EnumAction.Allin:
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
                    EnumAction = EnumAction.None;
                    return action;
                }
            }
        }

        private int RaiseAmount(int moneyLeft, int minRaise, int moneyToCall, int myMoneyInTheRound)
        {
            int result = 0;
            var wholeMinRaise = minRaise + myMoneyInTheRound + moneyToCall;
            if (wholeMinRaise >= moneyLeft + myMoneyInTheRound)
            {
                // Instant All-In
                return moneyLeft - moneyToCall;
            }

            var perfix = $"Raise amount [{wholeMinRaise}-{moneyLeft + myMoneyInTheRound}]:";
            _aggregator.GetEvent<ActionEvent>().Publish(new ActionEventPara { RaiseMoneyHint = perfix });

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
            
            if(isRaise)
            {
                var text = CurrentRoundMoney.ToString();
                if (int.TryParse(text, out result))
                {
                    if (result < wholeMinRaise)
                    {
                        return minRaise;
                    }
                    else if (result >= moneyLeft + myMoneyInTheRound)
                    {
                        // Raise All-in
                        return moneyLeft - moneyToCall;
                    }

                    return result - (myMoneyInTheRound + moneyToCall);
                }
                isRaise = false;
            }
            return result;

        }

        private void DrawPlayerOptions(int moneyToCall)
        {
            var col = 2;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "Select action: [");
            col += 16;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "C", ConsoleColor.Yellow);
            col++;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "]heck/[");
            col += 7;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "C", ConsoleColor.Yellow);
            col++;

            var callString = moneyToCall <= 0 ? "]all, [" : "]all(" + moneyToCall + "), [";

            ConsoleHelper.WriteOnConsole(this.row + 2, col, callString);
            col += callString.Length;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "R", ConsoleColor.Yellow);
            col++;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "]aise, [");
            col += 8;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "F", ConsoleColor.Yellow);
            col++;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "]old, [");
            col += 7;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "A", ConsoleColor.Yellow);
            col++;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "]ll-in");
        }

        private void DrawRestrictedPlayerOptions(int moneyToCall)
        {
            var col = 2;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "Select action: [");
            col += 16;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "C", ConsoleColor.Yellow);
            col++;

            var callString = moneyToCall <= 0 ? "]all, [" : "]all(" + moneyToCall + "), [";

            ConsoleHelper.WriteOnConsole(this.row + 2, col, callString);
            col += callString.Length;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "F", ConsoleColor.Yellow);
            col++;
            ConsoleHelper.WriteOnConsole(this.row + 2, col, "]old");
        }
    }
}
