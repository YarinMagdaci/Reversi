using System.Windows.Forms;

namespace Ex05_Othello
{
    public class ColoredPictureBox : PictureBox
    {
        private eColor m_Color;
        private int m_Row;
        private int m_Col;

        public ColoredPictureBox(int i_Row, int i_Col)
            : base()
        {
            this.Row = i_Row;
            this.Col = i_Col;
            this.PColor = eColor.Empty;
            this.Enabled = false;
        }

        public eColor PColor
        {
            get { return this.m_Color; }
            set { this.m_Color = value; }
        }

        public int Row
        {
            get { return this.m_Row; }
            set { this.m_Row = value; }
        }

        public int Col
        {
            get { return this.m_Col; }
            set { this.m_Col = value; }
        }
    }
}