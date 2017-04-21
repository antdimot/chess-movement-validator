using Xunit;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    public class RookTests
    {
        [Fact]
        public void MovePiece_VerticalPathISFree_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Rook>( ChessColor.White, 'A', 1 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_VerticalPathISNOTFree_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Rook>( ChessColor.White, 'A', 1 );

            board.SetPiece<Pawn>( ChessColor.White, 'A', 4 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.False( result.IsSuccess, "the piece pass over other piece" );
        }
    }
}
