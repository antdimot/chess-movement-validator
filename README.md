# Chess Movements Validator

### Abstract

Chess movement engine validator using .Net Core.

### Examples

```c#
var board = Board.NewGame();
board.SetWhite<Pawn>( 'A', 5 );
var result = board.MovePiece( 'A', 5, 'A', 6 );
```

### Useful information

Chess.Core is a .Net Standard Library which contains the chess domain model (ie. Piece, Board, Game .....).

Chess.Player is a simple .Net console useful for simulating a chess game directly from text console.


### Building and running instructions

* git clone https://github.com/antdimot/chess-movement-validator.git
* cd chess-movement-validator
* dotnet restore
* dotnet build
