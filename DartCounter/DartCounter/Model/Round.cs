using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Model
{
    public class Round
    {
        private const int NUMBER_OF_DARTS_PER_ROUND = 3;
        private int mNumberOfThrownDarts;

        public Round()
        {
            CreateDarts();
            
            mNumberOfThrownDarts = 0;
            RoundStatus = RoundStatus.Running;
        }

        public RoundStatus RoundStatus { get; private set; }

        public RoundStatus AddDart(Dart dart)
        {
            mNumberOfThrownDarts++;
            Darts[mNumberOfThrownDarts] = dart;
            CheckRoundStatus();
            return RoundStatus;
        }

        public Dart[] Darts { get; set; }

        public int NumberOfThrownDarts => mNumberOfThrownDarts;

        public int GetCurrentPointsMade()
        {
            int retVal = 0; ;
            for(int i=1; i<= mNumberOfThrownDarts; i++)
            {
                retVal = retVal + Darts[i].Points;
            }

            return retVal;
        }

        /// <summary>
        /// Undos the current dart of the round and returns the points made with the current dart
        /// </summary>
        public int Undo()
        {
            int pointsToUndo = 0;
            pointsToUndo = Darts[mNumberOfThrownDarts].Points;
            Darts[mNumberOfThrownDarts] = new Dart();
            mNumberOfThrownDarts--;
            CheckRoundStatus();

            return pointsToUndo;
        }

        private void CheckRoundStatus()
        {
            if(mNumberOfThrownDarts >= NUMBER_OF_DARTS_PER_ROUND)
            {
                RoundStatus = RoundStatus.Completed;
            }
            else
            {
                RoundStatus = RoundStatus.Running;
            }
        }

        private void CreateDarts()
        {
            Darts = new Dart[NUMBER_OF_DARTS_PER_ROUND + 1];
            for (int i = 1; i <= NUMBER_OF_DARTS_PER_ROUND; i++)
            {
                Darts[i] = new Dart();
            }
        }
    }
}
