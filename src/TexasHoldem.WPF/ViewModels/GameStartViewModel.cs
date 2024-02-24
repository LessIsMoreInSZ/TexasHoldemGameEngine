using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class GameStartViewModel : ObservableObject, IDialogAware
    {
        [ObservableProperty]
        ObservableCollection<PlayerAccount> playerAccounts = new(Enumerable.Range(1, 5)
    .Select(n => new PlayerAccount()
    {
        Name = $"Player {n}",
        Wealth = Random.Shared.NextDouble() * Random.Shared.Next(10, 10000),
        CreateTime = DateTime.Now.AddDays(Random.Shared.Next(-1000, 0)),
        TotalRounds = Random.Shared.Next(10, 10000)
    }));

        [ObservableProperty]
        PlayerAccount player;
        public IEnumerable<int> Numbers { get; set; } = Enumerable.Range(2, 10);

        public int Number { get; set; }
        public string Title => "Game";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()=>true;

        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        [RelayCommand]
        void Start()
        {
            App.PlayerNumber = Number;
            App.Player = Player;
            RequestClose(new DialogResult(ButtonResult.OK));
        }
        [RelayCommand]
        void Quit()
        {
            RequestClose(new DialogResult(ButtonResult.Cancel));
        }


    }
}
