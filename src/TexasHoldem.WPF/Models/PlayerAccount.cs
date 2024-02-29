using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Models
{
    public partial class PlayerAccount : ObservableObject
    {
        [ObservableProperty]
        string name;
        [ObservableProperty]
        DateTime createTime;
        [ObservableProperty]
        double wealth;

        partial void OnWealthChanged(double value)
        {

            if (value <= (double)Level.Junior)
            {
                Level = Level.Noob;
            }
            else if (value > (double)Level.Junior && value <= (double)Level.Intermediate)
            {
                Level = Level.Junior;
            }
            else if (value > (double)Level.Intermediate && value <= (double)Level.Senior)
            {
                Level = Level.Intermediate;
            }
            else if (value > (double)Level.Senior && value <= (double)Level.Skilled)
            {
                Level = Level.Senior;
            }
            else if (value > (double)Level.Skilled && value <= (double)Level.发哥)
            {
                Level = Level.Skilled;
            }
            else
            {
                Level = Level.发哥;
            }
        }
        [ObservableProperty]
        Level level;
        [ObservableProperty]
        int totalRounds;
        [ObservableProperty]
        string avatar;
        [ObservableProperty]
        double angle;
        [ObservableProperty]
        string strcurrentpot;
        [ObservableProperty]
        string strstatus;
        [ObservableProperty]
        string straction;

    }
    public enum Level
    {
        Noob = 0,
        Junior = 1000,
        Intermediate = 3000,
        Senior = 6000,
        Skilled = 10000,
        发哥 = 100000,
    }
}
