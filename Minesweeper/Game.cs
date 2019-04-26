using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace Minesweeper
{
    class Game
    {
        public event EventHandler DismantledMinesChanged;
        public event EventHandler Tick;
        public event EventHandler Victory;
        public event EventHandler Defeat;
        

        private int dismantledMines;
        Timer timer;
        Square[,] squares;
        private int incorrectDismantledMines;
        //public int second;
        public DateTime stopwatch;
        private DateTime date;


        public Game(Panel panel,int width,int height,int mines, Button button)
        {
            //stopwatch = DateTime.Now;
            date = DateTime.Now;
            Panel = panel;
            Width = width;
            Height = height;
            Mines = mines;
            Button = button;

        }

        public Button Button { get; }

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
            //second = 0;
            
            dismantledMines = 0;
            incorrectDismantledMines = 0;
            OnTick();
            Panel.Enabled = true;
            Panel.Controls.Clear();

            // Кнопочки
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

            // Мины
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
            //timer.Enabled = true;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            //second++;
            //timeCounter.AddSeconds(second);
            //stopwatch.Start();
            long tick = DateTime.Now.Ticks - date.Ticks;
            stopwatch = new DateTime();
            stopwatch = stopwatch.AddTicks(tick);
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
                timer.Stop();
                Panel.Enabled = false;
                Victory(this,new EventArgs());
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
            Images img = new Images();
            timer.Stop();
            Defeat(this,new EventArgs());

            foreach (Square s in squares)
            {
                s.RemoveEvents();
                if (s.Mined)
                {
                    
                    
                    



                    if (!s.Dismantled)
                    {
                        
                        s.Button.Image = img.Mine;
                        s.Button.FlatStyle = FlatStyle.Standard;
                        s.Button.BackColor = SystemColors.ControlLight;
                        //s.Button.BackColor = SystemColors.ControlLight;
                    }
                    else
                    {
                        s.Button.Image = img.Flag;
                        //s.Button.FlatStyle = FlatStyle.Flat;
                    }


                }
                else
                {
                    if(s.Dismantled)
                    {
                        s.Button.Image = img.MineCrossed;
                        s.Button.FlatStyle = FlatStyle.Standard;
                        s.Button.BackColor = SystemColors.ControlLight;
                    }
                }

                
            }
        }

        

        
    }
}
