namespace Chess.Core.Model
{
    /// <summary>
    /// Piece that can move diagonally any number of squares. It cannot jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies. It is represented by the letter "B" followed by the color of the piece (W for white and B for black). For example, "WB" for a white bishop and "BB" for a black bishop.
    /// </summary>
    public class Bishop : Piece
    {
        public override string Letter { get { return "BS"; } }

        public Bishop( PieceColor color ) : base( color ) { }

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
        }
    }
}
