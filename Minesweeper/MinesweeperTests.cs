using NUnit.Framework;

namespace Minesweeper;
//TODO
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
    public void TestRemainingUnmarkedMines()
    {
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(totalMine));
    }

    [Test]
    public void TestMarkCell()
    {
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(totalMine-1));
    }

    [Test]
    public void TestUnMarkCell()
    {
        game.MarkCell(1, 1);
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(totalMine));
    }

    [Test]
    public void TestOpenCellWithMark()
    {
        game.MarkCell(1, 1);
        bool expectedResult = game.TryOpenCell(1, 1);

        Assert.That(expectedResult, Is.EqualTo(false));
    }

    [Test]
    public void TestSelectCell()
    {
        game.SelectCell(1,1, out bool expectedResult);

        Assert.That(expectedResult, Is.EqualTo(true));
    }

    [Test]
    public void TestSelectSelectedCell()
    {
        game.SelectCell(1, 1, out bool isSelectedCellChanged);

        game.SelectCell(1, 1, out bool expectedResult);

        Assert.That(expectedResult, Is.EqualTo(false));
    }
}