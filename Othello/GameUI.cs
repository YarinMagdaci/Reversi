using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othello
{
    public partial class GameUI : Form
    {
        private static readonly int k_WidthOffset = 60;
        private static readonly int k_HeightOffset = 80;
        private static readonly int k_ExtraPadding = 18;
        private static readonly int k_PictureBoxSize = 40;
        private static readonly int k_PictureBoxSpacing = 2;
        private readonly ColoredPictureBox[,] m_PictureBoxes;

        public GameUI(int i_BoardSize, Board i_GameBoard)
        {
            m_PictureBoxes = i_GameBoard.Grid;
            InitializeComponent();
            initializeForm(i_BoardSize);
        }

        private void initializeForm(int i_BoardSize)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Size = new Size((PictureBoxSize * i_BoardSize) + k_WidthOffset, (PictureBoxSize * i_BoardSize) + k_HeightOffset);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            foreach (ColoredPictureBox currentColoredButton in m_PictureBoxes)
            {
                this.Controls.Add(currentColoredButton);
            }
        }

        public void ChangeText(eColor i_CurrentTurn)
        {
            this.Text = (i_CurrentTurn == eColor.White) ? "Othello White's Turn" : "Othello Black's Turn";
        }

        public bool PopUpMessageBoxOfEndGame(string i_MessageBoxString)
        {
            bool endGame = true;
            if (MessageBox.Show(i_MessageBoxString, "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                endGame = false;
            }

            return endGame;
        }

        public void PopSkipTurn(eColor i_CurrentTurn)
        {
            this.Text = (i_CurrentTurn == eColor.White) ? "Skips White's Turn" : "Skips Black's Turn";
            Timer timer = new Timer();
            timer.Interval = 1500;
            timer.Tick += (sender, e) =>
            {
                Timer t = (Timer)sender;
                i_CurrentTurn = (i_CurrentTurn == eColor.White) ? eColor.Black : eColor.White;
                this.ChangeText(i_CurrentTurn);
                t.Stop();
                t.Dispose();
            };
            timer.Start();
        }

        public static int ExtraPadding
        {
            get { return k_ExtraPadding; }
        }

        public static int PictureBoxSize
        {
            get { return k_PictureBoxSize; }
        }

        public static int PictureBoxSpacing
        {
            get { return k_PictureBoxSpacing; }
        }
    }
}
