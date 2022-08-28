namespace Minesweeper;

public class Cell
{
    private static readonly Dictionary<int, Brush> Brushes = new()
    {
        { 1, System.Drawing.Brushes.Blue },
        { 2, System.Drawing.Brushes.Green },
        { 3, System.Drawing.Brushes.Red },
        { 4, System.Drawing.Brushes.DarkRed },
        { 5, System.Drawing.Brushes.DarkBlue },
        { 6, System.Drawing.Brushes.DarkRed },
        { 7, System.Drawing.Brushes.DarkRed },
        { 8, System.Drawing.Brushes.DarkRed }
    };

    private static readonly FontStyle CellFontStyle = FontStyle.Bold;
    private static readonly Font CellFont = new(FontFamily.GenericMonospace, 25, CellFontStyle);
    private static readonly StringFormat Format = new();

    private static readonly Bitmap ClosedCellSprite = Resource.ClosedCellSprite;
    private static readonly Bitmap SelectedCellSprite = Resource.SelectedCellSprite;
    private static readonly Bitmap MineSprite = Resource.MineSprite;
    private static readonly Bitmap MarkSprite = Resource.MarkSprite;
    private static readonly Bitmap CrossSprite = Resource.CrossSprite;

    private readonly Rectangle cellRectangle;

    public CellState CellState;
    public bool IsMined;
    public int Number;

    public Cell(int x, int y, int cellSizeInPixels)
    {
        Format.Alignment = StringAlignment.Center;

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
                graphics.DrawImage(ClosedCellSprite, cellRectangle);
                break;
            case CellState.Marked:
                graphics.DrawImage(ClosedCellSprite, cellRectangle);
                graphics.DrawImage(MarkSprite, cellRectangle);
                break;
            case CellState.IncorrectMarket:
                graphics.DrawImage(ClosedCellSprite, cellRectangle);
                graphics.DrawImage(MarkSprite, cellRectangle);
                graphics.DrawImage(CrossSprite, cellRectangle);
                break;
            case CellState.Opened:
                graphics.DrawRectangle(Pens.Black, cellRectangle);
                if (IsMined)
                    graphics.DrawImage(MineSprite, cellRectangle);
                else if (Number > 0)
                    graphics.DrawString(Number.ToString(), CellFont, Brushes[Number], cellRectangle, Format);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void DrawSelectedCell(Graphics graphics)
    {
        if (CellState == CellState.Closed)
            graphics.DrawImage(SelectedCellSprite, cellRectangle);
    }

    public void DrawMistakenlyOpenedCellWithMine(Graphics graphics)
    {
        graphics.FillRectangle(System.Drawing.Brushes.Red, cellRectangle);
    }
}