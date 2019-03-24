using DartCounter.Model;
using DartCounter.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DartCounter.ViewModel
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly INavigation mNavigation;
        private readonly IGame mGameModel;
        private Multiplikator mMultiplikator;
        private Player mCurrentPlayer;
        private Dart[] mCurrentDarts;
        

        public GameViewModel(INavigation navigation, IGame gameModel)
        {
            mNavigation = navigation;
            mGameModel = gameModel;
            mGameModel.CurrentPlayerChanged += CurrentPlayerChangedHandler;

            FieldSelectCommand = new Command<object>((field) => SetPoints(field));
            MultiplikatorCommand = new Command<Multiplikator>((multiplikator) => SetMultiplikator(multiplikator));
            UndoCommand = new Command(() => Undo());
            mCurrentPlayer = mGameModel.StartGame();
            mCurrentDarts = mCurrentPlayer.CurrentRound.Darts;
        }

        private void CurrentPlayerChangedHandler(Player currentPlayer)
        {
            Thread.Sleep(2000);
            mCurrentPlayer = currentPlayer;
            mCurrentDarts = mCurrentPlayer.CurrentRound.Darts;
            RefreshCurrentPlayer();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand MultiplikatorCommand { get; private set; }
        public ICommand FieldSelectCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }

        public string CurrentPlayerName
        {
            get => mCurrentPlayer.PlayerName;
        }

        public int CurrentPlayerPoints
        {
            get => mCurrentPlayer.GetCurrentPoints();
        }

        public string CurrentPlayerDart1 => GetPointsOfDart(1);
        public string CurrentPlayerDart2 => GetPointsOfDart(2);
        public string CurrentPlayerDart3 => GetPointsOfDart(3);

        public Multiplikator Multiplikator
        {
            get => mMultiplikator;
            set
            {
                mMultiplikator = value;
                OnPropertyChanged(nameof(Multiplikator));
                OnPropertyChanged(nameof(IsBullSelectable));
            }
        }

        public bool IsBullSelectable => Multiplikator != Multiplikator.Triple;

        private void SetMultiplikator(Multiplikator multiplikator)
        {
            if (Multiplikator == multiplikator)
            {
                Multiplikator = Multiplikator.Single;
            }
            else
            {
                Multiplikator = multiplikator;
            }
        }

        private void Undo()
        {
            mGameModel.Undo();
        }

        private void SetPoints(object field)
        {
            var points = Convert.ToInt32(field);
            mGameModel.AddPointsToCurrentPlayer(Multiplikator, points, out Player nextPlayer);
            Multiplikator = Multiplikator.Single;
            RefreshCurrentPlayer();
        }

        private void RefreshCurrentPlayer()
        {
            OnPropertyChanged(nameof(CurrentPlayerName));
            OnPropertyChanged(nameof(CurrentPlayerPoints));
            OnPropertyChanged(nameof(CurrentPlayerDart1));
            OnPropertyChanged(nameof(CurrentPlayerDart2));
            OnPropertyChanged(nameof(CurrentPlayerDart3));

        }

        private string GetPointsOfDart(int dartNumber)
        {
            int field = mCurrentDarts[dartNumber].Field;
            if (field < 0)
                return "-";

            string prefix = string.Empty;
            switch (mCurrentDarts[dartNumber].Multiplikator)
            {
                case Multiplikator.Double:
                    prefix = "D";
                    break;
                case Multiplikator.Triple:
                    prefix = "T";
                    break;
            }

            return $"{prefix}{field}";
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
