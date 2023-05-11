using System;
using Ex02.ConsoleUtils;
using FlippedTicTacToe;

namespace FlippedTicTacToeInterface
{
    public class GameInterface
    {
        private GameEngine m_GameEngine = new GameEngine();

        private class UserInputValidator
        {
            public static bool ValidateYesOrNoInput(string i_UserInput)
            {
                string userInputUpperCase = i_UserInput.ToUpper();
                return userInputUpperCase == "Y" || userInputUpperCase == "N";
            }

            public static bool ValidateBoardInput(string i_BoardSize)
            {
                uint boardSize;
                return uint.TryParse(i_BoardSize, out boardSize);
            }

        }

        private class UserInputRequester
        {
            public static string RequestBoardSizeInput()
            {
                Console.WriteLine("Enter desired board size: ");
                return Console.ReadLine();
            }

            public static string RequestGameRestartInput()
            {
                Console.WriteLine("Do you want to play again? [Y/N]");
                return Console.ReadLine();
            }

            public static string RequestIfSecondPlayerIsBot()
            {
                Console.WriteLine("Do you want to play against a real player? [Y/N]");
                return Console.ReadLine();
            }

            public static string RequestUserNextMoveColumn()
            {
                Console.WriteLine("Enter column number: ");
                return Console.ReadLine();
            }

            public static string RequestUserNextMoveRow()
            {
                Console.WriteLine("Enter row number: ");
                return Console.ReadLine();
            }

        }

        internal class UserInterfaceConverter
        {
            public static bool ConvertYesOrNoToBool(string i_UserInput)
            {
                bool isYes;
                string userInputUpperCase = i_UserInput.ToUpper();
                if (userInputUpperCase == "Y")
                {
                    isYes = true;
                }
                else
                {
                    isYes = false;
                }

                return isYes;
            }

            public static char ConvertSymbolToChar(eSymbols i_Symbol)
            {
                char symbol;
                switch (i_Symbol)
                {
                    case eSymbols.Empty:
                        symbol = ' ';
                        break;
                    case eSymbols.X:
                        symbol = 'X';
                        break;
                    case eSymbols.O:
                        symbol = 'O';
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(i_Symbol), i_Symbol, "Invalid symbol value");
                }
                return symbol;
            }
        }

        public void InitGame()
        {
            bool stillPlaying = true;

            setInitialGameSettings();
            displayStartingAnnouncement();
            displayBoard();
            while (stillPlaying)
            {
                playNextMove();
                displayBoard();
                if (m_GameEngine.GameStatus != eGameStatus.InProgress)
                {
                    displayGameOverScreen(m_GameEngine.GameStatus);
                    bool shouldRestartGame = askUserIfShouldRestartGame();

                    if (shouldRestartGame)
                    {
                        m_GameEngine.RestartGame();
                        displayBoard();
                    }
                    else
                    {
                        stillPlaying = false;
                    }
                }

            }

            Console.WriteLine("Thanks for playing! Goodbye :)");
        }

        private void displayGameOverScreen(eGameStatus i_GameStatus)
        {
            switch(i_GameStatus)
            {
                case eGameStatus.Player1Win:
                    displayWinner(1);
                    break;
                case eGameStatus.Player2Win:
                    displayWinner(2);
                    break;
                case eGameStatus.Draw:
                    Console.WriteLine("It's a draw!");
                    break;
            }

            displayScore();
        }

        private void displayWinner(int i_WinnerId)
        {
            Console.WriteLine($"The winner of this round is player {i_WinnerId}!");
        }

        private void displayScore()
        {
            Console.WriteLine("Current score:");
            Console.WriteLine($"Player 1 - {m_GameEngine.Player1.Score}");
            Console.WriteLine($"Player 2 - {m_GameEngine.Player2.Score}");
        }

        private void playNextMove()
        {
            if(m_GameEngine.CurrentPlayer.IsComputer)
            {
                m_GameEngine.MakeRandomMove();
            }
            else
            {
                bool isValidMove = false;

                while(!isValidMove)
                {
                    try
                    {
                        Cell selectedCell = getNextUserMove();

                        m_GameEngine.MakeMove(selectedCell);
                        isValidMove = true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Try again!");
                    }
                }

            }
        }

        private uint getRowNumberFromUser()
        {
            uint rowNumber = default;
            bool waitingForValidInput = true;

            while (waitingForValidInput)
            {
                try
                {
                    string rowNumberString = UserInputRequester.RequestUserNextMoveRow();
                    rowNumber = uint.Parse(rowNumberString);
                    waitingForValidInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try again!");
                }
            }

            return rowNumber;
        }

        private uint getColNumberFromUser()
        {
            uint colNumber = default;
            bool waitingForValidInput = true;

            while (waitingForValidInput)
            {
                try
                {
                    string colNumberString = UserInputRequester.RequestUserNextMoveColumn();
                    colNumber = uint.Parse(colNumberString);
                    waitingForValidInput = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try again!");
                }
            }

            return colNumber;
        }

        private Cell getNextUserMove()
        {

            uint row = getRowNumberFromUser();
            uint col = getColNumberFromUser();

            return new Cell(row, col);
        }

        private bool askUserIfShouldRestartGame()
        {
            bool waitingForValidInput = true;
            bool shouldRestart = default;

            while (waitingForValidInput) 
            {
                string userInput = UserInputRequester.RequestGameRestartInput();
                bool isInputValid = UserInputValidator.ValidateYesOrNoInput(userInput);

                if (isInputValid)
                {
                    shouldRestart = UserInterfaceConverter.ConvertYesOrNoToBool(userInput);
                    waitingForValidInput = false;
                }
                else
                {
                    Console.WriteLine("Invalid response");
                    Console.WriteLine("Try again!");
                }
            }
           

            return shouldRestart;
        }

        private void displayBoard()
        {
            eSymbols[,] board = m_GameEngine.GameBoard.Board;

            Screen.Clear();
            GameConsoleUtils.PrintBoard(board);
        }

        private void setInitialGameSettings()
        {
            setGameBoard();
            setPlayers();
        }

        private void displayStartingAnnouncement()
        {
            Console.WriteLine("Everything is ready, let's start!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private void setGameBoard()
        {
            bool waitingForValidInput = true;

            while (waitingForValidInput)
            {
                try
                {
                    string userInput = UserInputRequester.RequestBoardSizeInput();
                    bool isInputValid = UserInputValidator.ValidateBoardInput(userInput);

                    if (isInputValid)
                    {
                        uint boardSize = uint.Parse(userInput);

                        m_GameEngine.SetGameBoardSize(boardSize);
                        waitingForValidInput = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try again!");
                }
            }
        }

        private void setPlayers()
        {
            setFirstPlayer();
            setSecondPlayer();
        }

        private void setFirstPlayer()
        {
            m_GameEngine.SetFirstPlayer();
        }

        private void setSecondPlayer()
        {
            bool waitingForValidInput = true;

            while (waitingForValidInput)
            {
                try
                {
                    string userInput = UserInputRequester.RequestIfSecondPlayerIsBot();
                    bool isInputValid = UserInputValidator.ValidateYesOrNoInput(userInput);

                    if (isInputValid)
                    {
                        bool usersAnswer = UserInterfaceConverter.ConvertYesOrNoToBool(userInput);

                        m_GameEngine.SetSecondPlayer(!usersAnswer);
                        waitingForValidInput = false;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Try again!");
                }
            }
        }
    }   
}