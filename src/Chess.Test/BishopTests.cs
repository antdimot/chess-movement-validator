using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    /// <summary>
    /// Suite of tests for the movement of the bishop. It tests that the bishop can move to a position that is on the same diagonal and that the path is free, that the bishop cannot move to a position that is on the same diagonal but the path is not free and that the bishop cannot move to a position that is not on the same diagonal. The bishop can move any number of squares along a diagonal, but it cannot jump over other pieces. Therefore, it is important to test both the valid movement and the invalid movement of the bishop to ensure that it behaves correctly according to the rules of chess.
    /// </summary>
    public class BishopTests
    {
        [Fact]
        public void MovePiece_Allow_DiagonalPathISFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Bishop>( 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_DiagonalPathISNOTFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Bishop>( 'D', 5 );

            board.SetWhite<Pawn>( 'B', 3 );

            var result = board.MovePiece( 'D', 5, 'A', 2 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_MovementISNOTCorrect()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Bishop>( 'D', 5 );

            var result = board.MovePiece( 'D', 5, 'E', 7 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
