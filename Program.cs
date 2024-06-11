using PushBox.GameMaps;
using PushBox.GamePlayer;

namespace PushBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //创建玩家对象
            Player player = new Player(1, 1, '&');
            PlayerController.Instance().CurrentPlayer = player;

            //创建地图对象
            GameMap gameMap = new GameMap();
            MapController.Instance().CurrentMap = gameMap;

            //创建渲染器
            Randerer randerer = new Randerer();
            //创建自定义输入
            InputController inputController = new InputController();

            //隐藏指针
            Console.CursorVisible = false;
            

            while (true)
            {
                //获取玩家输入
                Input input = inputController.GetInput();

                //获取玩家输入,如果为 true，那么玩家输入的键位不会显式在屏幕上
                //ConsoleKeyInfo input = Console.ReadKey(true);

                //更新玩家位置
                PlayerController.Instance().Move(input);
                //player.Move(input, gameMap);

                //绘制
                randerer.Rander(gameMap, player);
            }


        }
    }
}
