﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Minesweeper
{
    class Images
    {

        private Image mine = Properties.Resources.Mine;
        private Image mineExploded = Properties.Resources.MineExploded;
        private Image flag = Properties.Resources.Flag;
        private Image mineCrossed = Properties.Resources.MineCrossed;

        public Bitmap Mine { get; private set; }
        public Bitmap MineExploded { get; private set; }
        public Bitmap Flag { get; private set; }
        public Bitmap MineCrossed { get; private set; }

        public Images()
        {
            Mine = new Bitmap(mine, new Size(15, 15));
            MineExploded = new Bitmap(mineExploded, new Size(15, 15));
            Flag = new Bitmap(flag, new Size(20,20));
            MineCrossed = new Bitmap(mineCrossed, new Size(15, 15));
        }
        //public Image Mine
        //{
        //    set
        //    {
        //        Mine = Image.FromFile(@"C:\Users\Vertigo\Desktop\Minesweeper\Minesweeper\bomb.png");
        //    }
        //    get
        //    {
        //        return new Bitmap(Mine, new Size(15, 15));
        //    }
        //}
        //public Image MineExploded
        //{
        //    get
        //    {
        //        return Image.FromFile(@"C:\Users\Vertigo\Desktop\Minesweeper\Minesweeper\MineExploded.jpg");
        //    } 
        //}


    }
}
