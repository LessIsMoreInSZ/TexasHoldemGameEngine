using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldem.AI.DummyPlayer;
using TexasHoldem.AI.SmartPlayer;
using TexasHoldem.Logic.GameMechanics;
using TexasHoldem.Logic.Players;

namespace TexasHoldem.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int GameWidth = 66;

        private const int NumberOfCommonRows = 3; // place for community cards, pot, main pot, side pots

        private static readonly List<IPlayer> Players = new List<IPlayer>();
        public App()
        {
            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new PokerPlayer((6 * Players.Count) + NumberOfCommonRows));
            Players.Add(new DummyPlayer());
            Players.Add(new SmartPlayer());
            Players.Add(new DummyPlayer());

            var gameHeight = (6 * Players.Count) + NumberOfCommonRows;

            var game = Game();
            game.Start();
        }

        private static ITexasHoldemGame Game()
        {
            var list = new List<IPlayer>();

            for (int i = 0; i < Players.Count; i++)
            {
                list.Add(new PokerUiDecorator(Players[i], (6 * i) + NumberOfCommonRows, GameWidth, 1));
            }

            return new TexasHoldemGame(list);
        }
    }
}
