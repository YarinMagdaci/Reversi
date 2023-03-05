namespace Ex05_Othello
{
    public class Player
    {
        private readonly string r_Name;
        private eColor m_PlayerColor;
        private int m_Score;

        public Player(string i_Name, eColor i_Color)
        {
            r_Name = i_Name;
            m_PlayerColor = i_Color;
        }

        public string Name
        {
            get { return this.r_Name; }
        }

        public eColor PColor
        {
            get { return this.m_PlayerColor; }
            set { this.m_PlayerColor = value; }
        }

        public int Score
        {
            get { return this.m_Score; }
            set { this.m_Score = value; }
        }
    }
}