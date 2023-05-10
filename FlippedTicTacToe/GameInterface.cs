using System;

namespace FlippedTicTacToe
{
    public class GameInterface
    {
        private static GameEngine s_GameEngine = null;
        private const bool k_WaitingForValidInput = true;

        public static void StartGame()
        {
            bool stillWantToPlay = true;

            s_GameEngine = new GameEngine();
            initGameEngineSettings();
            while (stillWantToPlay)
            {
                runGameLoop();
                stillWantToPlay = askIfPlayerWantsToContinuePlaying();
            }
        }

        private static bool askIfPlayerWantsToContinuePlaying()
        {
            string userInput;
            bool userAnswer;

            while (k_WaitingForValidInput)
            {
                Console.WriteLine("Do you want to play again? Y/N");
                userInput = Console.ReadLine();
                switch (userInput.ToUpper())
                {
                    case "Y":
                        userAnswer = true;
                        break;
                    case "N":
                        userAnswer = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Input! try again!");
                        continue;
                }

                return userAnswer;
            }
        }

        private static void runGameLoop()
        {
            /*
            while game is still running:
            clear console
            display board
            if curr player is bot:
                make random move
            else:
                ask for move
                make move
            check game status
            
             */

            throw new NotImplementedException();
        }

        private static void initGameEngineSettings()
        {
            setBoardFromUserInput();
            setFirstPlayer();
            setSecondPlayerFromUserInput();
            // s_GameEngine.CurrentPlayer = FirstPlayer
        }

        private static void setFirstPlayer()
        {
            throw new NotImplementedException();          
        }

        private static void setSecondPlayerFromUserInput()
        {
            throw new NotImplementedException();
        }

        private static void setBoardFromUserInput()
        {
            const bool k_WaitingForValidInput = true;
            int sizeOfBoard;

            while (k_WaitingForValidInput)
            {
                try
                {
                    sizeOfBoard = getSizeOfBoardFromUser();
                    GameEngine.SetBoardSize(sizeOfBoard);
                    return;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please Try Again!");
                    continue;
                }
            }
        }
    }
}
