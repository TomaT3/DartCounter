using DartCounter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Services
{
    public interface IGame
    {
        event Action<Player> CurrentPlayerChanged;
        
        /// <summary>
        /// Starts the Game
        /// </summary>
        /// <returns>The first player of the game</returns>
        Player StartGame();

        DartStatus AddPointsToCurrentPlayer(Multiplikator multiplikator, int field, out Player nextPlayer);

        void Undo();
    }
}
