using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Models
{
    public partial class Player : ObservableRecipient
    {
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]
        string name;

        [ObservableProperty]
        [NotifyPropertyChangedRecipients]
        string avatar;

        [ObservableProperty]
        double angle;


    }
}
