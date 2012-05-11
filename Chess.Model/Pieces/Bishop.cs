using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model.Pieces
{
    public class Bishop : Piece
    {
        public Bishop( ChessColor color ) : base( color )
        {
        }

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
