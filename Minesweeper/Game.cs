namespace Minesweeper
{
    public class Game
    {
        private readonly Pen gridPen = Pens.Black;
        private readonly int widthInCells = 9;
        private int heightInCells = 9;
        private Cell[,] cells;

        public Game()
        {
            cells = new Cell[widthInCells, heightInCells];
            for (int x = 0; x < widthInCells; x++)
                for (int y = 0; y < heightInCells; y++)
                    cells[x, y] = new Cell();
        }

        public void DrawGameField(Graphics graphics, int widthInPixels, int heightInPixels)
        {
            int cellWidthInPixels  = widthInPixels  / widthInCells;
            int cellHeightInPixels = heightInPixels / heightInCells;

            for (int x = 0; x < widthInCells; x++)
                for (int y = 0; y < heightInCells; y++)
                    graphics.DrawRectangle(gridPen, cellWidthInPixels*x, cellHeightInPixels*y, cellWidthInPixels, cellHeightInPixels);
        }
    }
}
