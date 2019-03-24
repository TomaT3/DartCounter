using DartCounter.Model;
using DartCounter.Services;
using DartCounter.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DartCounter.ViewModel
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly INavigation mNavigation;
        private int mStartNumberSelectedIndex;
        private int mNumberOfPlayers;
        private int mNumberOfPlayersSelectedIndex;
        private int mStartNumber;
        private PlayMode mSelectedPlayMode;
        private readonly ISettingsService mSettingsService;

        public SettingsViewModel(INavigation navigation, ISettingsService settingsService)
        {
            mSettingsService = settingsService;
            mNavigation = navigation;
            mNumberOfPlayers = 1;
            PossibleNumberOfPlayers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            mStartNumber = 301;
            mSelectedPlayMode = PlayMode.DoubleOut;
            PossibleStartNumbers = new List<int> { 301, 501 };
            PlayModes = Enum.GetNames(typeof(PlayMode)).ToList();
            StartButtonEnabled = true;
            StartGameButtonCommand = new Command(async () => await StartGameAsync());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public List<int> PossibleStartNumbers { get; private set; }

        public int NumberOfPlayers {
            get => mNumberOfPlayers;
            set
            {
                if (mNumberOfPlayers != value)
                {
                    mNumberOfPlayers = value;
                    OnPropertyChanged(nameof(NumberOfPlayers));
                }
            }
        }

        public List<int> PossibleNumberOfPlayers { get; private set; }

        public int NumberOfPlayersSelectedIndex
        {
            get
            {
                return mNumberOfPlayersSelectedIndex;
            }
            set
            {
                if (mNumberOfPlayersSelectedIndex != value)
                {
                    mNumberOfPlayersSelectedIndex = value;
                    OnPropertyChanged(nameof(NumberOfPlayersSelectedIndex));
                    NumberOfPlayers = PossibleNumberOfPlayers[mNumberOfPlayersSelectedIndex];
                }
            }
        }

        public int StartNumber {
            get => mStartNumber;
            set
            {
                if (mStartNumber != value)
                {
                    mStartNumber = value;
                    OnPropertyChanged(nameof(StartNumber));
                }
            }
        }

        public int StartNumberSelectedIndex
        {
            get
            {
                return mStartNumberSelectedIndex;
            }
            set
            {
                if (mStartNumberSelectedIndex != value)
                {
                    mStartNumberSelectedIndex = value;
                    OnPropertyChanged(nameof(StartNumberSelectedIndex));
                    StartNumber = PossibleStartNumbers[mStartNumberSelectedIndex];
                }
            }
        }

        public List<String> PlayModes { get; private set; }

        public PlayMode SelectedPlayMode
        {
            get => mSelectedPlayMode;
            set
            {
                if (mSelectedPlayMode != value)
                {
                    mSelectedPlayMode = value;
                    OnPropertyChanged(nameof(SelectedPlayMode));
                }
            }
        }

        public bool StartButtonEnabled { get; private set; }

        public ICommand StartGameButtonCommand { get; private set; }

        private async Task StartGameAsync()
        {
            StartButtonEnabled = false;
            mSettingsService.SetStartCount(StartNumber);
            mSettingsService.SetNumberOfPlayers(NumberOfPlayers);
            mSettingsService.SetPlayMode(SelectedPlayMode);
            await mNavigation.PushAsync(new GameView());
            StartButtonEnabled = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
