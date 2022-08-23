using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckersGameLogic;
using System.Windows.Forms;

namespace CheckersGameUI
{
    public class GameManager
    {
        public event GameEndedEventHanlder GameEnded;
        private readonly GameSettingForm r_GameSettingForm = new GameSettingForm();
        private CheckersGame m_CheckersGameLogic;
        private CheckersGameForm m_CheckersGameForm;
        private Player m_CurrentPlayer;
        private bool m_IsNoNeedToEatAgain = true;
        private Timer m_CompuerTurnTimer;
        private bool m_ExitFlag = false;
        internal void Run()
        {
            r_GameSettingForm.ShowDialog();

            if (r_GameSettingForm.DialogResult == DialogResult.OK)
            {
                initializeCheckersGame();
                m_CheckersGameForm.ShowDialog();
            }
        }

        private void initializeCheckersGame()
        {
            initializeCheckersGameLogic();
            initializeCheckersGameForm();
            registerToEvents();
            m_CurrentPlayer = m_CheckersGameLogic.PlayerOne;

            if (!r_GameSettingForm.PlayerTwoIsHuman)
            {
                m_CompuerTurnTimer = new Timer();
                m_CompuerTurnTimer.Tick += ComputerTurnTimer_Tick;
                m_CompuerTurnTimer.Interval = 1800;
            }
        }

        private void initializeCheckersGameLogic()
        {
            ePlayerType opponentPlayerType = r_GameSettingForm.PlayerTwoIsHuman ?
                                                                      ePlayerType.HumanPlayer :
                                                                      ePlayerType.ComputerPlayer;
            m_CheckersGameLogic = new CheckersGame(r_GameSettingForm.PlayerOneName,
                                                   r_GameSettingForm.PlayerTwoName,
                                                   opponentPlayerType,
                                                   r_GameSettingForm.BoardSize);
        }

        private void initializeCheckersGameForm()
        {
            m_CheckersGameForm = new CheckersGameForm(m_CheckersGameLogic.CheckersBoard);
            updateScore();
        }

        private void registerToEvents()
        {
            m_CheckersGameForm.MoveSelected += GameForm_MoveSelected;
            m_CheckersGameLogic.MoveSelectedError += m_CheckersGameForm.GameLogic_MoveSelectedError;
            GameEnded += m_CheckersGameForm.CheckersGameLogic_GameEnded;
            m_CheckersGameForm.AnotherRoundSelected += GameForm_AnotherRoundSelected;
        }

        private void ComputerTurnTimer_Tick(Object sender, EventArgs e)
        {
            m_ExitFlag = true;
            m_CompuerTurnTimer.Stop();
            moveComputerPlayer();
            updateScore();
        }

        public void GameForm_MoveSelected(MoveSelectedEventArgs e)
        {
            CheckersGameLogic.eMoveType moveType;
            Spot fromSpot = m_CheckersGameLogic.CheckersBoard.GetSpot(e.FromSpotSelected.Row, e.FromSpotSelected.Col);
            Spot toSpot = m_CheckersGameLogic.CheckersBoard.GetSpot(e.ToSpotSelected.Row, e.ToSpotSelected.Col);

            moveType = m_CheckersGameLogic.CheckIfMoveIsValidAndUpdateBoard(fromSpot, toSpot, ref m_IsNoNeedToEatAgain);

            if (checkIfSelectedMoveIsDone(moveType))
            {
                updateScore();

                if (m_CheckersGameLogic.GameEnded)
                {
                    DoWhenGameEnded(m_CheckersGameLogic.GameStatus, m_CheckersGameLogic.CurrentPlayer.Name);
                }
                else if (m_IsNoNeedToEatAgain && !m_CheckersGameForm.StartNewGame)
                {
                    setCurrentPlayer();

                    if (m_CurrentPlayer.PlayerType == ePlayerType.ComputerPlayer)
                    {
                        playComputerMove();
                    }
                }
                else if (m_CheckersGameForm.StartNewGame)
                {
                    m_CheckersGameLogic.CurrentPlayer = m_CurrentPlayer = m_CheckersGameLogic.PlayerOne;
                }
            }
        }

        private bool checkIfSelectedMoveIsDone(eMoveType moveType)
        {
            return (moveType == eMoveType.Eat) ||
                   (moveType == eMoveType.Regular) ||
                   (moveType == eMoveType.NeedToEatAgain);
        }

        private void updateScore()
        {
            m_CheckersGameForm.PlayerOneScore.Text = string.Format("{0}: {1}", m_CheckersGameLogic.PlayerOne.Name,
                                                                                m_CheckersGameLogic.PlayerOne.PlayerScore);
            m_CheckersGameForm.PlayerTwoScore.Text = string.Format("{0}: {1}", m_CheckersGameLogic.PlayerTwo.Name,
                                                                                m_CheckersGameLogic.PlayerTwo.PlayerScore);
        }

        internal void DoWhenGameEnded(eGameStatus i_GameStatus, string i_WinnerName)
        {
            OnGameEnded(i_GameStatus, i_WinnerName);
        }

        protected virtual void OnGameEnded(eGameStatus i_GameStatus, string i_WinnerName)
        {
            GameEndedEventArgs e = new GameEndedEventArgs(i_GameStatus, i_WinnerName);

            if (GameEnded != null)
            {
                GameEnded.Invoke(e);
            }
        }

        private void setCurrentPlayer()
        {
            m_CurrentPlayer = m_CurrentPlayer == m_CheckersGameLogic.PlayerOne ? m_CheckersGameLogic.PlayerTwo :
                                                                                m_CheckersGameLogic.PlayerOne;
            m_CheckersGameLogic.CurrentPlayer = m_CurrentPlayer;
        }

        private void playComputerMove()
        {
            bool isComputerNeedToPlay = true;

            while (isComputerNeedToPlay && !m_CheckersGameLogic.GameEnded)
            {
                m_CheckersGameForm.DisableAllButtons();
                m_CompuerTurnTimer.Start();

                while (!m_ExitFlag)
                {
                    Application.DoEvents();
                }

                m_ExitFlag = false;
                isComputerNeedToPlay = !m_IsNoNeedToEatAgain;
            }

            if (m_CheckersGameLogic.GameEnded)
            {
                DoWhenGameEnded(m_CheckersGameLogic.GameStatus, m_CheckersGameLogic.PlayerTwo.Name);
            }
            else
            {
                m_CheckersGameForm.EnableAllButtons();
                setCurrentPlayer();
            }
        }

        private void moveComputerPlayer()
        {
            m_CheckersGameLogic.MakeComputerMove(ref m_IsNoNeedToEatAgain, out eMoveType o_MoveType);
        }

        internal void GameForm_AnotherRoundSelected()
        {
            m_CheckersGameLogic.IsFirstGame = false;
            m_IsNoNeedToEatAgain = true;
            m_CheckersGameLogic.InitializeNewRoundGame();
            updateScore();
            m_CheckersGameLogic.CurrentPlayer = m_CurrentPlayer = m_CheckersGameLogic.PlayerOne;
            m_CheckersGameForm.GameBoard = m_CheckersGameLogic.CheckersBoard;
        }
    }
}