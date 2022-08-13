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

    private static readonly Brush bombBrush = Brushes.Black;
    private static readonly Brush closedCellBrush = Brushes.Gray;
    private static readonly Pen edgingCell=Pens.Black;
    public static int cellWidthInPixels;
    public static int cellHeightInPixels;
    public bool isBomb = false;
    public bool isOpen = false;
    public int number = 0;



    public void Draw(Graphics graphics, int cellX, int cellY)
    {
        int cellXInPixels= cellX * cellWidthInPixels;
        int cellYInPixels = cellY * cellHeightInPixels;

        if (brushes.TryGetValue(number, out Brush brush))
            if (!isBomb)
                graphics.DrawString(number.ToString(), SystemFonts.DefaultFont, brush, cellXInPixels, cellYInPixels);
        if (isBomb)
            graphics.DrawString("Bomb", SystemFonts.DefaultFont, bombBrush, cellXInPixels, cellYInPixels);
        if (!isOpen)
        {
            graphics.FillRectangle(closedCellBrush, cellXInPixels, cellYInPixels, cellWidthInPixels, cellHeightInPixels);

        }
        graphics.DrawRectangle(edgingCell, cellXInPixels, cellYInPixels, cellWidthInPixels, cellHeightInPixels);
    }
}