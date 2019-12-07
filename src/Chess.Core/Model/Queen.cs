namespace Chess.Core.Model
{
    public class Queen : Piece
    {
        public override string Letter { get { return "QN"; } }

        public Queen( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            Rules.Add( new Rule(
                        m => m.StartX - m.EndX == m.StartY - m.EndY
                      ) );

            Rules.Add( new Rule(
                      m => m.StartX - m.EndX == -( m.StartY - m.EndY )
                      ) );

            Rules.Add( new Rule(
                       m => m.EndX == m.StartX
                       ) );

            Rules.Add( new Rule(
                        m => m.EndY == m.StartY
                        ) );
        }
    }
}
