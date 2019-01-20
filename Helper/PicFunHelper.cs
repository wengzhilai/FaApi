using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Helper
{
    public class PicFunHelper
    {
        public enum FillMode
        {
            /// <summary>
            /// 平铺
            /// </summary>
            /// <remarks></remarks>
            Title = 0,
            /// <summary>
            /// 居中
            /// </summary>
            /// <remarks></remarks>
            Center = 1,
            /// <summary>
            /// 拉伸
            /// </summary>
            /// <remarks></remarks>
            Struk = 2,
            /// <summary>
            /// 缩放
            /// </summary>
            /// <remarks></remarks>
            Zoom = 3
        }

        /// <summary>
        /// 求三点角度
        /// </summary>
        /// <param name="cen"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static float Angle(Point cen, Point first, Point second)
        {
            const double M_PI = 3.1415926535897;

            double ma_x = first.X - cen.X;
            double ma_y = first.Y - cen.Y;
            double mb_x = second.X - cen.X;
            double mb_y = second.Y - cen.Y;
            double v1 = (ma_x * mb_x) + (ma_y * mb_y);
            double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
            double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
            double cosM = v1 / (ma_val * mb_val);
            double angleAMB = Math.Acos(cosM) * 180 / M_PI;

            return float.Parse(angleAMB.ToString());
        }
        /// <summary>
        /// 两点之间的线相对于Y轴角度
        /// </summary>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns>
        public static float TwoPointToAngleY(Point s, Point e)
        {
            const float M_PI = 3.1415926535897f;
            float a = Math.Abs(s.X - e.X);
            float b = Math.Abs(s.Y - e.Y);
            if (b == 0 && a == 0)
            {
                return 0;
            }
            //double c = Math.Sqrt(a * a + b * b);
            float tmp = (float)Math.Atan(a / b) * 180 / M_PI;
            if (s.Y > e.Y && s.X > e.X)
            {
                return (90-tmp) + 90;
            }
            else if (s.Y > e.Y && s.X < e.X)
            {
                return tmp + 180;
            }
            else if (s.Y < e.Y && s.X < e.X)
            {
                return (90-tmp) + 270;
            }
            return tmp;
        }


        /// <summary>
        /// 两点距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double GetDistance(Point a, Point b)
        {
            int x = System.Math.Abs(b.X - a.X);
            int y = System.Math.Abs(b.Y - a.Y);
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string ValidateMake(int maxLeng=4)
        {
            string str = "";
            //string[] code = {"0","1","2","3","4","5","6","7","8","9",
            //                "A","B","C","D","E","F","G","H","I","J","K","L","M","N",
            //                "O","P","Q","R","S","T","U","V","W","X","Y","Z","a","b",
            //                "c","d","e","f","g","h","i","j","k","l","m","n","o","p",
            //                "q","r","s","t","u","v","w","x","y","z"};
            string[] code = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //随机产生字符串
            Random rand = new Random();
            for (int i = 0; i < maxLeng; i++)
            {
                str = str + code[rand.Next(0, code.Length)];
            }
            return str;
        }
    }

}
