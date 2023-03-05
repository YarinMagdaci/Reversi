using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othello
{
    public class Board
    {
        private readonly ColoredPictureBox[,] r_Grid;
        private readonly int r_Size;

        public Board(int i_Size)
        {
            this.r_Size = i_Size;
            this.r_Grid = new ColoredPictureBox[this.Size, this.Size];
            int extraPaddingRight, extraPaddingBottom;
            for (int i = 0; i < this.Size; i++)
            {
                for (int j = 0; j < this.Size; j++)
                {
                    extraPaddingRight = extraPaddingBottom = 0;
                    ColoredPictureBox picBox = new ColoredPictureBox(i, j);
                    picBox.PColor = eColor.Empty;
                    picBox.BorderStyle = BorderStyle.Fixed3D;
                    picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picBox.Size = new Size(GameUI.PictureBoxSize, GameUI.PictureBoxSize);
                    picBox.Location = new Point((j * GameUI.PictureBoxSize) + GameUI.ExtraPadding, (i * GameUI.PictureBoxSize) + GameUI.ExtraPadding);
                    if (i == this.Size - 1)
                    {
                        extraPaddingBottom = GameUI.ExtraPadding;
                    }

                    if (j == this.Size - 1)
                    {
                        extraPaddingRight = GameUI.ExtraPadding;
                    }

                    picBox.Margin = new Padding(1, 1, 1 + extraPaddingRight, 1 + extraPaddingBottom);
                    this.Grid[i, j] = picBox;
                }
            }
        }

        public bool ValidateBoundaryExceptionsOfMatrix(int i_Row, int i_Col)
        {
            bool resultOfValidation = true;
            if (i_Row < 0 || i_Col < 0 || i_Row >= this.Size || i_Col >= this.Size)
            {
                resultOfValidation = false;
            }

            return resultOfValidation;
        }

        public ColoredPictureBox[,] Grid
        {
            get { return this.r_Grid; }
        }

        public int Size
        {
            get { return this.r_Size; }
        }
    }
}
