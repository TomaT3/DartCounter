using DartCounter.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartCounter.Model
{
    public class Game : IGame
    {
        private Player[] mPlayers;
        private ISettingsService mSettings;
        private int mCurrentPlayerNumber;
        private Stack<int> mStack;

        public Game(ISettingsService settings)
        {
            mSettings = settings;
            mCurrentPlayerNumber = 0;
            mStack = new Stack<int>();
        }

        public event Action<Player> CurrentPlayerChanged;

        public Player CurrentPlayer { get; private set; }

        public DartStatus AddPointsToCurrentPlayer(Multiplikator multiplikator, int field, out Player nextPlayer)
        {
            var currentPoints = CurrentPlayer.GetCurrentPoints();
            Dart dart = new Dart(multiplikator, field);
            var thrownPoints = dart.Points;

            var dartStatus = CheckThrownPoints(currentPoints, dart);

            switch (dartStatus)
            {
                case DartStatus.Ok:
                    var roundStatus = AddPointsToCurrentPlayer(dart);
                    if (roundStatus == RoundStatus.Completed)
                    {
                        SetNextPlayer();
                    }
                    break;
                case DartStatus.OverThrown:
                case DartStatus.NoValidCheckOut:
                case DartStatus.NoCheckOutPossible:
                    CurrentPlayer.InvalidateCurrentRound();
                    SetNextPlayer();
                    break;
                case DartStatus.CheckedOut:
                    AddPointsToCurrentPlayer(dart);
                    break;
            }

            nextPlayer = CurrentPlayer;
            return dartStatus;
        }

        public void Undo()
        {
            if (mStack.Count > 0)
            {
                var playerNumber = mStack.Pop();
                SetCurrentPlayer(playerNumber);
                CurrentPlayer.Undo();
            }
        }


        private RoundStatus AddPointsToCurrentPlayer(Dart dart)
        {
            var retVal = CurrentPlayer.AddPoints(dart);
            mStack.Push(mCurrentPlayerNumber);
            return retVal;
        }

        private DartStatus CheckThrownPoints(int currentPoints, Dart thrownDart)
        {
            var thrownPoints = thrownDart.Points;
            var result = currentPoints - thrownPoints;

            if (result > 0)
            {
                if (result == 1 && mSettings.PlayMode == PlayMode.DoubleOut)
                    return DartStatus.NoCheckOutPossible;
                else
                    return DartStatus.Ok;
            }

            if (result < 0)
                return DartStatus.OverThrown;

            if (result == 0 &&
                ((mSettings.PlayMode == PlayMode.DoubleOut && thrownDart.Multiplikator == Multiplikator.Double) ||
                (mSettings.PlayMode == PlayMode.SingleOut)))
                return DartStatus.CheckedOut;
            else
                return DartStatus.NoValidCheckOut;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <returns>The first player in Game.</returns>
        public Player StartGame()
        {
            CreatePlayers();
            SetCurrentPlayer(mCurrentPlayerNumber);
            return CurrentPlayer;
        }

        private void CreatePlayers()
        {
            mPlayers = new Player[mSettings.NumberOfPlayers];
            for (int i = 0; i < mSettings.NumberOfPlayers; i++)
            {
                mPlayers[i] = new Player(mSettings.StartCount) { PlayerName = $"Player{i}" };
            }
        }

        private void SetNextPlayer()
        {
            mCurrentPlayerNumber++;
            if (mCurrentPlayerNumber >= mSettings.NumberOfPlayers)
            {
                mCurrentPlayerNumber = 0;
            }

            SetCurrentPlayer(mCurrentPlayerNumber);
        }

        //private void SetPreviousPlayer()
        //{
        //    mCurrentPlayerNumber--;
        //    if (mCurrentPlayerNumber < 0)
        //    {
        //        mCurrentPlayerNumber = mSettings.NumberOfPlayers - 1;
        //    }

        //    SetCurrentPlayer(mCurrentPlayerNumber);
        //}

        private void SetCurrentPlayer(int currentPlayer)
        {
            CurrentPlayer = mPlayers[currentPlayer];
            //ThreadStart asd = new ThreadStart(OnCurrentPlayerChanged);
            Task task = new Task(OnCurrentPlayerChanged);
            task.Start();
        }

        private void OnCurrentPlayerChanged()
        {
            CurrentPlayerChanged?.Invoke(CurrentPlayer);
        }

        
    }
}