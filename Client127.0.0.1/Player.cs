using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace 로그인화면
{
    public class Player
    {
        public Image image;
        public int Width, Height, steps;
        public int SlowDownFrameRate;
        public bool Go_Up, Go_Down, Go_Left, Go_Right;
        public bool Block_Up, Block_Down, Block_Left, Block_Right;
        public Location location;
        public Ch_Color clr;
        public Rectangle rectangle;

        public Player(Ch_Color clr)
        {
            image = BaseForm_test.images[0, (int)Direction.Down];
            Width = 50; Height = 50; steps = 0; SlowDownFrameRate = 0;
            Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;
            location = new Location();

            if (clr == Ch_Color.Black) { location.X = 0; location.Y = 0; }

            else if (clr == Ch_Color.Orange) { location.X = 0; location.Y = 100; }

            else if (clr == Ch_Color.Green) { location.X = 100; location.Y = 0; }

            rectangle = new Rectangle(location.X - 20, location.Y - 20, Width + 90, Height + 90);

        }
        public Player(Ch_Color clr, int arg_X, int arg_Y)
        {
            image = BaseForm_test.images[0, (int)Direction.Down];
            Width = 50; Height = 50; steps = 0; SlowDownFrameRate = 0;
            Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;

            location = new Location { X = arg_X, Y = arg_Y };
            rectangle = new Rectangle(location.X - 20, location.Y - 20, Width + 90, Height + 90);

        }
    }
}
