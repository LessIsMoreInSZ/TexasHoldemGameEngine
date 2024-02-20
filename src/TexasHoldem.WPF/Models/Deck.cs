using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Models
{
    public partial class Deck:ObservableObject
    {
        [ObservableProperty]
        double offset;
        [ObservableProperty]
        bool isDealed;
        [ObservableProperty]
        double toX; 
        [ObservableProperty]
        double toY;
    }
}
