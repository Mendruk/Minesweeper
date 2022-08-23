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
    private static readonly Bitmap MarkSprite = Resource.MarkSprite;
    private static readonly Bitmap crossSprite = Resource.CrossSprite;

    private readonly Rectangle cellRectangle;

    public bool IsIncorrectlyMarked;
    public bool IsMarked;
    public bool IsMined;
    public bool IsOpen;
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
        IsOpen = false;
        IsMarked = false;
        IsMined = false;
        IsIncorrectlyMarked = false;
        Number = 0;
    }

    public void DrawCell(Graphics graphics)
    {
        if (brushes.TryGetValue(Number, out Brush? brush) && !IsMined && IsOpen)
            graphics.DrawString(Number.ToString(), cellFont, brush, cellRectangle, format);

        if (IsMined && IsOpen)
            graphics.DrawImage(mineSprite, cellRectangle);

        if (!IsOpen)
            graphics.DrawImage(closedCellSprite, cellRectangle);
        else
            graphics.DrawRectangle(Pens.Black, cellRectangle);

        if (IsMarked)
            graphics.DrawImage(MarkSprite, cellRectangle);

        if (IsIncorrectlyMarked)
            graphics.DrawImage(crossSprite, cellRectangle);
    }

    public void DrawSelectedCell(Graphics graphics)
    {
        if (!IsOpen && !IsMarked)
            graphics.DrawImage(selectedCellSprite, cellRectangle);
    }
}