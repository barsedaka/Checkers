using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGameLogic
{
    public class Move
    {
        private Spot m_FromSpot;
        private Spot m_ToSpot;
        private bool m_EatMove;

        public Move(Spot i_FromSpot, Spot i_ToSpot, bool i_IsEatMove)
        {
            m_FromSpot = i_FromSpot;
            m_ToSpot = i_ToSpot;
            m_EatMove = i_IsEatMove;
        }

        public bool IsEatMove
        {
            get { return m_EatMove; }
        }

        public Spot FromSpot
        {
            get { return m_FromSpot; }
        }

        public Spot ToSpot
        {
            get { return m_ToSpot; }
        }
    }
}