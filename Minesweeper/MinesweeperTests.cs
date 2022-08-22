using NUnit.Framework;

namespace Minesweeper
{
    [TestFixture]
    internal class MinesweeperTests
    {
        [Test]
        public void TestMinesCount()
        {
            int width = 10;
            int height = 10;
            int cellSize = 1;
            Game game = new(width, height, cellSize);

            int expectedResult = game.RemainingUnmarkedMines;

            Assert.That(expectedResult, Is.EqualTo(10));
        }

        [Test]
        public void TestMarkCell()
        {
            int width = 10;
            int height = 10;
            int cellSize = 1;
            Game game = new(width, height, cellSize);

            game.MarkCell(1,1);
            int expectedResult = game.RemainingUnmarkedMines;

            Assert.That(expectedResult, Is.EqualTo(9));
        }

        [Test]
        public void TestUnMarkCell()
        {
            int width = 10;
            int height = 10;
            int cellSize = 1;
            Game game = new(width, height, cellSize);

            game.MarkCell(1,1);
            game.MarkCell(1,1);
            int expectedResult = game.RemainingUnmarkedMines;

            Assert.That(expectedResult, Is.EqualTo(10));
        }

        [Test]
        public void TestTryOpenSelectedCellWithFlag()
        {
            int width = 10;
            int height = 10;
            int cellSize = 1;
            Game game = new(width, height, cellSize);

            game.MarkCell(1,1);
            bool expectedResult = game.TryOpenSelectedCell(1, 1);

            Assert.That(expectedResult, Is.EqualTo(false));
        }
    }
}
