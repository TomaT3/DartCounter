using DartCounter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartCounter.Services
{
    public interface ISettingsService
    {
        void SetNumberOfPlayers(int numerOfPlayers);

        void SetStartCount(int startCount);

        void SetPlayMode(PlayMode playMode);

        int NumberOfPlayers { get; }

        int StartCount { get; }

        PlayMode PlayMode { get; }
    }
}
