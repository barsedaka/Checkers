using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckersGameLogic;

namespace CheckersGameUI
{
    public partial class CheckersGameForm : Form
    {
        private SpotButton[,] m_ButtonsArray;
        private SpotButton m_FromSpotButtonSelected = null;
        private Board m_Board;
        private bool m_StartNewGame;

        public event MoveEventHanlder MoveSelected;
        public event Action AnotherRoundSelected;

        public CheckersGameForm(Board i_Board)
        {
            m_StartNewGame = false;
            m_Board = i_Board;
            InitializeComponent();
            setPlayersLabels();
            intializeCheckersBoardButtons();
        }

        public bool StartNewGame
        {
            get { return m_StartNewGame; }
        }

        public Board GameBoard
        {
            set { m_Board = value; }
        }

        public Label PlayerOneScore
        {
            get { return LabelPlayerOneScore; }
        }

        public Label PlayerTwoScore
        {
            get { return LabelPlayerTwoScore; }
        }

        private void CheckersGameForm_Load(object sender, EventArgs e)
        {
            int height = m_Board.BoardSize * Constants.k_ButtonSize + Constants.k_FormExtraHeight;
            int width = m_Board.BoardSize * Constants.k_ButtonSize + Constants.k_FormExtraWidth;
            this.Size = new Size(height, width);
        }

        private void setPlayersLabels()
        {
            LabelPlayerOneScore.Left = this.Width / 6;
            LabelPlayerTwoScore.Left = LabelPlayerOneScore.Left * 3;
        }

        private void intializeCheckersBoardButtons()
        {
            m_ButtonsArray = new SpotButton[m_Board.BoardSize, m_Board.BoardSize];

            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                for (int j = 0; j < m_Board.BoardSize; j++)
                {
                    Spot spotInBoard = m_Board.GetSpot(i, j);
                    SpotButton newSpotButton = new SpotButton(i, j, spotInBoard);
                    spotInBoard.SpotChanged += newSpotButton.SpotButton_SpotChanged;
                    newSpotButton.Click += SpotButton_Click;
                    m_ButtonsArray[i, j] = newSpotButton;
                    this.Controls.Add(m_ButtonsArray[i, j]);
                }
            }
        }

        private void SpotButton_Click(object sender, EventArgs e)
        {
            SpotButton buttonSelected = sender as SpotButton;
            SpotButton fromButtonSelected, ToButtonSelected;

            if (m_FromSpotButtonSelected == null)
            {
                if (!buttonSelected.IsEmptySpot)
                {
                    buttonSelected.BackColor = Color.LightBlue;
                    m_FromSpotButtonSelected = buttonSelected;
                }
                else
                {
                    MessageBox.Show("You need to select spot with a player!");
                }
            }
            else
            {
                if (m_FromSpotButtonSelected == buttonSelected)
                {
                    buttonSelected.BackColor = Color.White;
                    m_FromSpotButtonSelected = null;
                }
                else
                {
                    fromButtonSelected = m_FromSpotButtonSelected;
                    ToButtonSelected = buttonSelected;
                    OnMoveSelected(fromButtonSelected, ToButtonSelected);
                    if(!m_StartNewGame)
                    {
                        m_FromSpotButtonSelected.BackColor = Color.White;
                        m_FromSpotButtonSelected = null;
                    }
                    else
                    {
                        m_StartNewGame = false;
                    }
                }
            }
        }

        protected virtual void OnMoveSelected(SpotButton i_FromSpotButtonSelected, SpotButton i_ToSpotButtonSelected)
        {
            MoveSelectedEventArgs e = new MoveSelectedEventArgs(i_FromSpotButtonSelected, i_ToSpotButtonSelected);

            if (MoveSelected != null)
            {
                MoveSelected.Invoke(e);
            }
        }

        public void GameLogic_MoveSelectedError(eMoveType i_MoveNotLegalType)
        {
            string errorMessage = string.Empty;

            getMoveErrorMessage(ref errorMessage, i_MoveNotLegalType);
            MessageBox.Show(errorMessage);
        }

        private void getMoveErrorMessage(ref string io_MoveErrorMessage, eMoveType i_MoveNotLegalType)
        {
            switch (i_MoveNotLegalType)
            {
                case CheckersGameLogic.eMoveType.Ilegal:
                    io_MoveErrorMessage = "Ilegal Move!";
                    break;
                case CheckersGameLogic.eMoveType.IlegalNotSameColor:
                    io_MoveErrorMessage = "Ilegal Move - move your piece!";
                    break;
                case CheckersGameLogic.eMoveType.IlegalNotEmptyDestination:
                    io_MoveErrorMessage = "Ilegal Move - Move is landing on occupied spot";
                    break;
                case CheckersGameLogic.eMoveType.IlegalNeedToEat:
                    io_MoveErrorMessage = "Ilegal Move - You have to eat";
                    break;
                case CheckersGameLogic.eMoveType.IlegalNotValidEatMove:
                    io_MoveErrorMessage = "Ilegal Move - This eat move is not valid";
                    break;
            }
        }

        public void CheckersGameLogic_GameEnded(GameEndedEventArgs e)
        {
            string message = getEndedStringMessage(e.GameStatus, e.WinnerName);
            string title = "Checkers";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult wantToPlayAgain = MessageBox.Show(message, title, buttons);

            if (wantToPlayAgain == DialogResult.Yes)
            {
                EnableAllButtons();
                OnAnotherRoundSelected();
                m_StartNewGame = true;
                m_FromSpotButtonSelected = null;
            }
            else
            {
                this.Close();
            }
        }

        private string getEndedStringMessage(eGameStatus i_GameStatus, string i_WinnerName)
        {
            StringBuilder msg = new StringBuilder();

            switch (i_GameStatus)
            {
                case eGameStatus.WhiteWin:
                    msg.AppendFormat("{0} Won!{1}", i_WinnerName, Environment.NewLine);
                    break;

                case eGameStatus.BlackWin:
                    msg.AppendFormat("{0} Won!{1}", i_WinnerName, Environment.NewLine);
                    break;

                case eGameStatus.Tie:
                    msg.AppendFormat("Tie!{0}", Environment.NewLine);
                    break;
            }

            msg.Append("Another Round?");

            return msg.ToString();
        }

        protected virtual void OnAnotherRoundSelected()
        {
            if (AnotherRoundSelected != null)
            {
                AnotherRoundSelected.Invoke();
            }
        }

        internal void DisableAllButtons()
        {
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                for (int j = 0; j < m_Board.BoardSize; j++)
                {
                    m_ButtonsArray[i, j].Enabled = false;
                }
            }
        }

        internal void EnableAllButtons()
        {
            for (int i = 0; i < m_Board.BoardSize; i++)
            {
                for (int j = 0; j < m_Board.BoardSize; j++)
                {
                    if (m_Board.GetSpot(i, j).IsActiveSpot)
                    {
                        m_ButtonsArray[i, j].Enabled = true;
                    }
                }
            }
        }
    }
}