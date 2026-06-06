using Chess.Core.Model;

namespace Chess.Core.Model
{
    /// <summary>
    /// Class that represents the result of a movement. It contains a boolean that indicates if the movement is correct, a description of the movement and a boolean that indicates if the movement is a capture. If the movement is a capture, it also contains the piece that was captured.
    /// </summary>
    public class MovementResult
    {
        public MovementResult()
        {
            IsSuccess = true;
            Description = "The movement is correct.";
        }

        /// <summary>
        /// Indicates if the movement is correct. It is true if the movement is correct and false if the movement is incorrect. The movement is incorrect if the target position is out of the bounds, if the current position and target position are equals, if there is no piece at start position, if the piece at start position does not belong to the player, if the movement is not valid for the piece or if there is a piece at target position that belongs to the player.
        /// </summary>
        public bool IsSuccess { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Indicates if the movement is a capture. It is true if there is a piece at target position that belongs to the opponent and false if there is no piece at target position or if there is a piece at target position that belongs to the player.
        /// </summary>
        public bool Capture { get; set; }

        /// <summary>
        /// If the movement is a capture, it contains the piece that was captured. It is null if the movement is not a capture.
        /// </summary>
        public Piece? CapturedPiece { get; set; }
    }
}
