using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;

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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            GetInfoCommand = new RelayCommand(() =>
            {
                MessageBox.Show("Test");
            });

            if (IsInDesignMode)
            {
                WindowTitle = "Hello MVVM Light (Design Mode)";
            }
            else
            {
                WindowTitle = "Hello MVVM Light";
            }
        }

        public string WindowTitle { get; set; }

        public ICommand GetInfoCommand { get; set; }
    }
}