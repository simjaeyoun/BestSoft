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
        public Move_Key Key;
        public bool Block_Up, Block_Down, Block_Left, Block_Right;
        public Location location;
        public Ch_Color clr;
        public Rectangle rectangle;

        public string Obstacle_Name;
        public Info_Next Info;
        public int Map;

        public string address;

        public Player(Ch_Color clr, string add)
        {
            image = BaseForm_test.images[(int)clr, (int)Direction.Down_1];
            Width = 48; Height = 48; steps = 0; SlowDownFrameRate = 0;
            Key = new Move_Key { Go_Down = false, Go_Left = false, Go_Right = false, Go_Up = false };
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;
            location = new Location();

            Info = new Info_Next { ObstacleName = null, result = false };

            Obstacle_Name = null;
            Map = 0;

            address = add;

            if (clr == Ch_Color.Black) { location.X = 0; location.Y = 0; }

            else if (clr == Ch_Color.Orange) { location.X = 0; location.Y = 100; }

            else if (clr == Ch_Color.Green) { location.X = 100; location.Y = 0; }
            
            else if (clr == Ch_Color.Blue) { location.X = 100; location.Y = 100; }

            rectangle = new Rectangle(location.X - 20, location.Y - 20, Width + 90, Height + 90);

        }
        public Player(Ch_Color clr, int arg_X, int arg_Y, string add)
        {
            image = BaseForm_test.images[(int)clr, (int)Direction.Down_1];
            Width = 48; Height = 48; steps = 0; SlowDownFrameRate = 0;
            Key = new Move_Key { Go_Down = false, Go_Left = false, Go_Right = false, Go_Up = false };
            Block_Up = true; Block_Down = true; Block_Left = true; Block_Right = true;
            this.clr = clr;

            Info = new Info_Next { ObstacleName = null, result = false };

            Obstacle_Name = null;
            Map = 0;

            address = add;

            location = new Location { X = arg_X, Y = arg_Y };
            rectangle = new Rectangle(location.X - 20, location.Y - 20, Width + 90, Height + 90);
        }
    }
}
