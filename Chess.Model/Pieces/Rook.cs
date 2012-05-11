using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model.Pieces
{
    public class Rook : Piece
    {
        public Rook( ChessColor color ) : base( color )
        {
        }

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
