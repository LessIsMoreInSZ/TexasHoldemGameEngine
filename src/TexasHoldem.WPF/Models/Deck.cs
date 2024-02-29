using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Models
{
    public partial class PlayingDeck:ObservableObject
    {
        [ObservableProperty]
        double offset;
        [ObservableProperty]
        bool isDealed;
        [ObservableProperty]
        bool isPublic;
    }
}
