namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            game=new Game();
            InitializeComponent();
        }

        private void pictureGameField_Paint(object sender, PaintEventArgs e)
        {
            game.DrawGameField(e.Graphics,pictureGameField.Width,pictureGameField.Height);
        }

        private void pictureGameField_MouseMove(object sender, MouseEventArgs e)
        {
            game.mouseX=e.X;
            game.mouseY=e.Y;

            pictureGameField.Refresh();
        }

        private void timer_Tick(object sender, EventArgs e)
        {

        }


        private void pictureGameField_Click(object sender, EventArgs e)
        {
            game.OpenCell();
            pictureGameField.Refresh();
        }
    }
}