using System;
using Xunit;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    public class BishopTests
    {
        [Fact]
        public void MovePiece_DiagonalPathISFree_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_DiagonalPathISNOTFree_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            board.SetPiece<Pawn>( ChessColor.White, 'B', 3 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_MovementISNOTCorrect_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Bishop>( ChessColor.White, 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'E', 7 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
