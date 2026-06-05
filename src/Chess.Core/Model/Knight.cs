namespace Chess.Core.Model
{
    /// <summary>
    /// Piece that can move in an "L" shape. It can move two squares in one direction and then one square in a perpendicular direction. It can jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies. It is represented by the letter "KN" followed by the color of the piece (W for white and B for black). For example, "WKN" for a white knight and "BKN" for a black knight.
    /// </summary>
    public class Knight : Piece
    {
        public override string Letter { get { return "KN"; } }

        public Knight( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX + 1,
                    m => m.EndY == m.StartY + 2
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX - 1,
                    m => m.EndY == m.StartY + 2
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX - 1,
                    m => m.EndY == m.StartY - 2
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX + 1,
                    m => m.EndY == m.StartY - 2
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX + 2,
                    m => m.EndY == m.StartY + 1
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX + 2,
                    m => m.EndY == m.StartY - 1
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX - 2,
                    m => m.EndY == m.StartY + 1
                ) );

            Rules.Add(
                new Rule(
                    m => m.EndX == m.StartX - 2,
                    m => m.EndY == m.StartY - 1
                ) );
        }
    }
}
