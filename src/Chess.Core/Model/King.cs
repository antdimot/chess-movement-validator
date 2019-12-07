namespace Chess.Core.Model
{ 
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
