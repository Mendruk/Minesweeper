namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;

        public MainForm()
        {
            InitializeComponent();
            game = new Game(pictureGameField.Width, pictureGameField.Height);
            timer.Stop();

            game.Defeat += ShowDefeatMessage;
            game.Victory += ShowVictoryMessage;

            game.StartTimer += StartTimer;
            game.StopTimer += StopTimer;

            game.ChangeSelectedCell += RefreshGameField;

        }

        private void pictureGameField_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGameField(e.Graphics);
            labelTimer.Text = string.Format("{0}:{1,0:00}:{2,0:00}", game.Time / 360, game.Time / 60 % 60, game.Time % 60);
        }

        private void pictureGameField_MouseMove(object sender, MouseEventArgs e)
        {
            game.SelectCell(e.X,e.Y);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.Time++;
            labelTimer.Text = string.Format("{0}:{1,0:00}:{2,0:00}", game.Time / 360, game.Time / 60 % 60, game.Time % 60);
        }


        private void pictureGameField_Click(object sender, EventArgs e)
        {
            pictureGameField.Refresh();
        }

        private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
        {
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
            pictureGameField.Refresh();
            DialogResult result = MessageBox.Show("You LOSE!", "Defeat", MessageBoxButtons.OK);
        }

        private void ShowVictoryMessage(object? sender, EventArgs e)
        {
            pictureGameField.Refresh();
            DialogResult result = MessageBox.Show("You Win!", "Victory", MessageBoxButtons.OK);
        }

        private void StartTimer(object? sender, EventArgs e)
        {
            timer.Start();
        }

        private void StopTimer(object? sender, EventArgs e)
        {
            timer.Stop();
        }
        private void RefreshGameField(object? sender, EventArgs e)
        {
            pictureGameField.Refresh();
        }
    }
}