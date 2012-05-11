using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public class MovementResult
    {
        public MovementResult()
        {
            IsSuccess = true;
            Description = "The movement is correct.";
        }

        public bool IsSuccess
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public bool Ate
        {
            get;
            set;
        }

        public Piece AtePiece
        {
            get;
            set;
        }
    }
}
