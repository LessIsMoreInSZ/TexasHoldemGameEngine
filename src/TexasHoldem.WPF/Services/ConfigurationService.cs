using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldem.WPF.Models;

namespace TexasHoldem.WPF.Services
{
    public class ConfigurationService:ObservableObject
    {
        public Configuration MyProperty { get; set; }
        public void SavePlayerConfig(string path)
        {

        }

        //public Player LoadPlayerConfig()
        //{

        //}
    }
}
