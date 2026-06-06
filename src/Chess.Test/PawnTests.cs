using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    /// <summary>
    /// Suite of tests for the movement of the pawn. It tests that the pawn can move two steps from the start position, that the pawn can move one step from the start position, that the pawn can capture from any position, that the pawn cannot capture from any position, that the pawn cannot move three steps from the start position and that the pawn cannot move one step back from any position. The pawn has specific rules of movement, such as being able to move two steps from the start position and being able to capture diagonally. Therefore, it is important to test both the valid movement and the invalid movement of the pawn to ensure that it behaves correctly according to the rules of chess.
    /// </summary>
    public class PawnTests
    {
        /// <summary>
        /// Test that the pawn can move two steps from the start position. The pawn can move two steps from the start position if it is on its initial square and if the path is clear. This test sets a white pawn on the A2 square and then attempts to move it to the A4 square. The test asserts that the move is successful, which means that the pawn was able to move two steps from its start position.
        /// </summary>
        [Fact]
        public void MovePiece_Allow_TwoStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 4 );

            Assert.True( result.IsSuccess, result.Description );
        }

        /// <summary>
        /// Test case that the pawn can move one step from the start position. The pawn can move one step from the start position if it is on its initial square and if the path is clear. This test sets a white pawn on the A2 square and then attempts to move it to the A3 square. The test asserts that the move is successful, which means that the pawn was able to move one step from its start position. Additionally, the test checks that the old position of the pawn (A2) is now empty and that the new position (A3) contains a white pawn.
        /// </summary>
        [Fact]
        public void MovePiece_Allow_OneStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 3 );

            Assert.True( result.IsSuccess, result.Description );

            var oldPosition = board.GetPiece( 'A', 2 );

            Assert.Null( oldPosition );

            var newPosition = board.GetPiece( 'A', 3 );

            Assert.NotNull( newPosition );

            Assert.IsType<Pawn>( newPosition );

            Assert.True( newPosition.Color == 'W', "The piece at new position is different." );
        }

        /// <summary>
        /// Test case that the pawn can capture from any position. The pawn can capture an opponent's piece diagonally if the target square is occupied by an opponent's piece. This test sets a white pawn on the B4 square and a black pawn on the C5 square. The test then attempts to move the white pawn to the C5 square, capturing the black pawn. The test asserts that the move is successful, that a capture occurred, and that the captured piece is a pawn.
        /// </summary>
        [Fact]
        public void MovePiece_Allow_WihCaptureFromAnyPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'B', 4 );

            board.SetBlack<Pawn>( 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.True( result.IsSuccess, result.Description );

            Assert.True( result.Capture );

            Assert.NotNull( result.CapturedPiece );

            Assert.IsType<Pawn>( result.CapturedPiece );
        }

        /// <summary>
        /// Test case that the pawn cannot capture from any position. The pawn cannot capture an opponent's piece if the target square is not occupied by an opponent's piece. This test sets a white pawn on the B4 square and another white pawn on the C5 square. The test then attempts to move the white pawn on B4 to the C5 square, which should not be allowed since it is occupied by a piece of the same color. The test asserts that the move is not successful, which means that the pawn was not able to capture from that position.
        /// </summary>
        [Fact]
        public void MovePiece_NotAllow_WithCaptureFromAnyPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'B', 4 );

            board.SetWhite<Pawn>( 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        /// <summary>
        /// Test case that the pawn cannot move three steps from the start position. The pawn cannot move three steps from the start position because it is not allowed by the rules of chess. This test sets a white pawn on the A2 square and then attempts to move it to the A5 square, which is three steps away. The test asserts that the move is not successful, which means that the pawn was not able to move three steps from its start position.
        /// </summary>
        [Fact]
        public void MovePiece_NotAllow_ThreeStepFromStartPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        /// <summary>
        /// Test case that the pawn cannot move one step back from any position. The pawn cannot move one step back from any position because it is not allowed by the rules of chess. This test sets a white pawn on the A5 square and then attempts to move it to the A4 square, which is one step back. The test asserts that the move is not successful, which means that the pawn was not able to move one step back from that position.
        /// </summary>
        [Fact]
        public void MovePiece_NotAllow_OneStepBackFromAnyPosition()
        {
            var board = Board.NewGame();

            board.SetWhite<Pawn>( 'A', 5 );

            var result = board.MovePiece( 'A', 5, 'A', 4 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
