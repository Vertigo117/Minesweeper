using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
            Game game = new Game(panel1, 10, 10, 10);
            game.Tick += (sender1,e1) => labelTime.Text = game.time.ToString();
            game.DismantledMinesChanged += (sender1,e1)=> labelBombs.Text = (game.Mines - game.DismantledMines).ToString();
            game.Victory += new EventHandler(Victory_Handler);
            game.Start();
        }

        public void Victory_Handler(object sender, EventArgs e)
        {
            
            Stream str = Properties.Resources.Ten;
            SoundPlayer sp = new SoundPlayer(str);
            sp.Play();
            MessageBox.Show("Victory!!!");
        }
    }
}
