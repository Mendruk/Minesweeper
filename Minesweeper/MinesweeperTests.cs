using NUnit.Framework;

namespace Minesweeper
{
    [TestFixture]
    internal class MinesweeperTests
    {
        [Test]
        public void TestMinesCount()
        {
            Game game = new(100, 100);
            int expectedResult = game.MinesCount;

            Assert.That(expectedResult, Is.EqualTo(10));
        }

        [Test]
        public void TestMarkCell()
        {
            Game game = new(100, 100);

            game.MarkCell();
            int expectedResult = game.MinesCount;

            Assert.That(expectedResult, Is.EqualTo(9));
        }

        [Test]
        public void TestUnMarkCell()
        {
            Game game = new(100, 100);

            game.MarkCell();
            game.MarkCell();
            int expectedResult = game.MinesCount;

            Assert.That(expectedResult, Is.EqualTo(10));
        }

        [Test]
        public void TestTryOpenSelectedCellWithFlag()
        {
            Game game = new(100, 100);
            game.MarkCell();
            bool expectedResult = game.TryOpenSelectedCell(0, 0);

            Assert.That(expectedResult, Is.EqualTo(false));
        }
    }
}
