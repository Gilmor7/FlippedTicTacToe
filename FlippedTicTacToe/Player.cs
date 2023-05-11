using System;

namespace FlippedTicTacToe
{
    public class Player
    {
        private uint m_Score = 0;
        private readonly eSymbols r_Symbol;
        private readonly bool r_IsComputer;

        public Player(eSymbols i_Symbol, bool i_IsComputer)
        {
            r_Symbol = i_Symbol;
            r_IsComputer = i_IsComputer;
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
                return r_Symbol;
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_IsComputer;
            }
        }
    }
}
