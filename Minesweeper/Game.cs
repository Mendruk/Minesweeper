﻿namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();

    private readonly Cell[,] cells; //[x,y]
    private Cell selectedCell;
    private readonly int heightInCells = 9;
    private readonly int widthInCells = 9;
    private bool isStarted;
    private int minesNumber;

    public int Time;

    public Game(int widthInPixels, int heightInPixels)
    {
        Cell.CellWidthInPixels = widthInPixels / widthInCells;
        Cell.CellHeightInPixels = heightInPixels / heightInCells;

        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell(x, y);

        selectedCell = cells[0, 0];

        PrepareToStart();
    }

    public int MinesCount { get; private set; }

    public event EventHandler Victory = delegate { };
    public event EventHandler Defeat = delegate { };

    public event EventHandler StopTimer = delegate { };
    public event EventHandler StartTimer = delegate { };

    public event EventHandler ChangeSelectedCell = delegate { };

    public void SelectCell(int mouseX, int mouseY)
    {
        int x = mouseX / Cell.CellWidthInPixels;
        int y = mouseY / Cell.CellHeightInPixels;

        if (x >= 0 && x < widthInCells &&
            y >= 0 && y < heightInCells &&
            selectedCell != cells[x, y])
        {
            selectedCell = cells[x, y];
            ChangeSelectedCell(this, EventArgs.Empty);
        }
    }

    public void PrepareToStart()
    {
        isStarted = false;
        MinesCount = 10;
        minesNumber = MinesCount;
        Time = 0;

        foreach (Cell cell in cells)
            cell.ClearCell();
    }

    private void Start(int x, int y)
    {
        for (int i = 0; i < minesNumber; i++)
            PlantNextMineExceptSelectedCell(x, y);

        StartTimer(this, EventArgs.Empty);
    }

    public void DrawGameField(Graphics graphics)
    {
        foreach (Cell cell in cells)
            cell.DrawCell(graphics);

        if (selectedCell != null)
            selectedCell.DrawBackLighting(graphics);
    }
    public bool TryOpenSelectedCell()
    {
        int x=selectedCell.X;
        int y=selectedCell.Y;

        return TryOpenSelectedCell(x, y);
    }

    public bool TryOpenSelectedCell(Cell cell)
    {
        return TryOpenSelectedCell(cell.X, cell.Y);
    }

    public bool TryOpenSelectedCell(int x, int y)
    {
        if (!isStarted)
        {
            Start(x, y);
            isStarted = true;
        }

        if (cells[x, y].IsMarked &&
            x >= 0 && x < widthInCells &&
            y >= 0 && y < heightInCells)
            return false;

        if (cells[x, y].IsMine)//Defeat
        {
            OpenCellsWithMine();

            StopTimer(this, EventArgs.Empty);

            Defeat(this, EventArgs.Empty);

            PrepareToStart();

            return false;
        }

        OpenOtherCells(x, y);

        if (CheckForVictory())
        {
            OpenCellsWithMine();

            StopTimer(this, EventArgs.Empty);

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
            if (cell.IsOpen)
                openedCellCount++;

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

            ActionAroundCell(cell.X, cell.Y, (x, y) =>
            {
                if (!cells[x, y].IsOpen &&
                    !cells[x, y].IsMine &&
                    !cells[x, y].IsMarked)
                    cellsQueue.Enqueue(cells[x, y]);
            });
        }
    }

    public void MarkCell()
    {
        int x = selectedCell.X;
        int y = selectedCell.Y;

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

    public void SmartClick()
    {
        int x = selectedCell.X;
        int y = selectedCell.Y;

        int flagNumber = 0;

        ActionAroundCell(x, y, (x, y) =>
        {
            if (cells[x, y].IsMarked)
                flagNumber++;
        });

        if (cells[x, y].Number == flagNumber)
        {
            List<Cell> cellsToOpen = new();

            ActionAroundCell(x, y, (x, y) =>
            {
                if (!cells[x, y].IsOpen &&
                    !cells[x, y].IsMarked)
                    cellsToOpen.Add(cells[x, y]);
            });

            foreach (Cell cell in cellsToOpen)
            {
                if (TryOpenSelectedCell(cell))
                    cell.IsOpen = true;
            }
        }
    }

    private void DefineCellNumberAroundCell(int x, int y)
    {
        ActionAroundCell(x, y, (x, y) => cells[x, y].Number++);
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

    private void ActionAroundCell(int x, int y, Action<int, int> action)
    {
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (x + dx >= 0 && x + dx < widthInCells &&
                    y + dy >= 0 && y + dy < heightInCells)
                    action(x + dx, y + dy);
    }
}