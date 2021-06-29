using System;
using System.Collections.Generic;

namespace FifteenPuzzle
{
    public static class ArrayExtensions
    {
        public static (int x, int y) Find(this int[,] array, int value)
        {
            var rows = (int)array.GetLongLength(0);
            var columns = (int)array.GetLongLength(1);
            for (var i = 0; i < rows; i++)
            {
                for (var j = columns - 1; j >= 0; j--)
                {
                    if (array[i, j] == value)
                        return (i, j);
                    if (array[i, j] < 0)
                        break;
                }
            }

            throw new ArgumentException($"Value: {value} - not found.");
        }

        public static int[,] To2dArray(this int[] array, int sizeX, int sizeY)
        {
            var matrix = new int[sizeX, sizeY];
            var arrayCount = 0;
            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    matrix[i, j] = array[arrayCount];
                    arrayCount++;
                }
            }

            return matrix;
        }

        public static int[] ToPlain(this int[,] matrix)
        {
            var collection = new List<int>();
            for (var i = 0; i < matrix.GetLongLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLongLength(1); j++)
                {
                    collection.Add(matrix[i, j]);
                }
            }

            return collection.ToArray();
        }
    }
}