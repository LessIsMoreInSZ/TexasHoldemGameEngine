﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TexasHoldem.Logic;
using TexasHoldem.Logic.Cards;

namespace TexasHoldem.WPF.Controls
{
    public class CardUI : Control
    {
        private static string Dir = Environment.CurrentDirectory + @"\Assets\";
       //private Image image;
        public bool IsShown
        {
            get { return (bool)GetValue(IsShownProperty); }
            set { SetValue(IsShownProperty, value); }
        }

        public static readonly DependencyProperty IsShownProperty =
            DependencyProperty.Register("IsShown", typeof(bool), typeof(CardUI), new PropertyMetadata(false));

        //***
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var border = GetTemplateChild("bd") as Border;
            border.Background=new ImageBrush(new BitmapImage(new Uri(Picture)));
            //image = GetTemplateChild("image") as Image;
            //image.Source = new BitmapImage(new Uri(Picture));
        }

        [Bindable(true)]
        public string Picture
        {
            get { return (string)GetValue(PictureProperty); }
            set { SetValue(PictureProperty, value); }
        }
        [Bindable(true)]
        public Value Value
        {
            get { return (Value)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        [Bindable(true)]
        public Suit Suit
        {
            get { return (Suit)GetValue(SuitProperty); }
            set { SetValue(SuitProperty, value); }
        }
        public static readonly DependencyProperty PictureProperty =
            DependencyProperty.Register("Picture", typeof(string), typeof(CardUI), new PropertyMetadata(Dir + "d1.png"));




        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Value), typeof(CardUI), new PropertyMetadata(Value.Zero, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (CardUI)d;
            var value = (Value)e.NewValue;
            switch (value)
            {
                case Value.Ace:
                case Value.Two:
                case Value.Three:
                case Value.Four:
                case Value.Five:
                case Value.Six:
                case Value.Seven:
                case Value.Eight:
                case Value.Nine:
                case Value.Ten:
                case Value.Jack:
                case Value.Queen:
                case Value.King:
                    var val = card.Picture.Replace(Dir, "").Substring(1);
                    card.Picture = card.Picture.Replace(val, ((int)value).ToString()) + ".png";
                    break;
                case Value.BigJoker:
                    card.Picture = Dir + "bj.png";
                    break;
                case Value.LittleJoker:
                    card.Picture = Dir + "lj.png";
                    break;
                default:
                    break;
            }

        }



        public static readonly DependencyProperty SuitProperty =
            DependencyProperty.Register("Suit", typeof(Suit), typeof(CardUI), new PropertyMetadata(Suit.None, OnSuitChanged));

        private static void OnSuitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = (CardUI)d;
            var suit = (Suit)e.NewValue;
            var shortPath = card.Picture.Replace(Dir, "");
            var cap = shortPath[0];
            switch (suit)
            {
                case Suit.None:
                    break;
                case Suit.Hearts:
                    card.Picture = Dir + shortPath.Replace(cap, 'h');
                    break;
                case Suit.Spades:
                    card.Picture = Dir + shortPath.Replace(cap, 's');
                    break;
                case Suit.Clubs:
                    card.Picture = Dir + shortPath.Replace(cap, 'c');
                    break;
                case Suit.Diamonds:
                    card.Picture = Dir + shortPath.Replace(cap, 'd');
                    break;
                default:
                    break;
            }
        }
    }
}
