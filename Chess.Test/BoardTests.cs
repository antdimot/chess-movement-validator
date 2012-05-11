using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void GetNewBoard_Success()
        {
            var board = Board.GetNewBoard();

            var piece = board.GetPiece( 'A', 2 );

            Assert.IsNull( piece, "the position is not empty" );
        }

        [TestMethod]
        public void SetPiece_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var piece = board.GetPiece( 'A', 2 );

            Assert.IsNotNull( piece, "the position is empty" );

            Assert.IsInstanceOfType( piece, typeof( Pawn ), "the piece is not a pawn" );
        }

        [TestMethod]
        public void InitializeForGame_PawnsPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            foreach( var column in Board.Columns.Keys )
            {
                var whitePiece = board.GetPiece( column, 2 );

                Assert.IsNotNull( whitePiece, "the position is empty" );

                Assert.IsInstanceOfType( whitePiece, typeof( Pawn ), "the piece is not a pawn" );

                Assert.IsTrue( whitePiece.ChessColor == ChessColor.White, "the color piece is not white" );

                var blackPiece = board.GetPiece( column, 7 );

                Assert.IsNotNull( blackPiece, "the position is empty" );

                Assert.IsInstanceOfType( blackPiece, typeof( Pawn ), "the piece is not a pawn" );

                Assert.IsTrue( blackPiece.ChessColor == ChessColor.Black, "the color piece is not black" );
            }
        }

        [TestMethod]
        public void InitializeForGame_RooksPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            // check first white rook
            var whiteRook1 = board.GetPiece( 'A', 1 );

            Assert.IsNotNull( whiteRook1, "the position is empty" );

            Assert.IsInstanceOfType( whiteRook1, typeof( Rook ), "the piece is not a rook" );

            Assert.IsTrue( whiteRook1.ChessColor == ChessColor.White, "the color piece is not white" );

            // check second white rook
            var whiteRook2 = board.GetPiece( 'H', 1 );

            Assert.IsNotNull( whiteRook2, "the position is empty" );

            Assert.IsInstanceOfType( whiteRook2, typeof( Rook ), "the piece is not a rook" );

            Assert.IsTrue( whiteRook2.ChessColor == ChessColor.White, "the color piece is not white" );

            // check first black rook
            var blackRook1 = board.GetPiece( 'A', 8 );

            Assert.IsNotNull( blackRook1, "the position is empty" );

            Assert.IsInstanceOfType( blackRook1, typeof( Rook ), "the piece is not a rook" );

            Assert.IsTrue( blackRook1.ChessColor == ChessColor.Black, "the color piece is not black" );

            // check second black rook
            var blackRook2 = board.GetPiece( 'H', 8 );

            Assert.IsNotNull( blackRook2, "the position is empty" );

            Assert.IsInstanceOfType( blackRook2, typeof( Rook ), "the piece is not a rook" );

            Assert.IsTrue( blackRook2.ChessColor == ChessColor.Black, "the color piece is not black" );
        }
        [TestMethod]
        public void InitializeForGame_KnightsPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            // check first white knight
            var whiteKnight1 = board.GetPiece( 'B', 1 );

            Assert.IsNotNull( whiteKnight1, "the position is empty" );

            Assert.IsInstanceOfType( whiteKnight1, typeof( Knight ), "the piece is not a knight" );

            Assert.IsTrue( whiteKnight1.ChessColor == ChessColor.White, "the color piece is not white" );

            // check second white knight
            var whiteKnight2 = board.GetPiece( 'G', 1 );

            Assert.IsNotNull( whiteKnight2, "the position is empty" );

            Assert.IsInstanceOfType( whiteKnight2, typeof( Knight ), "the piece is not a knight" );

            Assert.IsTrue( whiteKnight2.ChessColor == ChessColor.White, "the color piece is not white" );

            // check first black knight
            var blackKnight1 = board.GetPiece( 'B', 8 );

            Assert.IsNotNull( blackKnight1, "the position is empty" );

            Assert.IsInstanceOfType( blackKnight1, typeof( Knight ), "the piece is not a knight" );

            Assert.IsTrue( blackKnight1.ChessColor == ChessColor.Black, "the color piece is not black" );

            // check second white knight
            var blackKnight2 = board.GetPiece( 'G', 8 );

            Assert.IsNotNull( blackKnight2, "the position is empty" );

            Assert.IsInstanceOfType( blackKnight2, typeof( Knight ), "the piece is not a knight" );

            Assert.IsTrue( blackKnight2.ChessColor == ChessColor.Black, "the color piece is not black" );
        }

        [TestMethod]
        public void InitializeForGame_BishopsPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            // check first white bishop
            var whiteBishop1 = board.GetPiece( 'C', 1 );

            Assert.IsNotNull( whiteBishop1, "the position is empty" );

            Assert.IsInstanceOfType( whiteBishop1, typeof( Bishop ), "the piece is not a bishop" );

            Assert.IsTrue( whiteBishop1.ChessColor == ChessColor.White, "the color piece is not white" );

            // check second white bishop
            var whiteBishop2 = board.GetPiece( 'F', 1 );

            Assert.IsNotNull( whiteBishop2, "the position is empty" );

            Assert.IsInstanceOfType( whiteBishop2, typeof( Bishop ), "the piece is not a bishop" );

            Assert.IsTrue( whiteBishop2.ChessColor == ChessColor.White, "the color piece is not white" );

            // check first black bishop
            var blackBishop1 = board.GetPiece( 'C', 8 );

            Assert.IsNotNull( blackBishop1, "the position is empty" );

            Assert.IsInstanceOfType( blackBishop1, typeof( Bishop ), "the piece is not a bishop" );

            Assert.IsTrue( blackBishop1.ChessColor == ChessColor.Black, "the color piece is not black" );

            // check second black bishop
            var blackBishop2 = board.GetPiece( 'F', 8 );

            Assert.IsNotNull( blackBishop2, "the position is empty" );

            Assert.IsInstanceOfType( blackBishop2, typeof( Bishop ), "the piece is not a bishop" );

            Assert.IsTrue( blackBishop2.ChessColor == ChessColor.Black, "the color piece is not black" );
        }

        [TestMethod]
        public void InitializeForGame_QueensPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            // check white queen
            var whiteQueen = board.GetPiece( 'D', 1 );

            Assert.IsNotNull( whiteQueen, "the position is empty" );

            Assert.IsInstanceOfType( whiteQueen, typeof( Queen ), "the piece is not a queen" );

            Assert.IsTrue( whiteQueen.ChessColor == ChessColor.White, "the color piece is not white" );

            // check  black queen
            var blackQueen = board.GetPiece( 'D', 8 );

            Assert.IsNotNull( blackQueen, "the position is empty" );

            Assert.IsInstanceOfType( blackQueen, typeof( Queen ), "the piece is not a queen" );

            Assert.IsTrue( blackQueen.ChessColor == ChessColor.Black, "the color piece is not black" );
        }

        [TestMethod]
        public void InitializeForGame_KingsPresent_Success()
        {
            var board = Board.GetNewBoard();

            board.InitializeForStartGame();

            // check white queen
            var whiteKing = board.GetPiece( 'E', 1 );

            Assert.IsNotNull( whiteKing, "the position is empty" );

            Assert.IsInstanceOfType( whiteKing, typeof( King ), "the piece is not a king" );

            Assert.IsTrue( whiteKing.ChessColor == ChessColor.White, "the color piece is not white" );

            // check  black queen
            var blackKing = board.GetPiece( 'E', 8 );

            Assert.IsNotNull( blackKing, "the position is empty" );

            Assert.IsInstanceOfType( blackKing, typeof( King ), "the piece is not a king" );

            Assert.IsTrue( blackKing.ChessColor == ChessColor.Black, "the color piece is not black" );
        }
    }
}
