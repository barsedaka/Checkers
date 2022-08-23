using System;

namespace CheckersGameLogic
{
    public class Spot
    {
        public event Action<Spot> SpotChanged;

        private int m_Row;
        private int m_Col;
        private ePieceType m_pieceType;
        private eColor m_PieceColor;
        private bool m_IsActiveSpot;

        public Spot(int i_Row, int i_Col, ePieceType i_PieceType, eColor i_PieceColor, bool i_IsActiveSpot)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            m_pieceType = i_PieceType;
            m_PieceColor = i_PieceColor;
            m_IsActiveSpot = i_IsActiveSpot;
        }

        public bool IsActiveSpot
        {
            get { return m_IsActiveSpot; }
        }

        internal void DoWhenSpotChanged()
        {
            OnSpotChanged();
        }

        protected virtual void OnSpotChanged()
        {
            if (SpotChanged != null)
            {
                SpotChanged.Invoke(this);
            }
        }

        public eColor PieceColor
        {
            get { return m_PieceColor; }
            set { m_PieceColor = value; }
        }

        public int Col
        {
            get { return m_Col; }
        }

        public int Row
        {
            get { return m_Row; }
        }

        public ePieceType Type
        {
            get { return m_pieceType; }
            set { m_pieceType = value; }
        }

        internal void SetSpotAfterNewRoundGame(ePieceType i_PieceType, eColor i_PieceColor)
        {
            m_pieceType = i_PieceType;
            m_PieceColor = i_PieceColor;
            DoWhenSpotChanged();
        }
    }
}