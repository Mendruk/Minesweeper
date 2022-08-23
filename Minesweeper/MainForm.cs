namespace Minesweeper;

public partial class MainForm : Form
{    
    private readonly Game game;
    private readonly int cellSizeInPixels;
    private readonly int gameFieldHeightInCells = 9;
    private readonly int gameFieldWidthInCells = 9;

    public MainForm()
    {
        InitializeComponent();

        int cellWidthInPixels = pictureGameField.Width / gameFieldWidthInCells;
        int cellHeightInPixels = pictureGameField.Height / gameFieldHeightInCells;

        cellSizeInPixels = cellWidthInPixels >= cellHeightInPixels 
            ? cellHeightInPixels 
            : cellWidthInPixels;

        game = new Game(gameFieldWidthInCells, gameFieldHeightInCells, cellSizeInPixels);

        timer.Stop();

        game.Defeat += ShowDefeatMessage;
        game.Victory += ShowVictoryMessage;
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

        bool isSelectedCellChanged = false;
            game.SelectCell(x, y, out isSelectedCellChanged);

            if (isSelectedCellChanged)
                pictureGameField.Refresh();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        game.ElapsedSeconds++;
        labelTimer.Text = TimeSpan.FromSeconds(game.ElapsedSeconds).ToString();
    }


    private void pictureGameField_Click(object sender, EventArgs e)
    {
        pictureGameField.Refresh();
    }

    private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
    {
        timer.Start();

        int x = e.X / cellSizeInPixels;
        int y = e.Y / cellSizeInPixels;

        if (IsCellOutsideGameField(x, y))
            return;

        if (e.Button == MouseButtons.Left)
            game.TryOpenSelectedCell(x,y);

        if (e.Button == MouseButtons.Right)
            game.MarkCell(x,y);

        if (e.Button == MouseButtons.Middle)
            game.SmartClick(x,y);

        labelMinesCount.Text = game.RemainingUnmarkedMines.ToString();

        pictureGameField.Refresh();
    }

    private bool IsCellOutsideGameField(int x, int y)
    {
        if (x < 0 || x >= gameFieldWidthInCells ||
            y < 0 || y >= gameFieldHeightInCells)
            return true;

        return false;
    }
    private void buttonRestart_Click(object sender, EventArgs e)
    {
        timer.Stop();
        game.PrepareToStart();
        pictureGameField.Refresh();
    }

    private void ShowDefeatMessage(object? sender, EventArgs e)
    {
        timer.Stop();
        pictureGameField.Refresh();
        DialogResult result = MessageBox.Show("You LOSE!", "Defeat", MessageBoxButtons.OK);
    }

    private void ShowVictoryMessage(object? sender, EventArgs e)
    {
        timer.Stop();
        pictureGameField.Refresh();
        DialogResult result = MessageBox.Show("You Win!", "Victory", MessageBoxButtons.OK);
    }
}