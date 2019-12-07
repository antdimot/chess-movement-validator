using Chess.Core.Model;


namespace Chess.Core
{
    public class Game
    {
        public PieceColor NextPlayer { get; private set;}

        public Board Chess { get; private set; }

        public Game()
        {
            Chess = Board.NewGame();

            NextPlayer = PieceColor.White;
        }
    }
}