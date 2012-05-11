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
    public class PawnTests
    {
        [TestMethod]
        public void MovePiece_TwoStepFromStartPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 4 );

            Assert.IsTrue( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_OneStepFromStartPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 3 );

            Assert.IsTrue( result.IsSuccess, result.Description );

            var oldPosition = board.GetPiece( 'A', 2 );

            Assert.IsNull( oldPosition, "There is still a piece at old position" );

            var newPosition = board.GetPiece( 'A', 3 );

            Assert.IsNotNull( newPosition, "There is not a piece at new position" );

            Assert.IsInstanceOfType( newPosition, typeof( Pawn ), "The piece at new position is different." );

            Assert.IsTrue( newPosition.ChessColor == ChessColor.White, "The piece at new position is different." );
        }

        [TestMethod]
        public void MovePiece_WihEatFromAnyPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.Black, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.IsTrue( result.IsSuccess, result.Description );

            Assert.IsTrue( result.Ate, "Ate property was not set correctly" );

            Assert.IsNotNull( result.AtePiece, "AtePiece property was not set correctly" );

            Assert.IsInstanceOfType( result.AtePiece, typeof(Pawn), "AtePiece property was not set correctly" );
        }

        [TestMethod]
        public void MovePiece_WithEatFromAnyPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.White, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.IsFalse( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_ThreeStepFromStartPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 5 );

            Assert.IsFalse( result.IsSuccess, result.Description );
        }

        [TestMethod]
        public void MovePiece_OneStepBackFromAnyPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 5 );

            var result = board.MovePiece( 'A', 5, 'A', 4 );

            Assert.IsFalse( result.IsSuccess, result.Description );
        }
    }
}
