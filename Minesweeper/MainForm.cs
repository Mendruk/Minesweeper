namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            InitializeComponent();  
            game=new Game(pictureGameField.Width, pictureGameField.Height);
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
            labelTimer.Text = string.Format("{0}:{1}:{2}",(game.time/3600).ToString(),(game.time/600%60).ToString(),(game.time/10 % 60).ToString());
            pictureGameField.Refresh();
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

            labelMinesCount.Text = game.minesCount.ToString();

            pictureGameField.Refresh();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            game.Start();

            pictureGameField.Refresh();
        }
    }
}