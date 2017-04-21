﻿using System;
using Xunit;
using Chess.Model;
using Chess.Model.Pieces;

namespace Chess.Test
{
    public class PawnTests
    {
        [Fact]
        public void MovePiece_TwoStepFromStartPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 4 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_OneStepFromStartPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 3 );

            Assert.True( result.IsSuccess, result.Description );

            var oldPosition = board.GetPiece( 'A', 2 );

            Assert.NotNull( oldPosition );

            var newPosition = board.GetPiece( 'A', 3 );

            Assert.NotNull( newPosition );

            Assert.IsType<Pawn>( newPosition );

            Assert.True( newPosition.ChessColor == ChessColor.White, "The piece at new position is different." );
        }

        [Fact]
        public void MovePiece_WihEatFromAnyPosition_Success()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.Black, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.True( result.IsSuccess, result.Description );

            Assert.True( result.Ate );

            Assert.NotNull( result.AtePiece );

            Assert.IsType<Pawn>( result.AtePiece );
        }

        [Fact]
        public void MovePiece_WithEatFromAnyPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'B', 4 );

            board.SetPiece<Pawn>( ChessColor.White, 'C', 5 );

            var result = board.MovePiece( 'B', 4, 'C', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_ThreeStepFromStartPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 2 );

            var result = board.MovePiece( 'A', 2, 'A', 5 );

            Assert.False( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_OneStepBackFromAnyPosition_Error()
        {
            var board = Board.GetNewBoard();

            board.SetPiece<Pawn>( ChessColor.White, 'A', 5 );

            var result = board.MovePiece( 'A', 5, 'A', 4 );

            Assert.False( result.IsSuccess, result.Description );
        }
    }
}
