namespace CheckersGameLogic
{
    public class Board
    {
        private Spot[,] m_CheckersBoard;
        private int m_BoardSize;

        public Board(int i_BoardSize, bool i_FirstGame)
        {
            InitializeBoard(i_BoardSize, i_FirstGame);
        }

        public Spot[,] CheckersBoard
        {
            get { return m_CheckersBoard; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public void InitializeBoard(int i_BoardSize, bool i_FirstGame)
        {
            if(i_FirstGame)
            {
                m_CheckersBoard = new Spot[i_BoardSize, i_BoardSize];
                m_BoardSize = i_BoardSize;
                initializePiecesOnBoardOnFirstGame(i_BoardSize);
            }
            else
            {
                initializePiecesOnBoardOnNewRound(i_BoardSize);
            }
        }

        private void initializePiecesOnBoardOnFirstGame(int i_BoardSize)
        {
            int middleBoardLine = (i_BoardSize - 2) / 2;

            for (int i = 0; i < middleBoardLine; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        m_CheckersBoard[i, j] = new Spot(i, j, ePieceType.Empty, eColor.Transparent, false);
                        m_CheckersBoard[i_BoardSize - i - 1, j] = new Spot(i_BoardSize - i - 1, j, ePieceType.Soldier, eColor.White, true);
                    }
                    else
                    {
                        m_CheckersBoard[i, j] = new Spot(i, j, ePieceType.Soldier, eColor.Black, true);
                        m_CheckersBoard[i_BoardSize - i - 1, j] = new Spot(i_BoardSize - i - 1, j, ePieceType.Empty, eColor.Transparent, false);
                    }
                }
            }

            for (int j = 0; j < i_BoardSize; j++)
            {
                if((middleBoardLine + j) % 2 == 0)
                {
                    m_CheckersBoard[middleBoardLine, j] = new Spot(middleBoardLine, j, ePieceType.Empty, eColor.Transparent, false);
                    m_CheckersBoard[middleBoardLine + 1, j] = new Spot(middleBoardLine + 1, j, ePieceType.Empty, eColor.Transparent, true);
                }
                else
                {
                    m_CheckersBoard[middleBoardLine, j] = new Spot(middleBoardLine, j, ePieceType.Empty, eColor.Transparent, true);
                    m_CheckersBoard[middleBoardLine + 1, j] = new Spot(middleBoardLine + 1, j, ePieceType.Empty, eColor.Transparent, false);
                }
            }
        }

        private void initializePiecesOnBoardOnNewRound(int i_BoardSize)
        {
            int middleBoardLine = (i_BoardSize - 2) / 2;

            for (int i = 0; i < middleBoardLine; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        m_CheckersBoard[i, j].SetSpotAfterNewRoundGame(ePieceType.Empty, eColor.Transparent);
                        m_CheckersBoard[i_BoardSize - i - 1, j].SetSpotAfterNewRoundGame(ePieceType.Soldier, eColor.White);
                    }
                    else
                    {
                        m_CheckersBoard[i, j].SetSpotAfterNewRoundGame(ePieceType.Soldier, eColor.Black);
                        m_CheckersBoard[i_BoardSize - i - 1, j].SetSpotAfterNewRoundGame(ePieceType.Empty, eColor.Transparent);
                    }
                }
            }

            for (int j = 0; j < i_BoardSize; j++)
            {
                m_CheckersBoard[middleBoardLine, j].SetSpotAfterNewRoundGame(ePieceType.Empty, eColor.Transparent);
                m_CheckersBoard[middleBoardLine + 1, j].SetSpotAfterNewRoundGame(ePieceType.Empty, eColor.Transparent);
            }
        }

        public Spot GetSpot(int i_Row, int i_Col)
        {
            return m_CheckersBoard[i_Row, i_Col];
        }
        public bool IsSpotOccupiedByOpponent(eColor i_PlayerSpotColor, eColor i_BehindSpotColor)
        {
            bool isSpotNotOccupiedByOpponent = true;

            if ((i_PlayerSpotColor == eColor.White && i_BehindSpotColor == eColor.Black) || 
                (i_PlayerSpotColor == eColor.Black && i_BehindSpotColor == eColor.White))
            {
                isSpotNotOccupiedByOpponent = false;
            }

            return !isSpotNotOccupiedByOpponent;
        }

        public bool IsEmpySpotDestination(Spot i_DestinationSpot)
        {
            return i_DestinationSpot.PieceColor == eColor.Transparent;
        }

        public bool IsInsideTheBoard(int i_ToRow, int i_ToCol)
        {
            bool isInsideTheBoard = true;

            if (!(i_ToRow >= 0 && i_ToRow < m_BoardSize && i_ToCol >= 0 && i_ToCol < m_BoardSize))
            {
                isInsideTheBoard = false;
            }

            return isInsideTheBoard;
        }
    }
}