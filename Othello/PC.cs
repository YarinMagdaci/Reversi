using System;

namespace Ex05_Othello
{
    public class PC
    {
        public static void GeneratePCMove(bool[,] i_AvailableSpots, ref int io_Row, ref int io_Col)
        {
            int[,] matrixOfAvailableMoves = GenerateArrayOfAvailableMoves(i_AvailableSpots);
            Random random = new Random();
            int randomIndex = random.Next(0, matrixOfAvailableMoves.GetLength(0));
            io_Row = (matrixOfAvailableMoves == null) ? -1 : matrixOfAvailableMoves[randomIndex, 0];
            io_Col = (matrixOfAvailableMoves == null) ? -1 : matrixOfAvailableMoves[randomIndex, 1];
        }

        private static int[,] GenerateArrayOfAvailableMoves(bool[,] i_AvailableSpots)
        {
            int arrayLength = GenerateArrayOfAvailableMovesLength(i_AvailableSpots);
            int[,] matOfAvailbleMoves = null;
            if (arrayLength != 0)
            {
                matOfAvailbleMoves = new int[arrayLength, 2];
                int runningIndexOfArr = 0;
                for (int i = 0; i < i_AvailableSpots.GetLength(0); i++)
                {
                    for (int j = 0; j < i_AvailableSpots.GetLength(1); j++)
                    {
                        if (i_AvailableSpots[i, j] == true)
                        {
                            matOfAvailbleMoves[runningIndexOfArr, 0] = i;
                            matOfAvailbleMoves[runningIndexOfArr++, 1] = j;
                        }
                    }
                }
            }

            return matOfAvailbleMoves;
        }

        private static int GenerateArrayOfAvailableMovesLength(bool[,] i_AvailableSpots)
        {
            int potentialArrayLength = 0;
            for (int i = 0; i < i_AvailableSpots.GetLength(0); i++)
            {
                for (int j = 0; j < i_AvailableSpots.GetLength(1); j++)
                {
                    if (i_AvailableSpots[i, j] == true)
                    {
                        potentialArrayLength++;
                    }
                }
            }

            return potentialArrayLength;
        }
    }
}
