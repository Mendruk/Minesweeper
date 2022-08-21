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

    private static readonly Brush closedCellBrush = Brushes.Gray;
    private static readonly Pen edgingCell = Pens.Black;
    private static readonly Pen backLightPen = Pens.Aquamarine;

    private static readonly FontStyle cellFontStyle = FontStyle.Bold;
    private static readonly Font cellFont = new(FontFamily.GenericMonospace, 27, cellFontStyle);

    private static readonly Bitmap mineSprite = Resource.MineSprite;
    private static readonly Bitmap flagSprite = Resource.FlagSprite;
    private static readonly Bitmap crossSprite = Resource.CrossSprite;
    
    private readonly Rectangle cellRectangle;
    
    public static int CellWidthInPixels;
    public static int CellHeightInPixels;

    public bool IsCross;
    public bool IsMarked;
    public bool IsMine;
    public bool IsOpen;
    public int Number;

    public Cell(int x, int y)
    {
        ClearCell();

        X = x;
        Y = y;

        int cellXInPixels = x * CellWidthInPixels;
        int cellYInPixels = y * CellHeightInPixels;

        cellRectangle = new Rectangle(cellXInPixels, cellYInPixels, CellHeightInPixels, CellWidthInPixels);
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
        if (brushes.TryGetValue(Number, out Brush brush))
            if (!IsMine)
                graphics.DrawString(Number.ToString(), cellFont, brush, cellRectangle);

        if (IsMine)
            graphics.DrawImage(mineSprite, cellRectangle);

        if (!IsOpen)
            graphics.FillRectangle(closedCellBrush, cellRectangle);

        if (IsMarked)
            graphics.DrawImage(flagSprite, cellRectangle);

        if (IsCross)
            graphics.DrawImage(crossSprite, cellRectangle);

        graphics.DrawRectangle(edgingCell, cellRectangle);
    }

    public void DrawBackLighting(Graphics graphics)
    {
        if (!IsOpen)
            graphics.DrawRectangle(backLightPen, cellRectangle);
    }
}