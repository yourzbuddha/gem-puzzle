using FifteenPuzzle.Enums;

namespace FifteenPuzzle.Game.Models
{
    public class GameData
    {
        public GameState State { get; set; }
        public int Moves { get; set; }
        public int[,] Board { get; set; }
    }
}