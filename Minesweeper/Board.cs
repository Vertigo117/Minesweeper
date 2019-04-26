using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    class Board
    {
        public event EventHandler Dismantle;
        public event EventHandler Explode;

        Game game;

        public bool Dismantled { get; private set; }

        public bool Mined { get; set; }

        public Button Button { get; }

        public bool Opened { get; private set; }

        public int X { get; }

        public int Y { get; }

        public Board(Game game, int x,int y)
        {
            this.game = game;
            X = x;
            Y = y;
            Button = new Button();
            Button.Text = "";

            int w = this.game.Panel.Width / this.game.Width;
            int h = this.game.Panel.Height / this.game.Height;

            Button.Width = w + 1;
            Button.Height = h + 1;
            Button.Left = w * X;
            Button.Top = h * Y;
            Button.Font = new Font("Arial Black", 9F, FontStyle.Regular, GraphicsUnit.Point,0);
            Button.BackColor = Color.Silver;
            Button.Click += new EventHandler(Click);
            Button.MouseDown += new MouseEventHandler(DismantleClick);

            game.Panel.Controls.Add(Button);
        }

        private void DismantleClick(object sender, MouseEventArgs e)
        {
            if (!Opened && e.Button == MouseButtons.Right)
            {
                if (Dismantled)
                {
                    Dismantled = false;
                    Button.BackColor = Color.Silver;
                    Button.Image = null;
                    //Button.Text = "?";
                }
                //else if (Button.Text == "?")
                //{
                //    Dismantled = false;
                //    Button.Text = "";
                //}
                else
                {
                    
                    Dismantled = true;
                    Images img = new Images();
                    Button.Image = img.Flag;
                    Button.BackColor = Color.Green;
                }

                OnDismantle();
            }
        }

        protected void OnDismantle()
        {
            if(Dismantle!=null)
            {
                Dismantle(this, new EventArgs());
            }
        }

        protected void OnExplode()
        {
            if (Explode != null)
            {
                Explode(this, new EventArgs());
            }
        }

        public void Open()
        {
            if (!Opened && !Dismantled)
            {
                Opened = true;
                // Подсчёт бомб
                int c = 0;
                if (game.IsBomb(X - 1, Y - 1)) c++;
                if (game.IsBomb(X - 0, Y - 1)) c++;
                if (game.IsBomb(X + 1, Y - 1)) c++;
                if (game.IsBomb(X - 1, Y - 0)) c++;
                if (game.IsBomb(X - 0, Y - 0)) c++;
                if (game.IsBomb(X + 1, Y - 0)) c++;
                if (game.IsBomb(X - 1, Y + 1)) c++;
                if (game.IsBomb(X - 0, Y + 1)) c++;
                if (game.IsBomb(X + 1, Y + 1)) c++;

                if (c > 0)
                {
                    Button.Text = c.ToString();
                    switch (c)
                    {
                        case 1:
                            Button.ForeColor = Color.Blue;
                            break;
                        case 2:
                            Button.ForeColor = Color.Green;
                            break;
                        case 3:
                            Button.ForeColor = Color.Red;
                            break;
                        case 4:
                            Button.ForeColor = Color.DarkBlue;
                            break;
                        case 5:
                            Button.ForeColor = Color.DarkRed;
                            break;
                        case 6:
                            Button.ForeColor = Color.LightBlue;
                            break;
                        case 7:
                            Button.ForeColor = Color.Orange; 
                            break;
                        case 8:
                            Button.ForeColor = Color.Ivory;
                            break;
                    }
                }
                else
                {
                    Button.BackColor = SystemColors.ControlLight;
                    Button.FlatStyle = FlatStyle.Flat;
                    Button.Enabled = false;

                    game.OpenSpot(X - 1, Y - 1);
                    game.OpenSpot(X - 0, Y - 1);
                    game.OpenSpot(X + 1, Y - 1);
                    game.OpenSpot(X - 1, Y - 0);
                    game.OpenSpot(X - 0, Y - 0);
                    game.OpenSpot(X + 1, Y - 0);
                    game.OpenSpot(X - 1, Y + 1);
                    game.OpenSpot(X - 0, Y + 1);
                    game.OpenSpot(X + 1, Y + 1);
                }
            }
        }

        private void Click(object sender, EventArgs e)
        {
            if (!Dismantled)
            {
                if (Mined)
                {
                    Button.BackColor = Color.Red;
                    Images mineImage = new Images();
                    Button.Image = mineImage.MineExploded;
                    OnExplode();
                }
                else
                {
                    Open();
                }
            }
        }

        public void RemoveEvents()
        {
            Button.Click -= new EventHandler(Click);
            Button.MouseDown -= new MouseEventHandler(DismantleClick);
        }
    }
}
