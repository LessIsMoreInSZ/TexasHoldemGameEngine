using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.WPF.Events
{
    public class ActionEvent : PubSubEvent<ActionEventPara>
    {

    }

    public class ActionEventPara
    {
        public string RaiseMoneyHint { get; set; }
        //public EnumAction EnumAction { get; set; }
    }

    public enum EnumAction
    {
        None,
        CheckOrCall,
        Raise,
        Fold,
        Allin
    }
}
