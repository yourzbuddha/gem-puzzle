using System;
using FifteenPuzzle.Enums;

namespace FifteenPuzzle.Game
{
    public class Board4X4Generator : IBoardGenerator
    {
        public int[,] GetBoard(BoardComplexity complexity)
        {
            return complexity switch
            {
                BoardComplexity.Default => GetShuffledBoard(),
                BoardComplexity.Hardest => GetHardestBoard(),
                _ => throw new ArgumentException($"{complexity} is unknown board complexity.")
            };
        }

        private int[,] GetShuffledBoard()
        {
            var array = GetFinalSequence();
            var board = array;

            return Shuffle(board, 42 * 10);
        }

        private int[,] GetHardestBoard()
        {
            var hardest = BoardVariations.Hardest4X4;
            var random = new Random();
            var array = hardest[random.Next(hardest.Count)];
            var board = array.To2dArray(4, 4);

            return board;
        }

        public int[,] GetFinalSequence()
        {
            return new[,] { {1, 2, 3, 4}, {5, 6, 7, 8}, {9, 10, 11, 12}, {13, 14, 15, 0} };
        }

        private int[,] Shuffle(int[,] array, int iterations)
        {
            var blank = array.Find(0);
            var random = new Random();
            for (var i = 0; i < iterations; i++)
            {
                var coordinate = blank;
                switch (random.Next(4))
                {
                    case 0:
                        coordinate.y++;
                        break;
                    case 1:
                        coordinate.y--;
                        break;
                    case 2:
                        coordinate.x++;
                        break;
                    case 3:
                        coordinate.x--;
                        break;
                }

                try
                {
                    array[blank.x, blank.y] = array[coordinate.x, coordinate.y];
                    array[coordinate.x, coordinate.y] = 0;

                    blank.x = coordinate.x;
                    blank.y = coordinate.y;
                }
                catch (IndexOutOfRangeException)
                {
                }
            }

            return array;
        }
    }
}