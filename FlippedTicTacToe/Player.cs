using System;

namespace FlippedTicTacToe
{
    class Player
    {
        private uint m_Score = 0;
        private readonly eSymbols m_Symbol;
        private readonly bool m_IsComputer;

        public Player(eSymbols i_Symbol, bool i_IsComputer)
        {
            m_Symbol = i_Symbol;
            m_IsComputer = i_IsComputer;
        }

        public uint Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public eSymbols Symbol
        {
            get
            {
                return m_Symbol;
            }
        }

        public bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
        }
    }
}
