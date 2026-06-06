using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    /// <summary>
    /// Suite of tests for the base movement of pieces. It tests that a piece cannot move to a position that is occupied by a piece of the same color. This is a basic rule of chess that applies to all pieces, so it is tested in a separate suite of tests. Each piece has its own rules of movement, so the specific movement rules are tested in the tests for each piece. This suite of tests focuses on the general rules of movement that apply to all pieces, such as not being able to move to a position that is occupied by a piece of the same color.
    /// </summary>
    public class BaseMovementTests
    {
        [Fact]
        public void MovePiece_NotAllow_BusyPostionWithSameColor()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'A', 5 );
            board.SetWhite<Pawn>( 'A', 6 );

            var result = board.MovePiece( 'A', 5, 'A', 6 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
