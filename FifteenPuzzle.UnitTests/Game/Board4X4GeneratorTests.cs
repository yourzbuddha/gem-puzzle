using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using FifteenPuzzle.Enums;
using FifteenPuzzle.Game;
using Xunit;

namespace FifteenPuzzle.UnitTests.Game
{
    public class Board4X4GeneratorTests
    {
        private Board4X4Generator BoardGenerator { get; }
        private IFixture Fixture { get; }

        public Board4X4GeneratorTests()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());

            BoardGenerator = Fixture.Create<Board4X4Generator>();
        }

        [Theory]
        [InlineData(BoardComplexity.Default)]
        [InlineData(BoardComplexity.Hardest)]
        public void GetBoard_Generates4X4(BoardComplexity complexity)
        {
            var board = BoardGenerator.GetBoard(complexity);

            Assert.Equal(4, board.GetLongLength(0));
            Assert.Equal(4, board.GetLongLength(1));
        }

        [Theory]
        [InlineData(BoardComplexity.Default)]
        [InlineData(BoardComplexity.Hardest)]
        public void GetBoard_ContainsAllNumbers(BoardComplexity complexity)
        {
            var board = BoardGenerator.GetBoard(complexity);
            var finalSequence = Boards4X4Provider.GetSolvedMatrix();

            var plainBoard = board.ToPlain();
            var plainFinalSequence = finalSequence.ToPlain();

            var equal = !plainBoard.Except(plainFinalSequence).Any()
                        && !plainFinalSequence.Except(plainBoard).Any();
            Assert.True(equal);
        }

        [Fact]
        public void GetBoard_UnknownComplexity_ThrowsException()
        {
            var exception = Record.Exception(()=> BoardGenerator.GetBoard(BoardComplexity.None));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }
    }
}