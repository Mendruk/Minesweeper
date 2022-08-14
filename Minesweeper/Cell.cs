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

    private static readonly Brush mineBrush = Brushes.Black;
    private static readonly Brush closedCellBrush = Brushes.Gray;
    private static readonly Pen edgingCell = Pens.Black;
    private static readonly Pen backLightPen = Pens.Aquamarine;

    private static readonly Bitmap mineSprite = Resource.MineSprite;
    private static readonly Bitmap flagSprite = Resource.FlagSprite;

    public static int cellWidthInPixels;
    public static int cellHeightInPixels;


    public bool isMine = false;
    public bool isOpen = false;
    public bool isMarked = false;

    public int number = 0;

    public void ClearCell()
    {
        isOpen = false;
        isMarked = false;
        isMine = false;
        number = 0;
    }
    public void Draw(Graphics graphics, int cellX, int cellY)
    {
        int cellXInPixels = cellX * cellWidthInPixels;
        int cellYInPixels = cellY * cellHeightInPixels;

        if (brushes.TryGetValue(number, out Brush brush))
            if (!isMine)
                graphics.DrawString(number.ToString(), SystemFonts.DefaultFont, brush, cellXInPixels, cellYInPixels);

        if (isMine)
            graphics.DrawImage(mineSprite, cellXInPixels,cellYInPixels,cellHeightInPixels,cellWidthInPixels);

        if (!isOpen)
            graphics.FillRectangle(closedCellBrush, cellXInPixels, cellYInPixels, cellWidthInPixels, cellHeightInPixels);

        if (isMarked)
            graphics.DrawImage(flagSprite, cellXInPixels, cellYInPixels, cellHeightInPixels, cellWidthInPixels);

        graphics.DrawRectangle(edgingCell, cellXInPixels, cellYInPixels, cellWidthInPixels, cellHeightInPixels);
    }

    public void DrawBackLighting(Graphics graphics, int cellX, int cellY)
    {
        int cellXInPixels = cellX * cellWidthInPixels;
        int cellYInPixels = cellY * cellHeightInPixels;

        graphics.DrawRectangle(backLightPen, cellXInPixels, cellYInPixels, cellWidthInPixels, cellHeightInPixels);
    }
}