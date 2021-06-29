namespace FifteenPuzzle.UnitTests
{
    public static class Boards4X4Provider
    {
        public static int[,] GetSolvedMatrix()
        {
            return new[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };
        }

        public static int[] GetSolvedArray()
        {
            return new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
        }

        public static int[,] GetShuffledMatrix()
        {
            return new[,] { { 14, 15, 8, 12 }, { 10, 11, 9, 13 }, { 2, 6, 5, 1 }, { 3, 7, 4, 0 } };
        }

        public static int[,] GetMatrix_BlankInTheMiddle()
        {
            return new[,] { { 1, 2, 3, 4 }, { 5, 6, 0, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 7 } };
        }
    }
}