using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Interfaces;

namespace TexasHoldem.WPF.Services
{
    public class MetroDialogService : IMetroDialogService
    {
        MetroWindow _metroWindow;
        public void Register(MetroWindow metroWindow)
        {
            _metroWindow = metroWindow;
        }

        public async Task<MessageDialogResult> ShowAsync(string title, string message, MessageDialogStyle style=MessageDialogStyle.Affirmative,MetroDialogSettings settings=null)
        {
          return  await _metroWindow.ShowMessageAsync(title, message,style,settings);
        }
    }
}
