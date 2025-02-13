﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;
using TexasHoldem.Logic.Players;
using TexasHoldem.UI.Console;
using TexasHoldem.Logic.Extensions;
using System.Collections.Specialized;
using System.Windows.Data;
using Prism.Events;
using TexasHoldem.WPF.Events;

namespace TexasHoldem.WPF
{
    public class PokerUiDecorator : PlayerDecorator
    {
        private IEventAggregator aggregator;

        private Card firstCard;

        private Card secondCard;

        private IReadOnlyCollection<Card> CommunityCards { get; set; }

        private void ChangeText(string playerName, CurrentControl currentControl, string message)
        {
            TextChangeEventPara textChangeEventPara = new TextChangeEventPara();
            textChangeEventPara.playerName = playerName;
            textChangeEventPara.CurrentControl = currentControl;
            textChangeEventPara.message = message;
            aggregator.GetEvent<TextChangeEvent>().Publish(textChangeEventPara);
        }

        public PokerUiDecorator(IEventAggregator _aggregator, IPlayer player)
            : base(player)
        {
            this.aggregator = _aggregator;
            

        }

        

        public override void StartHand(IStartHandContext context)
        {
            //this.UpdateCommonRows(0, 0, new int[] { });
            var dealerSymbol = context.FirstPlayerName == this.Player.Name ? "D" : " ";

            ChangeText(this.Player.Name, CurrentControl.CurrentPot, context.MoneyLeft.ToString());
            ////ConsoleHelper.WriteOnConsole(this.row + 1, 1, dealerSymbol, ConsoleColor.Green);
            ////ConsoleHelper.WriteOnConsole(this.row + 3, 2, "                            ");

            ////ConsoleHelper.WriteOnConsole(this.row + 1, 2, context.MoneyLeft.ToString());
            this.firstCard = context.FirstCard.DeepClone();
            this.secondCard = context.SecondCard.DeepClone();
            this.DrawSingleCard(this.firstCard);
            this.DrawSingleCard(this.secondCard);

            base.StartHand(context);
        }

        public override void StartRound(IStartRoundContext context)
        {
            this.CommunityCards = context.CommunityCards;
            this.UpdateCommonRows(
                context.CurrentPot,
                context.CurrentMainPot.AmountOfMoney,
                context.CurrentSidePots.Select(s => s.AmountOfMoney));

            ChangeText(this.Player.Name, CurrentControl.Status, context.RoundType+"");
            base.StartRound(context);
        }

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            this.UpdateCommonRows(context.CurrentPot, context.CurrentPot, new int[] { });

            var action = base.PostingBlind(context);

            //ConsoleHelper.WriteOnConsole(this.row + 2, 2, new string(' ', this.width - 3));
            //ConsoleHelper.WriteOnConsole(this.row + 3, 2, "Last action: " + action.Type + "(" + action.Money + ")");
            ChangeText(this.Player.Name, CurrentControl.Action, "Last action: " + action.Type + "(" + action.Money + ")");
            var moneyAfterAction = context.CurrentStackSize;
            ChangeText(this.Player.Name, CurrentControl.CurrentPot, moneyAfterAction.ToString());

            //ConsoleHelper.WriteOnConsole(this.row + 1, 2, moneyAfterAction + "   ");

            return action;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            this.UpdateCommonRows(
                context.CurrentPot,
                context.MainPot.AmountOfMoney,
                context.SidePots.Select(s => s.AmountOfMoney));

            ChangeText(this.Player.Name, CurrentControl.CurrentPot, context.MoneyLeft.ToString());

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

            ChangeText(this.Player.Name, CurrentControl.Action, "Last action: " + lastAction);

            //ConsoleHelper.WriteOnConsole(this.row + 3, 2, new string(' ', this.width - 3));
            //ConsoleHelper.WriteOnConsole(this.row + 3, 2, "Last action: " + lastAction);

            var moneyAfterAction = action.Type == PlayerActionType.Fold
                ? context.MoneyLeft
                : context.MoneyLeft - action.Money - context.MoneyToCall;

            //ConsoleHelper.WriteOnConsole(this.row + 1, 2, moneyAfterAction + "   ");

            return action;
        }

        /// <summary>
        /// Fold 
        /// </summary>
        /// <param name="moneyLeft"></param>
        private void Muck(int moneyLeft)
        {
            //this.DrawMuckedSingleCard(this.row + 1, 10, this.firstCard);
            //this.DrawMuckedSingleCard(this.row + 1, 14, this.secondCard);
        }

        private void UpdateCommonRows(int pot, int mainPot, IEnumerable<int> sidePots)
        {
            // Clear the first common row
            //ConsoleHelper.WriteOnConsole(this.commonRow, 0, new string(' ', this.width - 1));

            //20240218
            //this.DrawCommunityCards();
            aggregator.GetEvent<CommCardEvent>().Publish(CommunityCards);

            //var potAsString = "Pot: " + pot;
            var potAsString =  pot;
            ChangeText(this.Player.Name, CurrentControl.MainCommPot, pot.ToString());

            //ConsoleHelper.WriteOnConsole(this.commonRow, this.width - potAsString.Length - 2, potAsString);

            if (sidePots.Count() == 0)
            {
                // Clear the side pots
                ChangeText(this.Player.Name, CurrentControl.SideCommPot, 0.ToString());
                //ConsoleHelper.WriteOnConsole(this.commonRow + 1, 0, new string(' ', this.width - 1));
            }
            else
            {
                var mainPotAsString = "Main Pot: " + mainPot;
                ChangeText(this.Player.Name, CurrentControl.MainCommPot,mainPotAsString);
                //ConsoleHelper.WriteOnConsole(this.commonRow, 2, mainPotAsString);

                var sidePotsAsString = "Side Pots: ";
                foreach (var item in sidePots)
                {
                    sidePotsAsString += item + " | ";
                }
                ChangeText(this.Player.Name, CurrentControl.SideCommPot, sidePotsAsString);

                //ConsoleHelper.WriteOnConsole(this.commonRow + 1, 2, sidePotsAsString.Remove(sidePotsAsString.Length - 2, 2));
            }
        }

        private void DrawGameBox()
        {
            //ConsoleHelper.WriteOnConsole(this.row, 0, new string('═', this.width), PlayerBoxColor);
            //ConsoleHelper.WriteOnConsole(this.row + 4, 0, new string('═', this.width), PlayerBoxColor);
            //ConsoleHelper.WriteOnConsole(this.row, 0, "╔", PlayerBoxColor);
            //ConsoleHelper.WriteOnConsole(this.row, this.width - 1, "╗", PlayerBoxColor);
            //ConsoleHelper.WriteOnConsole(this.row + 4, 0, "╚", PlayerBoxColor);
            //ConsoleHelper.WriteOnConsole(this.row + 4, this.width - 1, "╝", PlayerBoxColor);
            for (var i = 1; i < 4; i++)
            {
                //ConsoleHelper.WriteOnConsole(this.row + i, 0, "║", PlayerBoxColor);
                //ConsoleHelper.WriteOnConsole(this.row + i, this.width - 1, "║", PlayerBoxColor);
            }
        }

        private void DrawCommunityCards()
        {
            if (this.CommunityCards != null)
            {
                var cardsAsString = this.CommunityCards.CardsToString();
                var cardsLength = cardsAsString.Length / 2;
                //var cardsStartCol = (this.width / 2) - (cardsLength / 2);
                var cardIndex = 0;
                var spacing = 0;

                foreach (var communityCard in this.CommunityCards)
                {
                    this.DrawSingleCard(communityCard);
                    cardIndex++;

                    spacing += communityCard.Type == CardType.Ten ? 1 : 0;
                }
            }
        }

        private void DrawSingleCard(Card card)
        {
            var cardColor = this.GetCardColor(card);
            aggregator.GetEvent<CardEvent>().Publish(card);
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
}
