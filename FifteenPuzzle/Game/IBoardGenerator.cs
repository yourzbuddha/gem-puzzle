using FifteenPuzzle.Enums;

namespace FifteenPuzzle.Game
{
    public interface IBoardGenerator
    {
        int[,] GetBoard(BoardComplexity complexity);
        int[,] GetFinalSequence();
    }
}