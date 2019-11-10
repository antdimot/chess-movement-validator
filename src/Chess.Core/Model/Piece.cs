using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace Chess.Core.Model
{
    public abstract class Piece
    {
        public int Id { get; set; }
        
        public PieceColor Color { get; private set; }

        protected Collection<Rule> Rules;

        public override bool Equals( object obj )
        {
            if( obj == null ) return false;

            if( this.GetType() != obj.GetType() ) return false;

            return true;
        }

        public override int GetHashCode() {
            return this.Id;
        }

        public Piece( PieceColor color )
        {
            Rules = new Collection<Rule>();

            Color = color;

            InitializeRules();
        }

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

        public override string ToString()
        {
            return String.Format( "{0} {1}", Color.ToString(), this.GetType().Name );
        }

        public abstract void InitializeRules();
    }
}
