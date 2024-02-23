using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Standard;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Constants;
using TexasHoldem.WPF.Interfaces;

namespace TexasHoldem.WPF.ViewModels
{
   public partial class ShellWindowViewModel:ObservableObject
    {
        IRegionNavigationService _mainNavigationService;
        IRegionNavigationService _splitNavigationService;
        IMetroDialogService _dialogService;
        public ShellWindowViewModel(IRegionManager regionManager, IMetroDialogService dialogService)
        {
            _mainNavigationService = regionManager.Regions[Regions.Main].NavigationService;
            _splitNavigationService = regionManager.Regions[Regions.Split].NavigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        void Loaded()
        {
          _mainNavigationService.RequestNavigate(PageKeys.Index);
        }
        [RelayCommand]
         async Task Return()
        {
            //返回上一界面
            if (_mainNavigationService.Journal.CurrentEntry.Uri?.ToString() == PageKeys.Index)
            {
                //退出程序
                //导航到主界面
               var result=await  _dialogService.ShowAsync("Attention", "Are you sure you want to quit this game ?",MessageDialogStyle.AffirmativeAndNegative);
                if (result==MessageDialogResult.Affirmative)
                {
                    /*
                      保存当前玩家信息
                    */
                    Environment.Exit(0);
                }
            }
            else
            {
                var res =await  _dialogService.ShowAsync("Tips", "Are you sure you want to quit this game ?", MessageDialogStyle.AffirmativeAndNegative);
                if (res == MessageDialogResult.Affirmative)
                {
                    _mainNavigationService.RequestNavigate(PageKeys.Index);
                }
               
                //导航到主界面
            }
        }

    }
}
