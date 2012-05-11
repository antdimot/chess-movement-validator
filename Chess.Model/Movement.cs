using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public struct Movement
    {
        public bool WithEat;
        public int StartX, StartY;
        public int EndX, EndY;

        public Movement( int startX, int startY, int endX, int endY )
            : this( false, startX, startY, endX, endY )
        {
            if( endX < 1 || endX > 8 || endY < 1 || endY > 8 )
            {
                throw new Exception( "target position is out of the bounds" );
            }

            if( ( startX == endX ) && ( startY == endY ) )
            {
                throw new Exception( "current position and target position are equals" );
            }

            StartX = startX;
            StartY = startY;

            EndX = endX;
            EndY = endY;
        }

        public Movement( bool withEat, int startX, int startY, int endX, int endY )
        {
            WithEat = withEat;

            if( endX < 1 || endX > 8 || endY < 1 || endY > 8 )
            {
                throw new Exception( "target position is out of the bounds" );
            }

            if( ( startX == endX ) && ( startY == endY ) )
            {
                throw new Exception( "current position and target position are equals" );
            }

            StartX = startX;
            StartY = startY;

            EndX = endX;
            EndY = endY;
        }
    }
}
