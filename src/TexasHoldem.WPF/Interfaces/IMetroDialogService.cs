using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldem.WPF.Interfaces
{
    public interface IMetroDialogService
    {
        public void Register(MetroWindow metroWindow);
        public Task<MessageDialogResult> ShowAsync(string title, string message, MessageDialogStyle style=MessageDialogStyle.Affirmative, MetroDialogSettings settings=null);
    }
}
