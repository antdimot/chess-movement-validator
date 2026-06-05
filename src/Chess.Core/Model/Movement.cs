using System;

namespace Chess.Core.Model
{
    /// <summary>
    /// Struct that represents a movement. It contains a boolean that indicates if the movement is a capture, the start position of the movement and the end position of the movement.
    /// </summary>
    public struct Movement
    {
        public bool WithCaputure;
        public int StartX, StartY;
        public int EndX, EndY;        
    }
}
