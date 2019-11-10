namespace Chess.Core.Model
{
    public class Pawn : Piece
    {
      //   public Pawn( ChessColor color ) : base( color ) { }

        public Pawn( PieceColor color ) : base( color ) { }

        public override void InitializeRules()
        {
            // white pawn rules
            Rules.Add( new Rule(
                       m => Color == PieceColor.White,
                       m => !m.WithCaputure,
                       m => m.EndX == m.StartX,
                       m => m.EndY == m.StartY + 1
                     ) );

            Rules.Add( new Rule(
                        m => Color == PieceColor.White,
                        m => !m.WithCaputure,
                        m => m.StartY == 1,
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY + 2
                     ) );

            Rules.Add( new Rule(
                       m => Color == PieceColor.White,
                       m => m.WithCaputure,
                       m => m.EndX == m.StartX + 1,
                       m => m.EndY == m.StartY + 1
                    ) );

            Rules.Add( new Rule(
                       m => Color == PieceColor.White,
                       m => m.WithCaputure,
                       m => m.EndX == m.StartX - 1,
                       m => m.EndY == m.StartY + 1
                    ) );


            // black pawn rules
            Rules.Add( new Rule(
                       m => Color == PieceColor.Black,
                       m => !m.WithCaputure,
                       m => m.EndX == m.StartX,
                       m => m.EndY == m.StartY - 1
                     ) );

            Rules.Add( new Rule(
                        m => Color == PieceColor.Black,
                        m => !m.WithCaputure,
                        m => m.StartY == 6,
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY - 2
                     ) );

            Rules.Add( new Rule(
                       m => Color == PieceColor.Black,
                       m => m.WithCaputure,
                       m => m.EndX == m.StartX + 1,
                       m => m.EndY == m.StartY - 1
                    ) );

            Rules.Add( new Rule(
                       m => Color == PieceColor.Black,
                       m => m.WithCaputure,
                       m => m.EndX == m.StartX - 1,
                       m => m.EndY == m.StartY - 1
                    ) );
        }
    }
}
