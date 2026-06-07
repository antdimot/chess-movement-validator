using System;
using System.Collections.Generic;

namespace Chess.Core.Model
{
    /// <summary>
    /// Represents a chess board with pieces and their positions.
    /// </summary>
    public class Board
    {
        public static readonly char[] Letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

        public static readonly Dictionary<char, int> Columns = new() {
                { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }
        };

        // piece's position status on board
        private readonly object?[,] _cases;
        private readonly IList<Piece> _pieces;

        public object?[,] Cases() => _cases;

        private Board()
        {
            _cases = new object?[8, 8];
            _pieces = [];
        }

        /// <summary>
        /// Create a new chess board with pieces at start position for new game.
        /// </summary>
        /// <returns></returns>
        public static Board NewGame()
        {
            var board = new Board();

            board.InitGame();

            return board;
        }

        public static Board NewEmpty() => new Board();
        public Piece? GetPiece(char column, int row) => _cases[row - 1, Columns[column] - 1] as Piece;

        /// <summary>
        /// Set a piece with color white at position column-row and return the piece created.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public T SetWhite<T>(char column,int row) where T : Piece
        {
            T? piece = Activator.CreateInstance(typeof(T), PieceColor.White) as T;

            _pieces.Add(piece!);

            SetPiece(piece!, column, row);

            return piece!;
        }

        /// <summary>
        /// Set a piece with color black at position column-row and return the piece created.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public T SetBlack<T>(char column,int row) where T : Piece
        {
            T? piece = Activator.CreateInstance( typeof( T ),  PieceColor.Black ) as T;

            _pieces.Add( piece! );

            SetPiece(piece!,column,row);

            return piece!;
        }

        /// <summary>
        /// Chess board initialization with pieces at start position for new game.
        /// </summary>
        public void InitGame()
        {
            foreach( var column in Columns.Keys )
            {

                SetWhite<Pawn>( column, 2 );
                SetBlack<Pawn>( column, 7 );
            }

            SetWhite<Rook>( 'A', 1 );
            SetWhite<Rook>( 'H', 1 );
            SetBlack<Rook>( 'A', 8 );
            SetBlack<Rook>( 'H', 8 );

            SetWhite<Knight>( 'B', 1 );
            SetWhite<Knight>( 'G', 1 );
            SetBlack<Knight>( 'B', 8 );
            SetBlack<Knight>( 'G', 8 );

            SetWhite<Bishop>( 'C', 1 );
            SetWhite<Bishop>( 'F', 1 );
            SetBlack<Bishop>( 'C', 8 );
            SetBlack<Bishop>( 'F', 8 );

            SetWhite<Queen>( 'D', 1 );
            SetBlack<Queen>( 'D', 8 );

            SetWhite<King>( 'E', 1 );
            SetBlack<King>( 'E', 8 );
        }

        /// <summary>
        /// Move a piece from position column-row to targetColumn-targetRow and return the result of movement with information about capture if it is the case.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="targetColumn"></param>
        /// <param name="targetRow"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public MovementResult MovePiece( char column, int row, char targetColumn, int targetRow, PieceColor? color = null )
        {
            var result = new MovementResult();
        
            if( Columns[targetColumn] < 1 || Columns[targetColumn] > 8 || targetRow < 1 || targetRow > 8 )
            {
                result.IsSuccess = false;
                result.Description = "Target position is out of the bounds.";

                return result;
            }

            if( ( column == targetColumn ) && ( row == targetRow ) )
            {
                result.IsSuccess = false;
                result.Description = "Current position and target position are equals.";

                return result;
            }

            var selectPiece = GetPiece( column, row );

            // check if there is a piece at start position
            if( selectPiece == null )
            {
                result.IsSuccess = false;
                result.Description = $"The piece was not found at position {column}{row.ToString()}";

                return result;
            }

            // check color of piece
            if( color.HasValue ) {
                if( ( color == PieceColor.White && selectPiece.Color == 'B' ) ||
                    ( color == PieceColor.Black && selectPiece.Color == 'W' )  ) {
                    result.IsSuccess = false;
                    result.Description = $"The piece selected has wrong color.";

                    return result;
                }
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
            if( !(selectPiece is Knight) && !CheckIfPathIsFree( column, row, targetColumn, targetRow ) )
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
                targetPiece!.IsAlive = false;
            }

            // change position of piece
            SetPiece( selectPiece, targetColumn, targetRow );
            ClearBoardPosition( column, row );

            return result;
        }

        // set piece at position, internal logic without any check, use it only after all checks are done for movement
        private void SetPiece(Piece piece, char column, int row) => _cases[row - 1, Columns[column] - 1] = piece;

        // clear piece at position, internal logic without any check, use it only after all checks are done for movement
        private void ClearBoardPosition(char column, int row)
        {
            _cases[row - 1, Columns[column] - 1] = null;
        }

        // check if the path for select piece is free
        private bool CheckIfPathIsFree(char column, int row, char targetColumn, int targetRow)
        {
            bool result = true;

            int stepx = (Columns[targetColumn] - 1).CompareTo( Columns[column] - 1 );

            int stepy = targetRow.CompareTo( row );

            // start position
            int c = Columns[column] - 1;
            int r = row - 1;

            // next position
            c += stepx;
            r += stepy;

            while( !(c == ( Columns[targetColumn] - 1 ) && r == (targetRow -1)) )
            {
                var p = _cases[r, c];

                if( p != null )
                {
                    result = false;
                    break;
                }

                // next position
                c += stepx;
                r += stepy;
            }

            return result;
        }
    }
}
