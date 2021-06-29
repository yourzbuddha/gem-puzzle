using System;
using FifteenPuzzle.Enums;
using FifteenPuzzle.Game;
using FifteenPuzzle.Ui;

namespace FifteenPuzzle
{
    public class Main
    {
        private IUiRenderer UiRenderer { get; }
        private IPuzzle Puzzle { get; }

        public Main()
        {
            UiRenderer = new ConsoleRenderer();
            Puzzle = new Puzzle(new Board4X4Generator());
        }

        public void Start()
        {
            StartNew(BoardComplexity.Default);
        }

        private void StartNew(BoardComplexity boardComplexity)
        {
            Console.Clear();

            Puzzle.StartNew(boardComplexity);
            UiRenderer.Render(Puzzle.GetGameData());

            ListenGameEvents();
        }

        private void ListenGameEvents()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        Puzzle.Apply(GameAction.LeftMove);
                        break;
                    case ConsoleKey.UpArrow:
                        Puzzle.Apply(GameAction.UpMove);
                        break;
                    case ConsoleKey.RightArrow:
                        Puzzle.Apply(GameAction.RightMove);
                        break;
                    case ConsoleKey.DownArrow:
                        Puzzle.Apply(GameAction.DownMove);
                        break;
                    case ConsoleKey.E:
                        return;
                    default:
                        continue;
                }

                var data = Puzzle.GetGameData();
                UiRenderer.Render(data);

                if (data.State != GameState.Final) continue;

                GoToFinalState();
            }
        }

        private void GoToFinalState()
        {
            Console.WriteLine("Hooray! You are Wixing!");
            Console.WriteLine("One more? Press: 'R' for restart.");
            Console.WriteLine("Too easy for you? Press: 'H' for the hardest one.");

            while (true)
            {
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.R:
                        StartNew(BoardComplexity.Default);
                        break;
                    case ConsoleKey.H:
                        StartNew(BoardComplexity.Hardest);
                        break;
                    default:
                        ClearCurrentConsoleLine();
                        Console.Write($"Have seen '{key}' somewhere?");
                        break;
                }
            }
        }

        private static void ClearCurrentConsoleLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}