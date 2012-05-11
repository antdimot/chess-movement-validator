using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Chess.Model
{
    public abstract class Piece
    {
        protected ChessColor Color;

        public ChessColor ChessColor
        {
            get
            {
                return Color;
            }
        }

        protected Collection<Rule> Rules;

        public Piece( ChessColor color )
        {
            Rules = new Collection<Rule>();

            Color = color;

            InitializeRules();
        }

        public bool IsValidMovement( bool withEat, int startRow, int startColumn, int endRow, int endColumn )
        {
            var movement = new Movement
            {
                WithEat = withEat,
                StartX = startColumn,
                StartY = startRow,
                EndX = endColumn,
                EndY = endRow
            };

            return Rules.Where( r => r.Validate( movement ) ).Any();
        }

        public override string ToString()
        {
            return String.Format( "{0} {1}", Color.ToString(), this.GetType().Name );
        }

        public abstract void InitializeRules();
    }
}
