using System;

namespace Chess.Core.Model
{
    public struct Movement
    {
        public bool WithCaputure;
        public int StartX, StartY;
        public int EndX, EndY;        
    }
}
