using System;
using System.Collections.Generic;

namespace FlippedTicTacToe
{
    public class GameBoard
    {
        private eSymbols[,] m_GameBoard;
        private readonly uint m_MatrixWidth;
        private ulong m_NumberOfEmptyCells;

        public GameBoard(uint i_BoardSize)
        {
            m_MatrixWidth = i_BoardSize;
            m_NumberOfEmptyCells = i_BoardSize * i_BoardSize;
            m_GameBoard = new eSymbols[i_BoardSize, i_BoardSize];
        }

        public ulong NumberOfEmptyCells
        {
            get
            {
                return m_NumberOfEmptyCells;
            }
        }

        public eSymbols[,] Board
        {
            get
            {
                eSymbols[,] deepCopy = new eSymbols[m_MatrixWidth, m_MatrixWidth];
                for (uint i = 0; i < m_MatrixWidth; i++)
                {
                    for (uint j = 0; j < m_MatrixWidth; j++)
                    {
                        deepCopy[i, j] = m_GameBoard[i, j];
                    }
                }
                return deepCopy;
            }
        }

        public eSymbols GetSymbolAtIndex(uint i_Row, uint i_Col)
        {
            bool indicesAreValid = checkIfIndicesAreInRange(i_Row, i_Col);
            if (indicesAreValid)
            {
                return m_GameBoard[i_Row, i_Col];
            }
            throw new IndexOutOfRangeException("Indices are out of range!");
        }

        public void ResetBoard()
        {
            for (uint i = 0; i < m_MatrixWidth; i++)
            {
                for (uint j = 0; j < m_MatrixWidth; j++)
                {
                    m_GameBoard[i, j] = eSymbols.Empty;
                }
            }
            m_NumberOfEmptyCells = m_MatrixWidth * m_MatrixWidth;
        }

        public void UpdateCell(uint i_Row, uint i_Col, eSymbols i_symbol)
        {
            bool indicesAreValid = checkIfIndicesAreInRange(i_Row, i_Col);
            if (indicesAreValid)
            {
                updateNumberOfEmptyCells(m_GameBoard[i_Row, i_Col], i_symbol);
                m_GameBoard[i_Row, i_Col] = i_symbol;
            }
            else
            {
                throw new IndexOutOfRangeException("Indices are out of range!");
            }
        }

        public bool CheckIfCellIsEmpty(uint i_Row, uint i_Col)
        {
            bool indicesAreValid = checkIfIndicesAreInRange(i_Row, i_Col);
            if (indicesAreValid)
            {
                return m_GameBoard[i_Row, i_Col] == eSymbols.Empty;
            }
            else
            {
                throw new IndexOutOfRangeException("Indices are out of range!");
            }
        }

        private bool checkIfIndicesAreInRange(uint i_Row, uint i_Col)
        {
            bool rowCheck = checkIfIndexIsInRange(i_Row);
            bool colCheck = checkIfIndexIsInRange(i_Col);
            return rowCheck && colCheck;
        }

        private bool checkIfIndexIsInRange(uint i_Index)
        {
            return 0 <= i_Index && i_Index < m_MatrixWidth;
        }

        public bool IsBoardEmpty()
        {
            return m_NumberOfEmptyCells == m_MatrixWidth * m_MatrixWidth;
        }

        public bool isBoardFull()
        {
            return m_NumberOfEmptyCells == 0;
        }

        private void updateNumberOfEmptyCells(eSymbols i_MatSymbol, eSymbols i_InputSymbol)
        {
            if (i_MatSymbol == eSymbols.Empty && i_InputSymbol != eSymbols.Empty)
            {
                m_NumberOfEmptyCells--;
            }
            else if (i_MatSymbol != eSymbols.Empty && i_InputSymbol == eSymbols.Empty)
            {
                m_NumberOfEmptyCells++;
            }
        }

        public bool CheckForSingleSymbolFullSequenceInRow(uint i_Row, eSymbols i_Symbol)
        {
            bool singleSymbolFullSequenceFound = true;

            for(int i = 0; i < m_MatrixWidth; i++)
            {
                if(m_GameBoard[i_Row, i] != i_Symbol)
                {
                    singleSymbolFullSequenceFound = false;
                    break;
                }
            }

            return singleSymbolFullSequenceFound;
        }

        public bool CheckForSingleSymbolFullSequenceInColumn(uint i_Col, eSymbols i_Symbol)
        {
            bool singleSymbolFullSequenceFound = true;

            for (int i = 0; i < m_MatrixWidth; i++)
            {
                if (m_GameBoard[i, i_Col] != i_Symbol)
                {
                    singleSymbolFullSequenceFound = false;
                    break;
                }
            }

            return singleSymbolFullSequenceFound;
        }

        public bool CheckForSingleSymbolFullSequenceInDiagonal(Cell i_Cell, eSymbols i_Symbol)
        {
            bool singleSymbolFullSequenceFound = true;
            bool isCellOnMainDiagonal = i_Cell.Row == i_Cell.Column;
            bool isCellOnSecondaryDiagonal = i_Cell.Row + i_Cell.Column == (m_MatrixWidth - 1);

            if(isCellOnMainDiagonal)
            {
                for (int i = 0; i < m_MatrixWidth; i++)
                {
                    if (m_GameBoard[i, i] != i_Symbol)
                    {
                        singleSymbolFullSequenceFound = false;
                        break;
                    }
                }
            }
            else if(isCellOnSecondaryDiagonal)
            {
                for (int i = 0; i < m_MatrixWidth; i++)
                {
                    int row = (int)m_MatrixWidth - i - 1;

                    if (m_GameBoard[row, i] != i_Symbol)
                    {
                        singleSymbolFullSequenceFound = false;
                        break;
                    }
                }
            }
            else
            {
                singleSymbolFullSequenceFound = false;
            }

            return singleSymbolFullSequenceFound;
        }

        public List<Cell> GetAllAvailableCells()
        {
            List<Cell> availableCells = new List<Cell>();

            for (uint i = 0; i < m_MatrixWidth; i++)
            {
                for (uint j = 0; j < m_MatrixWidth; j++)
                {
                    if (m_GameBoard[i, j] == eSymbols.Empty)
                    {
                        availableCells.Add(new Cell(i, j));
                    }
                }
            }

            return availableCells;
        }

    }
}
