using Chess.Core.Model;

namespace Chess.Core
{
    public class MovementResult
    {
        public MovementResult()
        {
            IsSuccess = true;
            Description = "The movement is correct.";
        }

        public bool IsSuccess { get; set; }

        public string Description { get; set; }
        
        public bool Capture { get; set; }

        public Piece CapturedPiece { get; set; }
    }
}
