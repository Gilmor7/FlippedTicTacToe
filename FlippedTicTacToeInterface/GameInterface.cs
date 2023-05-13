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
            displayStartingAnnouncement();
            while (stillPlaying)
            {
                displayBoard();
                displayCurrentPlayerTurn();
                try
                {
                    playNextMove();
                }
                catch(PlayerQuitException e)
                {
                    m_GameEngine.ForfeitCurrPlayer();
                    
                }
                displayBoard();
                if (m_GameEngine.GameStatus != eGameStatus.InProgress)
                {
                    displayGameOverScreen(m_GameEngine.GameStatus);
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

        private void throwIfUserQuit(string i_UserInput)
        {
            if(i_UserInput == "Q")
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
                string userInputString = askForUserInput("Enter row number: ");
                throwIfUserQuit(userInputString);
                bool isParseOk = uint.TryParse(userInputString, out uint colNumber);
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

        private void displayBoard()
        {
            eSymbols[,] board = m_GameEngine.GameBoard.Board;

            Screen.Clear();
            GameConsoleUtils.PrintBoard(board);
        }

        private void displayCurrentPlayerTurn()
        {
            Player currentPlayer = m_GameEngine.CurrentPlayer;
            if(currentPlayer == m_GameEngine.Player1)
            {
                Console.WriteLine("Player 1 turn!");
            }
            else
            {
                Console.WriteLine("Player 2 turn!");
            }
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
            const bool k_WaitingForValidInput = true;
            while (k_WaitingForValidInput)
            {
                string userInput = askForUserInput("Enter desired board size: ");
                bool isParseOk = uint.TryParse(userInput, out uint boardSize);

                if (isParseOk)
                {
                    m_GameEngine.SetGameBoardSize(boardSize);
                    break;
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
                    m_GameEngine.SetSecondPlayer(!usersAnswer);
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