namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();

    private readonly Cell[,] cells; //[x,y]
    private readonly int heightInCells = 9;
    private readonly int widthInCells = 9;
    private int minesNumber;
    private bool isEnd = false;
    
    public int time;
    public int MinesCount { get; private set; }

    public event EventHandler Victory = delegate { };
    public event EventHandler Defeat = delegate { };

    public Game(int widthInPixels, int heightInPixels)
    {
        Cell.cellWidthInPixels = widthInPixels / widthInCells;
        Cell.cellHeightInPixels = heightInPixels / heightInCells;

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
        time = 0;
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

        foreach (Cell cell in cells)
        {
            cell.DrawCell(graphics);
        }
    }

    //TODO
    public bool TryOpenSelectedCell(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;

        if (isEnd)
        {
            Start(x, y);
            isEnd = false;
        }

        if (cells[x, y].isMarked)
            return false;

        if (cells[x, y].isMine)
        {
            OpenCellsWithMine();

            Defeat(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }

        OpenOtherCells(x, y);

        int openedCellCount = 0;

        foreach (Cell cell in cells)
        {
            if (cell.isOpen)
                openedCellCount++;
        }

        if (cells.Length - openedCellCount == minesNumber)
        {
            OpenCellsWithMine();

            Victory(this, EventArgs.Empty);

            PrepareToStart();
        }
        return true;
    }

    private void OpenOtherCells(int x, int y)
    {
        if (cells[x, y].number != 0)
        {
            cells[x, y].isOpen = true;
            return;
        }

        Queue<Cell> cellsQueue = new();
        cellsQueue.Enqueue(cells[x, y]);

        while (cellsQueue.Count > 0)
        {
            Cell cell = cellsQueue.Dequeue();

            cell.isOpen = true;

            if (cell.number != 0)
                continue;

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (cell.X + dx < 0 || cell.X + dx >= widthInCells ||
                        cell.Y + dy < 0 || cell.Y + dy >= heightInCells)
                        continue;

                    if (cells[cell.X + dx, cell.Y + dy].isOpen ||
                        cells[cell.X + dx, cell.Y + dy].isMine ||
                        cells[cell.X + dx, cell.Y + dy].isMarked)
                        continue;

                    cellsQueue.Enqueue(cells[cell.X + dx, cell.Y + dy]);
                }
        }
    }

    public void MarkCell(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;

        if (cells[x, y].isMarked)
        {
            cells[x, y].isMarked = false;
            MinesCount++;
        }
        else if (MinesCount > 0 && !cells[x, y].isOpen)
        {
            cells[x, y].isMarked = true;
            MinesCount--;
        }
    }
    //TODO
    public void SmartClick(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;
        int flagNumber = 0;

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (x + dx >= 0 && x + dx < widthInCells &&
                    y + dy >= 0 && y + dy < heightInCells &&
                    cells[x + dx, y + dy].isMarked)
                    flagNumber++;

        if (cells[x, y].number == flagNumber)
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                    if (x + dx >= 0 && x + dx < widthInCells &&
                        y + dy >= 0 && y + dy < heightInCells &&
                        !cells[x + dx, y + dy].isOpen &&
                        !cells[x + dx, y + dy].isMarked)
                    {
                        if (!TryOpenSelectedCell((x + dx)* Cell.cellWidthInPixels, (y + dy)* Cell.cellHeightInPixels))
                        {
                            return;
                        }
                    }
    }

    private void OpenCellsWithMine()
    {
        foreach (Cell cell in cells)
        {
            if (!cell.isMine && cell.isMarked)
                cell.isCross = true;
            if (cell.isMine && !cell.isMarked)
                cell.isOpen = true;
        }

    }

    private void DefineCellNumber(int x, int y)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (x + dx >= 0 && x + dx < widthInCells &&
                    y + dy >= 0 && y + dy < heightInCells)
                    cells[x + dx, y + dy].number++;
    }

    private void PlantNextMineExceptSelectedCell(int x, int y)
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);

        if (cells[randomX, randomY].isMine == false && cells[x, y] != cells[randomX, randomY])
        {
            cells[randomX, randomY].isMine = true;
            DefineCellNumber(randomX, randomY);
        }

        else
        {
            PlantNextMineExceptSelectedCell(x, y);
        }
    }


}