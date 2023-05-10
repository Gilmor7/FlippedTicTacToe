namespace FlippedTicTacToe
{
    public struct Cell
    {
        public uint Row { get; }
        public uint Column { get; }

        public Cell(uint i_Row, uint i_Column)
        {
            Row = i_Row;
            Column = i_Column;
        }
    }
}
