using System;
using Xunit;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    public class BaseMovementTests
    {
        [Fact]
        public void MovePiece_NotAllow_ToBusyPostionWithSameColor()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 5 );
            board.SetPiece<Pawn>( ChessColor.White, 'A', 6 );

            var result = board.MovePiece( 'A', 5, 'A', 6 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
