﻿namespace Minesweeper;

public class Game
{
    private static readonly Random random = new();

    private readonly Cell[,] cells; //[x][y]
    private readonly int heightInCells = 9;
    private readonly int widthInCells = 9;

    private bool isEnd;

    public int minesCount;
    public int minesNumber;
    public int time = 0;

    public int mouseX;
    public int mouseY;

    public Game(int widthInPixels, int heightInPixels)
    {
        Cell.cellWidthInPixels = widthInPixels / widthInCells;
        Cell.cellHeightInPixels = heightInPixels / heightInCells;

        cells = new Cell[widthInCells, heightInCells];

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y] = new Cell(x, y);

        Start();
    }

    public void Start()
    {
        time = 0;
        isEnd = false;
        minesCount = 10;
        minesNumber = minesCount;

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y].ClearCell();

        for (int i = 0; i < minesNumber; i++)
            PlantNextMine();
    }

    private void DefineCellNumber(int x, int y)
    {
        for (int maskX = x - 1; maskX <= x + 1; maskX++)
            for (int maskY = y - 1; maskY <= y + 1; maskY++)
                if (maskX >= 0 && maskX < widthInCells && maskY >= 0 && maskY < heightInCells)
                    cells[maskX, maskY].number++;
    }

    public void OpenSelectedCell()
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;

        if (cells[x, y].isMarked)
            return;

        if (cells[x, y].isMine)
        {
            OpenCellsWithMine();

            ShowFailMessage();
            isEnd = true;

            Start();

            return;
        }

        OpenOtherCells(x, y);

        int openedCellCount = 0;

        for (int i = 0; i < widthInCells; i++)
            for (int j = 0; j < heightInCells; j++)
                if (cells[i, j].isOpen)
                    openedCellCount++;

        if (cells.Length - openedCellCount == minesNumber)
        {
            OpenCellsWithMine();
            ShowViktoryMessage();
            isEnd = true;
            Start();
        }
    }

    private void OpenOtherCells(int x, int y)
    {
        if (cells[x, y].number != 0)
        {
            cells[x, y].isOpen = true;
            return;
        }

        for (int maskX = x - 1; maskX <= x + 1; maskX++)
            for (int maskY = y - 1; maskY <= y + 1; maskY++)
                if (maskX >= 0 && maskX < widthInCells &&
                    maskY >= 0 && maskY < heightInCells &&
                    !cells[maskX, maskY].isOpen &&
                    !cells[maskX, maskY].isMine &&
                    !cells[maskX, maskY].isMarked)
                {
                    cells[maskX, maskY].isOpen = true;

                    OpenOtherCells(maskX, maskY);
                }
    }

    public void MarkCell()
    {
        int x = mouseX / Cell.cellWidthInPixels;
        int y = mouseY / Cell.cellHeightInPixels;

        if (cells[x, y].isMarked)
        {
            cells[x, y].isMarked = false;
            minesCount++;
        }
        else if (minesCount > 0 && !cells[x, y].isOpen)
        {
            cells[x, y].isMarked = true;
            minesCount--;
        }
    }

    private void OpenCellsWithMine()
    {
        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                if (cells[x, y].isMine && !cells[x, y].isMarked)
                    cells[x, y].isOpen = true;
    }

    private void PlantNextMine()
    {
        int randomX = random.Next(widthInCells);
        int randomY = random.Next(heightInCells);

        if (cells[randomX, randomY].isMine == false)
        {
            cells[randomX, randomY].isMine = true;
            DefineCellNumber(randomX, randomY);
        }

        else
        {
            PlantNextMine();
        }
    }

    private void ShowFailMessage()
    {
        DialogResult result = MessageBox.Show("You LOSE!", "Fail", MessageBoxButtons.OK);
    }

    private void ShowViktoryMessage()
    {
        DialogResult result = MessageBox.Show("You Win!", "Viktory", MessageBoxButtons.OK);

    }
    public void DrawGameField(Graphics graphics)
    {

        for (int x = 0; x < widthInCells; x++)
            for (int y = 0; y < heightInCells; y++)
                cells[x, y].Draw(graphics);

        int backLightingCellX = mouseX / Cell.cellWidthInPixels;
        int backLightingCellY = mouseY / Cell.cellHeightInPixels;

        if (backLightingCellX >= 0 && backLightingCellX < widthInCells &&
            backLightingCellY >= 0 && backLightingCellY < heightInCells)
            cells[backLightingCellX, backLightingCellY].DrawBackLighting(graphics);

        if (isEnd)
        {
            for (int x = 0; x < widthInCells; x++)
                for (int y = 0; y < heightInCells; y++)
                    if (cells[x, y].isMarked && !cells[x, y].isMine)
                        cells[x, y].DrawCross(graphics);
        }
    }
}