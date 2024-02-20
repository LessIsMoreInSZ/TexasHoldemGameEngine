using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.WPF.Events
{
    public class TextChangeEvent : PubSubEvent<TextChangeEventPara>
    {

    }

    public class TextChangeEventPara
    {
        public string playerName;

        //public string controlName;

        public CurrentControl CurrentControl;

        public string message;

    }

    public enum CurrentControl
    {
        CommPot,
        MainCommPot,
        SideCommPot,
        CurrentPot,
        Status,
        Action
    }
}
