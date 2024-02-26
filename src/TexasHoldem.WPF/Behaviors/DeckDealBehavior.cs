using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TexasHoldem.WPF.Constants;

namespace TexasHoldem.WPF.Behaviors
{
    public class DeckDealBehavior : Behavior<Border>
    {
        static double offset = 10;
        static double Diameter = 200;

        private static int turn;
        public static int Turn
        {
            get { return turn; }
            set { turn = value > App.PlayerNumber - 1 ? 0 : value; }
        }

        private static bool isSecond;
        public static bool IsSecond
        {
            get { return isSecond; }
            set { isSecond = value; }
        }

        private static int publicTurn;
        public static int PublicTurn
        {
            get { return publicTurn; }
            set { publicTurn = value > 4 ? 0 : value; }
        }
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.IsEnabledChanged += OnDealedToPlayer;
            AssociatedObject.FocusableChanged += OnDealedToPublic;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.IsEnabledChanged -= OnDealedToPlayer;
            AssociatedObject.FocusableChanged -= OnDealedToPublic;
        }
        private void OnDealedToPlayer(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            var angle = 360d / App.PlayerNumber * Turn / 180 * Math.PI;
            var toX = DeckDealBehavior.Diameter * Math.Sin(angle) + (IsSecond ? offset : 0);
            var toY = DeckDealBehavior.Diameter * Math.Cos(angle);
            var duration = TimeSpan.FromSeconds(0.2);
            if ((bool)e.NewValue)
            {
                var bd = (Border)sender;
                DoubleAnimation xAnimation = new(toX, duration);
                DoubleAnimation yAnimation = new(toY, duration);
                var rt = new TranslateTransform();
                bd.RenderTransform = rt;
                rt.BeginAnimation(TranslateTransform.XProperty, xAnimation);
                rt.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            }
        }
        private async void OnDealedToPublic(object sender, DependencyPropertyChangedEventArgs e)
        {
            var bd = (Border)sender;
            var duration = TimeSpan.FromSeconds(0.2);
            DoubleAnimation xAnimation = new(222d, duration);
            DoubleAnimation yAnimation = new(80d* (PublicTurn-2) , duration);
            DoubleAnimation scaleAnimation=new(1d,2d, duration);
            var rt = new TransformGroup();
            var trans=new TranslateTransform();
            var scale = new ScaleTransform();
            rt.Children.Add(trans);
            rt.Children.Add(scale);
            bd.RenderTransform = rt;
            trans.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            trans.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            await Task.Delay(duration);
                bd.Visibility = Visibility.Collapsed;
        }


    }
}
