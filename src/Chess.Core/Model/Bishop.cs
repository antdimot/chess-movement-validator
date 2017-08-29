namespace Chess.Core.Model
{
    public class Bishop : Piece
    {
        public Bishop( ChessColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add( new Rule(
                        m => m.StartX - m.EndX == m.StartY - m.EndY
                        ) );

            Rules.Add( new Rule(
                        m => m.StartX - m.EndX == -( m.StartY - m.EndY )
                        ) );
        }
    }
}
