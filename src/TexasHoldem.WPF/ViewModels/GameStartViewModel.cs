using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.ViewModels
{
    public class GameStartViewModel : ObservableObject, IDialogAware
    {
        public string Title => "Game";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()=>true;

        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
