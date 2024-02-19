using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class  IndexViewModel:ObservableObject
    {
        public IEnumerable<GameMenuItem> Menu { get; set; } = new List<GameMenuItem>()
        {
           new( "Single Play",""),
           new( "Online Play",""),
           new("System Config",""),
           new("Exit ","")
        };
    }
}
