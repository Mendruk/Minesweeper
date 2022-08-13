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

    private readonly Brush bombBrush = Brushes.Black;
    public bool isBomb;
    private bool isOpened = false;
    public int number;


    public void Draw(Graphics graphics, int x, int y)
    {
        if (brushes.TryGetValue(number, out Brush brush))
            if (!isBomb)
                graphics.DrawString(number.ToString(), SystemFonts.DefaultFont, brush, x, y);
        if (isBomb)
            graphics.DrawString("Bomb", SystemFonts.DefaultFont, bombBrush, x, y);

    }
}