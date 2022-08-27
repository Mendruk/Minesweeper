namespace Minesweeper;

public partial class MainForm : Form
{    
    private const int GameFieldHeightInCells = 9;
    private const int GameFieldWidthInCells = 9;
    private const int TotalMine = 10;
    private readonly int cellSizeInPixels;
    private readonly Game game;

    public MainForm()
    {
        InitializeComponent();

        int cellWidthInPixels = pictureGameField.Width / GameFieldWidthInCells;
        int cellHeightInPixels = pictureGameField.Height / GameFieldHeightInCells;

        cellSizeInPixels = Math.Min(cellWidthInPixels, cellHeightInPixels);

        game = new Game(GameFieldWidthInCells, GameFieldHeightInCells, cellSizeInPixels, TotalMine);

        timer.Stop();

        game.Defeat += OnDefeat;
        game.Victory += OnVictory;
    }

    private void pictureGameField_Paint(object sender, PaintEventArgs e)
    {
        game.DrawGameField(e.Graphics);

        labelTimer.Text = TimeSpan.FromSeconds(game.ElapsedSeconds).ToString();
    }

    private void pictureGameField_MouseMove(object sender, MouseEventArgs e)
    {
        int x = e.X / cellSizeInPixels;
        int y = e.Y / cellSizeInPixels;

        if(IsCellOutsideGameField(x,y))
            return;

        game.SelectCell(x, y, out bool isSelectedCellChanged);

        if (isSelectedCellChanged)
            pictureGameField.Refresh();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        game.ElapsedSeconds++;
        labelTimer.Text = TimeSpan.FromSeconds(game.ElapsedSeconds).ToString();
    }

    private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
    {
        timer.Start();

        int x = e.X / cellSizeInPixels;
        int y = e.Y / cellSizeInPixels;

        if (IsCellOutsideGameField(x, y))
            return;

        if (e.Button == MouseButtons.Left)
            game.TryOpenCell(x,y);

        if (e.Button == MouseButtons.Right)
            game.MarkCell(x,y);

        if (e.Button == MouseButtons.Middle)
            game.SmartClick(x,y);

        labelMinesCount.Text = game.RemainingUnmarkedMines.ToString();

        pictureGameField.Refresh();
    }

    private bool IsCellOutsideGameField(int x, int y)
    {
        return (x < 0 || x >= GameFieldWidthInCells ||
                y < 0 || y >= GameFieldHeightInCells);
    }
    private void buttonRestart_Click(object sender, EventArgs e)
    {
        timer.Stop();
        game.Restart();
        pictureGameField.Refresh();
    }

    private void OnDefeat(object? sender, EventArgs e)
    {
        timer.Stop();
        pictureGameField.Refresh();
        MessageBox.Show("You LOSE!", "Defeat");
        game.Restart();
    }

    private void OnVictory(object? sender, EventArgs e)
    {
        timer.Stop();
        pictureGameField.Refresh();
        MessageBox.Show("You Win!", "Victory");
        game.Restart();
    }
}