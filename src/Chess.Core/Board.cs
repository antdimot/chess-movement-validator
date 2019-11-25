using System;
using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public class Board
    {
        public static Dictionary<char, int> Columns = new Dictionary<char, int>() {
                { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }
        };

        // piece position status on board
        private Piece[,] _pieces;

        public Board( bool startGame = false )
        {
            _pieces = new Piece[8, 8];

            if( startGame ) Initialize();
        }

        // board factory
        public static Board NewBoard()
        {
            return new Board();
        }
    
        public Piece GetPiece( char column, int row )
        {
            return _pieces[row - 1, Columns[column] - 1 ];
        }

        public T SetPiece<T,K>( char column, int row ) where T : Piece
                                                       where K : PieceColor
        {
            PieceColor usedColor = PieceColor.Black;
            
            if( typeof(K) == typeof(White) ) usedColor = PieceColor.White;

            var piece = Activator.CreateInstance( typeof( T ),  usedColor ) as T;

            putPiece( piece, column, row );

            return piece;
        }

        public void Initialize()
        {
            // set pawns
            foreach( var c in Columns.Keys )
            {
                SetPiece<Pawn,White>( c, 2 );
                SetPiece<Pawn,Black>( c, 7 );
            }

            // set rocks
            SetPiece<Rook,White>( 'A', 1 );
            SetPiece<Rook,White>( 'H', 1 );
            SetPiece<Rook,Black>( 'A', 8 );
            SetPiece<Rook,Black>( 'H', 8 );

            // set knights
            SetPiece<Knight,White>( 'B', 1 );
            SetPiece<Knight,White>( 'G', 1 );
            SetPiece<Knight,Black>( 'B', 8 );
            SetPiece<Knight,Black>( 'G', 8 );

            // set bishops
            SetPiece<Bishop,White>( 'C', 1 );
            SetPiece<Bishop,White>( 'F', 1 );
            SetPiece<Bishop,Black>( 'C', 8 );
            SetPiece<Bishop,Black>( 'F', 8 );

            // set queens
            SetPiece<Queen,White>( 'D', 1 );
            SetPiece<Queen,Black>( 'D', 8 );

            // set kings
            SetPiece<King,White>( 'E', 1 );
            SetPiece<King,Black>( 'E', 8 );
        }


        public MovementResult MovePiece<Piece,PieceColor>( char targetColumn, int targetRow )
        {
            char startColumn = '1';
            int  startRow = 0;

            return MovePiece( startColumn, startRow, targetColumn, targetRow );
        }

        // try to do a movement of piece
        public MovementResult MovePiece( char column, int row, char targetColumn, int targetRow )
        {
            var result = new MovementResult();

            var selectPiece = GetPiece( column, row );

            // check if there is a piece at start position
            if( selectPiece == null )
            {
                result.IsSuccess = false;
                result.Description = String.Format( "The piece was not found at position {0}{1}", column, row.ToString() );

                return result;
            }

            var targetPiece = GetPiece( targetColumn, targetRow );

            // check it is a valid movement for piece (rules piece validator)
            if( !selectPiece.IsValidMovement(
                ( targetPiece != null && !selectPiece.Color.Equals( targetPiece.Color ) ),
                row - 1, Columns[column] - 1 , targetRow - 1, Columns[targetColumn] -1 ) )
            {
                result.IsSuccess = false;
                result.Description =
                    String.Format( "The {0} {1} at position {2}{3} cannot move to {4}{5}",
                    selectPiece.Color.ToString(), selectPiece.GetType().Name,
                    column, row.ToString(), targetColumn, targetRow.ToString() );

                return result;
            }

            // check if the path is free if piece is not a knight
            if( !(selectPiece is Knight) && !checkIfPathIsFree( column, row, targetColumn, targetRow ) )
            {
                result.IsSuccess = false;
                result.Description =
                    String.Format( "The path from {0}{1} to {2}{3} for {4}{5} is not free.",
                     column, row.ToString(), targetColumn, targetRow.ToString(),
                     selectPiece.Color.ToString(), selectPiece.GetType().Name );

                return result;
            }

            // check if target position there is already present a piece with same color
            if( targetPiece != null && selectPiece.Color.Equals( targetPiece.Color ) )
            {
                result.IsSuccess = false;
                result.Description =
                    String.Format( "There is already present a {0} piece at position {1}{2}",
                    selectPiece.Color.ToString(), targetColumn, targetRow );

                return result;
            }

            // set result information after capture
            result.Capture = ( targetPiece != null && !selectPiece.Color.Equals( targetPiece.Color ) );
            if( result.Capture )
            {
                result.CapturedPiece = targetPiece;
            }

            // change position of piece
            putPiece( selectPiece, targetColumn, targetRow );
            clearBoardPosition( column, row );

            return result;
        }

                // set piece at position
        private void putPiece( Piece piece, char column, int row )
        {
            _pieces[ row - 1, Columns[column] - 1 ] = piece;
        }

        // clear piece at position
        private void clearBoardPosition( char column, int row )
        {
            _pieces[ row - 1, Columns[column] - 1 ] = null;
        }

        // check if the path for select piece is free
        private bool checkIfPathIsFree( char column, int row, char targetColumn, int targetRow )
        {
            bool result = true;

            int stepx = (Columns[targetColumn] - 1).CompareTo( Columns[column] - 1 );

            int stepy = targetRow.CompareTo( row );

            // start position
            int c = Columns[column] - 1;
            int r = row - 1;

            // next position
            c = c + stepx;
            r = r + stepy;

            while( !(c == ( Columns[targetColumn] - 1 ) && r == (targetRow -1)) )
            {
                var p = _pieces[r, c];

                if( p != null )
                {
                    result = false;
                    break;
                }

                // next position
                c = c + stepx;
                r = r + stepy;
            }

            return result;
        }

    }
}
