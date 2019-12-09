using System;
using System.IO;
using Chess.Core.Model;

namespace Chess.Core
{
    public class ChessGame
    {
        private PieceColor _nextPlayerColor;

        public Board ChessBoard { get; private set; }

        public ChessGame()
        {
            ChessBoard = Board.NewGame();

            _nextPlayerColor = PieceColor.White;
        }

        public string Move( char fromColumn, int fromRow, char toColumn, int toRow )
        {
            var result = ChessBoard.MovePiece( fromColumn, fromRow, toColumn, toRow, _nextPlayerColor );

            if( result.IsSuccess ) {
                _nextPlayerColor = ( _nextPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White );
            }

            return result.Description;
        }

        public string ShowNextPlayer()
        {
            return ( _nextPlayerColor == PieceColor.White ? "WHITE" : "BLACK" );
        }

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