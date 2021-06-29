using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using FifteenPuzzle.Enums;
using FifteenPuzzle.Game;
using Moq;
using Xunit;

namespace FifteenPuzzle.UnitTests.Game
{
    public class PuzzleTests
    {
        private Puzzle Puzzle { get; }
        private IFixture Fixture { get; }

        public PuzzleTests()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetShuffledMatrix());
            generatorMock.Setup(_ => _.GetFinalSequence())
                .Returns(Boards4X4Provider.GetSolvedMatrix());

            Puzzle = Fixture.Create<Puzzle>();
        }

        [Fact]
        public void Apply_DownMove_Succeed()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetMatrix_BlankInTheMiddle());

            Puzzle.StartNew();
            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.DownMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(0, newBlank.y - blank.y);
            Assert.Equal(-1, newBlank.x - blank.x);
        }

        [Fact]
        public void Apply_UpMove_Succeed()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetMatrix_BlankInTheMiddle());

            Puzzle.StartNew();
            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.UpMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(0, newBlank.y - blank.y);
            Assert.Equal(1, newBlank.x - blank.x);
        }


        [Fact]
        public void Apply_LeftMove_Succeed()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetMatrix_BlankInTheMiddle());

            Puzzle.StartNew();
            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.LeftMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(1, newBlank.y - blank.y);
            Assert.Equal(0, newBlank.x - blank.x);
        }

        [Fact]
        public void Apply_RightMove_Succeed()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetMatrix_BlankInTheMiddle());

            Puzzle.StartNew();
            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.RightMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(-1, newBlank.y - blank.y);
            Assert.Equal(0, newBlank.x - blank.x);
        }

        [Fact]
        public void Apply_LeftMoveNearBorder_NoChanges()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetSolvedMatrix());

            Puzzle.StartNew();
            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.LeftMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(0, newBlank.y - blank.y);
            Assert.Equal(0, newBlank.x - blank.x);
        }

        [Fact]
        public void Apply_InvalidAction_NoChanges()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetShuffledMatrix());

            Puzzle.StartNew();

            var exception = Record.Exception(() => Puzzle.Apply(GameAction.None));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void Apply_LeftMove_GameNotStarted_NoChanges()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetSolvedMatrix());

            var blank = Puzzle.GetGameData().Board.Find(0);
            Puzzle.Apply(GameAction.LeftMove);
            var newBlank = Puzzle.GetGameData().Board.Find(0);

            Assert.Equal(0, newBlank.y - blank.y);
            Assert.Equal(0, newBlank.x - blank.x);
        }

        [Fact]
        public void Apply_IncreaseMoves()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetMatrix_BlankInTheMiddle());

            Puzzle.StartNew();
            Puzzle.Apply(GameAction.DownMove);

            Assert.Equal(1, Puzzle.GetGameData().Moves);
        }

        [Fact]
        public void Puzzle_SwitchToFinalState()
        {
            var generatorMock = Fixture.Freeze<Mock<IBoardGenerator>>();
            generatorMock.Setup(_ => _.GetBoard(BoardComplexity.Default))
                .Returns(Boards4X4Provider.GetSolvedMatrix());

            Puzzle.StartNew();

            Assert.Equal(GameState.Final, Puzzle.GetGameData().State);
        }
    }
}