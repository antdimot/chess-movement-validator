namespace Chess.Core.Model
{
    /// <summary>
    /// Piece color. It can be either white or black. It is used to determine which pieces belong to which player and to determine the rules of the game. For example, a white piece can only move to a square that is not occupied by a white piece, and a black piece can only move to a square that is not occupied by a black piece.
    /// </summary>
    public enum PieceColor
    {
        White,
        Black
    }
}