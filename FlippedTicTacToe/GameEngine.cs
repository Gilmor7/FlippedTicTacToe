using System;

namespace FlippedTicTacToe
{
    class GameEngine
    {
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        private GameBoard m_Board = null;
        private Player m_Player1 = null;
        private Player m_Player2 = null;
        private Player m_CurrentPlayer = null;
        private bool m_IsGameRunning = false;
        // Maybe should be saved as enum game status??? 

        // should we use constructor after getting input from user or method of init instead?

        public GameBoard GetGameBoard()
        {
            return m_Board; // if we want a clone maybe we should create clone method instead of getter?
        }

        public bool MakeMove(int i_Line, int i_Column)
        {
            //Todo: complete method
            return true;
        }
        public static bool IsBoardSizeValid(int i_BoardSize)
        {
            return i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize;
        }
    }
}
