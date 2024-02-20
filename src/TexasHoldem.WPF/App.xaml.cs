using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TexasHoldem.AI.DummyPlayer;
using TexasHoldem.AI.SmartPlayer;
using TexasHoldem.Logic.GameMechanics;
using TexasHoldem.Logic.Players;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Interfaces;
using TexasHoldem.WPF.Models;
using TexasHoldem.WPF.Services;
using TexasHoldem.WPF.ViewModels;
using TexasHoldem.WPF.Views;

namespace TexasHoldem.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public static int PlayerNumer = 0;
        public static Player Player;
        protected override Window CreateShell()
        {
            return Container.Resolve<ShowPoker>();
            //return Container.Resolve<ShellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMetroDialogService,MetroDialogService>();
            containerRegistry.RegisterForNavigation<ShellWindow, ShellWindowViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>(PageKeys.Index);
            containerRegistry.RegisterForNavigation<ShowPoker, ShowPokerVM>();
        }
    }
}
