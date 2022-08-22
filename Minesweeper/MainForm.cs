namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;
        private int gameFieldWidthInCells = 9;
        private int gameFieldHeightInCells = 9;
        private int cellWidthInPixels;
        private int cellHeightInPixels;

        public MainForm()
        {
            InitializeComponent();

            cellWidthInPixels = pictureGameField.Width / gameFieldWidthInCells;
            cellHeightInPixels = pictureGameField.Height / gameFieldHeightInCells;

            game = new Game(gameFieldWidthInCells, gameFieldHeightInCells, cellWidthInPixels, cellHeightInPixels);

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
            int x = e.X / cellWidthInPixels;
            int y = e.Y / cellHeightInPixels;

            if (x >= 0 && x < gameFieldWidthInCells &&
                y >= 0 && y < gameFieldHeightInCells)
            {
                bool isSelectedCellChanged=false;
                game.SelectCell(x, y, out isSelectedCellChanged);
                
                if(isSelectedCellChanged) 
                    pictureGameField.Refresh();
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.ElapsedSeconds++;
            labelTimer.Text = TimeSpan.FromSeconds(game.ElapsedSeconds).ToString();
            pictureGameField.Refresh();
        }


        private void pictureGameField_Click(object sender, EventArgs e)
        {
            pictureGameField.Refresh();
        }

        private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
        {
            timer.Start();

            if (e.Button == MouseButtons.Left)
                game.TryOpenSelectedCell();

            if (e.Button == MouseButtons.Right)
                game.MarkCell();

            if (e.Button == MouseButtons.Middle)
                game.SmartClick();

            labelMinesCount.Text = game.MinesCount.ToString();

            pictureGameField.Refresh();
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
}