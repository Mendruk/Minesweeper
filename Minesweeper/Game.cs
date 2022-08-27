namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();
    private readonly int widthInCells;
    private readonly int heightInCells;
    private readonly int totalMines;
    private readonly Cell[,] cells; //[x,y]
    private Cell selectedCell;
    private bool areMinesPlanted;

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
        Restart();
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

    public void Restart()
    {
        areMinesPlanted = false;
        RemainingUnmarkedMines = totalMines;
        ElapsedSeconds = 0;

        foreach (Cell cell in cells)
            cell.ClearCell();
    }

    private void PlantMines(int x, int y)
    {
        for (int i = 0; i < totalMines; i++)
            PlantNextMineExceptCell(x, y);

        areMinesPlanted = true;
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
        if (!areMinesPlanted)
            PlantMines(x, y);

        if (cells[x, y].CellState == CellState.Marked)
            return false;

        if (cells[x, y].IsMined)//Defeat
        {
            OpenCellsWithMine();

            Defeat(this, EventArgs.Empty);

            return false;
        }

        cells[x, y].CellState = CellState.Opened;

        if (cells[x, y].Number == 0)
            OpenConnectedZeroArea(x, y);

        if (AreVictoryConditionsMet())
        {
            OpenCellsWithMine();

            Victory(this, EventArgs.Empty);

            return false;
        }

        return true;
    }

    private bool AreVictoryConditionsMet()
    {
        int closedCellCount = cells
            .OfType<Cell>()
            .Count(cell => (cell.CellState != CellState.Opened));

        return closedCellCount == totalMines;
    }

    private void OpenConnectedZeroArea(int x, int y)
    {
        Queue<Cell> cellsQueue = new();
        cellsQueue.Enqueue(cells[x, y]);

        while (cellsQueue.Count > 0)
        {
            Cell cell = cellsQueue.Dequeue();

            cell.CellState = CellState.Opened;

            if (cell.Number != 0)
                continue;

            foreach (Cell cellToEnqueue in EnumerateAdjacentCells(cell))
            {
                if (cellToEnqueue.CellState==CellState.Closed&&
                    !cellToEnqueue.IsMined )
                    cellsQueue.Enqueue(cellToEnqueue);
            }
        }
    }

    public void MarkCell(int x, int y)
    {
        Cell cellToMark = cells[x, y];

        if (cellToMark.CellState==CellState.Marked)
        {
            cellToMark.CellState=CellState.Closed;
            RemainingUnmarkedMines++;
        }
        else if (RemainingUnmarkedMines > 0 && cellToMark.CellState == CellState.Closed)
        {
            cellToMark.CellState = CellState.Marked;
            RemainingUnmarkedMines--;
        }
    }

    public void SmartClick(int x, int y)
    {
        Cell clickedCell = cells[x, y];

        if (clickedCell.CellState == CellState.Marked ||
            clickedCell.CellState == CellState.Closed || 
            !areMinesPlanted)
            return;

        int marksCount = EnumerateAdjacentCells(clickedCell).Count(cell => cell.CellState==CellState.Marked);

        if (clickedCell.Number != marksCount)
            return;

        foreach (Cell cell in EnumerateAdjacentCells(clickedCell))
        {
            if (cell.CellState == CellState.Opened ||
                cell.CellState == CellState.Marked)
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
    
    //todo
    private void OpenCellsWithMine()
    {
        foreach (Cell cell in cells)
        {
            if (!cell.IsMined && cell.CellState==CellState.Marked)
                cell.CellState= CellState.IncorrectMarket;
            if (cell.IsMined && cell.CellState != CellState.Marked)
                cell.CellState = CellState.Opened;
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