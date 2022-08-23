using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckersGameUI
{
    internal partial class GameSettingForm : Form
    {
        private RadioButton m_RadioButtonBoardSize = null;
        private int m_BoardSize;

        public GameSettingForm()
        {
            InitializeComponent();
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public string PlayerOneName
        {
            get { return textBoxPlayer1.Text;}
        }

        public string PlayerTwoName
        {
            get
            {
                if(!checkBoxPlayer2.Checked)
                {
                    textBoxPlayer2.Text = "Computer";
                }

                return textBoxPlayer2.Text;
            }
        }

        public bool PlayerTwoIsHuman
        {
            get { return textBoxPlayer2.Enabled; }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            string message = string.Empty;

            if (checkIfFormIsValid(ref message))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private bool checkIfFormIsValid(ref string io_Message)
        {
            bool validForm = false;
            bool gameWithTwoPlayersSelected = textBoxPlayer2.Enabled;

            if (m_RadioButtonBoardSize != null)
            {
                if (gameWithTwoPlayersSelected)
                {
                    validForm = playerNameValidate(textBoxPlayer1, Constants.k_PlayerOneNumber, ref io_Message) &&
                                playerNameValidate(textBoxPlayer2, Constants.k_PlayerTwoNumber, ref io_Message);
                }
                else
                {
                    validForm = playerNameValidate(textBoxPlayer1, Constants.k_PlayerOneNumber, ref io_Message);
                }
            }
            else
            {
                io_Message = "You have to choose board size";
            }

            return validForm;
        }

        private bool playerNameValidate(TextBox i_TextBoxPlayerName, int i_PlayerNumber, ref string io_ErrorMessage)
        {
            bool playerNameIsValid = false;

            if(i_TextBoxPlayerName.Text == string.Empty)
            {
                io_ErrorMessage = string.Format("You must enter a player {0} name!", i_PlayerNumber);
            }
            else if (i_TextBoxPlayerName.Text.Length > Constants.k_MaxNameLength)
            {
                io_ErrorMessage = string.Format("Player name max length is{0}", Constants.k_MaxNameLength);
            }
            else if(i_TextBoxPlayerName.Text.Contains(" "))
            {
                io_ErrorMessage = "Player name cannot contain spaces";
            }
            else
            {
                playerNameIsValid = true;
            }

            return playerNameIsValid;
        }

        private void radioButtonBoardSize_CheckedChanged(object sender, EventArgs e)
        {
            m_RadioButtonBoardSize = sender as RadioButton;
            setBoardSize();
        }

        private void setBoardSize()
        {
            if (radioButtonBoardSize6.Checked)
            {
                m_BoardSize = Constants.k_SmallBoardSize;
            }
            else if (radioButtonBoardSize8.Checked)
            {
                m_BoardSize = Constants.k_MediumBoardSize;
            }
            else if (radioButtonBoardSize10.Checked)
            {
                m_BoardSize = Constants.k_BigBoardSize;
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Clear();
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
                textBoxPlayer2.Enabled = false;
            }
        }
    }
}