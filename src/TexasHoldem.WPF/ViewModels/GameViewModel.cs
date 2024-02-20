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
            PlayerList = new()
            {
                new() { Name = "三重刘德华", Avatar = "/Assets/ldh.jfif" ,Angle=0},
                new() { Name = "不愿意收手的阿祖", Avatar = "/Assets/wyz.jfif" ,Angle=60},
                new() { Name = "赌神黄四郎", Avatar = "/Assets/zrf.jfif",Angle=120 },
                new() { Name = "Player",Avatar="" ,Angle=180},
                new() { Name = "Solid本体", Avatar = "/Assets/pyy.jfif" ,Angle=240},
                new() { Name = "寒战总指挥刘sir", Avatar = "/Assets/gfc.jfif" ,Angle=300},

            };
        }
    }
}
