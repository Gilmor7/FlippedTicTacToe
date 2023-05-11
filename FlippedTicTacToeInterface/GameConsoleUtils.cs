using System;
using FlippedTicTacToe;

namespace FlippedTicTacToeInterface
{
    public class GameConsoleUtils
    {
        private const string k_Space = " ";
        private const string k_ColumnSeparator = "|";

        public static void PrintBoard(eSymbols[,] i_Board)
        {
            int numOfRowsIncludeSeparations = i_Board.GetLength(0) * 2;

            printColumnIndexes(i_Board.GetLength(0));
            for(int rowIndex = 0; rowIndex < numOfRowsIncludeSeparations; rowIndex++)
            {
                if(rowIndex % 2 == 0)
                {
                    Console.Write($"{rowIndex / 2}{k_ColumnSeparator}");
                    printSymbolsRow(i_Board, rowIndex / 2);
                }
                else
                {
                    Console.Write(k_Space);
                    printSeparationRow(i_Board.GetLength(0));
                }
            }
        }

        private static void printColumnIndexes(int i_Size)
        {
            Console.Write(k_Space);
            for(int colIndex = 0; colIndex < i_Size; colIndex++)
            {
                Console.Write($"  {colIndex} ");
            }

            Console.Write("\n");
        }

        private static void printSymbolsRow(eSymbols[,] i_Board, int i_RowIndex)
        {
            for(int colIndex = 0; colIndex < i_Board.GetLength(0); colIndex++)
            {
                char symbol = GameInterface.UserInterfaceConverter.ConvertSymbolToChar(i_Board[i_RowIndex, colIndex]);
                Console.Write($" {symbol.ToString()} {k_ColumnSeparator}");
            }

            Console.Write("\n");
        }

        private static void printSeparationRow(int i_Width)
        {
            for (int colIndex = 0; colIndex < i_Width; colIndex++)
            {
                Console.Write("====");
            }

            Console.Write("\n");
        }
    }
}
