using DartCounter.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartCounter.Services
{
    public class SettingsService : ISettingsService
    {
        public int NumberOfPlayers { get; private set; }
        public int StartCount { get; private set; }
        public PlayMode PlayMode { get; private set; }

        public void SetNumberOfPlayers(int numerOfPlayers)
        {
            NumberOfPlayers = numerOfPlayers;
        }

        public void SetStartCount(int startCount)
        {
            StartCount = startCount;
        }

        public void SetPlayMode(PlayMode playMode)
        {
            PlayMode = playMode;
        }
    }
}
