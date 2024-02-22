using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Controls;

namespace TexasHoldem.WPF.Behaviors
{
    public class ClickToFlipBehavior:Behavior<CardUI>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching(); 
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var card = (CardUI)sender;
            card.IsShown = !card.IsShown;
        }
    }
}
