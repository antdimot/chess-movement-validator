using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Chess.Core.Model
{
    /// <summary>
    /// Base class for chess pieces. It contains the color of the piece and the rules of movement for the piece.
    /// </summary>
    public abstract class Piece
    {
        public abstract string Letter { get; }
        public char Color { get; private set; }
        
        public Boolean IsAlive { get; set; } = true;

        protected Collection<Rule> Rules = [];

        public Piece( PieceColor color )
        {
            Color = ( color == PieceColor.White ? 'W' : 'B' );

            InitializeRules();
        }

        /// <summary>
        /// Checks if the movement is valid for the piece. It checks if the movement is valid for any of the rules of the piece.
        /// </summary>
        /// <param name="withCaputure"></param>
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="endRow"></param>
        /// <param name="endColumn"></param>
        /// <returns></returns>
        public bool IsValidMovement( bool withCaputure, int startRow, int startColumn, int endRow, int endColumn )
        {
            var movement = new Movement
            {
                WithCaputure = withCaputure,
                StartX = startColumn,
                StartY = startRow,
                EndX = endColumn,
                EndY = endRow
            };

            return Rules.Where( r => r.Validate( movement ) ).Any();
        }

        /// <summary>
        /// Initializes the rules of movement for the piece. Each piece has its own rules of movement, so this method is implemented in each piece class.
        /// </summary>
        public abstract void InitializeRules();

        /// <summary>
        /// Returns a string representation of the piece, which is the letter of the piece followed by the color of the piece. For example, "WP" for a white pawn and "BK" for a black king.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Letter}{Color}";
    }
}
