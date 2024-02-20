using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TexasHoldem.WPF.Behaviors
{
    public class DeckDealBehavior:Behavior<ItemsControl>
    {
        //不知道用什么事件 先放着不管
        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
        protected override void OnAttached()
        {
            base.OnAttached();
        }
    }
}
