using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Player> playerList;

        [ObservableProperty]
        List<string> test = new()
        {
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
        };

        public GameViewModel()
        {
            PlayerList = new(Enumerable.Range(1, App.PlayerNumer).
        Select(n => new Player
        {
            Name = $"AI Player {n}",
            Angle = n * 360d / App.PlayerNumer
        }));
        }
    }
}
