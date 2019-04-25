using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace Minesweeper
{
    class Game
    {
        public event EventHandler DismantledMinesChanged;
        public event EventHandler Tick;

        private int dismantledMines;
        Timer timer;
        Square[,] squares;
        private int incorrectDismantledMines;
        public int time;

        public Game(Panel panel,int width,int height,int mines)
        {
            Panel = panel;
            Width = width;
            Height = height;
            Mines = mines;
        }

        public Panel Panel { get; }

        public int Height { get; }

        public int Width { get; }

        public int Mines { get; }

        public int DismantledMines
        {
            get
            {
                return dismantledMines + incorrectDismantledMines;
            }
        }

        private void OnTick()
        {
            if(Tick!=null)
            {
                Tick(this, new EventArgs());
            }
        }

        public void Start()
        {

            //Panel.SuspendLayout();
            time = 0;
            dismantledMines = 0;
            incorrectDismantledMines = 0;
            OnTick();
            Panel.Enabled = true;
            Panel.Controls.Clear();

            // Create Spots
            squares = new Square[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Square s = new Square(this, x, y);
                    s.Explode += new EventHandler(Explode);
                    s.Dismantle += new EventHandler(Dismantle);
                    squares[x, y] = s;
                }
            }

            // Place Mines
            int b = 0;
            Random r = new Random();
            while (b < Mines)
            {
                int x = r.Next(Width);
                int y = r.Next(Height);

                Square s = squares[x, y];
                if (!s.Mined)
                {
                    s.Mined = true;
                    b++;
                }
            }

            OnDismantledMinesChanged();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(TimerTick);
            timer.Enabled = true;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            time++;
            OnTick();
        }

        private void Dismantle(object sender, EventArgs e)
        {
            Square s = (Square)sender;

            if(s.Dismantled)
            {
                if(s.Mined)
                {
                    dismantledMines++;
                }
                else
                {
                    incorrectDismantledMines++;
                }
            }
            else
            {
                if(s.Mined)
                {
                    dismantledMines--;
                }
                else
                {
                    incorrectDismantledMines--;
                }
            }

            OnDismantledMinesChanged();

            if(dismantledMines==Mines)
            {
                timer.Enabled = false;
                Panel.Enabled = false;
            }
        }

        protected void OnDismantledMinesChanged()
        {
            if (DismantledMinesChanged != null)
            {
                DismantledMinesChanged(this, new EventArgs());
            }
        }

        public bool IsBomb(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    return squares[x, y].Mined;
                }
            }
            return false;
        }

        public void OpenSpot(int x, int y)
        {
            if (x >= 0 && x < Width)
            {
                if (y >= 0 && y < Height)
                {
                    squares[x, y].Open();
                }
            }
        }

        private void Explode(object sender, EventArgs e)
        {
            timer.Enabled = false;

            foreach (Square s in squares)
            {
                s.RemoveEvents();
                if (s.Mined)
                {
                    

                    Images mineImage = new Images();
                    s.Button.Image = mineImage.Mine;


                }
            }
        }

        

        
    }
}
