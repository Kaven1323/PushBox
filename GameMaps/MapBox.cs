using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GameMaps
{
    internal class MapBox : MapElement
    {
        //当作地图的箱子元素
        public MapBox(int x, int y, char avatar) : base(x, y, avatar) { }
    }
}
