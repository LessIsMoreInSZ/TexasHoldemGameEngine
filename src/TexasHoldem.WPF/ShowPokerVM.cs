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

namespace TexasHoldem.WPF
{
    public class ShowPokerVM: BindableBase
    {
        private int playerCnt=0;
        private IEventAggregator aggregator;
        private static readonly List<IPlayer> Players = new List<IPlayer>();
        public ShowPokerVM(IEventAggregator _aggregator)
        {
            this.aggregator = _aggregator;
            aggregator.GetEvent<CardEvent>().Subscribe(ShowCard);

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

        private ITexasHoldemGame Game()
        {
            var list = new List<IPlayer>();

            for (int i = 0; i < Players.Count; i++)
            {
                //list.Add(new PokerUiDecorator(Players[i]));
                list.Add(new PokerUiDecorator(aggregator,Players[i]));
            }

            return new TexasHoldemGame(list);
        }

        private void ShowCard(Card card)
        {
            string strSuit=string.Empty;
            if(card.Suit==CardSuit.Club)
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
                strSuit = " ♥";
            }
            str6 += strSuit + card.Type.ToString()+"\r\n";
            playerCnt++;
            if (playerCnt % 2 == 0)
                str6 += "---\r\n";
        }

    }
}
