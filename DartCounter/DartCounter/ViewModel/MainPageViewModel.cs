using DartCounter.View;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DartCounter.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private INavigation mNavigation;
        private bool mButtonEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonSwitchToSettingsViewCommand { get; private set; }

        public MainPageViewModel(INavigation navigation)
        {
            mNavigation = navigation;
            ButtonEnabled = true;
            ButtonSwitchToSettingsViewCommand = new Command(async () => await ButtonSwitchToSettingsViewCommandClickedHandler());
        }

        public bool ButtonEnabled {
            get => mButtonEnabled;
            set {
                mButtonEnabled = value;
                OnPropertyChanged(nameof(ButtonEnabled));
            }
        }

        private async Task ButtonSwitchToSettingsViewCommandClickedHandler()
        {
            ButtonEnabled = false;
            await mNavigation.PushAsync(new SettingsView());
            ButtonEnabled = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
