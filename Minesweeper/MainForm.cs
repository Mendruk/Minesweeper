namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;

        public MainForm()
        {
            InitializeComponent();  

            game=new Game(pictureGameField.Width, pictureGameField.Height);

            game.Defeat += ShowDefeatMessage;
            game.Victory += ShowVictoryMessage;

        }

        private void pictureGameField_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGameField(e.Graphics);
        }

        private void pictureGameField_MouseMove(object sender, MouseEventArgs e)
        {
            game.mouseX=e.X;
            game.mouseY=e.Y;

            pictureGameField.Refresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.time++;
            labelTimer.Text = (game.time/360).ToString() +":"+ (game.time / 60 % 60).ToString() + ":" + (game.time % 60).ToString();
        }


        private void pictureGameField_Click(object sender, EventArgs e)
        {
            pictureGameField.Refresh();
        }

        private void pictureGameField_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
                game.OpenSelectedCell();

            if (e.Button == MouseButtons.Right)
                game.MarkCell();

            if (e.Button == MouseButtons.Middle)
                game.SmartClick();

            labelMinesCount.Text = game.MinesCount.ToString();

            pictureGameField.Refresh();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            game.Start();

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