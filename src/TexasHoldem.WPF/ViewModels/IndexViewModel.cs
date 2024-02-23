using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using Prism.Services.Dialogs;
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
        IMetroDialogService _mahDialog;
        IRegionNavigationService _navigationService;
        IDialogService _dialogService;

        //触发IndexView的动画
        [ObservableProperty]
        bool b=true;

        //当前游戏菜单选项
        [ObservableProperty]
        GameMenuItem mode;

        //游戏菜单
        public IEnumerable<GameMenuItem> Menu { get; set; } = new List<GameMenuItem>()
        {
           new( "Single Play",""),
           new( "Online Play",""),
           new("System Config",""),
           new("Exit ","")
        };

        [RelayCommand]
        async Task ModeSelected()
        {
            if (mode != null)
            {
                switch (Mode.Text)
                {
                    case "Single Play":
                        _dialogService.ShowDialog();

                        B = false;
                        await Task.Delay(1000);
                        _navigationService.RequestNavigate(PageKeys.Game);
                        break;
                    case "Online Play":
                        await _mahDialog.ShowAsync("Banned", "This mode is permanently banned under the relevant law!");
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
        public IndexViewModel(IRegionManager regionManager, IMetroDialogService metroDialogService,IDialogService dialogService)
        {
            _navigationService = regionManager.Regions[Regions.Main].NavigationService;
            _mahDialog = metroDialogService;
            _dialogService = dialogService;
        }

        //公共构造，给设计时绑定用
        public IndexViewModel()
        {
            
        }

      
    }
}
