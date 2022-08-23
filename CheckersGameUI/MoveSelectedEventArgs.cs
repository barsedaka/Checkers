using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CheckersGameUI
{
    public delegate void MoveEventHanlder(MoveSelectedEventArgs e);

    public class MoveSelectedEventArgs : EventArgs
    {
        private SpotButton m_FromSpotSelected;
        private SpotButton m_ToSpotSelected;

        public MoveSelectedEventArgs(SpotButton i_FromSpotButtonSelected, SpotButton i_ToSpotButtonSelected)
        {
            m_FromSpotSelected = i_FromSpotButtonSelected;
            m_ToSpotSelected = i_ToSpotButtonSelected;
        }

        public SpotButton FromSpotSelected
        {
            get { return m_FromSpotSelected; }
        }

        public SpotButton ToSpotSelected
        {
            get { return m_ToSpotSelected; }
        }
    }
}