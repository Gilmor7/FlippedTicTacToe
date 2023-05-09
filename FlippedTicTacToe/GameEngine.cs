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
        private eGameStatus gameStatus = eGameStatus.InProgress;
        // Maybe should be saved as enum game status??? 

        public GameBoard GetGameBoard()
        {
            return m_Board;
        }

        public bool MakeMove(int i_Line, int i_Column)
        {
            //Todo: complete method
            return true;
        }

        public class RulesValidator
        { 
            public static bool IsBoardSizeValid(int i_BoardSize)
            {
                return i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize;
            }
        }
     
    }
}
