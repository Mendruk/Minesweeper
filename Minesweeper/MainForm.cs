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
    }
}