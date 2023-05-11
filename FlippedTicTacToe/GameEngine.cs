using System;
using System.Collections.Generic;

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

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public GameBoard GameBoard
        {
            get
            {
                return m_Board;
            }
        }

        public void SetGameBoardSize(uint i_BoardSize)
        {
            if (RulesValidator.IsBoardSizeValid(i_BoardSize))
            {
                m_Board = new GameBoard(i_BoardSize);
            }
            else
            {
                throw new ArgumentException("Board size must be in range of 3-9 (inclusive)!");
            }
        }

        public void SetFirstPlayer()
        {
            m_Player1 = new Player(eSymbols.X, false);
            m_CurrentPlayer = m_Player1;
        }

        public void SetSecondPlayer(bool i_IsComputer)
        {
            m_Player2 = new Player(eSymbols.O, i_IsComputer);
        }

        public void RestartGame()
        {
            m_Board.ResetBoard();
            initializeGameStatus();
            m_CurrentPlayer = m_Player1;
        }

        private void initializeGameStatus()
        {
            m_GameStatus = eGameStatus.InProgress;
        }

        public void MakeMove(Cell i_SelectedCell)
        {
            try
            {
                bool isCellEmpty = m_Board.CheckIfCellIsEmpty(i_SelectedCell.Row, i_SelectedCell.Column);

                if(!isCellEmpty)
                {
                    throw new ArgumentException("The specified cell is already taken");
                }

                m_Board.UpdateCell(i_SelectedCell.Row, i_SelectedCell.Column, m_CurrentPlayer.Symbol);
                updateGameStatusAndScoreIfNeeded(i_SelectedCell);
                switchCurrentPlayer();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void MakeRandomMove()
        {
            List<Cell> availableCells = m_Board.GetAllAvailableCells();
            Cell selectedCell = selectRandomCellFromList(availableCells);

            m_Board.UpdateCell(selectedCell.Row, selectedCell.Column, m_CurrentPlayer.Symbol);
            updateGameStatusAndScoreIfNeeded(selectedCell);
            switchCurrentPlayer();
        }

        private static Cell selectRandomCellFromList(List<Cell> i_CellsList)
        {
            Random rand = new Random();
            int randomListItemIndex = rand.Next(i_CellsList.Count);

            return i_CellsList[randomListItemIndex];
        }

        private void updateGameStatusAndScoreIfNeeded(Cell i_SelectedCell)
        {
            bool isCurrentPlayerLoose = checkIfCurrentPlayerLoose(i_SelectedCell);
            bool isBoardFull = m_Board.IsBoardFull();

            if (isCurrentPlayerLoose)
            {
                if(m_CurrentPlayer == m_Player1)
                {
                    m_GameStatus = eGameStatus.Player2Win;
                    m_Player2.Score++;
                }
                else
                {
                    m_GameStatus = eGameStatus.Player1Win;
                    m_Player1.Score++;
                }
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
            public static bool IsBoardSizeValid(uint i_BoardSize)
            {
                return i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize;
            }
        }
     
    }
}
