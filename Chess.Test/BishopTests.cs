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
    public class BishopTests
    {
        [TestMethod]
        public void MovePiece_DiagonalPathISFree_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.IsTrue( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_DiagonalPathISNOTFree_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            board.SetPiece<Pawn>( ChessColor.White, 'B', 3 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.IsFalse( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_MovementISNOTCorrect_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'E', 7 );

            Assert.IsFalse( result.IsSuccess, result.Description );
        }
    }
}
