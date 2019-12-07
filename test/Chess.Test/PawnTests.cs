using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    public class PawnTests
    {
        [Fact]
        public void MovePiece_Allow_TwoStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 4 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_Allow_OneStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 3 );

            Assert.True( result.IsSuccess, result.Description );

            var oldPosition = board.GetPiece( 'A', 2 );

            Assert.Null( oldPosition );

            var newPosition = board.GetPiece( 'A', 3 );

            Assert.NotNull( newPosition );

            Assert.IsType<Pawn>( newPosition );

            Assert.True( newPosition.Color == PieceColor.White, "The piece at new position is different." );
        }

        [Fact]
        public void MovePiece_Allow_WihCaptureFromAnyPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'B', 4 );

            board.AddBlackPiece<Pawn>( 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.True( result.IsSuccess, result.Description );

            Assert.True( result.Capture );

            Assert.NotNull( result.CapturedPiece );

            Assert.IsType<Pawn>( result.CapturedPiece );
        }

        [Fact]
        public void MovePiece_NotAllow_WithCaptureFromAnyPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'B', 4 );

            board.AddWhitePiece<Pawn>( 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_ThreeStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_OneStepBackFromAnyPosition()
        {
            var board = Board.NewGame();

            board.AddWhitePiece<Pawn>( 'A', 5 );

            var result = board.MovePiece( 'A', 5, 'A', 4 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
