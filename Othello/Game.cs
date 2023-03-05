using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othello
{
    public class Game
    {
        private const string k_ImagesPath = "C:\\MyProjects\\A23 Ex05 YarinMagdaci 207702994 DavidDaida 313374373\\Ex05_Othello\\";
        private readonly int r_GameBoardSize;
        private readonly Board r_GameBoard;
        private readonly GameSettingsUI r_GameSettingsUI;
        private readonly GameUI r_GameUI;
        private readonly bool r_VsPC;
        private readonly Player r_PlayerOne;
        private readonly Player r_PlayerTwo;
        private bool[,] m_AvailableSpots;
        private bool m_EndGame = false;
        private int m_CountOfSkips;
        private int m_PlayerOneVictories = 0;
        private int m_PlayerTwoVictories = 0;
        private int m_NumOfGames = 1;
        private eColor m_ColorOfWinner = eColor.Empty;
        private eColor m_CurrentTurn = eColor.Black;

        public Game()
        {
            r_PlayerOne = new Player("Black", eColor.Black);
            r_PlayerTwo = new Player("White", eColor.White);
            r_GameSettingsUI = new GameSettingsUI();
            SettingsUI.ShowDialog();
            if (SettingsUI.DialogResult == DialogResult.OK)
            {
                r_GameBoardSize = SettingsUI.BoardSize;
                r_GameBoard = new Board(GameBoardSize);
            }

            r_VsPC = (SettingsUI.GameOption == eGameOption.PlayAgainstPC) ? true : false;
            startGame();
            r_GameUI = new GameUI(SettingsUI.BoardSize, GameBoard);
            PGameUI.ChangeText(this.CurrentTurn);
            PGameUI.ShowDialog();
        }

        private void startGame()
        {
            this.AvailableSpots = new bool[GameBoardSize, GameBoardSize];
            this.m_CountOfSkips = 0;
            initializeBoard();
            CurrentTurn = (ColorOfWinner == eColor.Empty || ColorOfWinner == eColor.Black) ? eColor.Black : ColorOfWinner;
            if (PGameUI != null)
            {
                PGameUI.ChangeText(CurrentTurn);
                if (CurrentTurn != eColor.Black && VsPC)
                {
                    allPossibleMovesOfCurrentPlayer(CurrentTurn);
                    this.runPCTurn();
                }
            }

            allPossibleMovesOfCurrentPlayer(CurrentTurn);
        }

        public void RunSession(int i_Row, int i_Col, eColor i_Color)
        {
            PlacePiece(i_Row, i_Col, i_Color);
            CurrentTurn = (CurrentTurn == eColor.Black) ? eColor.White : eColor.Black;
        }

        private void setWinnerAndLoser()
        {
            int blackDiscs = 0;
            int whiteDiscs = 0;
            for (int i = 0; i < GameBoard.Size; i++)
            {
                for (int j = 0; j < GameBoard.Size; j++)
                {
                    if (GameBoard.Grid[i, j].PColor == eColor.Black)
                    {
                        blackDiscs++;
                    }
                    else if (GameBoard.Grid[i, j].PColor == eColor.White)
                    {
                        whiteDiscs++;
                    }
                }
            }

            PlayerOne.Score = blackDiscs;
            PlayerTwo.Score = whiteDiscs;
            if (blackDiscs > whiteDiscs)
            {
                this.PlayerOneVictories++;
                ColorOfWinner = PlayerOne.PColor;
                DeclareWinner(PlayerOne, PlayerTwo);
            }
            else if (blackDiscs < whiteDiscs)
            {
                this.PlayerTwoVictories++;
                ColorOfWinner = PlayerTwo.PColor;
                DeclareWinner(PlayerTwo, PlayerOne);
            }
            else
            {
                ColorOfWinner = (ColorOfWinner == eColor.White) ? eColor.Black : eColor.White;
                DeclareWinner(PlayerOne, PlayerTwo);
            }
        }

        public void DeclareWinner(Player i_PlayerOne, Player i_PlayerTwo)
        {
            string messageBoxString;
            if (i_PlayerOne.Score == i_PlayerTwo.Score)
            {
                messageBoxString = $"It's a tie!! ({i_PlayerOne.Score}/{i_PlayerTwo.Score}) ({NumOfGames - PlayerOneVictories - PlayerTwoVictories}/{NumOfGames}){Environment.NewLine}Would you like another round?";
            }
            else if (i_PlayerOne.PColor == eColor.Black)
            {
                messageBoxString = $"{i_PlayerOne.PColor} Won!! ({i_PlayerOne.Score}/{i_PlayerTwo.Score}) ({PlayerOneVictories}/{NumOfGames}){Environment.NewLine}Would you like another round?";
            }
            else
            {
                messageBoxString = $"{i_PlayerOne.PColor} Won!! ({i_PlayerTwo.Score}/{i_PlayerOne.Score}) ({PlayerTwoVictories}/{NumOfGames}){Environment.NewLine}Would you like another round?";
            }

            EndGame = PGameUI.PopUpMessageBoxOfEndGame(messageBoxString);
            if (EndGame == false)
            {
                NumOfGames++;
                startGame();
            }
        }

        private void initializeBoard()
        {
            foreach (ColoredPictureBox currentPictureBox in this.GameBoard.Grid)
            {
                currentPictureBox.Click += coloredPictureBoxOnClick;
                currentPictureBox.PColor = eColor.Empty;
                currentPictureBox.Image = null;
            }

            changeCurrentButtonState(GameBoard.Grid[(GameBoard.Size / 2) - 1, (GameBoard.Size / 2) - 1], eColor.White);
            changeCurrentButtonState(GameBoard.Grid[GameBoard.Size / 2, GameBoard.Size / 2], eColor.White);
            changeCurrentButtonState(GameBoard.Grid[(GameBoard.Size / 2) - 1, GameBoard.Size / 2], eColor.Black);
            changeCurrentButtonState(GameBoard.Grid[GameBoard.Size / 2, (GameBoard.Size / 2) - 1], eColor.Black);
        }

        private void coloredPictureBoxOnClick(object sender, EventArgs e)
        {
            PGameUI.ChangeText(this.CurrentTurn);
            ColoredPictureBox currentButtonWasPressed = sender as ColoredPictureBox;
            if (currentButtonWasPressed == null || !allPossibleMovesOfCurrentPlayer(CurrentTurn))
            {
                CountOfSkips++;
                PGameUI.PopSkipTurn(CurrentTurn);
                CurrentTurn = (CurrentTurn == eColor.Black) ? eColor.White : eColor.Black;
            }
            else if (CountOfSkips < 2)
            {
                CountOfSkips = 0;
                RunSession(currentButtonWasPressed.Row, currentButtonWasPressed.Col, CurrentTurn);
            }

            if (!allPossibleMovesOfCurrentPlayer(CurrentTurn))
            {
                CountOfSkips++;
                PGameUI.PopSkipTurn(CurrentTurn);
                CurrentTurn = (CurrentTurn == eColor.Black) ? eColor.White : eColor.Black;
                if (!allPossibleMovesOfCurrentPlayer(CurrentTurn))
                {
                    CountOfSkips++;
                }
            }
            else if (CountOfSkips < 2 && VsPC)
            {
                runPCTurn();
            }
            else if (CountOfSkips < 2 && !VsPC)
            {
                PGameUI.ChangeText(CurrentTurn);
            }

            if (CountOfSkips >= 2 && EndGame == false)
            {
                setWinnerAndLoser();
            }
        }

        private void runPCTurn()
        {
            int pc_Row, pc_Col;
            pc_Row = pc_Col = -1;
            PGameUI.ChangeText(CurrentTurn);
            PC.GeneratePCMove(AvailableSpots, ref pc_Row, ref pc_Col);
            RunSession(pc_Row, pc_Col, CurrentTurn);
            if (!allPossibleMovesOfCurrentPlayer(CurrentTurn))
            {
                coloredPictureBoxOnClick(null, EventArgs.Empty);
            }

            PGameUI.ChangeText(this.CurrentTurn);
        }

        private void changeCurrentButtonState(ColoredPictureBox io_CurrentPictureBox, eColor i_Color)
        {
            io_CurrentPictureBox.BackColor = Color.Empty;
            StringBuilder imageLocationStringBuilder = new StringBuilder();
            imageLocationStringBuilder.Append(k_ImagesPath);
            imageLocationStringBuilder.Append(i_Color == eColor.Black ? "CoinRed.png" : "CoinYellow.png");
            io_CurrentPictureBox.ImageLocation = imageLocationStringBuilder.ToString();
            io_CurrentPictureBox.PColor = i_Color;
            io_CurrentPictureBox.Click -= coloredPictureBoxOnClick;
        }

        private bool allPossibleMovesOfCurrentPlayer(eColor i_ColorOfCurrentUser)
        {
            bool foundAvailableSpot = false;
            for (int i = 0; i < this.AvailableSpots.GetLength(0); i++)
            {
                for (int j = 0; j < this.AvailableSpots.GetLength(1); j++)
                {
                    if (isValidPlacement(i, j, i_ColorOfCurrentUser) && this.GameBoard.Grid[i, j].PColor == eColor.Empty)
                    {
                        this.AvailableSpots[i, j] = true;
                        this.GameBoard.Grid[i, j].Enabled = true;
                        this.GameBoard.Grid[i, j].BackColor = Color.LimeGreen;
                        foundAvailableSpot = true;
                    }
                    else
                    {
                        this.GameBoard.Grid[i, j].Enabled = false;
                        this.GameBoard.Grid[i, j].BackColor = Color.Empty;
                        this.AvailableSpots[i, j] = false;
                    }
                }
            }

            return foundAvailableSpot;
        }

        private bool isValidPlacement(int i_Row, int i_Col, eColor i_ColorOfCurrentUser)
        {
            bool validPlacement = false;
            eColor rivalColor = (i_ColorOfCurrentUser == eColor.Black) ? eColor.White : eColor.Black;
            int[,] directions = new int[8, 2]
            {
                { 0, 1 },
                { 0, -1 },
                { -1, 0 },
                { 1, 0 },
                { 1, 1 },
                { -1, -1 },
                { -1, 1 },
                { 1, -1 },
            };
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int directionRow = directions[i, 0];
                int directionCol = directions[i, 1];
                int runningRow = i_Row;
                int runningCol = i_Col;
                if (!checkCurrentPathFlank(i_Row, i_Col, directionRow, directionCol, rivalColor, ref runningRow, ref runningCol))
                {
                    continue;
                }

                validPlacement = true;
                break;
            }

            return validPlacement;
        }

        private bool checkCurrentPathFlank(int i_Row, int i_Col, int i_DirectionRow, int i_DirectionCol, eColor i_RivalColor, ref int io_RunningRow, ref int io_RunningCol)
        {
            bool flankValid = true;
            if (!GameBoard.ValidateBoundaryExceptionsOfMatrix(i_Row + i_DirectionRow, i_Col + i_DirectionCol)
                || GameBoard.Grid[i_Row + i_DirectionRow, i_Col + i_DirectionCol].PColor != i_RivalColor)
            {
                flankValid = false;
            }
            else
            {
                io_RunningRow = i_Row + i_DirectionRow;
                io_RunningCol = i_Col + i_DirectionCol;
                while (GameBoard.ValidateBoundaryExceptionsOfMatrix(io_RunningRow, io_RunningCol) && GameBoard.Grid[io_RunningRow, io_RunningCol].PColor == i_RivalColor)
                {
                    io_RunningRow += i_DirectionRow;
                    io_RunningCol += i_DirectionCol;
                }

                if (GameBoard.ValidateBoundaryExceptionsOfMatrix(io_RunningRow, io_RunningCol) == false || GameBoard.Grid[io_RunningRow, io_RunningCol].PColor == eColor.Empty)
                {
                    flankValid = false;
                }
            }

            return flankValid;
        }

        private void PlacePiece(int i_Row, int i_Col, eColor i_ColorOfCurrentUser)
        {
            eColor rivalColor = (i_ColorOfCurrentUser == eColor.Black) ? eColor.White : eColor.Black;
            int[,] directions = new int[8, 2]
            {
                { 0, 1 },
                { 0, -1 },
                { -1, 0 },
                { 1, 0 },
                { 1, 1 },
                { -1, -1 },
                { -1, 1 },
                { 1, -1 },
            };
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int directionRow = directions[i, 0];
                int directionCol = directions[i, 1];
                int runningRow = i_Row;
                int runningCol = i_Col;
                if (!checkCurrentPathFlank(i_Row, i_Col, directionRow, directionCol, rivalColor, ref runningRow, ref runningCol))
                {
                    continue;
                }

                flipDiscsIngameBoardPath(runningRow, runningCol, directionRow, directionCol, i_Row, i_Col, i_ColorOfCurrentUser);
            }
        }

        private void flipDiscsIngameBoardPath(int i_EndRow, int i_EndCol, int i_DirectionRow, int i_DirectionCol, int i_StartRow, int i_StartCol, eColor i_ColorOfCurrentUser)
        {
            do
            {
                i_EndRow -= i_DirectionRow;
                i_EndCol -= i_DirectionCol;
                changeCurrentButtonState(GameBoard.Grid[i_EndRow, i_EndCol], i_ColorOfCurrentUser);
            }
            while (!(i_EndRow == i_StartRow && i_EndCol == i_StartCol));
        }

        public Board GameBoard
        {
            get { return this.r_GameBoard; }
        }

        public bool[,] AvailableSpots
        {
            get { return this.m_AvailableSpots; }
            set { this.m_AvailableSpots = value; }
        }

        public Player PlayerOne
        {
            get { return this.r_PlayerOne; }
        }

        public Player PlayerTwo
        {
            get { return this.r_PlayerTwo; }
        }

        public eColor ColorOfWinner
        {
            get { return this.m_ColorOfWinner; }
            set { this.m_ColorOfWinner = value; }
        }

        public int CountOfSkips
        {
            get { return this.m_CountOfSkips; }
            set { this.m_CountOfSkips = value; }
        }

        public int GameBoardSize
        {
            get { return this.r_GameBoardSize; }
        }

        public int PlayerOneVictories
        {
            get { return this.m_PlayerOneVictories; }
            set { this.m_PlayerOneVictories = value; }
        }

        public int PlayerTwoVictories
        {
            get { return this.m_PlayerTwoVictories; }
            set { this.m_PlayerTwoVictories = value; }
        }

        public bool VsPC
        {
            get { return this.r_VsPC; }
        }

        public eColor CurrentTurn
        {
            get { return this.m_CurrentTurn; }
            set { this.m_CurrentTurn = value; }
        }

        public bool EndGame
        {
            get { return this.m_EndGame; }
            set { this.m_EndGame = value; }
        }

        public int NumOfGames
        {
            get { return this.m_NumOfGames; }
            set { this.m_NumOfGames = value; }
        }

        public GameSettingsUI SettingsUI
        {
            get { return this.r_GameSettingsUI; }
        }

        public GameUI PGameUI
        {
            get { return this.r_GameUI; }
        }
    }
}
