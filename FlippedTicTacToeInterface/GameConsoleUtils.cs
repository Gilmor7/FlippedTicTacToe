using System;
using Ex02.ConsoleUtils;
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

        public static void DisplayBoard(GameBoard i_GameBoard)
        {
            eSymbols[,] board = i_GameBoard.Board;

            Screen.Clear();
            GameConsoleUtils.PrintBoard(board);
        }

        public static void DisplayCurrentPlayerTurn(GameEngine i_GameEngine)
        {
            Player currentPlayer = i_GameEngine.CurrentPlayer;

            if (currentPlayer == i_GameEngine.Player1)
            {
                Console.WriteLine("Player 1 turn!");
            }
            else
            {
                Console.WriteLine("Player 2 turn!");
            }
        }

        public static void DisplayStartingAnnouncement()
        {
            Console.WriteLine("Everything is ready, let's start!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void DisplayGameOverScreen(eGameStatus i_GameStatus)
        {
            const byte k_Player1Id = 1;
            const byte k_Player2Id = 2;

            switch (i_GameStatus)
            {
                case eGameStatus.Player1Win:
                    DisplayWinner(k_Player1Id);
                    break;
                case eGameStatus.Player2Win:
                    DisplayWinner(k_Player2Id);
                    break;
                case eGameStatus.Draw:
                    Console.WriteLine("It's a draw!");
                    break;
            }
        }

        public static void DisplayWinner(byte i_WinnerId)
        {
            Console.WriteLine($"The winner of this round is player {i_WinnerId}!");
        }

        public static void DisplayScore(uint i_Player1Score, uint i_Player2Score)
        {
            Console.WriteLine("Current score:");
            Console.WriteLine($"Player 1 - {i_Player1Score}");
            Console.WriteLine($"Player 2 - {i_Player2Score}");
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
