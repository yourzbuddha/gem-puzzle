using System;
using System.Linq;
using Xunit;

namespace FifteenPuzzle.UnitTests.Game
{
    public class ArrayExtensionsTests
    {
        [Theory]
        [InlineData(0, 3, 3)]
        [InlineData(1, 0, 0)]
        public void Find_Succeed(int value, int x, int y)
        {
            var sequence = Boards4X4Provider.GetSolvedMatrix();

            var result = sequence.Find(value);

            Assert.Equal(x, result.x);
            Assert.Equal(y, result.y);
        }

        [Fact]
        public void Find_NotExistingValue_ThrowsError()
        {
            var sequence = Boards4X4Provider.GetSolvedMatrix();

            var exception = Record.Exception(() => sequence.Find(42));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void ToPlain_Succeed()
        {
            var matrix = Boards4X4Provider.GetSolvedMatrix();
            var result = matrix.ToPlain();
            var expected = Boards4X4Provider.GetSolvedArray();

            Assert.True(result.SequenceEqual(expected));
        }
    }
}