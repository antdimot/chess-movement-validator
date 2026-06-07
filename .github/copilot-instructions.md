# Chess Movement Validator - Copilot Instructions

## Project Overview
This is a .NET 10 chess engine focused on validating chess piece movements. The architecture uses a **rule-based movement validation system** where each piece defines its legal moves through predicate compositions rather than imperative logic.

## Architecture

### Core Components
- **Chess.Core** (.NET Standard library): Domain model (Piece, Board, ChessGame)
- **Chess.Test** (xUnit): Movement validation tests
- **Chess.Player**: Console-based chess game simulator

### Key Design Patterns

#### Rule-Based Movement Validation
Each `Piece` subclass (Pawn, Rook, Bishop, etc.) defines movement rules via `Rule` objects containing predicates:

```csharp
// Example from Pawn.cs - rules are predicates, not imperative code
Rules.Add(new Rule(
	m => Color == 'W',           // Predicate 1: must be white
	m => !m.WithCaputure,        // Predicate 2: not capturing
	m => m.EndX == m.StartX,     // Predicate 3: same column
	m => m.EndY == m.StartY + 1  // Predicate 4: move forward one
));
```

**When adding piece movement logic**: Use composition of predicates in `InitializeRules()`, not if-else chains.

#### Board Coordinate System
- **Columns**: 'A'-'H' mapped to 1-8 via `Board.Columns` dictionary
- **Rows**: 1-8 (human notation) mapped to 0-7 array indices
- **Internal representation**: `_cases[row-1, column-1]` (zero-indexed)

**Critical**: Convert user input (e.g., 'A', 2) to array indices before accessing `_cases`.

#### Movement Validation Flow
`Board.MovePiece()` executes this sequence:
1. Bounds checking (target position valid)
2. Position equality check (not moving to same square)
3. Source piece existence check
4. Color validation (if player color specified)
5. Piece-specific rule validation (`Piece.IsValidMovement()`)
6. Path obstruction check (except for Knights)
7. Target square color conflict check
8. Execute move and capture logic

**When modifying movement logic**: Follow this exact sequence - don't skip steps or reorder checks.

#### Piece State Management
- **Creation pattern**: Use `Board.SetWhite<T>()` / `Board.SetBlack<T>()` with generic types
- **Internal tracking**: Board maintains `_pieces` collection AND `_cases` 2D array
- **Capture handling**: Set `IsAlive = false` and clear from `_cases`, but keep in `_pieces` list

## Project Conventions

### Naming Patterns
- **Piece letters**: 2-char codes (PA=Pawn, RK=Rook, BS=Bishop, KN=Knight, QU=Queen, KG=King)
- **Colors**: Single char 'W' or 'B' (not full enum values in display)
- **Movement coordinates**: Use `startX/startY/endX/endY` (converted to 1-based columns/rows)

### Test Structure
- Test classes organized by piece type (`PawnTests.cs`, `RookTests.cs`)
- Test naming: `MovePiece_[Allow|NotAllow]_[SpecificScenario]`
- Always test both valid AND invalid movements for each rule
- Use `Board.NewGame()` for standard setup, `Board.NewEmpty()` for custom scenarios

Example test pattern:
```csharp
[Fact]
public void MovePiece_Allow_TwoStepFromStartPosition()
{
	var board = Board.NewGame();
	board.SetWhite<Pawn>('A', 2);
	var result = board.MovePiece('A', 2, 'A', 4);
	Assert.True(result.IsSuccess, result.Description);
}
```

### XML Documentation
All public APIs have triple-slash comments explaining:
- Purpose and behavior
- Chess rule context (why this logic exists)
- Parameter meanings and expected formats

**When adding new methods**: Follow this documentation pattern - explain the chess logic, not just the code.

## Development Workflow

### Build & Test
```powershell
dotnet restore
dotnet build
dotnet test  # Runs xUnit tests in Chess.Test
```

### Running the Console Player
```powershell
cd src/Chess.Player
dotnet run
# Commands: m (move), p (print board), n (next player), h (help), q (quit)
```

### Adding New Pieces
1. Create class inheriting `Piece` in `Chess.Core/Model/`
2. Override `Letter` property with 2-char code
3. Implement `InitializeRules()` using predicate-based `Rule` objects
4. Add to `Board.InitGame()` for standard setup
5. Create `[PieceName]Tests.cs` with movement scenarios

### Modifying Movement Logic
- **DO**: Add predicates to `Rule` objects in piece classes
- **DON'T**: Add conditional logic to `Board.MovePiece()` for specific pieces
- Path obstruction checking is in `Board.CheckIfPathIsFree()` - Knights bypass this

## Important Files
- `src/Chess.Core/Model/Board.cs`: Central movement validation (264 lines)
- `src/Chess.Core/Model/Piece.cs`: Base class with rule validation engine
- `src/Chess.Core/Model/Rule.cs`: Predicate composition for movement rules
- `src/Chess.Core/ChessGame.cs`: Turn management and player API
