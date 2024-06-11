using PushBox.GameMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GamePlayer
{
    internal class Player
    {
        //构造方法（如果不强制player生成的时候传入初始位置，那么x y 会默认为 0）
        public Player(int x, int y, char avatar)
        {
            PosX = x;
            PosY = y;
            Avatar = avatar;
        }

        //玩家的 x，y 位置(习惯用属性的方法)
        public int PosX
        {
            get; set;
        }

        public int PosY
        {
            get; set;
        }

        //玩家形象 avatar 
        public char Avatar
        {
            get; set;
        }

        ////draw 方法：在某一个 x y 位置，绘制玩家形象
        //public void Draw()
        //{
        //    APITools.Draw(PosX, PosY, Avatar);
        //    ////1 将光标移动到对应玩家的位置
        //    //Console.CursorLeft = PosX;
        //    //Console.CursorTop = PosY;

        //    ////2 绘制 Avatar
        //    //Console.Write(Avatar);
        //}

    }
}
