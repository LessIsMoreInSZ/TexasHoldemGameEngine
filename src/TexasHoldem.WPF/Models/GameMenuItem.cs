using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Models
{
    public class GameMenuItem
    {
        public GameMenuItem(string text, string icon)
        {
            Text = text;
            Icon = icon;
        }
        public string Text { get; set; }
        public string Icon { get; set; }
    }
}
