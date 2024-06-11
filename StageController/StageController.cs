using Newtonsoft.Json;
using PushBox.GameMaps;
using PushBox.GamePlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.StageController
{
    internal class StageController
    {
        private static StageController instance = new StageController();
        public static StageController Instance()
        {
            return instance;
        }
        private StageController() { }

        //读取关卡配置文件后，生成的关卡信息数组
        private StageInfo[] stageInfos = null;

        //读取配置文件，并且将读取的配置文件，反序列化为 stageInsos 数组
        public void ReadIni(string path)
        {
            string jsonStr = File.ReadAllText(path);
            stageInfos = JsonConvert.DeserializeObject<StageInfo[]>(jsonStr);
        }

        //用来开启第id个关卡，如果数组中存在第id个关卡，则正常开启，返回true
        //如果数组中不存在第id个关卡，则返回false
        public bool StartStage(int id)
        {
            if (id < 0 || id >= stageInfos.Length)
            {
                return false;
            }

            //1 根据id，取出对应关卡的信息
            StageInfo stageInfo = stageInfos[id];

            //2 初始化玩家对象
            Player player = new Player(stageInfo.PlayerX, stageInfo.PlayerY, '&');
            PlayerController.Instance().CurrentPlayer = player;

            //3 初始化地图信息
            int[,] mapInfos = stageInfo.Elements;
            int width = stageInfo.Width;
            int height = stageInfo.Height;
            int boxCount = stageInfo.BoxCount;

            GameMap map = new GameMap();
            MapController.Instance().CurrentMap = map;
            map.Width = width;
            map.Height = height;

            //3.1 初始化地图中各类存储数组的空间
            map.StaticElements = new MapElement[height, width]; //注意 height在前面
            map.BoxElements = new MapElement[boxCount];
            map.TargetElements = new MapElement[boxCount];

            //3.2 根据mapinfos记录的地图数据，生成对应的mapElements对象
            map.CreateMapElements(mapInfos, width, height);

            return true;
        }

        public void ShowSelection()
        {
            //1 清理屏幕，显式光标
            Console.Clear();
            Console.CursorVisible = true;

            //2 打印欢迎语及关卡名字
            Console.WriteLine("*************欢迎来到推箱子的世界*************");
            for (int i = 0; i < stageInfos.Length; i++)
            {
                Console.WriteLine($"{i + 1}" + stageInfos[i].Name);
            }

            //3 准备变量
            int selection = 0; //记录玩家选择了第多少关卡

            //4 光标归位，移动到第一个关卡的id号上
            Console.CursorTop = 1;
            bool selectionOver = false;

            //5 循环获取用户输入，直到用户点击了enter为止
            while (true)
            {
                Input input = InputTools.GetInput();

                switch (input)
                {
                    case Input.UP:
                        {
                            if (selection > 0)
                            {
                                selection--;
                                Console.CursorTop--;
                            }
                        }
                        break;
                    case Input.DOWN:
                        {
                            if (selection < stageInfos.Length - 1)
                            {
                                selection++;
                                Console.CursorTop++;
                            }
                        }
                        break;
                    case Input.ENTER:
                        {
                            selectionOver = true;
                        }
                        break;
                }

                if (selectionOver)
                {
                    break;
                }

                /*
                Console.WriteLine("1 开始游戏");
                Console.WriteLine("2 推出游戏");
                */

                /* while (true)
                 {
                     ConsoleKey key = Console.ReadKey(true).Key;

                     switch (key)
                     {
                         case ConsoleKey.D1:
                             {
                                 //清理控制台
                                 Console.Clear();

                                 //停止当前程序
                                 return;
                             }
                         case ConsoleKey.D2:
                             {
                                 //C#提供的系统API方法，直接推出当前程序
                                 Environment.Exit(0);
                             } 
                             break;
                     }

             }*/

            }

            //循环结束之后，selection变量里面存储着用户当前选择的关卡id（0 - seleciton-1）
            //6 清理屏幕，隐藏光标
            Console.Clear();
            Console.CursorVisible = false;

            //7 开启关卡
            StartStage(selection);
        }
        //通关规则：每个目标上面都覆盖了一个箱子
        public bool CheckClear()
        {

            //1 从当前地图对象中，取出来目标数组/箱子数组
            GameMap map = MapController.Instance().CurrentMap;
            MapElement[] targeElements = map.TargetElements;
            MapElement[] boxElements = map.BoxElements;

            //2 遍历目标数组，查看每个目标上面是否覆盖了一个箱子
            foreach (MapElement target in targeElements)
            {
                bool isCovered = false;

                //遍历每一个boxElements里面的元素，查看有没有覆盖在target上面的box
                foreach (MapElement box in boxElements)
                {
                    if (box.PosX == target.PosX && box.PosY == target.PosY)
                    {
                        isCovered = true;
                        break;
                    }
                }

                //如果某个目标没有被箱子覆盖，返回 false
                if (!isCovered)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
