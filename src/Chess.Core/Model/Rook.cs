namespace Chess.Core.Model
{
    /// <summary>
    /// Piece that can move any number of squares along a rank or file. It cannot jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies. It is represented by the letter "R" followed by the color of the piece (W for white and B for black). For example, "WR" for a white rook and "BR" for a black rook.
    /// </summary>
    public class Rook : Piece
    {
        public override string Letter { get { return "RK"; } }

        public Rook( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
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
