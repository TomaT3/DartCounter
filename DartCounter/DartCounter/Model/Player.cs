using System.Collections.Generic;
using System.Linq;

namespace DartCounter.Model
{
    public class Player
    {
        private int mCurrentRound;
        private int mCurrentPoints;
        public Player(int startCount)
        {
            mCurrentPoints = startCount;
            Rounds = new Dictionary<int, Round>();
            mCurrentRound = 0;
            AddRound();
        }

        public string PlayerName { get; set; }
        
        public Round CurrentRound
        {
            get
            {
                return Rounds[mCurrentRound];
            }
        }
        public Dictionary<int, Round> Rounds { get; set; }

        public int GetCurrentPoints()
        {
            return mCurrentPoints;
        }

        public RoundStatus AddPoints(Dart dart)
        {
            if (CurrentRound.RoundStatus == RoundStatus.Completed)
            {
                AddRound();
            }

            var roundStatus = CurrentRound.AddDart(dart);
            mCurrentPoints = mCurrentPoints - dart.Points;

            return roundStatus;
        }

        public void InvalidateCurrentRound()
        {
            var pointsInCurrentround = CurrentRound.GetCurrentPointsMade();
            mCurrentPoints = mCurrentPoints + pointsInCurrentround;
            AddRound();
        }

        /// <summary>
        /// Undo points of last dart from player
        /// </summary>
        /// <returns>true if undo is possible for current round, otherwise false</returns>
        public void Undo()
        {
            int pointsToUndo = CurrentRound.Undo();
            mCurrentPoints = mCurrentPoints + pointsToUndo;
            DecreaseCurrentRoundIfPossible();
        }

        private void DecreaseCurrentRoundIfPossible()
        {
            if (mCurrentRound > 1)
            {
                mCurrentRound--;
            }
        }

        private void AddRound()
        {
            mCurrentRound++;
            bool roundPresent = Rounds.TryGetValue(mCurrentRound, out Round round);
            if (!roundPresent)
            {
                Rounds.Add(mCurrentRound, new Round());
            }
        }
    }
}
