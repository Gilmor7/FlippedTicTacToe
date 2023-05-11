using System;
using Ex02.ConsoleUtils;

namespace FlippedTicTacToe
{
    public class GameInterface
    {
        private static GameEngine s_GameEngine = null;

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

            public static bool ValidateUserMove(string i_RowMove, string i_ColMove)
            {
                int rowNumber, colNumber;

                return int.TryParse(i_RowMove, out rowNumber) && int.TryParse(i_ColMove, out colNumber);
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
                Console.WriteLine("Do you want to play aginst a real player? [Y/N]");
                return Console.ReadLine();
            }

            public static (string, string) RequestUserMove()
            {
                string rowNumberString;
                string colNumberString;

                Console.WriteLine("Enter row number: ");
                rowNumberString = Console.ReadLine();

                Console.WriteLine("Enter col number: ");
                colNumberString = Console.ReadLine();

                return (rowNumberString, colNumberString);
            }
        }

        private class UserInterfaceConverter
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

        public static void Play()
        {
            bool stillPlaying = true;

            setInitialGameSettings();
            while (stillPlaying)
            {
                displayBoard();
                makeCurrPlayerMove();
                if(GameEngine.GameStatus == "OVER")
                {
                    displayGameOverScreen();
                    bool userWantsToRestartGame = askIfUserWantsToRestartGame();    
                    if (askIfUserWantsToResetGame())
                    {
                        s_GameEngine.RestartGame();
                    }
                    else
                    {
                        stillPlaying = false;
                    }
                }

            }

            Console.WriteLine("Thanks for playing! Goodbye :)");
        }

        private static void displayBoard()
        {
            Screen.Clear();
            // display screen here, use Symbol to Char in Converter class
        }

        private static void setInitialGameSettings()
        {
            setGameBoard();
            setPlayers();
        }

        private static void setGameBoard()
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

                        s_GameEngine.SetGameBoardSize(boardSize);
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

        private static void setPlayers()
        {
            setFirstPlayer();
            setSecondPlayer();
        }

        private static void setFirstPlayer()
        {
            s_GameEngine.SetFirstPlayer();
        }

        private static void setSecondPlayer()
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

                        s_GameEngine.SetSecondPlayer(usersAnswer);
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