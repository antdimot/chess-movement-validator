namespace Chess.Core.Model
{
    public class Rook : Piece
    {
        public Rook( ChessColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add( new Rule(
                      m => m.EndX == m.StartX
                      ) );

            Rules.Add( new Rule(
                      m => m.EndY == m.StartY
                      ) );
        }
    }
}
