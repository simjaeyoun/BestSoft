using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace 로그인화면
{
    internal class Player
    {
        public Image image;
        public int Width, Height, steps;
        public int SlowDownFrameRate;
        public bool Go_Up, Go_Down, Go_Left, Go_Right;
        public bool Block_Up, Block_Down, Block_Left, Block_Right;
        public Location location;
        public Ch_Color clr;

        public Player(Ch_Color clr)
        {
            image = In_Game.images[0, (int)Direction.Down];
            Width = 50; Height = 50; steps = 0; SlowDownFrameRate = 0;
            Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;

            if (clr == Ch_Color.Black) { location = new Location { X = 0, Y = 0 }; }

            else if (clr == Ch_Color.Orange) { location = new Location { X = 0, Y = 100 }; }

            else if (clr == Ch_Color.Green) { location = new Location { X = 100, Y = 0 }; }
        }
        public Player(Ch_Color clr, int arg_X, int arg_Y)
        {
            image = In_Game.images[0, (int)Direction.Down];
            Width = 50; Height = 50; steps = 0; SlowDownFrameRate = 0;
            Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;

            location = new Location { X = arg_X, Y = arg_Y };  
            
        }
    }
}
