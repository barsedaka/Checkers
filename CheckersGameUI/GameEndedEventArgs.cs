using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckersGameLogic;

namespace CheckersGameUI
{

    public delegate void GameEndedEventHanlder(GameEndedEventArgs e);

    public class GameEndedEventArgs : EventArgs
    {
        private readonly eGameStatus r_GameStatus;
        private readonly string r_WinnerName;

        public GameEndedEventArgs(eGameStatus i_GameStatus, string i_WinnerName)
        {
            r_GameStatus = i_GameStatus;
            r_WinnerName = i_WinnerName;
        }

        public eGameStatus GameStatus
        {
            get { return r_GameStatus; }
        }

        public string WinnerName
        {
            get { return r_WinnerName; }
        }
    }
}