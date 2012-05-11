using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    [TestClass]
    public class RookTests
    {
        [TestMethod]
        public void MovePiece_VerticalPathISFree_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Rook>( ChessColor.White, 'A', 1 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.IsTrue( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_VerticalPathISNOTFree_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Rook>( ChessColor.White, 'A', 1 );

            board.SetPiece<Pawn>( ChessColor.White, 'A', 4 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.IsFalse( result.IsSuccess, "the piece pass over other piece" );
        }
    }
}
