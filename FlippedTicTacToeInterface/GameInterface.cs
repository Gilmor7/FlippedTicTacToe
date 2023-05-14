using System;
using Ex02.ConsoleUtils;
using FlippedTicTacToe;

namespace FlippedTicTacToeInterface
{
    public class GameInterface
    {
        private GameEngine m_GameEngine = new GameEngine();

        private class PlayerQuitException : Exception
        {
            public PlayerQuitException() : base("Player has quit the game.")
            {
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

        private string askForUserInput(string i_OutputMessage)
        {
            Console.WriteLine(i_OutputMessage);
            return Console.ReadLine();
        }

        private static bool validateYesOrNoInput(string i_UserInput)
        {
            string userInputUpperCase = i_UserInput.ToUpper();
            return userInputUpperCase == "Y" || userInputUpperCase == "N";
        }

        public void StartGame()
        {
            bool stillPlaying = true;

            setInitialGameSettings();
            GameConsoleUtils.DisplayStartingAnnouncement();
            while (stillPlaying)
            {
                GameConsoleUtils.DisplayBoard(m_GameEngine.GameBoard);
                GameConsoleUtils.DisplayCurrentPlayerTurn(m_GameEngine);
                try
                {
                    playNextMove();
                }
                catch(PlayerQuitException e)
                {
                    m_GameEngine.ForfeitCurrPlayer();
                    
                }
                GameConsoleUtils.DisplayBoard(m_GameEngine.GameBoard);
                if (m_GameEngine.GameStatus != eGameStatus.InProgress)
                {
                    GameConsoleUtils.DisplayGameOverScreen(m_GameEngine.GameStatus);
                    GameConsoleUtils.DisplayScore(m_GameEngine.Player1.Score
                        , m_GameEngine.Player2.Score);
                    bool shouldRestartGame = askUserIfShouldRestartGame();

                    if (shouldRestartGame)
                    {
                        m_GameEngine.RestartGame();
                    }
                    else
                    {
                        stillPlaying = false;
                    }
                }

            }

            Console.WriteLine("Thanks for playing! Goodbye :)");
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
                    catch(PlayerQuitException e)
                    {
                        throw new PlayerQuitException();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Try again!");
                    }
                }

            }
        }

        private void throwIfUserQuit(string i_UserInput)
        {
            if(i_UserInput.ToUpper() == "Q")
            {
                throw new PlayerQuitException();
            }
        }

        private uint getRowNumberFromUser()
        {
            const bool k_WaitingForValidInput = true;

            while (k_WaitingForValidInput)
            {
                string userInputString = askForUserInput("Enter row number: ");
                throwIfUserQuit(userInputString);
                bool parseWasOk = uint.TryParse(userInputString, out uint rowNumber);
                if (parseWasOk)
                {
                    return rowNumber;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again!");
                }
            }
        }

        private uint getColNumberFromUser()
        {
            const bool k_WaitingForValidInput = true;

            while (k_WaitingForValidInput)
            {
                string colNumberString = askForUserInput("Enter col number: ");
                bool isParseOk = uint.TryParse(colNumberString, out uint colNumber);
                if (isParseOk)
                {
                    return colNumber;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again!");
                }
            }
        }

        private Cell getNextUserMove()
        {
            Player currPlayer = m_GameEngine.CurrentPlayer;
            uint row = getRowNumberFromUser();
            uint col = getColNumberFromUser();

            return new Cell(row, col);
        }

        private bool askUserIfShouldRestartGame()
        {
            const bool k_WaitingForValidInput = true;

            while (k_WaitingForValidInput)
            {
                string userInput = askForUserInput("Do you want to play again? [Y/N]");
                bool isInputValid = validateYesOrNoInput(userInput);

                if (isInputValid)
                {
                    return UserInterfaceConverter.ConvertYesOrNoToBool(userInput);
                }
                else
                {
                    Console.WriteLine("Invalid response");
                    Console.WriteLine("Try again!");
                }
            }
        }

        private void setInitialGameSettings()
        {
            setGameBoard();
            setPlayers();
        }

        private void setGameBoard()
        {
            const bool k_WaitingForValidInput = true;
            while (k_WaitingForValidInput)
            {
                string userInput = askForUserInput("Enter desired board size: ");
                bool isParseOk = uint.TryParse(userInput, out uint boardSize);

                if (isParseOk)
                {
                    try
                    {
                        m_GameEngine.SetGameBoardSize(boardSize);
                        break;
                    }
                    catch(ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Try again!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again!");
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
            const bool k_WaitingForValidInput = true;

            while (k_WaitingForValidInput)
            {
                string userInput = askForUserInput("Do you want to play against the computer? [Y/N]");
                bool isInputValid = validateYesOrNoInput(userInput);

                if (isInputValid)
                {
                    bool usersAnswer = UserInterfaceConverter.ConvertYesOrNoToBool(userInput);
                    m_GameEngine.SetSecondPlayer(usersAnswer);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again!");
                }
            }
        }

    }
}