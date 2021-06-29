using System;
using FifteenPuzzle.Enums;
using FifteenPuzzle.Game.Models;

namespace FifteenPuzzle.Game
{
    public class Puzzle : IPuzzle
    {
        private GameData GameData { get; set; }
        private IBoardGenerator BoardGenerator { get; }

        public Puzzle(IBoardGenerator boardGenerator)
        {
            BoardGenerator = boardGenerator;
            GameData = new GameData
            {
                Board = BoardGenerator.GetFinalSequence(),
                Moves = 0,
                State = GameState.Idle
            };

            UpdateGameState();
        }

        public GameData GetGameData() => GameData;

        public void Apply(GameAction gameAction)
        {
            if (GameData.State != GameState.InProcess)
                return;

            var blank = GameData.Board.Find(0);

            switch (gameAction)
            {
                case GameAction.UpMove:
                    Swap(blank, (blank.x + 1, blank.y));
                    break;
                case GameAction.RightMove:
                    Swap(blank, (blank.x, blank.y - 1));
                    break;
                case GameAction.DownMove:
                    Swap(blank, (blank.x - 1, blank.y));
                    break;
                case GameAction.LeftMove:
                    Swap(blank, (blank.x, blank.y + 1));
                    break;
                default:
                    throw new ArgumentException("Invalid action.");
            }

            UpdateGameState();
        }

        public void StartNew(BoardComplexity complexity = BoardComplexity.Default)
        {
            GameData = new GameData
            {
                State = GameState.InProcess,
                Moves = 0,
                Board = BoardGenerator.GetBoard(complexity)
            };

            UpdateGameState();
        }

        private void Swap((int x, int y) blank, (int x, int y) target)
        {
            try
            {
                GameData.Board[blank.x, blank.y] =  GameData.Board[target.x, target.y];
                GameData.Board[target.x, target.y] = 0;

                GameData.Moves++;
            }
            catch (IndexOutOfRangeException)
            {
                // We cannot cross the board.
            }
        }

        private void UpdateGameState() =>
            GameData.State = IsSolved() ? GameState.Final : GameState.InProcess;

        private bool IsSolved()
        {
            var rows = (int)GameData.Board.GetLongLength(0);
            var columns = (int)GameData.Board.GetLongLength(1);

            var finalSequence = BoardGenerator.GetFinalSequence();
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (GameData.Board[i, j] != finalSequence[i,j])
                        return false;
                }
            }

            return true;
        }
    }
}