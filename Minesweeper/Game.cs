namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();

    private readonly Cell[,] cells; //[x,y]
    private readonly int heightInCells = 9;
    private readonly int widthInCells = 9;
    private int minesNumber;
    private bool isEnd = false;

    public int Time;
    public int MouseX;
    public int MouseY;

    public int MinesCount { get; private set; }

    public event EventHandler Victory = delegate { };
    public event EventHandler Defeat = delegate { };

    public Game(int widthInPixels, int heightInPixels)
    {
        Cell.CellWidthInPixels = widthInPixels / widthInCells;
        Cell.CellHeightInPixels = heightInPixels / heightInCells;

        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell(x, y);

        PrepareToStart();

    }

    private void Start(int x, int y)
    {
        for (int i = 0; i < minesNumber; i++)
            PlantNextMineExceptSelectedCell(x, y);
    }

    public void PrepareToStart()
    {
        Time = 0;
        isEnd = true;
        MinesCount = 10;
        minesNumber = MinesCount;

        foreach (Cell cell in cells)
        {
            cell.ClearCell();
        }
    }

    public void DrawGameField(Graphics graphics)
    {
        int x = MouseX / Cell.CellWidthInPixels;
        int y = MouseY/ Cell.CellHeightInPixels;

        foreach (Cell cell in cells)
        {
            cell.DrawCell(graphics);
        }

        if (x >= 0 && x < widthInCells &&
            y  >= 0 && y  < heightInCells)
            cells[x,y].DrawBackLighting(graphics);
    }

    //TODO
    public bool TryOpenSelectedCell(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.CellWidthInPixels;
        int y = mouseY / Cell.CellHeightInPixels;

        if (isEnd)
        {
            Start(x, y);
            isEnd = false;
        }

        if (cells[x, y].IsMarked)
            return false;

        if (cells[x, y].IsMine)
        {
            OpenCellsWithMine();

            Defeat(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }

        OpenOtherCells(x, y);


        if (CheckForVictory())
        {
            OpenCellsWithMine();

            Victory(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }
        return true;
    }

    private bool CheckForVictory()
    {
        int openedCellCount = 0;

        foreach (Cell cell in cells)
        {
            if (cell.IsOpen)
                openedCellCount++;
        }

        if (cells.Length - openedCellCount == minesNumber)
            return true;

        return false;
    }

    private void OpenOtherCells(int x, int y)
    {
        if (cells[x, y].Number != 0)
        {
            cells[x, y].IsOpen = true;
            return;
        }

        Queue<Cell> cellsQueue = new();
        cellsQueue.Enqueue(cells[x, y]);

        while (cellsQueue.Count > 0)
        {
            Cell cell = cellsQueue.Dequeue();

            cell.IsOpen = true;

            if (cell.Number != 0)
                continue;

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (cell.X + dx < 0 || cell.X + dx >= widthInCells ||
                        cell.Y + dy < 0 || cell.Y + dy >= heightInCells)
                        continue;

                    if (cells[cell.X + dx, cell.Y + dy].IsOpen ||
                        cells[cell.X + dx, cell.Y + dy].IsMine ||
                        cells[cell.X + dx, cell.Y + dy].IsMarked)
                        continue;

                    cellsQueue.Enqueue(cells[cell.X + dx, cell.Y + dy]);
                }
        }
    }

    public void MarkCell(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.CellWidthInPixels;
        int y = mouseY / Cell.CellHeightInPixels;

        if (cells[x, y].IsMarked)
        {
            cells[x, y].IsMarked = false;
            MinesCount++;
        }
        else if (MinesCount > 0 && !cells[x, y].IsOpen)
        {
            cells[x, y].IsMarked = true;
            MinesCount--;
        }
    }
    //TODO
    public void SmartClick(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.CellWidthInPixels;
        int y = mouseY / Cell.CellHeightInPixels;

        int flagNumber = 0;

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (x + dx >= 0 && x + dx < widthInCells &&
                    y + dy >= 0 && y + dy < heightInCells &&
                    cells[x + dx, y + dy].IsMarked)
                    flagNumber++;

        if (cells[x, y].Number == flagNumber)
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (x + dx >= 0 && x + dx < widthInCells &&
                        y + dy >= 0 && y + dy < heightInCells &&
                        !cells[x + dx, y + dy].IsOpen &&
                        !cells[x + dx, y + dy].IsMarked)
                    {
                        if (!TryOpenSelectedCell((x + dx) * Cell.CellWidthInPixels, (y + dy) * Cell.CellHeightInPixels))
                        {
                            return;
                        }
                    }
    }

    private void OpenCellsWithMine()
    {
        foreach (Cell cell in cells)
        {
            if (!cell.IsMine && cell.IsMarked)
                cell.IsCross = true;
            if (cell.IsMine && !cell.IsMarked)
                cell.IsOpen = true;
        }

    }

    private void DefineCellNumberAroundCell(int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (x + dx >= 0 && x + dx < widthInCells &&
                    y + dy >= 0 && y + dy < heightInCells)
                    cells[x + dx, y + dy].Number++;
    }

    private void PlantNextMineExceptSelectedCell(int x, int y)
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);

        if (cells[randomX, randomY].IsMine == false && cells[x, y] != cells[randomX, randomY])
        {
            cells[randomX, randomY].IsMine = true;
            DefineCellNumberAroundCell(randomX, randomY);
        }

        else
        {
            PlantNextMineExceptSelectedCell(x, y);
        }
    }


}