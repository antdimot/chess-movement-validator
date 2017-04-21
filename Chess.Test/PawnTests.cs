using System;
using Xunit;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    public class PawnTests
    {
        [Fact]
        public void MovePiece_Allow_TwoStepFromStartPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 4 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_Allow_OneStepFromStartPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 3 );

            Assert.True( result.IsSuccess, result.Description );

            var oldPosition = board.GetPiece( 'A', 2 );

            Assert.Null( oldPosition );

            var newPosition = board.GetPiece( 'A', 3 );

            Assert.NotNull( newPosition );

            Assert.IsType<Pawn>( newPosition );

            Assert.True( newPosition.ChessColor == ChessColor.White, "The piece at new position is different." );
        }

        [Fact]
        public void MovePiece_Allow_WihEatFromAnyPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.Black, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.True( result.IsSuccess, result.Description );

            Assert.True( result.Ate );

            Assert.NotNull( result.AtePiece );

            Assert.IsType<Pawn>( result.AtePiece );
        }

        [Fact]
        public void MovePiece_Allow_WithEatFromAnyPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.White, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_Allow_ThreeStepFromStartPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_Allow_OneStepBackFromAnyPosition()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 5 );

            var result = board.MovePiece( 'A', 5, 'A', 4 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
