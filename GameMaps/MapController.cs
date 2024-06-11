using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GameMaps
{
    internal class MapController
    {
        private static MapController instance = new MapController();
        public static MapController Instance()
        {
            return instance;
        }
        private MapController() { }

        public GameMap CurrentMap { get; set; }

        public bool ChackMove(Input input, int x, int y)  //检测玩家移动到哪个方向上的坐标
        {
            var staticElements = CurrentMap.StaticElements;
            var boxElements = CurrentMap.BoxElements;

            //1 最新的 x y 位置上有没有 MapBlock（围墙）
            if (staticElements[y, x] is MapBlock)
            {
                return false;
            }

            //最新的 x y 位置上有没有 MapBox（箱子）
            for (int i = 0; i < boxElements.Length; i++)
            {
                // 2.1 检查有没有箱子在 x y 位置上
                if (boxElements[i].PosX == x && boxElements[i].PosY == y)
                {
                    //2.2 如果找到了一个箱子，正好在 x y 位置上，判断这个箱子是否可以移动
                    MapElement box = boxElements[i];
                    //a.需要知道往哪个方向移动箱子
                    //b. 推动箱子之后，获得箱子的最新位置
                    int oldx = box.PosX, oldy = box.PosY;
                    switch (input)
                    {
                        case Input.UP: box.PosY--; break;
                        case Input.DOWN: box.PosY++; break;
                        case Input.LEFT: box.PosX--; break;
                        case Input.RIGHT: box.PosX++; break;
                    }

                    //c.最新箱子的位置上，有没有 MapBlock 或 其他箱子
                    //检测 box 最新位置上有没有 MapBlock
                    if (staticElements[box.PosY, box.PosX] is MapBlock)
                    {
                        //如果无法移动箱子，那么将箱子退回原处
                        box.PosX = oldx;
                        box.PosY = oldy;
                        return false;
                    }

                    //检测 box 最新位置上有没有其他 box
                    foreach (MapElement m in boxElements) //遍历MapElement 里面的 m 会依次等于 BoxElements 里面的每一个元素
                    {
                        //如果 m 不是箱子的时候（m 不能自己是箱子）
                        //对象之间判断是否相等，如果二者指向同一个堆内存，那么二者相等，否则不相等
                        if (m != box)
                        {
                            if (m.PosX == box.PosX && m.PosY == box.PosY)
                            {
                                box.PosX = oldx;
                                box.PosY = oldy;
                                return false;
                            }
                        }
                    }

                }
            }

            return true;
        }
    }
}
