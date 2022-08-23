using NUnit.Framework;

namespace Minesweeper;

[TestFixture]
internal class MinesweeperTests
{
    private readonly int width = 10;
    private readonly int height = 10;
    private readonly int cellSize = 1;
    private readonly int totalMine = 10;
    private Game game;

    [SetUp]
    public void BeforeEachTests()
    {
        game = new(width, height, cellSize, totalMine);
    }

    [Test]
    public void TestMinesCount()
    {
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(10));
    }

    [Test]
    public void TestMarkCell()
    {
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(9));
    }

    [Test]
    public void TestUnMarkCell()
    {
        game.MarkCell(1, 1);
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(10));
    }

    [Test]
    public void TestTryOpenSelectedCellWithFlag()
    {
        game.MarkCell(1, 1);
        bool expectedResult = game.TryOpenSelectedCell(1, 1);

        Assert.That(expectedResult, Is.EqualTo(false));
    }
}