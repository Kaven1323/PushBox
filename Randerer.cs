using PushBox.GameMaps;
using PushBox.GamePlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PushBox
{
    internal class Randerer
    {
        //构造方法
        public Randerer() 
        {
           
        }

        //静态方法，存储绘制地图和玩家的逻辑
        public void Rander(GameMap gameMap, Player player)
        {
            //绘制地图
            RanderMap(gameMap);

            //地图元素，注意这里不能用 public，因为地图不能被随便修改
            static void RanderMap(GameMap gameMap)
            {
                int width = gameMap.Width;
                int height = gameMap.Height;

                MapElement[,] staticElements = gameMap.StaticElements;
                MapElement[] boxElements = gameMap.BoxElements;
                MapElement[] targetElements = gameMap.TargetElements;

                //1 绘制地图静态元素
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        APITools.Draw(staticElements[y, x].PosX, staticElements[y, x].PosY, staticElements[y, x].Avatar);
                        //staticElements[y, x].Draw();
                    }
                }

                //2 绘制地图目标元素
                for (int i = 0; i < targetElements.Length; i++)
                {
                    APITools.Draw(targetElements[i].PosX, targetElements[i].PosY, targetElements[i].Avatar);
                    //targetElements[i].Draw();
                }

                //3 绘制地图箱子元素
                for (int i = 0; i < boxElements.Length; i++)
                {
                    APITools.Draw(boxElements[i].PosX, boxElements[i].PosY, boxElements[i].Avatar);
                    //boxElements[i].Draw();
                }
                
            }

            //4 绘制玩家
            APITools.Draw(player.PosX, player.PosY, player.Avatar);
            //player.Draw();
        }
        
    }
}
