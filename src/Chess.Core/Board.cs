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
        private Object[,] _cells;
        private IList<Piece> _whitePieces;
        private IList<Piece> _blackPieces;

        private Board()
        {
            _cells = new Object[8, 8];
            _whitePieces = new List<Piece>();
            _blackPieces = new List<Piece>();
        }

        public static Board NewGame()
        {
            var board = new Board();

            board.InitGame();

            return board;
        }

        public static Board NewEmpty()
        {
            return new Board();
        }

        public Piece GetPiece( char column, int row )
        {
            return _cells[row - 1, Columns[column] - 1 ] as Piece;
        }

        public T AddWhitePiece<T>( char column, int row ) where T : Piece
        {
            T piece = Activator.CreateInstance( typeof( T ),  PieceColor.White ) as T;
            _whitePieces.Add( piece );

            setPiece( piece, column, row );

            return piece;
        }

        public T AddBlackPiece<T>( char column, int row ) where T : Piece
        {
            T piece = Activator.CreateInstance( typeof( T ),  PieceColor.Black ) as T;
            _blackPieces.Add( piece );

            setPiece( piece, column, row );

            return piece;
        }

        // public T AddPiece<T,K>( char column, int row ) where T : Piece
        //                                                where K : PieceColor
        // {
        //     T piece;
            
        //     if( typeof(K) == typeof(White) )
        //     {
        //         piece = Activator.CreateInstance( typeof( T ),  PieceColor.White ) as T;
        //         _whitePieces.Add( piece );
        //     }
        //     else {
        //          piece = Activator.CreateInstance( typeof( T ),  PieceColor.Black ) as T;
        //          _blackPieces.Add( piece );
        //     }

        //     setPiece( piece, column, row );

        //     return piece;
        // }

        public void InitGame()
        {
            foreach( var c in Columns.Keys )
            {

                AddWhitePiece<Pawn>( c, 2 );
                AddBlackPiece<Pawn>( c, 7 );
            }

            AddWhitePiece<Rook>( 'A', 1 );
            AddWhitePiece<Rook>( 'H', 1 );
            AddBlackPiece<Rook>( 'A', 8 );
            AddBlackPiece<Rook>( 'H', 8 );

            AddWhitePiece<Knight>( 'B', 1 );
            AddWhitePiece<Knight>( 'G', 1 );
            AddBlackPiece<Knight>( 'B', 8 );
            AddBlackPiece<Knight>( 'G', 8 );

            AddWhitePiece<Bishop>( 'C', 1 );
            AddWhitePiece<Bishop>( 'F', 1 );
            AddBlackPiece<Bishop>( 'C', 8 );
            AddBlackPiece<Bishop>( 'F', 8 );

            AddWhitePiece<Queen>( 'D', 1 );
            AddBlackPiece<Queen>( 'D', 8 );

            AddWhitePiece<King>( 'E', 1 );
            AddBlackPiece<King>( 'E', 8 );
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
                targetPiece.IsAlive = false;
            }

            // change position of piece
            setPiece( selectPiece, targetColumn, targetRow );
            clearBoardPosition( column, row );

            return result;
        }

                // set piece at position
        private void setPiece( Piece piece, char column, int row )
        {
            _cells[ row - 1, Columns[column] - 1 ] = piece;
        }

        // clear piece at position
        private void clearBoardPosition( char column, int row )
        {
            _cells[ row - 1, Columns[column] - 1 ] = null;
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
                var p = _cells[r, c];

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
