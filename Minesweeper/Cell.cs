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
    private static readonly Bitmap flagSprite = Resource.FlagSprite;
    private static readonly Bitmap crossSprite = Resource.CrossSprite;

    private readonly Rectangle cellRectangle;

    public bool IsCross;
    public bool IsMarked;
    public bool IsMine;
    public bool IsOpen;
    public int Number;

    public Cell(int x, int y, int widthInPixels, int heightInPixels)
    {
        format.Alignment = StringAlignment.Center;

        ClearCell();

        X = x;
        Y = y;

        cellRectangle = new Rectangle(x * widthInPixels, y * heightInPixels, widthInPixels, heightInPixels);
    }

    public int X { get; }
    public int Y { get; }

    public void ClearCell()
    {
        IsOpen = false;
        IsMarked = false;
        IsMine = false;
        IsCross = false;
        Number = 0;
    }

    public void DrawCell(Graphics graphics)
    {
        if (brushes.TryGetValue(Number, out Brush? brush) && !IsMine && IsOpen)
            graphics.DrawString(Number.ToString(), cellFont, brush, cellRectangle, format);

        if (IsMine && IsOpen)
            graphics.DrawImage(mineSprite, cellRectangle);

        if (!IsOpen)
            graphics.DrawImage(closedCellSprite, cellRectangle);
        else
            graphics.DrawRectangle(Pens.Black, cellRectangle);

        if (IsMarked)
            graphics.DrawImage(flagSprite, cellRectangle);

        if (IsCross)
            graphics.DrawImage(crossSprite, cellRectangle);
    }

    public void DrawBackLighting(Graphics graphics)
    {
        if (!IsOpen && !IsMarked)
            graphics.DrawImage(selectedCellSprite, cellRectangle);
    }
}