﻿using System;
using System.Collections.Generic;
using Chess.Core.Infrastructure;
using Chess.Core.Model;

namespace Chess.Core.Model
{
    public class Board
    {
        public static Dictionary<char, int> Columns = new Dictionary<char, int>() {
                { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }
        };

        // piece's position status on board
        private Object[,] _cells;
        private IList<Piece> _pieces;

        private Board()
        {
            _cells = new Object[8, 8];
            _pieces = new List<Piece>();
        }

        public static Board NewGame()
        {
            var board = new Board();

            board.InitGame();

            return board;
        }

        public static Board NewEmpty() => new Board();

        public Piece GetPiece( char column, int row ) => _cells[row - 1, Columns[column] - 1 ] as Piece;

        public T AddWhite<T>( char column, int row ) where T : Piece
        {
            T piece = Activator.CreateInstance( typeof( T ),  PieceColor.White ) as T;
            _pieces.Add( piece );

            setPiece( piece, column, row );

            return piece;
        }

        public T AddBlack<T>( char column, int row ) where T : Piece
        {
            T piece = Activator.CreateInstance( typeof( T ),  PieceColor.Black ) as T;
            _pieces.Add( piece );

            setPiece( piece, column, row );

            return piece;
        }

        public void InitGame()
        {
            foreach( var column in Columns.Keys )
            {

                AddWhite<Pawn>( column, 2 );
                AddBlack<Pawn>( column, 7 );
            }

            AddWhite<Rook>( 'A', 1 );
            AddWhite<Rook>( 'H', 1 );
            AddBlack<Rook>( 'A', 8 );
            AddBlack<Rook>( 'H', 8 );

            AddWhite<Knight>( 'B', 1 );
            AddWhite<Knight>( 'G', 1 );
            AddBlack<Knight>( 'B', 8 );
            AddBlack<Knight>( 'G', 8 );

            AddWhite<Bishop>( 'C', 1 );
            AddWhite<Bishop>( 'F', 1 );
            AddBlack<Bishop>( 'C', 8 );
            AddBlack<Bishop>( 'F', 8 );

            AddWhite<Queen>( 'D', 1 );
            AddBlack<Queen>( 'D', 8 );

            AddWhite<King>( 'E', 1 );
            AddBlack<King>( 'E', 8 );
        }

        // try to do a movement of piece
        public MovementResult MovePiece( char column, int row, char targetColumn, int targetRow )
        {
            if( Columns[targetColumn] < 1 || Columns[targetColumn] > 8 || targetRow < 1 || targetRow > 8 )
            {
                throw new InvalidMovementException( "Target position is out of the bounds." );
            }

            if( ( column == targetColumn ) && ( row == targetRow ) )
            {
                throw new InvalidMovementException( "Current position and target position are equals." );
            }

            var result = new MovementResult();

            var selectPiece = GetPiece( column, row );

            // check if there is a piece at start position
            if( selectPiece == null )
            {
                result.IsSuccess = false;
                result.Description = $"The piece was not found at position {column}{row.ToString()}";

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
