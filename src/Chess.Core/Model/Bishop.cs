namespace Chess.Core.Model
{
    public class Bishop : Piece
    {
        public override string Letter { get { return "BS"; } }

        public Bishop( PieceColor color ) : base( color ) { }

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
