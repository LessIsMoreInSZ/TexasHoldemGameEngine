using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Interfaces;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.ViewModels
{
    public partial class IndexViewModel : ObservableObject
    {
        [ObservableProperty]
        bool b=true;

        IMetroDialogService _dialogService;
        IRegionNavigationService _navigationService;
        public IEnumerable<GameMenuItem> Menu { get; set; } = new List<GameMenuItem>()
        {
           new( "Single Play",""),
           new( "Online Play",""),
           new("System Config",""),
           new("Exit ","")
        };
        public IndexViewModel(IRegionManager regionManager, IMetroDialogService metroDialogService)
        {
            _navigationService = regionManager.Regions[Regions.Main].NavigationService;
            _dialogService = metroDialogService;
        }

        public IndexViewModel()
        {
            
        }
        [ObservableProperty]
        GameMenuItem mode;
        [RelayCommand]
        async Task ModeSelected()
        {
            if (mode != null)
            {
                switch (Mode.Text)
                {
                    case "Single Play":
                        B = false;
                        await Task.Delay(1000);
                        _navigationService.RequestNavigate(PageKeys.Game);
                        break;
                    case "Online Play":
                        await _dialogService.ShowAsync("Banned", "This mode is permanently banned under the relevant law!");
                        break;
                    case "System Config":
                        break;
                    case "Exit":
                        break;
                    default:
                        break;
                }
                Mode = null;
            }
        }
    }
}
