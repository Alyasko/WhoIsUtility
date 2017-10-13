using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WhoIsUtility.ViewModels
{
    class MainWindowVm : INotifyPropertyChanged
    {
        public MainWindowVm()
        {
            WindowTitle = "WHOIS Utility";
        }

        public string WindowTitle { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
