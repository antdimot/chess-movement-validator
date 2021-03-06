﻿using Xunit;
using Chess.Core;
using Chess.Core.Model;

namespace Chess.Test
{
    public class RookTests
    {
        [Fact]
        public void MovePiece_Allow_VerticalPathISFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Rook>( 'A', 1 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.True( result.IsSuccess, result.Description );
        }

        [Fact]
        public void MovePiece_NotAllow_VerticalPathISNOTFree()
        {
            var board = Board.NewEmpty();

            board.SetWhite<Rook>( 'A', 1 );

            board.SetWhite<Pawn>( 'A', 4 );

            var result = board.MovePiece( 'A', 1, 'A', 8 );

            Assert.False( result.IsSuccess, "the piece pass over other piece" );
        }
    }
}
