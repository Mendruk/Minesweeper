namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;

        private int mouseX;
        private int mouseY;

        public MainForm()
        {
            InitializeComponent();
            game = new Game(pictureGameField.Width, pictureGameField.Height);
            game.Defeat += ShowDefeatMessage;
            game.Victory += ShowVictoryMessage;
        }

        private void pictureGameField_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGameField(e.Graphics);
        }

        private void pictureGameField_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX=e.X;
            mouseY=e.Y;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.MouseX=mouseX;
            game.MouseY=mouseY;

            game.Time++;//Interval = 100
            labelTimer.Text = game.Time / 3600 + ":" + game.Time / 600 % 60 + ":" + game.Time/10 % 60;

            pictureGameField.Refresh();
        }


        private void pictureGameField_Click(object sender, EventArgs e)
        {
            pictureGameField.Refresh();
        }

        private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                game.TryOpenSelectedCell(e.X,e.Y);

            if (e.Button == MouseButtons.Right)
                game.MarkCell(e.X,e.Y);

            if (e.Button == MouseButtons.Middle)
                game.SmartClick(e.X,e.Y);

            labelMinesCount.Text = game.MinesCount.ToString();

            pictureGameField.Refresh();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
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
    }
}