using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    public class BoardTests
    {
        [Fact]
        public void GetNewBoard()
        {
            var board = Board.NewBoard();

            var piece = board.GetPiece( 'A', 2 );

            Assert.Null( piece ); //the position is not empty
        }

        [Fact]
        public void SetPiece()
        {
            var board = Board.NewBoard();

            board.AddPiece<Pawn,White>( 'A', 2 );

            var piece = board.GetPiece( 'A', 2 );

            Assert.NotNull( piece ); //the position is empty

            Assert.IsType<Pawn>( piece );
        }

        [Fact]
        public void InitializeForGame_PawnsReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            foreach( var column in Board.Columns.Keys )
            {
                var whitePiece = board.GetPiece( column, 2 );

                Assert.NotNull( whitePiece );

                Assert.IsType<Pawn>( whitePiece );

                Assert.True( whitePiece.Color == PieceColor.White );

                var blackPiece = board.GetPiece( column, 7 );

                Assert.NotNull( blackPiece );

                Assert.IsType<Pawn>( blackPiece );

                Assert.True( blackPiece.Color == PieceColor.Black );
            }
        }

        [Fact]
        public void InitializeForGame_RooksReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            // check first white rook
            var whiteRook1 = board.GetPiece( 'A', 1 );

            Assert.NotNull( whiteRook1 );

            Assert.IsType<Rook>( whiteRook1 );

            Assert.True( whiteRook1.Color == PieceColor.White, "the color piece is not white" );

            // check second white rook
            var whiteRook2 = board.GetPiece( 'H', 1 );

            Assert.NotNull( whiteRook2 );

            Assert.IsType<Rook>( whiteRook2 );

            Assert.True( whiteRook2.Color == PieceColor.White, "the color piece is not white" );

            // check first black rook
            var blackRook1 = board.GetPiece( 'A', 8 );

            Assert.NotNull( blackRook1 );

            Assert.IsType<Rook>( blackRook1 );

            Assert.True( blackRook1.Color == PieceColor.Black, "the color piece is not black" );

            // check second black rook
            var blackRook2 = board.GetPiece( 'H', 8 );

            Assert.NotNull( blackRook2 );

            Assert.IsType<Rook>( blackRook2 );

            Assert.True( blackRook2.Color == PieceColor.Black, "the color piece is not black" );
        }

        [Fact]
        public void InitializeForGame_KnightsReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            // check first white knight
            var whiteKnight1 = board.GetPiece( 'B', 1 );

            Assert.NotNull( whiteKnight1 );

            Assert.IsType<Knight>( whiteKnight1 );

            Assert.True( whiteKnight1.Color == PieceColor.White, "the color piece is not white" );

            // check second white knight
            var whiteKnight2 = board.GetPiece( 'G', 1 );

            Assert.NotNull( whiteKnight2 );

            Assert.IsType<Knight>( whiteKnight2 );

            Assert.True( whiteKnight2.Color == PieceColor.White, "the color piece is not white" );

            // check first black knight
            var blackKnight1 = board.GetPiece( 'B', 8 );

            Assert.NotNull( blackKnight1 );

            Assert.IsType<Knight>( blackKnight1 );

            Assert.True( blackKnight1.Color == PieceColor.Black, "the color piece is not black" );

            // check second white knight
            var blackKnight2 = board.GetPiece( 'G', 8 );

            Assert.NotNull( blackKnight2 );

            Assert.IsType<Knight>( blackKnight2 );

            Assert.True( blackKnight2.Color == PieceColor.Black, "the color piece is not black" );
        }

        [Fact]
        public void InitializeForGame_BishopsReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            // check first white bishop
            var whiteBishop1 = board.GetPiece( 'C', 1 );

            Assert.NotNull( whiteBishop1 );

            Assert.IsType<Bishop>( whiteBishop1 );

            Assert.True( whiteBishop1.Color == PieceColor.White, "the color piece is not white" );

            // check second white bishop
            var whiteBishop2 = board.GetPiece( 'F', 1 );

            Assert.NotNull( whiteBishop2 );

            Assert.IsType<Bishop>( whiteBishop2 );

            Assert.True( whiteBishop2.Color == PieceColor.White, "the color piece is not white" );

            // check first black bishop
            var blackBishop1 = board.GetPiece( 'C', 8 );

            Assert.NotNull( blackBishop1 );

            Assert.IsType<Bishop>( blackBishop1 );

            Assert.True( blackBishop1.Color == PieceColor.Black, "the color piece is not black" );

            // check second black bishop
            var blackBishop2 = board.GetPiece( 'F', 8 );

            Assert.NotNull( blackBishop2 );

            Assert.IsType<Bishop>( blackBishop2 );

            Assert.True( blackBishop2.Color == PieceColor.Black, "the color piece is not black" );
        }

        [Fact]
        public void InitializeForGame_QueensReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            // check white queen
            var whiteQueen = board.GetPiece( 'D', 1 );

            Assert.NotNull( whiteQueen );

            Assert.IsType<Queen>( whiteQueen );

            Assert.True( whiteQueen.Color == PieceColor.White, "the color piece is not white" );

            // check  black queen
            var blackQueen = board.GetPiece( 'D', 8 );

            Assert.NotNull( blackQueen );

            Assert.IsType<Queen>( blackQueen );

            Assert.True( blackQueen.Color == PieceColor.Black, "the color piece is not black" );
        }

        [Fact]
        public void InitializeForGame_KingsReady()
        {
            var board = Board.NewBoard();

            board.Initialize();

            // check white queen
            var whiteKing = board.GetPiece( 'E', 1 );

            Assert.NotNull( whiteKing );

            Assert.IsType<King>( whiteKing );

            Assert.True( whiteKing.Color == PieceColor.White, "the color piece is not white" );

            // check  black queen
            var blackKing = board.GetPiece( 'E', 8 );

            Assert.NotNull( blackKing );

            Assert.IsType<King>( blackKing );

            Assert.True( blackKing.Color == PieceColor.Black, "the color piece is not black" );
        }
    }
}
