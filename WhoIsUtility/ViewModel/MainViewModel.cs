using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using WhoIsUtility.Core;

namespace WhoIsUtility.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IWhoIs _whoIs;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            GetInfoCommand = new RelayCommand(GetInfoHandler);

            if (IsInDesignMode)
            {
                WindowTitle = "WhoIs Utility (Design Mode)";
            }
            else
            {
                WindowTitle = "WhoIs Utility";
                _whoIs = new WhoIs();
            }
        }

        private async void GetInfoHandler()
        {
            ResponseText = await _whoIs.GetHostInfo(HostName);
        }

        public string WindowTitle { get; set; }
        public string HostName { get; set; } = "google.com";

        private string _responseText;

        public string ResponseText
        {
            get { return _responseText; }
            set
            {
                _responseText = value;
                RaisePropertyChanged(nameof(ResponseText));
            }
        }

        public ICommand GetInfoCommand { get; set; }
    }
}