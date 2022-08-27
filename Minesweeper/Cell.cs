namespace Minesweeper;

public class Cell
{
    private static readonly Dictionary<int, Brush> brushes = new()
    {
        { 1, Brushes.Blue },
        { 2, Brushes.Green },
        { 3, Brushes.Red },
        { 4, Brushes.DarkRed },
        { 5, Brushes.DarkBlue },
        { 6, Brushes.DarkRed },
        { 7, Brushes.DarkRed },
        { 8, Brushes.DarkRed }
    };

    private static readonly FontStyle cellFontStyle = FontStyle.Bold;
    private static readonly Font cellFont = new(FontFamily.GenericMonospace, 25, cellFontStyle);
    private static readonly StringFormat format = new();

    private static readonly Bitmap closedCellSprite = Resource.ClosedCellSprite;
    private static readonly Bitmap selectedCellSprite = Resource.SelectedCellSprite;
    private static readonly Bitmap mineSprite = Resource.MineSprite;
    private static readonly Bitmap markSprite = Resource.MarkSprite;
    private static readonly Bitmap crossSprite = Resource.CrossSprite;

    private readonly Rectangle cellRectangle;

    public CellState CellState;
    public bool IsMined;
    public int Number;

    public Cell(int x, int y, int cellSizeInPixels)
    {
        format.Alignment = StringAlignment.Center;

        ClearCell();

        X = x;
        Y = y;

        cellRectangle = new Rectangle(x * cellSizeInPixels, y * cellSizeInPixels, cellSizeInPixels, cellSizeInPixels);
    }

    public int X { get; }
    public int Y { get; }

    public void ClearCell()
    {
        CellState = CellState.Closed;
        IsMined = false;
        Number = 0;
    }

    public void DrawCell(Graphics graphics)
    {
        switch (CellState)
        {
            case CellState.Closed:
                graphics.DrawImage(closedCellSprite, cellRectangle);
                break;
            case CellState.Marked:
                graphics.DrawImage(closedCellSprite, cellRectangle);
                graphics.DrawImage(markSprite, cellRectangle);
                break;
            case CellState.IncorrectMarket:
                graphics.DrawImage(closedCellSprite, cellRectangle);
                graphics.DrawImage(markSprite, cellRectangle);
                graphics.DrawImage(crossSprite, cellRectangle);
                break;
            case CellState.Opened:
                graphics.DrawRectangle(Pens.Black, cellRectangle);
                if (IsMined)
                    graphics.DrawImage(mineSprite, cellRectangle);
                else
                    if (Number > 0)
                    graphics.DrawString(Number.ToString(), cellFont, brushes[Number], cellRectangle, format);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void DrawSelectedCell(Graphics graphics)
    {
        if (CellState==CellState.Closed)
            graphics.DrawImage(selectedCellSprite, cellRectangle);
    }
}