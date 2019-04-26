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
        Stream str; 
        SoundPlayer sp;

        public Form1()
        {
            InitializeComponent();
            str = Properties.Resources.war_never_changes;
            sp = new SoundPlayer(str);
            sp.Play();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            

            Game game = new Game(panel1, 10, 10, 10);
            game.Tick += (sender1, e1) => labelTime.Text = string.Format("{0:mm:ss}", game.stopwatch);
            game.DismantledMinesChanged += (sender1,e1)=> labelBombs.Text = (game.Mines - game.DismantledMines).ToString();
            game.Victory += new EventHandler(Victory_Handler);
            game.Defeat += new EventHandler(Defeat_Handler);
            game.Start();
        }

        private void Defeat_Handler(object sender, EventArgs e)
        {
            str = Properties.Resources.darkSouls;
            sp = new SoundPlayer(str);
            sp.Play();
            //MessageBox.Show("Defeat!");
        }

        public void Victory_Handler(object sender, EventArgs e)
        {
            
            str = Properties.Resources.Ten;
            sp = new SoundPlayer(str);
            sp.Play();
            //MessageBox.Show("Victory!!!");
        }
    }
}
