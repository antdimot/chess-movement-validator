using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model.Pieces
{
    public class King : Piece
    {
        public King( ChessColor color ) : base( color )
        {
        }

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
