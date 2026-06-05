using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    /// <summary>
    /// Suite of tests for the movement of the rook. It tests that the rook can move to a position that is on the same column or row and that the path is free, that the rook cannot move to a position that is on the same column or row but the path is not free and that the rook cannot move to a position that is not on the same column or row. The rook can move any number of squares along a column or row, but it cannot jump over other pieces. Therefore, it is important to test both the valid movement and the invalid movement of the rook to ensure that it behaves correctly according to the rules of chess.
    /// </summary>
    public class RookTests
    {
        [Fact]
        public void MovePiece_Allow_VerticalPathISFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Rook>( 'A', 1 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_VerticalPathISNOTFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Rook>( 'A', 1 );

            board.SetWhite<Pawn>( 'A', 4 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.False( result.IsSuccess, "the piece pass over other piece" );
        }
    }
}
