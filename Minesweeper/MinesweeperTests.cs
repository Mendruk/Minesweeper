//using NUnit.Framework;

//namespace Minesweeper
//{
//    [TestFixture]
//    internal class MinesweeperTests
//    {
//        [Test]
//        public void TestMinesCount()
//        {
//            int width = 10;
//            int height = 10;
//            Size cellSize = new Size(1, 1);
//            Game game = new(width,height,cellSize);

//            int expectedResult = game.MinesCount;

//            Assert.That(expectedResult, Is.EqualTo(10));
//        }

//        [Test]
//        public void TestMarkCell()
//        {
//            int width = 10;
//            int height = 10;
//            Size cellSize = new Size(1, 1);
//            Game game = new(width, height, cellSize);

//            game.MarkCell();
//            int expectedResult = game.MinesCount;

//            Assert.That(expectedResult, Is.EqualTo(9));
//        }

//        [Test]
//        public void TestUnMarkCell()
//        {
//            int width = 10;
//            int height = 10;
//            Size cellSize = new Size(1, 1);
//            Game game = new(width, height, cellSize);

//            game.MarkCell();
//            game.MarkCell();
//            int expectedResult = game.MinesCount;

//            Assert.That(expectedResult, Is.EqualTo(10));
//        }

//        [Test]
//        public void TestTryOpenSelectedCellWithFlag()
//        {
//            int width = 10;
//            int height = 10;
//            Size cellSize = new Size(1, 1);
//            Game game = new(width, height, cellSize);

//            game.MarkCell();
//            bool expectedResult = game.TryOpenSelectedCell(0, 0);

//            Assert.That(expectedResult, Is.EqualTo(false));
//        }
//    }
//}
