using NUnit.Framework;

namespace Minesweeper;

//TODO
[TestFixture]
internal class MinesweeperTests
{
    private const int Width = 10;
    private const int Height = 10;
    private const int CellSize = 1;
    private const int TotalMine = 10;
    private Game game;

    [SetUp]
    public void BeforeEachTests()
    {
        game = new Game(Width, Height, CellSize, TotalMine);
    }

    [Test]
    public void TestRemainingUnmarkedMines()
    {
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(TotalMine));
    }

    [Test]
    public void TestMarkCell()
    {
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(TotalMine - 1));
    }

    [Test]
    public void TestUnMarkCell()
    {
        game.MarkCell(1, 1);
        game.MarkCell(1, 1);
        int expectedResult = game.RemainingUnmarkedMines;

        Assert.That(expectedResult, Is.EqualTo(TotalMine));
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
        game.SelectCell(1, 1, out bool expectedResult);

        Assert.That(expectedResult, Is.EqualTo(true));
    }

    [Test]
    public void TestSelectSelectedCell()
    {
        game.SelectCell(1, 1, out bool isSelectedCellChanged);

        game.SelectCell(1, 1, out bool expectedResult);

        Assert.That(expectedResult, Is.EqualTo(false));
    }

    [Test]
    public void TestVictoryOrDefeatEvents()
    {
        game.Victory += (sender, args) => Assert.Pass();
        game.Defeat += (sender, args) => Assert.Pass();

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                game.TryOpenCell(x, y);
            }
        Assert.Fail();
    }
}