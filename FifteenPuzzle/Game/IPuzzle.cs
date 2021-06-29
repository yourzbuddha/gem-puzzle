using FifteenPuzzle.Enums;
using FifteenPuzzle.Game.Models;

namespace FifteenPuzzle.Game
{
    public interface IPuzzle
    {
        void Apply(GameAction gameAction);
        GameData GetGameData();
        void StartNew(BoardComplexity complexity = BoardComplexity.Default);
    }
}