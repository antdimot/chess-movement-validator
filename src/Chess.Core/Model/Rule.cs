using System;
using System.Linq;

namespace Chess.Core.Model
{
    public class Rule
    {
        /// <summary>
        /// Internal identifier for the rule. This can be used to reference the rule in other parts of the code, such as when defining special moves or conditions. It is not required for the rule to function, but it can be helpful for organization and readability.
        /// </summary>
        public int Id { get; set; }

        private readonly Predicate<Movement>[] _predicates;

        internal Rule( params Predicate<Movement>[] predicates )
        {
            _predicates = predicates;
        }

        internal Rule( int id, params Predicate<Movement>[] predicates ) : this( predicates )
        {
            Id = id;
        }

        /// <summary>
        /// Validates a movement against the rule's predicates. The movement is considered valid if all predicates return true. If any predicate returns false, the movement is considered invalid. This method can be used to check if a move is legal according to the rules of chess, such as checking for valid piece movements, ensuring that the king is not in check, and so on.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool Validate( Movement move )
        {
            return !_predicates.Where( p => !p( move ) ).Any();
        }
    }
}
