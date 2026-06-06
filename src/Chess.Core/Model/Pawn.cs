namespace Chess.Core.Model
{
    /// <summary>
    /// piece that can move forward one square. On its first move, it can move forward two squares. It cannot move backward. It cannot jump over other pieces. It can capture an opponent's piece by moving to the square that the opponent's piece occupies, but only if the opponent's piece is one square diagonally in front of the pawn. It is represented by the letter "PA" followed by the color of the piece (W for white and B for black). For example, "WPA" for a white pawn and "BPA" for a black pawn.
    /// </summary>
    public class Pawn : Piece
   {
      public override string Letter { get { return "PA"; } }

      public Pawn( PieceColor color ) : base( color ) { }

      public override void InitializeRules()
      {
         // white pawn rules
         Rules.Add( new Rule(
                     m => Color == 'W',
                     m => !m.WithCaputure,
                     m => m.EndX == m.StartX,
                     m => m.EndY == m.StartY + 1
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'W',
                     m => !m.WithCaputure,
                     m => m.StartY == 1,
                     m => m.EndX == m.StartX,
                     m => m.EndY == m.StartY + 2
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'W',
                     m => m.WithCaputure,
                     m => m.EndX == m.StartX + 1,
                     m => m.EndY == m.StartY + 1
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'W',
                     m => m.WithCaputure,
                     m => m.EndX == m.StartX - 1,
                     m => m.EndY == m.StartY + 1
                  ) );


         // black pawn rules
         Rules.Add( new Rule(
                     m => Color == 'B',
                     m => !m.WithCaputure,
                     m => m.EndX == m.StartX,
                     m => m.EndY == m.StartY - 1
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'B',
                     m => !m.WithCaputure,
                     m => m.StartY == 6,
                     m => m.EndX == m.StartX,
                     m => m.EndY == m.StartY - 2
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'B',
                     m => m.WithCaputure,
                     m => m.EndX == m.StartX + 1,
                     m => m.EndY == m.StartY - 1
                  ) );

         Rules.Add( new Rule(
                     m => Color == 'B',
                     m => m.WithCaputure,
                     m => m.EndX == m.StartX - 1,
                     m => m.EndY == m.StartY - 1
                  ) );
        }
    }
}
