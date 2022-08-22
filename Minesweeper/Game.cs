namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();
    private int widthInCells;
    private int heightInCells;
    private readonly Cell[,] cells; //[x,y]
    private Cell highlightCell;
    private bool isStarted;
    private int totalMine;

    public int ElapsedSeconds;

    public Game(int gameFieldWidthInCells, int gameFieldHeightInCells, int cellSizeInPixels)
    {
        this.widthInCells = gameFieldWidthInCells;
        this.heightInCells = gameFieldHeightInCells;

        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell(x, y, cellSizeInPixels);

        highlightCell = cells[0, 0];

        PrepareToStart();
    }

    public int RemainingUnmarkedMines { get; private set; }

    public event EventHandler Victory = delegate { };
    public event EventHandler Defeat = delegate { };

    public void SelectCell(int x, int y, out bool isSelectedCellChanged)
    {
        if (highlightCell != cells[x, y])
        {
            highlightCell = cells[x, y];
            isSelectedCellChanged = true;
        }
        else
        {
            isSelectedCellChanged = true;
        }
    }

    public void PrepareToStart()
    {
        isStarted = false;
        RemainingUnmarkedMines = 10;
        totalMine = RemainingUnmarkedMines;
        ElapsedSeconds = 0;

        foreach (Cell cell in cells)
            cell.ClearCell();
    }

    private void Start(int x, int y)
    {
        for (int i = 0; i < totalMine; i++)
            PlantNextMineExceptSelectedCell(x, y);
    }

    public void DrawGameField(Graphics graphics)
    {
        foreach (Cell cell in cells)
            cell.DrawCell(graphics);

        if (highlightCell != null)
            highlightCell.DrawBackLighting(graphics);
    }

    public bool TryOpenSelectedCell(int x, int y)
    {
        if (!isStarted)
        {
            Start(x, y);
            isStarted = true;
        }

        if (cells[x, y].IsMarked)
            return false;

        if (cells[x, y].IsMined)//Defeat
        {
            OpenCellsWithMine();

            Defeat(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }

        OpenOtherCells(x, y);

        if (AreVictoryConditionsMet())
        {
            OpenCellsWithMine();

            Victory(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }

        return true;
    }

    private bool AreVictoryConditionsMet()
    {
        int openedCellCount = 0;

        foreach (Cell cell in cells)
            if (cell.IsOpen)
                openedCellCount++;

        if (cells.Length - openedCellCount == totalMine)
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

            foreach (Cell cellToEnqueue in ActionAroundCell(cell))
            {
                if (!cellToEnqueue.IsOpen &&
                    !cellToEnqueue.IsMined &&
                    !cellToEnqueue.IsMarked)
                    cellsQueue.Enqueue(cellToEnqueue);
            }
        }
    }

    public void MarkCell(int x, int y)
    {
        if (cells[x, y].IsMarked)
        {
            cells[x, y].IsMarked = false;
            RemainingUnmarkedMines++;
        }
        else if (RemainingUnmarkedMines > 0 && !cells[x, y].IsOpen)
        {
            cells[x, y].IsMarked = true;
            RemainingUnmarkedMines--;
        }
    }

    public void SmartClick(int x, int y)
    {
        if (cells[x, y].IsMarked || !cells[x, y].IsOpen || !isStarted)
            return;

        int flagNumber = 0;

        foreach (Cell cell in ActionAroundCell(cells[x, y]))
        {
            if (cell.IsMarked)
                flagNumber++;
        }

        if (cells[x, y].Number != flagNumber)
            return;

        foreach (Cell cell in ActionAroundCell(cells[x, y]))
        {
            if (cell.IsOpen ||
                cell.IsMarked)
                continue;

            if (!TryOpenSelectedCell(cell.X, cell.Y))
                return;
        }
    }

    private void DefineCellNumberAroundCell(int x, int y)
    {
        foreach (Cell cell in ActionAroundCell(cells[x, y]))
        {
            cell.Number++;
        }
    }

    private void OpenCellsWithMine()
    {
        foreach (Cell cell in cells)
        {
            if (!cell.IsMined && cell.IsMarked)
                cell.IsIncorrectlyMarked = true;
            if (cell.IsMined && !cell.IsMarked)
                cell.IsOpen = true;
        }
    }

    private void PlantNextMineExceptSelectedCell(int x, int y)
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);

        if (cells[randomX, randomY].IsMined == false && cells[x, y] != cells[randomX, randomY])
        {
            cells[randomX, randomY].IsMined = true;
            DefineCellNumberAroundCell(randomX, randomY);
        }
        else
        {
            PlantNextMineExceptSelectedCell(x, y);
        }
    }

    private IEnumerable<Cell> ActionAroundCell(Cell cell)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (cell.X + dx >= 0 && cell.X + dx < widthInCells &&
                    cell.Y + dy >= 0 && cell.Y + dy < heightInCells)
                    yield return cells[cell.X + dx, cell.Y + dy];
    }
}