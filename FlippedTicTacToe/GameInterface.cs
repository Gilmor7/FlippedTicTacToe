using System;

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
                int boardSize;
                return int.TryParse(i_BoardSize, out boardSize);
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
                        GameEngine.ResetGame();
                    }
                    else
                    {
                        stillPlaying = false;
                    }
                }

            }

            Console.WriteLine("Thanks for playing! Goodbye :)");
        }
    }
}
