using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGameLogic
{
    public class Player
    {
        private readonly string r_PlayerName;
        private readonly ePlayerType r_PlayerType;
        private readonly eColor r_PlayerColor;
        private int m_PlayerScore;
        private List<Spot> m_AllPlayerSpots;
        private List<Move> m_AllPlayerValidMoves;

        public Player(string i_PlayerName, ePlayerType i_PlayerType, eColor i_PlayerColor, int i_BoardSize)
        {
            r_PlayerName = i_PlayerName;
            r_PlayerType = i_PlayerType;
            r_PlayerColor = i_PlayerColor;
            m_PlayerScore = (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
            m_AllPlayerSpots = new List<Spot>();
            m_AllPlayerValidMoves = new List<Move>();
        }

        public List<Spot> SpotArr
        {
            get { return m_AllPlayerSpots; }
        }

        public eColor PlayerColor
        {
            get { return r_PlayerColor; }
        }

        public int PlayerScore
        {
            get { return m_PlayerScore; }
            set { m_PlayerScore = value; }
        }

        public ePlayerType PlayerType
        {
            get { return r_PlayerType; }
        }

        public List<Move> ValidMoves
        {
            get { return m_AllPlayerValidMoves; }
        }

        public string Name
        {
            get { return r_PlayerName; }
        }

        internal int NewGamePlayerScore(int i_BoardSize)
        {
            return (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
        }
    }
}