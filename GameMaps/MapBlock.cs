using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GameMaps
{
    internal class MapBlock : MapElement
    {
        //地图上的围墙元素
        public MapBlock(int x, int y, char avatar):base(x, y, avatar) { }

    }
}
