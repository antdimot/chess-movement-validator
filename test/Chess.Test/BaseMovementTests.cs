using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    public class BaseMovementTests
    {
        [Fact]
        public void MovePiece_NotAllow_BusyPostionWithSameColor()
        {
            var board = Board.NewGame();

            board.AddPiece<Pawn,White>( 'A', 5 );
            board.AddPiece<Pawn,White>( 'A', 6 );

            var result = board.MovePiece( 'A', 5, 'A', 6 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
