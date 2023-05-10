using System;

namespace FlippedTicTacToe
{
    public class GameEngine
    {
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        private GameBoard m_Board = null;
        private Player m_Player1 = null;
        private Player m_Player2 = null;
        private Player m_CurrentPlayer = null;
        private eGameStatus m_GameStatus = eGameStatus.InProgress;

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public void InitializeGameStatus()
        {
            m_GameStatus = eGameStatus.InProgress;
        }

        public GameBoard GetGameBoard()
        {
            return m_Board;
        }

        public void MakeMove(Cell i_SelectedCell)
        {
            try
            {
                bool isCellEmpty = m_Board.CheckIfCellIsEmpty(i_SelectedCell.Row, i_SelectedCell.Column);
                if(!isCellEmpty)
                {
                    throw new Exception("The specified cell is already taken");
                }

                m_Board.UpdateCell(i_SelectedCell.Row, i_SelectedCell.Column, m_CurrentPlayer.Symbol);
                updateGameStatusIfNeeded(i_SelectedCell);
                switchCurrentPlayer();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void MakeRandomMove()
        {
            //moves = getAllPossibleMoves
            //selectRandomMove
            //selectedCell = selectRandomCellFromList();
            //m_Board.UpdateCell(selectedCell.Row, selectedCell.Column, m_CurrentPlayer.Symbol);
            //updateGameStatusIfNeeded(selectedCell);
            //switchCurrentPlayer();
        }

        private void updateGameStatusIfNeeded(Cell i_SelectedCell)
        {
            bool isCurrentPlayerLoose = checkIfCurrentPlayerLoose(i_SelectedCell);
            bool isBoardFull = m_Board.isBoardFull();

            if (isCurrentPlayerLoose)
            {
                m_GameStatus = m_CurrentPlayer == m_Player1 ? eGameStatus.Player2Win : eGameStatus.Player1Win;
            }
            else if (isBoardFull)
            {
                m_GameStatus = eGameStatus.Draw;
            }
        }

        private void switchCurrentPlayer()
        {
            if (m_CurrentPlayer == m_Player1)
            {
                m_CurrentPlayer = m_Player2;
            }
            else
            {
                m_CurrentPlayer = m_Player1;
            }
        }

        private bool checkIfCurrentPlayerLoose(Cell i_SelectedCell)
        {
            bool isSingleSymbolFullSequenceFound = 
                m_Board.CheckForSingleSymbolFullSequenceInRow(i_SelectedCell.Row, m_CurrentPlayer.Symbol) ||
                m_Board.CheckForSingleSymbolFullSequenceInColumn(i_SelectedCell.Row, m_CurrentPlayer.Symbol) ||
                m_Board.CheckForSingleSymbolFullSequenceInDiagonal(i_SelectedCell, m_CurrentPlayer.Symbol);

            return isSingleSymbolFullSequenceFound;
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
