# Chess Movement Validator - AI Coding Agent Instructions

## Architecture Overview

This is a **rule-based chess engine** focused on validating piece movements according to chess rules. The codebase uses a **predicate-based validation system** rather than traditional if-else logic.

**Three-project structure:**
- `Chess.Core` (.NET Standard library): Domain model (Board, Piece hierarchy, Game orchestration)
- `Chess.Test` (xUnit): Test suite for movement validation
- `Chess.Player` (Console app): Interactive text-based chess player

## Core Design Patterns

### 1. Rule-Based Movement Validation
Pieces define movement rules using **predicate chains** in `InitializeRules()`. Each `Rule` contains multiple `Predicate<Movement>` delegates that ALL must pass.

**Example from Pawn.cs:**
```csharp
Rules.Add(new Rule(
	m => Color == 'W',           // Must be white
	m => !m.WithCapture,         // Not capturing
	m => m.StartY == 1,          // On starting row
	m => m.EndX == m.StartX,     // Same column
	m => m.EndY == m.StartY + 2  // Two squares forward
));
```

**When adding new piece types or modifying movement rules:**
- Create predicates for each condition (color, direction, distance, capture state)
- All predicates in a Rule must return `true` for the movement to be valid
- Use 0-based internal coordinates (converted from 1-based user input)

### 2. Board Coordinate System
- **User-facing:** Column letters (A-H) + rows (1-8)
- **Internal storage:** 0-based 2D array `_cases[row, column]`
- **Conversion:** `Board.Columns` dictionary maps 'A'-'H' to 1-8; subtract 1 for array indexing

### 3. Movement Validation Pipeline (Board.MovePiece)
Six checks executed in order:
1. Bounds validation (target in A1-H8)
2. Non-identity check (from ≠ to)
3. Piece presence at source
4. Color validation (if ChessGame enforces turn)
5. **Piece-specific rule validation** (IsValidMovement)
6. Path obstruction (Knights skip this)
7. Target square color conflict

### 4. Knight Special Case
Knights jump over pieces. `CheckIfPathIsFree()` is explicitly skipped for Knight instances in `MovePiece()`:
```csharp
if (!(selectPiece is Knight) && !CheckIfPathIsFree(...))
```

## Key Classes

### Board
- `NewGame()`: Initializes standard chess starting position
- `NewEmpty()`: Creates blank board for custom setups
- `SetWhite<T>()/SetBlack<T>()`: Place pieces using generics
- `MovePiece()`: Core validation and state mutation (returns `MovementResult`)

### Piece (abstract)
- `Letter`: Display identifier (e.g., "PA"=Pawn, "KN"=Knight)
- `Color`: 'W' or 'B' (char, not enum in display logic)
- `IsAlive`: Tracks captured state
- `Rules`: Collection of movement validators

### ChessGame
- Wraps Board with turn enforcement (`_nextPlayerColor`)
- `Move()`: Delegates to Board.MovePiece with color check, then alternates turn
- `ShowBoard()`: Renders ASCII board to stream

### MovementResult
- `IsSuccess`: Boolean validation outcome
- `Description`: User-friendly error/success message
- `Capture`: Whether opponent piece was taken
- `CapturedPiece`: Reference to captured piece (sets `IsAlive = false`)

## Testing Conventions

Tests in `Chess.Test` use xUnit with **descriptive method names** following pattern:
`MethodName_Allow|NotAllow_ScenarioDescription`

**Examples:**
- `MovePiece_Allow_TwoStepFromStartPosition` (Pawn initial double move)
- `MovePiece_NotAllow_BusyPositionWithSameColor` (collision detection)

**Setup pattern:**
```csharp
var board = Board.NewEmpty();  // or NewGame() for full setup
board.SetWhite<Pawn>('A', 2);
var result = board.MovePiece('A', 2, 'A', 4);
Assert.True(result.IsSuccess, result.Description);
```

## Build & Run

```bash
dotnet restore          # Restore dependencies
dotnet build           # Build all projects
dotnet test            # Run xUnit tests
dotnet run --project src/Chess.Player  # Launch console player
```

**Console commands (Chess.Player):**
- `m` - Move piece (prompts for "A2" to "A4" style input)
- `p` - Print board
- `n` - Show next player
- `h` - Help
- `q` - Quit

## Adding New Features

### New Piece Type
1. Create class inheriting `Piece` in `Chess.Core/Model/`
2. Override `Letter` (e.g., "AR" for Archbishop)
3. Implement `InitializeRules()` with predicate-based rules
4. Add to `Board.InitGame()` if standard piece
5. Create test suite `{PieceName}Tests.cs` covering all movement patterns

### Modifying Movement Logic
- **DO:** Add new Rules to existing pieces' `InitializeRules()`
- **DON'T:** Modify `Board.MovePiece()` validation order (breaks existing tests)
- Test both success and failure cases (path obstruction, capture, color conflict)

### Special Moves (Castling, En Passant)
Not currently implemented. Would require:
- Game state tracking (king/rook move history, last move)
- Rule IDs (already supported: `Rule(int id, ...)`)
- Post-validation state updates in `Board.MovePiece()`

## Code Style Notes

- Uses C# 10+ features (collection expressions `[]`, file-scoped namespaces implicit in .NET 10)
- `Nullable` enabled project-wide
- Piece color stored as `char` ('W'/'B') not enum for display simplicity
- XML doc comments on all public methods (maintain this pattern)
- Activator pattern for generic piece instantiation (`SetWhite<T>()`)
