using System;
using System.IO;
using Chess.Core.Model;

namespace Chess.Core
{
    /// <summary>
    /// Class that represents a chess game. It contains the chess board and the next player to move. It also contains methods to move pieces, show the next player and show the board.
    /// </summary>
    public class ChessGame
    {
        /// The color of the next player to move. It is initialized to white at the start of the game and it is updated after each successful move. It can be used to determine which player's turn it is and to enforce the rules of the game, such as preventing a player from moving a piece that does not belong to them.
        private PieceColor _nextPlayerColor;

        /// <summary>
        /// The chess board of the game. It is initialized with the pieces at the start position for a new game. It can be used to move pieces, get the pieces at a specific position, and so on.
        /// </summary>
        public Board ChessBoard { get; private set; }

        public ChessGame()
        {
            ChessBoard = Board.NewGame();

            _nextPlayerColor = PieceColor.White;
        }

        /// <summary>
        /// Method to move a piece from one position to another. It takes the from and to positions as parameters and returns a string with the result of the move. It also updates the next player to move if the move is successful.
        /// </summary>
        /// <param name="fromColumn"></param>
        /// <param name="fromRow"></param>
        /// <param name="toColumn"></param>
        /// <param name="toRow"></param>
        /// <returns></returns>
        public string Move( char fromColumn, int fromRow, char toColumn, int toRow )
        {
            var result = ChessBoard.MovePiece( fromColumn, fromRow, toColumn, toRow, _nextPlayerColor );

            if( result.IsSuccess ) {
                _nextPlayerColor = ( _nextPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White );
            }

            return result.Description;
        }

        /// <summary>
        /// Utility method to show the next player to move. It returns a string with the color of the next player. It can be used to show the next player in the console or in a file.
        /// </summary>
        /// <returns></returns>
        public string ShowNextPlayer()
        {
            return ( _nextPlayerColor == PieceColor.White ? "WHITE" : "BLACK" );
        }

        /// <summary>
        /// Utility method to show the chess board in a stream. It can be used to show the board in the console or in a file. It shows the board with the pieces and the coordinates.
        /// </summary>
        /// <param name="outStream"></param>
        public void ShowBoard( Stream outStream )
        {
            var sw = new StreamWriter( outStream );
            sw.AutoFlush = true;
            // Console.SetOut(sw);

            sw.WriteLine( "\n   -------------------------------------------------" );

            for (int i = 8; i >= 1; i--)
            {
                sw.Write( $" {i} | " );

                for (int j = 1; j <= 8; j++)
                {
                    var position = ChessBoard.Cases()[i-1,j-1] as Piece;

                    if( position != null ) sw.Write( position.ToString() );
                    else sw.Write( "   " );

                    sw.Write( " | " );
                }

                sw.WriteLine( "\n   -------------------------------------------------" );
            }

            sw.Write( "     " );
            foreach (var letter in Board.Letters )
            {
                sw.Write( $" {letter}    " );
            }
            sw.Write( "\n" );
        }
    }
}