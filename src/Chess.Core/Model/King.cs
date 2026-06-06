namespace Chess.Core.Model
{
    /// <summary>
    /// Piece that can move one square in any direction. It cannot jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies. It is represented by the letter "K" followed by the color of the piece (W for white and B for black). For example, "WK" for a white king and "BK" for a black king.
    /// </summary>
    public class King : Piece
    {
        public override string Letter { get { return "KG"; } }

        public King( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY + 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY - 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY + 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY - 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY + 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY - 1
                        ) );
        }
    }
}
