using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Constants;

namespace TexasHoldem.WPF.ViewModels
{
   public partial class ShellWindowViewModel:ObservableObject
    {
        IRegionNavigationService _mainNavigationService;
        IRegionNavigationService _splitNavigationService;
        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _mainNavigationService = regionManager.Regions[Regions.Main].NavigationService;
            _splitNavigationService = regionManager.Regions[Regions.Split].NavigationService;
        }

        [RelayCommand]
        void Loaded()
        {
            _mainNavigationService.RequestNavigate(PageKeys.Index);
        }


    }
}
