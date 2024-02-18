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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //List<CardUI> deck = new();
            //for (int i = 1; i < 5; i++)
            //{
            //    for (int j = 1; j < 14; j++)
            //    {
            //        deck.Add(new CardUI { Value = (Value)j, Suit = (Suit)i });
            //    }
            //}
            //deck.Add(new CardUI { Value = Value.BigJoker });
            //deck.Add(new CardUI { Value = Value.LittleJoker });
            //lb.ItemsSource = deck;
        }
        private async void playBt_Click(object sender, RoutedEventArgs e)
        {
            myCards.ItemsSource = null;
            await Task.Delay(500);
            List<CardUI> cards = new()
            {
                new(){Value=Value.BigJoker},
                new(){Value=Value.BigJoker},
                new(){Value=Value.BigJoker},
                new(){Value=Value.BigJoker},
                new(){Value=Value.BigJoker}
            };
            myCards.ItemsSource = cards;
            foreach (CardUI card in cards)
            {
                await Task.Delay(50);
                card.IsShown = true;
            }

        }
    }
}
