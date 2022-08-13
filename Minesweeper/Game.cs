namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();
    private readonly Pen gridPen = Pens.Black;
    private readonly int widthInCells = 9;
    private readonly int bombsCount = 10;
    private readonly Cell[,] cells; //[x][y]
    private readonly int heightInCells = 9;
    private readonly Pen backLightPen = Pens.Aquamarine;

    public int mouseX;
    public int mouseY;

    public Game()
    {
        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell();

        Start();
    }

    private void Start()
    {
        for (int i = 0; i < bombsCount; i++) PlantNextBomb();
    }

    //TODO
    private void DefineCellNumber(int x, int y)
    {
        for (int maskX = x - 1; maskX <= x + 1; maskX++)
        {
            for (int maskY = y - 1; maskY <= y + 1; maskY++)
            {
                if (maskX >= 0 && maskX < widthInCells && maskY >= 0 && maskY < heightInCells)
                {
                    cells[maskX, maskY].number++;
                }
            }
        }
    }

    public void OpenCell()
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;

        OpenCellsWithRecursion(x, y);
    }

    private void OpenCellsWithRecursion(int x, int y)
    {
        if (cells[x, y].number != 0)
        {
            cells[x, y].isOpen = true;
            return;
        }

        for (int maskX = x - 1; maskX <= x + 1; maskX++)
        {
            for (int maskY = y - 1; maskY <= y + 1; maskY++)
            {
                if (maskX >= 0 && maskX < widthInCells && maskY >= 0 && maskY < heightInCells && !cells[maskX, maskY].isOpen && !cells[maskX, maskY].isBomb)
                {
                    cells[maskX, maskY].isOpen = true;
                    if (cells[maskX, maskY].number != 0)
                        continue;

                    OpenCellsWithRecursion(maskX, maskY);
                }
            }
        }
    }
    private void PlantNextBomb()
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);

        if (cells[randomX, randomY].isBomb == false)
        {
            cells[randomX, randomY].isBomb = true;
            DefineCellNumber(randomX, randomY);
        }

        else
        {
            PlantNextBomb();
        }
    }

    public void DrawGameField(Graphics graphics, int widthInPixels, int heightInPixels)
    {
        Cell.cellWidthInPixels = widthInPixels / widthInCells;
        Cell.cellHeightInPixels = heightInPixels / heightInCells;

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
            {
                cells[x, y].Draw(graphics, x, y);
            }

    }
}