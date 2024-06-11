using PushBox.GameMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GamePlayer
{
    internal class PlayerController
    {
        //单例类
        //在类内访问内部属性
        private static PlayerController instance = new PlayerController();

        //使外界可以访问 PlayerController 中的属性
        public static PlayerController Instance()
        {
            return instance;
        }

        //私有构造方法，无法在主程序中new出 PlayerController
        private PlayerController() { }

        public Player CurrentPlayer { get; set; }

        //move 方法；根据键盘输入来决定移动玩家的位置
        public void Move(Input input)
        {
            //记录玩家旧的坐标
            int oldx = CurrentPlayer.PosX, oldy = CurrentPlayer.PosY;

            switch (input)
            {

                case Input.UP: CurrentPlayer.PosY--; break;
                case Input.DOWN: CurrentPlayer.PosY++; break;
                case Input.LEFT: CurrentPlayer.PosX--; break;
                case Input.RIGHT: CurrentPlayer.PosX++; break;
                default: break;
            }

            //检测玩家是否可以移动到最新的位置
            //ChackMove 返回值是 true，对其取反表示返回值是 false
            if (!MapController.Instance().ChackMove(input, CurrentPlayer.PosX, CurrentPlayer.PosY))
            {
                //如果玩家无法移动到最新的 x y 位置，将玩家回退到之前的位置
                CurrentPlayer.PosX = oldx;
                CurrentPlayer.PosY = oldy;
            }

        }

    }
}
