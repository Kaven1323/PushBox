using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox
{
    enum Input
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }
    internal class InputController
    {
        public InputController() { }
        public Input GetInput()
        {
            Input input = Input.NONE;
            //1 获取用户输入
            ConsoleKey key = Console.ReadKey(true).Key;

            //2 将 C# 的系统API枚举类型翻译成自己的枚举类型 Input
            switch (key)
            {
                case ConsoleKey.W: input = Input.UP; break;
                case ConsoleKey.S: input = Input.DOWN; break;
                case ConsoleKey.A: input = Input.LEFT; break;
                case ConsoleKey.D: input = Input.RIGHT; break;
                default: input = Input.NONE; break;
            }

            return input;
        }
    }
}
