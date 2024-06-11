using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GameMaps
{
    internal class GameMap
    {
        //构造方法
        public GameMap()
        {
            Width = 9;
            Height = 9;

            //制作地图二维数组
            int[,] mapInfoArr =
            {
                {1,1,1,1,1,0,0,0,0 },
                {1,0,0,0,1,0,0,0,0 },
                {1,0,2,2,1,0,1,1,1 },
                {1,1,2,0,1,0,1,3,1 },
                {1,1,1,0,0,0,0,3,1 },
                {0,1,1,0,0,0,0,3,1 },
                {0,1,0,0,0,1,0,0,1 },
                {0,1,0,0,0,1,1,1,1 },
                {0,1,1,1,1,1,0,0,0 },
            };

            //new 出对应的储存数组
            StaticElements = new MapElement[Height, Width];
            BoxElements = new MapElement[3];
            TargetElements = new MapElement[3];

            //生成对应的元素们
            int boxCount = 0; //记录生成多少个 box 了
            int targetCount = 0; //记录生成多少个 target

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    switch (mapInfoArr[y, x])
                    {
                        case 0: StaticElements[y, x] = new MapEmpty(x, y, ' '); break;
                        case 1: StaticElements[y, x] = new MapBlock(x, y, '#'); break;
                        case 2:
                            {
                                //生成一个 MapBox ，放到 BoxElement 数组里面
                                //用 boxCount 变量记录生成 box 的数量
                                BoxElements[boxCount] = new MapBox(x, y, '@');
                                boxCount++;
                                StaticElements[y, x] = new MapEmpty(x, y, ' ');
                                break;
                            }
                        case 3:
                            {
                                TargetElements[targetCount] = new MapTarget(x, y, 'A');
                                targetCount++;
                                StaticElements[y, x] = new MapEmpty(x, y, ' ');
                                break;
                            }
                    }
                }
            }
        }

        //长度/宽度
        public int Width { get; set; }
        public int Height { get; set; }

        //1 静态地图元素（围墙、空白元素）
        //MapElement 是父类，里面可以装任何继承自 MapElement 的元素
        public MapElement[,] StaticElements { get; set; }

        //2 箱子元素构成数组
        public MapElement[] BoxElements { get; set; }

        //3 目标元素构成数组
        public MapElement[] TargetElements { get; set; }

        //侦测玩家移动是否成功
       
    }
}
