using System;
using System.Collections.Generic;
using Chess.Core.Model;

namespace Chess.Core
{
    public class Board
    {
        public static Dictionary<char, int> Columns = new Dictionary<char, int>() {
                { 'A', 0 }, { 'B', 1 }, { 'C', 2 }, { 'D', 3 }, { 'E', 4 }, { 'F', 5 }, { 'G', 6 }, { 'H', 7 }
        };

        // piece position status on board
        private Piece[,] _pieces;

        public Board() : this( false )
        {
            Initialize();
        }

        public Board( bool startGame )
        {
            if( startGame )
                InitializeForStartGame();
        }

        // board factory
        public static Board GetNewBoard()
        {
            return new Board();
        }

        private void Initialize()
        {
            _pieces = new Piece[8, 8];
        }

        public void InitializeForStartGame()
        {
            // set pawns
            foreach( var c in Columns.Keys )
            {
                SetPiece<Pawn>( ChessColor.White, c, 2 );
                SetPiece<Pawn>( ChessColor.Black, c, 7 );
            }

            // set rocks
            SetPiece<Rook>( ChessColor.White, 'A', 1 );
            SetPiece<Rook>( ChessColor.White, 'H', 1 );
            SetPiece<Rook>( ChessColor.Black, 'A', 8 );
            SetPiece<Rook>( ChessColor.Black, 'H', 8 );

            // set knights
            SetPiece<Knight>( ChessColor.White, 'B', 1 );
            SetPiece<Knight>( ChessColor.White, 'G', 1 );
            SetPiece<Knight>( ChessColor.Black, 'B', 8 );
            SetPiece<Knight>( ChessColor.Black, 'G', 8 );

            // set bishops
            SetPiece<Bishop>( ChessColor.White, 'C', 1 );
            SetPiece<Bishop>( ChessColor.White, 'F', 1 );
            SetPiece<Bishop>( ChessColor.Black, 'C', 8 );
            SetPiece<Bishop>( ChessColor.Black, 'F', 8 );

            // set queens
            SetPiece<Queen>( ChessColor.White, 'D', 1 );
            SetPiece<Queen>( ChessColor.Black, 'D', 8 );

            // set kings
            SetPiece<King>( ChessColor.White, 'E', 1 );
            SetPiece<King>( ChessColor.Black, 'E', 8 );
        }

        public Piece GetPiece( char column, int row )
        {
            return _pieces[row - 1, Columns[column] ];
        }

        // piece factory
        public void SetPiece<T>( ChessColor color, char column, int row ) where T : Piece 
        {
            var piece = Activator.CreateInstance( typeof( T ), color ) as Piece;

            putPiece( piece, column, row );
        }

        // set piece at position
        private void putPiece( Piece piece, char column, int row )
        {
            _pieces[row - 1, Columns[column]] = piece;
        }

        // clear piece at position
        private void clearBoardPosition( char column, int row )
        {
            _pieces[row - 1, Columns[column]] = null;
        }

        // try to do a movement of piece
        public MovementResult MovePiece( char column, int row, char targetColumn, int targetRow )
        {
            MovementResult result = new MovementResult();

            var selectPiece = GetPiece( column, row );

            // check if there is a piece at start position
            if( selectPiece == null )
            {
                result.IsSuccess = false;
                result.Description = String.Format( "No piece is present at position {0}{1}", column, row.ToString() );

                return result;
            }

            var targetPiece = GetPiece( targetColumn, targetRow );

            // check it is a valid movement for piece (rules piece validator)
            if( !selectPiece.IsValidMovement(
                ( targetPiece != null && !selectPiece.ChessColor.Equals( targetPiece.ChessColor ) ),
                row - 1, Columns[column], targetRow - 1, Columns[targetColumn] ) )
            {
                result.IsSuccess = false;
                result.Description =
                    String.Format( "The {0} {1} at position {2}{3} cannot move to {4}{5}",
                    selectPiece.ChessColor.ToString(), selectPiece.GetType().Name,
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
                     selectPiece.ChessColor.ToString(), selectPiece.GetType().Name );

                return result;
            }

            // check if target position there is already present a piece with same color
            if( targetPiece != null && selectPiece.ChessColor.Equals( targetPiece.ChessColor ) )
            {
                result.IsSuccess = false;
                result.Description =
                    String.Format( "There is already present a {0} piece at position {1}{2}",
                    selectPiece.ChessColor.ToString(), targetColumn, targetRow );

                return result;
            }

            // set result information after capture
            result.Capture = ( targetPiece != null && !selectPiece.ChessColor.Equals( targetPiece.ChessColor ) );
            if( result.Capture )
            {
                result.CapturedPiece = targetPiece;
            }

            // change position of piece
            putPiece( selectPiece, targetColumn, targetRow );
            clearBoardPosition( column, row );

            return result;
        }

        // check if the path for select piece is free
        private bool checkIfPathIsFree( char column, int row, char targetColumn, int targetRow )
        {
            bool result = true;

            int stepx = Columns[targetColumn].CompareTo( Columns[column] );

            int stepy = targetRow.CompareTo( row );

            // start position
            int c = Columns[column];
            int r = row - 1;

            // next position
            c = c + stepx;
            r = r + stepy;

            while( !(c == Columns[targetColumn] && r == (targetRow -1)) )
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
