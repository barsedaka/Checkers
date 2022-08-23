using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersGameLogic
{
    public class CheckersGame
    {
        public event Action<eMoveType> MoveSelectedError;
        private bool m_FirstGame;
        private Player m_CurrentPlayer;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;
        private readonly Board r_Board;
        private bool m_GameEnded;
        private readonly Random r_Random;
        private eGameStatus m_GameStatus = eGameStatus.Active;

        public CheckersGame(string i_PlayerOneName, string i_PlayerTwoName, ePlayerType i_PlayerTwoType, int i_BoardSize)
        {
            m_FirstGame = true;
            r_PlayerOne = new Player(i_PlayerOneName, ePlayerType.HumanPlayer, eColor.White, i_BoardSize);
            m_CurrentPlayer = r_PlayerOne;
            r_PlayerTwo = new Player(i_PlayerTwoName, i_PlayerTwoType, eColor.Black, i_BoardSize);
            r_Board = new Board(i_BoardSize, m_FirstGame);
            r_Random = new Random();
            m_GameEnded = false;
            InitializeNewGame();
        }

        public bool IsFirstGame
        {
            set { m_FirstGame = value; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }

        public Player PlayerOne
        {
            get { return r_PlayerOne; }
        }

        public Player PlayerTwo
        {
            get { return r_PlayerTwo; }
        }

        public Board CheckersBoard
        {
            get { return r_Board; }
        }

        public bool GameEnded
        {
            get { return m_GameEnded; }
        }
       
        public eGameStatus GameStatus
        {
            get { return m_GameStatus; }
            set { m_GameStatus = value; }
        }

        public void InitializeNewGame()
        {
            initializePlayerSpots();
            initializeValidMovesArr();
        }

        public void InitializeNewRoundGame()
        {
            m_GameStatus = eGameStatus.Active;
            m_GameEnded = false;
            r_PlayerOne.PlayerScore = r_PlayerOne.NewGamePlayerScore(r_Board.BoardSize);
            r_PlayerTwo.PlayerScore = r_PlayerTwo.NewGamePlayerScore(r_Board.BoardSize);
            r_Board.InitializeBoard(r_Board.BoardSize, m_FirstGame);
            InitializeNewGame();
        }

        private void initializePlayerSpots()
        {
            int middleBoardLine = (r_Board.BoardSize - 2) / 2;

            r_PlayerOne.SpotArr.Clear();
            r_PlayerTwo.SpotArr.Clear();
            for (int i = 0; i < middleBoardLine; i++)
            {
                for (int j = 0; j < r_Board.BoardSize; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        r_PlayerOne.SpotArr.Add(r_Board.GetSpot(r_Board.BoardSize - i - 1, j));
                    }
                    else
                    {
                        r_PlayerTwo.SpotArr.Add(r_Board.GetSpot(i, j));
                    }
                }
            }
        }

        private void initializeValidMovesArr()
        {
            r_PlayerOne.ValidMoves.Clear();
            r_PlayerTwo.ValidMoves.Clear();
            isPlayerCanMove(r_PlayerOne.PlayerColor, r_PlayerOne.SpotArr.Count, r_PlayerOne);
            isPlayerCanMove(r_PlayerTwo.PlayerColor, r_PlayerTwo.SpotArr.Count, r_PlayerTwo);
        }

        public void MakeComputerMove(ref bool io_IsNoNeedToEatAgain, out eMoveType o_MoveType)
        {
            Move moveChosen;
            bool isValidMove;
            bool isCurrentPlayerHaveEatMoves = isPlayerHaveEatMoves(r_PlayerTwo.ValidMoves);

            do
            {
                moveChosen = r_PlayerTwo.ValidMoves[r_Random.Next(r_PlayerTwo.ValidMoves.Count)];
                isValidMove = (isCurrentPlayerHaveEatMoves && moveChosen.IsEatMove) || (!isCurrentPlayerHaveEatMoves);
            }
            while (!isValidMove);

            updateAfterMove(out o_MoveType, moveChosen, ref io_IsNoNeedToEatAgain);
        }

        public eMoveType CheckIfMoveIsValidAndUpdateBoard(Spot i_FromSpot, Spot i_ToSpot, ref bool io_IsNoNeedToEatAgain)
        {
            bool isValidMove = false;
            eMoveType moveType;
            Move newMove = new Move(i_FromSpot, i_ToSpot, isValidEatMove(i_FromSpot, i_ToSpot));

            if (i_FromSpot.PieceColor != m_CurrentPlayer.PlayerColor)
            {
                moveType = eMoveType.IlegalNotSameColor;
            }
            else if (r_Board.IsEmpySpotDestination(i_ToSpot) == false)
            {
                moveType = eMoveType.IlegalNotEmptyDestination;
            }
            else if (isCurrentMoveOnList(newMove, m_CurrentPlayer.ValidMoves) == false)
            {
                moveType = eMoveType.Ilegal;
            }
            else
            {
                if (isPlayerHaveEatMoves(m_CurrentPlayer.ValidMoves) && !newMove.IsEatMove)
                {
                    moveType = eMoveType.IlegalNeedToEat;
                }
                else
                {
                    isValidMove = true;
                    updateAfterMove(out moveType, newMove, ref io_IsNoNeedToEatAgain);
                }
            }

            if (!isValidMove)
            {
                OnMoveSelectedError(moveType);
            }

            return moveType;
        }

        private bool isCurrentMoveOnList(Move i_NewMove, List<Move> i_CurrPlayerValidMoves)
        {
            bool isMoveNotOnList = true;

            foreach (Move move in i_CurrPlayerValidMoves)
            {
                if (isMovesEqual(move, i_NewMove))
                {
                    isMoveNotOnList = false;
                    break;
                }
            }

            return !isMoveNotOnList;
        }

        private bool isMovesEqual(Move i_FirstMove, Move i_SecondMove)
        {
            return i_FirstMove.ToSpot == i_SecondMove.ToSpot && i_FirstMove.FromSpot == i_SecondMove.FromSpot
                && i_FirstMove.IsEatMove == i_SecondMove.IsEatMove;
        }

        private bool isPlayerHaveEatMoves(List<Move> i_CurrPlayerValidMoves)
        {
            bool isPlayerNotHaveEatMoves = true;

            foreach (Move move in i_CurrPlayerValidMoves)
            {
                if (move.IsEatMove)
                {
                    isPlayerNotHaveEatMoves = false;
                    break;
                }
            }

            return !isPlayerNotHaveEatMoves;
        }

        private void updateAfterMove(out eMoveType o_CurrentMoveType, Move i_MoveChosen, ref bool io_IsNoNeedToEatAgain)
        {
            bool isBecameKing = checkedIfBecameKingAndUpdate(i_MoveChosen.FromSpot, i_MoveChosen.ToSpot);

            o_CurrentMoveType = i_MoveChosen.IsEatMove ? eMoveType.Eat : eMoveType.Regular;
            updateScore(m_CurrentPlayer.PlayerColor, o_CurrentMoveType, i_MoveChosen.FromSpot, i_MoveChosen.ToSpot, isBecameKing);
            updateMoveOnBoardAndGameStatus(i_MoveChosen.FromSpot, i_MoveChosen.ToSpot, o_CurrentMoveType);

            if (m_GameStatus == eGameStatus.Active)
            {
                updateValidMovesArr();
                io_IsNoNeedToEatAgain = !(i_MoveChosen.IsEatMove && isSpotNeedToEat(i_MoveChosen.ToSpot, m_CurrentPlayer));
                if (!io_IsNoNeedToEatAgain)
                {
                    updatePlayerListToEatMovesFromSpot(m_CurrentPlayer.ValidMoves, i_MoveChosen.ToSpot);
                    o_CurrentMoveType = eMoveType.NeedToEatAgain;
                }
            }
        }

        private bool checkedIfBecameKingAndUpdate(Spot i_OldSpot, Spot i_NewSpot)
        {
            bool isWhiteNotBecameKing = true;
            bool isBlackNotBecameKing = true;

            if (i_OldSpot.Type == ePieceType.Soldier)
            {
                isWhiteNotBecameKing = !(i_NewSpot.Row == 0 && i_OldSpot.PieceColor == eColor.White);
                isBlackNotBecameKing = !(i_NewSpot.Row == r_Board.BoardSize - 1 && i_OldSpot.PieceColor == eColor.Black);
                if (!isWhiteNotBecameKing || !isBlackNotBecameKing)
                {
                    i_OldSpot.Type = ePieceType.King;
                }
            }

            return !isWhiteNotBecameKing || !isBlackNotBecameKing;
        }

        private void updateScore(eColor i_PlayerColor, eMoveType i_MoveType, Spot i_FromSpot, Spot i_ToSpot, bool i_IsBecameKing)
        {
            if (i_MoveType == eMoveType.Eat)
            {
                updateScoreAfterEat(i_PlayerColor, i_FromSpot, i_ToSpot);
            }

            if (i_IsBecameKing)
            {
                updateScoreAfterBecameKing(i_PlayerColor);
            }
        }

        private void updateScoreAfterBecameKing(eColor i_PlayerColor)
        {
            if (i_PlayerColor == eColor.White)
            {
                r_PlayerOne.PlayerScore += 3;
            }
            else
            {
                r_PlayerTwo.PlayerScore += 3;
            }
        }

        private void updateScoreAfterEat(eColor i_PlayerColor, Spot i_FromSpot, Spot i_ToSpot)
        {
            Spot middleSpot = r_Board.GetSpot((i_FromSpot.Row + i_ToSpot.Row) / 2, (i_FromSpot.Col + i_ToSpot.Col) / 2);

            if (i_PlayerColor == eColor.White)
            {
                if (middleSpot.Type == ePieceType.Soldier)
                {
                    r_PlayerTwo.PlayerScore -= 1;
                }
                else
                {
                    r_PlayerTwo.PlayerScore -= 4;
                }
            }
            else
            {
                if (middleSpot.Type == ePieceType.Soldier)
                {
                    r_PlayerOne.PlayerScore -= 1;
                }
                else
                {
                    r_PlayerOne.PlayerScore -= 4;
                }
            }
        }

        private void updateMoveOnBoardAndGameStatus(Spot i_FromSpot, Spot i_ToSpot, eMoveType i_MoveType)
        {
            i_ToSpot.PieceColor = i_FromSpot.PieceColor;
            i_ToSpot.Type = i_FromSpot.Type;
            updatePlayerSpotArr(i_FromSpot, i_ToSpot, i_MoveType);
            i_FromSpot.PieceColor = eColor.Transparent;
            i_FromSpot.Type = ePieceType.Empty;
            i_FromSpot.DoWhenSpotChanged();
            i_ToSpot.DoWhenSpotChanged();
            updateGameStatusAfterMove();
        }

        private void updatePlayerSpotArr(Spot i_FromSpot, Spot i_ToSpot, eMoveType i_MoveType)
        {
            if (i_MoveType == eMoveType.Eat)
            {
                updateOpponentPlayersSpotArrAfterEat(i_FromSpot, i_ToSpot);
            }

            if (m_CurrentPlayer == r_PlayerOne)
            {
                r_PlayerOne.SpotArr.Remove(i_FromSpot);
                r_PlayerOne.SpotArr.Add(i_ToSpot);
            }
            else
            {
                r_PlayerTwo.SpotArr.Remove(i_FromSpot);
                r_PlayerTwo.SpotArr.Add(i_ToSpot);
            }
        }

        private void updateGameStatusAfterMove()
        {
            checkTie();
            checkGameWin();
            checkIfGameEnded();
        }

        private void checkTie()
        {
            if (r_PlayerOne.ValidMoves.Count == 0 && r_PlayerTwo.ValidMoves.Count == 0)
            {
                m_GameStatus = eGameStatus.Tie;
            }
        }

        private void checkGameWin()
        {
            if (r_PlayerOne.SpotArr.Count == 0 || r_PlayerOne.ValidMoves.Count == 0)
            {
                m_GameStatus = eGameStatus.BlackWin;
            }
            else if (r_PlayerTwo.SpotArr.Count == 0 || r_PlayerTwo.ValidMoves.Count == 0)
            {
                m_GameStatus = eGameStatus.WhiteWin;
            }
        }

        private void checkIfGameEnded()
        {
            m_GameEnded = m_GameStatus == eGameStatus.BlackWin ||
                          m_GameStatus == eGameStatus.WhiteWin ||
                          m_GameStatus == eGameStatus.Tie;
        }

        private void updateValidMovesArr()
        {
            r_PlayerOne.ValidMoves.Clear();
            r_PlayerTwo.ValidMoves.Clear();
            initializeValidMovesArr();
        }

        private void updatePlayerListToEatMovesFromSpot(List<Move> io_CurrPlayerValidMoves, Spot i_ToSpot)
        {
            for (int i = 0; i < io_CurrPlayerValidMoves.Count; i++)
            {
                if (!io_CurrPlayerValidMoves[i].IsEatMove || !checkIfTwoSpotsEqual(io_CurrPlayerValidMoves[i].FromSpot, i_ToSpot))
                {
                    io_CurrPlayerValidMoves.Remove(io_CurrPlayerValidMoves[i]);
                }
            }
        }

        protected virtual void OnMoveSelectedError(eMoveType i_MoveNotLegalType)
        {
            if (MoveSelectedError != null)
            {
                MoveSelectedError.Invoke(i_MoveNotLegalType);
            }
        }
        
        private bool checkIfTwoSpotsEqual(Spot i_FirstSpot, Spot i_SecondSpot)
        {
            return i_FirstSpot.Row == i_SecondSpot.Row && i_FirstSpot.Col == i_SecondSpot.Col && 
                   i_FirstSpot.PieceColor == i_SecondSpot.PieceColor && i_FirstSpot.Type == i_SecondSpot.Type;
        }

        private void updateOpponentPlayersSpotArrAfterEat(Spot i_FromSpot, Spot i_ToSpot)
        {
            Spot middleSpot = r_Board.GetSpot((i_FromSpot.Row + i_ToSpot.Row) / 2, (i_FromSpot.Col + i_ToSpot.Col) / 2);

            if(m_CurrentPlayer == r_PlayerOne)
            {
                r_PlayerTwo.SpotArr.Remove(middleSpot);
            }
            else
            {
                r_PlayerOne.SpotArr.Remove(middleSpot);
            }

            middleSpot.Type = ePieceType.Empty;
            middleSpot.PieceColor = eColor.Transparent;
            middleSpot.DoWhenSpotChanged();
        }

        private void isPlayerCanMove(eColor i_PlayerColor, int i_ArrSpotSize, Player i_Player)
        {
            bool isCanMoveRegular = canMoveRegular(i_PlayerColor, i_ArrSpotSize, i_Player);
            bool isCanPlayerNeedToEat = isPlayerNeedToEat(i_PlayerColor, i_ArrSpotSize, i_Player);
        }

        private bool isSoldierCanMoveRegular(Spot i_Spot, Player i_Player)
        {
            bool isSoldierCanNotMoveRegular;

            if (i_Spot.PieceColor == eColor.White)
            {
                isSoldierCanNotMoveRegular = !canMoveUp(i_Spot, i_Player);
            }
            else
            {
                isSoldierCanNotMoveRegular = !canMoveDown(i_Spot, i_Player);
            }

            return !isSoldierCanNotMoveRegular;
        }

        private bool isKingCanMoveRegular(Spot i_Spot, Player i_Player)
        {
            bool isCanMoveUp = canMoveUp(i_Spot, i_Player);
            bool isCanMoveDown = canMoveDown(i_Spot, i_Player);
            return isCanMoveUp || isCanMoveDown;
        }

        private bool isSpotCanMoveRegular(Spot i_Spot, Player i_Player)
        {
            bool isSpotCanNotMoveRegular;

            if (i_Spot.Type == ePieceType.Soldier)
            {
                isSpotCanNotMoveRegular = !isSoldierCanMoveRegular(i_Spot, i_Player);
            }
            else
            {
                isSpotCanNotMoveRegular = !isKingCanMoveRegular(i_Spot, i_Player);
            }

            return !isSpotCanNotMoveRegular;
        }

        private bool canMoveRegular(eColor i_PlayerColor, int i_ArrSpotSize, Player i_Player)
        {
            bool isPlayerCanNotMoveRegular = true;

            for (int i = 0; i < i_ArrSpotSize; i++)
            {
                if (i_PlayerColor == eColor.White)
                {
                    isPlayerCanNotMoveRegular = !isSpotCanMoveRegular(r_PlayerOne.SpotArr[i], i_Player);
                }
                else
                {
                    isPlayerCanNotMoveRegular = !isSpotCanMoveRegular(r_PlayerTwo.SpotArr[i], i_Player);
                }
            }

            return !isPlayerCanNotMoveRegular;
        }

        private bool canMoveDown(Spot i_Spot, Player i_Player)
        {
            bool isCanMoveDownRight = canMoveDownRight(i_Spot, i_Player);
            bool isCanMoveDownLeft = canMoveDownLeft(i_Spot, i_Player);

            return isCanMoveDownRight || isCanMoveDownLeft;
        }

        private bool canMoveDownRight(Spot i_Spot, Player i_Player)
        {
            Spot nextSpot;
            bool isCanNotMoveDownRight = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row + 1, i_Spot.Col + 1))
            {
                nextSpot = r_Board.GetSpot(i_Spot.Row + 1, i_Spot.Col + 1);
                isCanNotMoveDownRight = !r_Board.IsEmpySpotDestination(nextSpot);
                if (!isCanNotMoveDownRight)
                {
                    newMove = new Move(i_Spot, nextSpot, false);
                    i_Player.ValidMoves.Add(newMove);
                }
            }

            return !isCanNotMoveDownRight;
        }

        private bool canMoveDownLeft(Spot i_Spot, Player i_Player)
        {
            Spot nextSpot;
            bool isCanNotMoveDownLeft = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row + 1, i_Spot.Col - 1))
            {
                nextSpot = r_Board.GetSpot(i_Spot.Row + 1, i_Spot.Col - 1);
                isCanNotMoveDownLeft = !r_Board.IsEmpySpotDestination(nextSpot);
                if (!isCanNotMoveDownLeft)
                {
                    newMove = new Move(i_Spot, nextSpot, false);
                    i_Player.ValidMoves.Add(newMove);
                }
            }

            return !isCanNotMoveDownLeft;
        }

        private bool canMoveUp(Spot i_Spot, Player i_Player)
        {
            bool isCanMoveUpRight = canMoveUpRight(i_Spot, i_Player);
            bool isCanMoveUpLeft = canMoveUpLeft(i_Spot, i_Player);

            return isCanMoveUpRight || isCanMoveUpLeft;
        }

        private bool canMoveUpRight(Spot i_Spot, Player i_Player)
        {
            Spot nextSpot;
            bool isCanNotMoveUpRight = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row - 1, i_Spot.Col + 1))
            {
                nextSpot = r_Board.GetSpot(i_Spot.Row - 1, i_Spot.Col + 1);
                isCanNotMoveUpRight = !r_Board.IsEmpySpotDestination(nextSpot);
                if (!isCanNotMoveUpRight)
                {
                    newMove = new Move(i_Spot, nextSpot, false);
                    i_Player.ValidMoves.Add(newMove);
                }
            }

            return !isCanNotMoveUpRight;
        }

        private bool canMoveUpLeft(Spot i_Spot, Player i_Player)
        {
            Spot nextSpot;
            bool isCanNotMoveUpLeft = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row - 1, i_Spot.Col - 1))
            {
                nextSpot = r_Board.GetSpot(i_Spot.Row - 1, i_Spot.Col - 1);
                isCanNotMoveUpLeft = !r_Board.IsEmpySpotDestination(nextSpot);
                if (!isCanNotMoveUpLeft)
                {
                    newMove = new Move(i_Spot, nextSpot, false);
                    i_Player.ValidMoves.Add(newMove);
                }
            }

            return !isCanNotMoveUpLeft;
        }

        private bool isSpotNeedToEat(Spot i_Spot, Player i_Player)
        {
            bool isNoNeedToEat;

            if (i_Spot.Type == ePieceType.Soldier)
            {
                isNoNeedToEat = !isSoldierNeedToEat(i_Spot, i_Player);
            }
            else
            {
                isNoNeedToEat = !isKingNeedToEat(i_Spot, i_Player);
            }

            return !isNoNeedToEat;
        }

        private bool isSoldierNeedToEat(Spot i_Spot, Player i_Player)
        {
            bool isSoldierNoNeedToEat;

            if (i_Spot.PieceColor == eColor.White)
            {
                isSoldierNoNeedToEat = !isNeedToEatUp(i_Spot, i_Player);
            }
            else
            {
                isSoldierNoNeedToEat = !isNeedToEatDown(i_Spot, i_Player);
            }

            return !isSoldierNoNeedToEat;
        }

        private bool isKingNeedToEat(Spot i_Spot, Player i_Player)
        {
            bool isKingNeedToEatUp = isNeedToEatUp(i_Spot, i_Player);
            bool isKingNeedToEatDown = isNeedToEatDown(i_Spot, i_Player);

            return isKingNeedToEatUp || isKingNeedToEatDown;
        }

        private bool isNeedToEatUp(Spot i_Spot, Player i_Player)
        {
            bool isCanEatUpRight = isNeedToEatUpRight(i_Spot, i_Player);
            bool isCanEatUpLeft = isNeedToEatUpLeft(i_Spot, i_Player);

            return isCanEatUpRight || isCanEatUpLeft;
        }

        private bool isNeedToEatUpRight(Spot i_Spot, Player i_Player)
        {
            Spot behindSpot, behindBehindSpot;
            bool isNoNeedToEatUpRight = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row - 1, i_Spot.Col + 1))
            {
                behindSpot = r_Board.GetSpot(i_Spot.Row - 1, i_Spot.Col + 1);
                if (r_Board.IsSpotOccupiedByOpponent(i_Spot.PieceColor, behindSpot.PieceColor) &&
                    r_Board.IsInsideTheBoard(i_Spot.Row - 2, i_Spot.Col + 2))
                {
                    behindBehindSpot = r_Board.GetSpot(i_Spot.Row - 2, i_Spot.Col + 2);
                    if (r_Board.IsEmpySpotDestination(behindBehindSpot))
                    {
                        isNoNeedToEatUpRight = false;
                        if (!isNoNeedToEatUpRight)
                        {
                            newMove = new Move(i_Spot, behindBehindSpot, true);
                            i_Player.ValidMoves.Add(newMove);
                        }
                    }
                }
            }

            return !isNoNeedToEatUpRight;
        }

        private bool isNeedToEatUpLeft(Spot i_Spot, Player i_Player)
        {
            Spot behindSpot, behindBehindSpot;
            bool isNoNeedToEatUpLeft = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row - 1, i_Spot.Col - 1))
            {
                behindSpot = r_Board.GetSpot(i_Spot.Row - 1, i_Spot.Col - 1);
                if (r_Board.IsSpotOccupiedByOpponent(i_Spot.PieceColor, behindSpot.PieceColor) &&
                    r_Board.IsInsideTheBoard(i_Spot.Row - 2, i_Spot.Col - 2))
                {
                    behindBehindSpot = r_Board.GetSpot(i_Spot.Row - 2, i_Spot.Col - 2);
                    if (r_Board.IsEmpySpotDestination(behindBehindSpot))
                    {
                        isNoNeedToEatUpLeft = false;
                        if (!isNoNeedToEatUpLeft)
                        {
                            newMove = new Move(i_Spot, behindBehindSpot, true);
                            i_Player.ValidMoves.Add(newMove);
                        }
                    }
                }
            }

            return !isNoNeedToEatUpLeft;
        }

        private bool isNeedToEatDown(Spot i_Spot, Player i_Player)
        {
            bool canEatDownRight = isNeedToEatDownRight(i_Spot, i_Player);
            bool canEatDownLeft = isNeedToEatDownLeft(i_Spot, i_Player);

            return canEatDownRight || canEatDownLeft;
        }

        private bool isNeedToEatDownRight(Spot i_Spot, Player i_Player)
        {
            Spot behindSpot, behindBehindSpot;
            bool isNoNeedToEatDownRight = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row + 1, i_Spot.Col + 1))
            {
                behindSpot = r_Board.GetSpot(i_Spot.Row + 1, i_Spot.Col + 1);
                if (r_Board.IsSpotOccupiedByOpponent(i_Spot.PieceColor, behindSpot.PieceColor) &&
                    r_Board.IsInsideTheBoard(i_Spot.Row + 2, i_Spot.Col + 2))
                {
                    behindBehindSpot = r_Board.GetSpot(i_Spot.Row + 2, i_Spot.Col + 2);
                    if (r_Board.IsEmpySpotDestination(behindBehindSpot))
                    {
                        isNoNeedToEatDownRight = false;
                        if (!isNoNeedToEatDownRight)
                        {
                            newMove = new Move(i_Spot, behindBehindSpot, true);
                            i_Player.ValidMoves.Add(newMove);
                        }
                    }
                }
            }

            return !isNoNeedToEatDownRight;
        }

        private bool isNeedToEatDownLeft(Spot i_Spot, Player i_Player)
        {
            Spot behindSpot, behindBehindSpot;
            bool isNoNeedToEatDownLeft = true;
            Move newMove;

            if (r_Board.IsInsideTheBoard(i_Spot.Row + 1, i_Spot.Col - 1))
            {
                behindSpot = r_Board.GetSpot(i_Spot.Row + 1, i_Spot.Col - 1);
                if (r_Board.IsSpotOccupiedByOpponent(i_Spot.PieceColor, behindSpot.PieceColor) &&
                    r_Board.IsInsideTheBoard(i_Spot.Row + 2, i_Spot.Col - 2))
                {
                    behindBehindSpot = r_Board.GetSpot(i_Spot.Row + 2, i_Spot.Col - 2);
                    if (r_Board.IsEmpySpotDestination(behindBehindSpot))
                    {
                        isNoNeedToEatDownLeft = false;
                        if (!isNoNeedToEatDownLeft)
                        {
                            newMove = new Move(i_Spot, behindBehindSpot, true);
                            i_Player.ValidMoves.Add(newMove);
                        }
                    }
                }
            }

            return !isNoNeedToEatDownLeft;
        }

        private bool isPlayerNeedToEat(eColor i_PlayerColor, int i_ArrSpotSize, Player i_Player)
        {
            bool isPlayerNotNeedToEat = true;

            for (int i = 0; i < i_ArrSpotSize; i++)
            {
                if (i_PlayerColor == eColor.White)
                {
                    isPlayerNotNeedToEat = !isSpotNeedToEat(r_PlayerOne.SpotArr[i], i_Player);
                }
                else
                {
                    isPlayerNotNeedToEat = !isSpotNeedToEat(r_PlayerTwo.SpotArr[i], i_Player);
                }
            }

            return !isPlayerNotNeedToEat;
        }

        private bool isValidEatMove(Spot i_FromSpot, Spot i_ToSpot)
        {
            Spot middleSpot = r_Board.GetSpot((i_FromSpot.Row + i_ToSpot.Row) / 2, (i_FromSpot.Col + i_ToSpot.Col) / 2);

            return i_FromSpot.PieceColor != middleSpot.PieceColor && middleSpot.PieceColor != eColor.Transparent;
        }
    }
}