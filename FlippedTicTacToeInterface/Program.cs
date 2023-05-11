using System;
using FlippedTicTacToe;

namespace FlippedTicTacToeInterface
{
    public class Program
    {
        public static void Main()
        {
            GameEngine g = new GameEngine();
            g.SetFirstPlayer();
            g.SetSecondPlayer(false);
            g.SetGameBoardSize(8);
            g.RestartGame();
            GameConsoleUtils.PrintBoard(g.GameBoard.Board);
        }
    }
}
