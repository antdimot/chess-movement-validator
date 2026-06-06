using Chess.Core;

var game = new ChessGame();
var finished = false;

Console.WriteLine("Chess player started.");

while (!finished)
{
    Console.WriteLine("Make your choice (h -> help) ?");

    switch (Console.ReadLine())
    {
        case "q":
            finished = true;
            break;
        case "h":
            Console.WriteLine("""
                Available commands:
                  p -> print chess board
                  n -> next player
                  m -> move piece
                """);
            break;
        case "n":
            Console.WriteLine($"The next player is {game.ShowNextPlayer()}");
            break;
        case "m":
            Console.WriteLine("Move a piece on Chessboard:");

            Console.Write("From (example A2): ");
            var source = Console.ReadLine()?.ToUpper();
            if (source is not { Length: 2 })
            {
                Console.WriteLine("Invalid input. It should be two chars, example A2.");
                break;
            }
            var fromColumn = source[0];
            var fromRow = int.Parse(source[1..]);

            Console.Write("To (example A3): ");
            var target = Console.ReadLine()?.ToUpper();
            if (target is not { Length: 2 })
            {
                Console.WriteLine("Invalid input. It should be two chars, example A2.");
                break;
            }
            var toColumn = target[0];
            var toRow = int.Parse(target[1..]);

            Console.WriteLine(game.Move(fromColumn, fromRow, toColumn, toRow));
            Console.WriteLine($"The next player is {game.ShowNextPlayer()}");
            break;
        case "p":
            game.ShowBoard(Console.OpenStandardOutput());
            break;
        default:
            Console.WriteLine("Invalid command!");
            break;
    }
}

Console.WriteLine("End of program.");

