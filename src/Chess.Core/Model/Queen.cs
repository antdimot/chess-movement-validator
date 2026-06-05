namespace Chess.Core.Model
{
    /// <summary>
    /// Piece that can move any number of squares along a rank, file, or diagonal. It cannot jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies. It is represented by the letter "Q" followed by the color of the piece (W for white and B for black). For example, "WQ" for a white queen and "BQ" for a black queen.
    /// </summary>
    public class Queen : Piece
    {
        public override string Letter { get { return "QN"; } }

        public Queen( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add(
                new Rule(
                    m => m.StartX - m.EndX == m.StartY - m.EndY
                ) );

            Rules.Add(
                new Rule(
                    m => m.StartX - m.EndX == -( m.StartY - m.EndY )
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndY == m.StartY
                ) );
        }
    }
}
