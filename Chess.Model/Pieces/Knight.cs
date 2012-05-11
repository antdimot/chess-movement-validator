using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model.Pieces
{
    public class Knight : Piece
    {
        public Knight( ChessColor color ) : base( color )
        {
        }

        public override void InitializeRules()
        {
            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY + 2
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY + 2
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY - 2
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY - 2
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 2,
                        m => m.EndY == m.StartY + 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX + 2,
                        m => m.EndY == m.StartY - 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 2,
                        m => m.EndY == m.StartY + 1
                        ) );

            Rules.Add( new Rule(
                        m => m.EndX == m.StartX - 2,
                        m => m.EndY == m.StartY - 1
                        ) );
        }
    }
}
