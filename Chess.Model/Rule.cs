using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Model
{
    public class Rule
    {
        public int Id
        {
            get;
            set;
        }

        private readonly Predicate<Movement>[] _predicates;

        internal Rule( params Predicate<Movement>[] predicates )
        {
            _predicates = predicates;
        }

        internal Rule( int id, params Predicate<Movement>[] predicates ) : this( predicates )
        {
            Id = id;
        }

        public bool Validate( Movement move )
        {
            return !_predicates.Where( p => !p( move ) ).Any();
        }
    }
}
