using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using CheckersGameLogic;

namespace CheckersGameUI
{
    public class SpotButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Col;
        private bool m_IsEmptySpot;
        private const int k_ButtonSize = Constants.k_ButtonSize;
        public SpotButton(int i_Row, int i_Col, Spot i_Spot)
        {
            r_Row = i_Row;
            r_Col = i_Col;
            SetSpot(i_Spot);
        }

        public bool IsEmptySpot
        {
            get { return m_IsEmptySpot; }
        }

        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }

        public void SpotButton_SpotChanged(Spot i_Spot)
        {
            setSpotButtonImage(i_Spot);
        }

        internal void SetSpot(Spot i_Spot)
        {
            Point currentLocation = new Point();
            currentLocation.Y = r_Row * k_ButtonSize + 50;
            currentLocation.X = r_Col * k_ButtonSize + 10;
            this.Location = currentLocation;
            this.Width = k_ButtonSize;
            this.Height = k_ButtonSize;
            this.Enabled = i_Spot.IsActiveSpot;

            setSpotButtonImage(i_Spot);
        }

        private void setSpotButtonImage(Spot i_Spot)
        {
            m_IsEmptySpot = i_Spot.PieceColor == eColor.Transparent;
            this.FlatStyle = FlatStyle.Flat;

            if (i_Spot.IsActiveSpot)
            {
                this.BackColor = Color.White;

                if (i_Spot.PieceColor == eColor.White)
                {
                    this.BackgroundImage = i_Spot.Type == ePieceType.Soldier ? CheckersGameUI.Properties.Resources.PlayerX :
                                                                                   CheckersGameUI.Properties.Resources.KingXWhite;

                    this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                }
                else if (i_Spot.PieceColor == eColor.Black)
                {
                    this.BackgroundImage = i_Spot.Type == ePieceType.Soldier ? CheckersGameUI.Properties.Resources.PlayerO :
                                                                                  CheckersGameUI.Properties.Resources.KingOBlack;
                    this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
                }
                else
                {
                    this.BackgroundImage = null;
                }
            }
            else
            {
                this.BackColor = Color.Black;
            }
            
        }
    }
}