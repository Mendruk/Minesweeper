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

    private Rectangle cellRectangle;

    public static int cellWidthInPixels;
    public static int cellHeightInPixels;

    public bool isMine;
    public bool isOpen;
    public bool isMarked;
    public int number;

    public int X { get; }
    public int Y { get; }

    public Cell(int x, int y)
    {
        ClearCell();

        X = x;
        Y = y;

        int cellXInPixels = x * cellWidthInPixels;
        int cellYInPixels = y * cellHeightInPixels;

        cellRectangle = new Rectangle(cellXInPixels, cellYInPixels, cellHeightInPixels, cellWidthInPixels);
    }

    public void ClearCell()
    {
        isOpen = false;
        isMarked = false;
        isMine = false;
        number = 0;
    }

    public void Draw(Graphics graphics)
    {

        if (brushes.TryGetValue(number, out Brush brush))
            if (!isMine)
                graphics.DrawString(number.ToString(), cellFont, brush, cellRectangle);

        if (isMine)
            graphics.DrawImage(mineSprite, cellRectangle);

        if (!isOpen)
            graphics.FillRectangle(closedCellBrush, cellRectangle);

        if (isMarked)
            graphics.DrawImage(flagSprite, cellRectangle);

        graphics.DrawRectangle(edgingCell, cellRectangle);
    }

    public void DrawBackLighting(Graphics graphics)
    {
        if(!isOpen)
            graphics.DrawRectangle(backLightPen, cellRectangle);
    }

    public void DrawCross(Graphics graphics)
    {
        if (isMarked && !isMine)
            graphics.DrawImage(crossSprite, cellRectangle);
    }
}