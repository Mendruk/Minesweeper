namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();
    private int widthInCells;
    private int heightInCells;
    private int totalMines;
    private readonly Cell[,] cells; //[x,y]
    private Cell selectedCell;
    private bool isStarted;

    public int ElapsedSeconds;

    public Game(int gameFieldWidthInCells, int gameFieldHeightInCells, int cellSizeInPixels, int totalMines)
    {
        widthInCells = gameFieldWidthInCells;
        heightInCells = gameFieldHeightInCells;
        this.totalMines = totalMines;

        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell(x, y, cellSizeInPixels);

        selectedCell = null;

        PrepareToStart();
    }

    public int RemainingUnmarkedMines { get; private set; }

    public event EventHandler Victory = delegate { };
    public event EventHandler Defeat = delegate { };

    public void SelectCell(int x, int y, out bool isSelectedCellChanged)
    {
        if (selectedCell != cells[x, y])
        {
            selectedCell = cells[x, y];
            isSelectedCellChanged = true;
        }
        else
        {
            isSelectedCellChanged = false;
        }
    }

    public void PrepareToStart()
    {
        isStarted = false;
        RemainingUnmarkedMines = totalMines;
        ElapsedSeconds = 0;

        foreach (Cell cell in cells)
            cell.ClearCell();
    }

    private void Start(int x, int y)
    {
        for (int i = 0; i < totalMines; i++)
            PlantNextMineExceptCell(x, y);
    }

    public void DrawGameField(Graphics graphics)
    {
        foreach (Cell cell in cells)
            cell.DrawCell(graphics);

        if (selectedCell != null)
            selectedCell.DrawSelectedCell(graphics);
    }

    public bool TryOpenCell(int x, int y)
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

        if (cells.Length - openedCellCount == totalMines)
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

            foreach (Cell cellToEnqueue in EnumerateAdjacentCells(cell))
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
        Cell cellToMark = cells[x, y];

        if (cellToMark.IsMarked)
        {
            cellToMark.IsMarked = false;
            RemainingUnmarkedMines++;
        }
        else if (RemainingUnmarkedMines > 0 && !cellToMark.IsOpen)
        {
            cellToMark.IsMarked = true;
            RemainingUnmarkedMines--;
        }
    }

    public void SmartClick(int x, int y)
    {
        Cell clickedCell = cells[x, y];

        if (clickedCell.IsMarked || !clickedCell.IsOpen || !isStarted)
            return;

        int flagNumber = 0;

        foreach (Cell cell in EnumerateAdjacentCells(clickedCell))
        {
            if (cell.IsMarked)
                flagNumber++;
        }

        if (clickedCell.Number != flagNumber)
            return;

        foreach (Cell cell in EnumerateAdjacentCells(clickedCell))
        {
            if (cell.IsOpen ||
                cell.IsMarked)
                continue;

            if (!TryOpenCell(cell.X, cell.Y))
                return;
        }
    }

    private void DefineCellNumberAroundCell(int x, int y)
    {
        foreach (Cell cell in EnumerateAdjacentCells(cells[x, y]))
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

    private void PlantNextMineExceptCell(int x, int y)
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);
        Cell randomCell = cells[randomX, randomY];

        if (randomCell.IsMined == false && cells[x, y] != randomCell)
        {
            randomCell.IsMined = true;
            DefineCellNumberAroundCell(randomX, randomY);
        }
        else
        {
            PlantNextMineExceptCell(x, y);
        }
    }

    private IEnumerable<Cell> EnumerateAdjacentCells(Cell cell)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (cell.X + dx >= 0 && cell.X + dx < widthInCells &&
                    cell.Y + dy >= 0 && cell.Y + dy < heightInCells)
                    yield return cells[cell.X + dx, cell.Y + dy];
    }
}