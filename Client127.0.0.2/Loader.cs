using OpenQA.Selenium.DevTools.V110.HeapProfiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace 로그인화면
{
    public enum Ch_Color
    {
        Black,
        Orange,
        Green,
        Blue,
        UnKnown
    }
    public enum Direction
    {
        Down_1, Down_2, Down_3, Down_4,
        Left_1, Left_2, Left_3, Left_4,
        Right_1, Right_2, Right_3, Right_4,
        Up_1, Up_2, Up_3, Up_4
    }
    public class Move_Key
    {
        public bool Go_Up { get; set; }
        public bool Go_Down { get; set; }
        public bool Go_Left { get; set; }
        public bool Go_Right { get; set; }
    }
    internal class Loader
    {
        private static List<Bitmap> spriteSheet = new List<Bitmap>();
        private static int spriteWidth; private static int spriteHeight;
        private static int x, y;

        public static bool[] Selected_Character = new bool[4] { true, true, true, true };

        public static void Check_Character(StudentData studentData)
        {
            if (studentData.clr == Ch_Color.Black) { Selected_Character[(int)Ch_Color.Black] = false; }
            if (studentData.clr == Ch_Color.Orange) { Selected_Character[(int)Ch_Color.Orange] = false; }
            if (studentData.clr == Ch_Color.Green) { Selected_Character[(int)Ch_Color.Green] = false; }
            if (studentData.clr == Ch_Color.Blue) { Selected_Character[(int)Ch_Color.Blue] = false; }

        }

        public static void ImageLoad()
        {
            spriteSheet.Add(Properties.Resources.Player_Q);
            spriteSheet.Add(Properties.Resources.Player_W);
            spriteSheet.Add(Properties.Resources.Player_E);
            spriteSheet.Add(Properties.Resources.Player_R);
            for (int Ch_Type = 0; Ch_Type < 4; Ch_Type++)
            {
                spriteWidth = (spriteSheet[Ch_Type].Width / 4);
                spriteHeight = (spriteSheet[Ch_Type].Height / 4);

                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        x = col * spriteWidth;
                        y = row * spriteHeight;

                        Rectangle spriteRect = new Rectangle(x + 4, y + 4, spriteWidth - 8, spriteHeight - 8);

                        Bitmap sprite = spriteSheet[Ch_Type].Clone(spriteRect, spriteSheet[Ch_Type].PixelFormat);
                        BaseForm_test.images[Ch_Type, (row * 4) + col] = sprite;
                    }
                }
            }
        }
        public static void LocationLoad(int Map)
        {
            if (Map == MapType.PS_First)
            {
                Setup_Location(BaseForm_test.Me.clr);
            }
        }
        public static void Setup_Location(Ch_Color clr)
        {
            switch (clr)
            {
                case Ch_Color.Black:
                    {
                        BaseForm_test.Me.location.X = 600;
                        BaseForm_test.Me.location.Y = 300;
                        BaseForm_test.SendLocation();
                        break;
                    }
                case Ch_Color.Orange:
                    {

                        BaseForm_test.Me.location.X = 700;
                        BaseForm_test.Me.location.Y = 300;
                        BaseForm_test.SendLocation();
                        break;
                    }
                case Ch_Color.Green:
                    {

                        BaseForm_test.Me.location.X = 600;
                        BaseForm_test.Me.location.Y = 400;
                        BaseForm_test.SendLocation();
                        break;
                    }
                case Ch_Color.Blue:
                    {

                        BaseForm_test.Me.location.X = 700;
                        BaseForm_test.Me.location.Y = 400;
                        BaseForm_test.SendLocation();
                        break;
                    }
            }
        }

    }
}