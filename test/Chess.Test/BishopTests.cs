using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
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
