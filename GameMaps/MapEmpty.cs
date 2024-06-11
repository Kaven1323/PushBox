using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushBox.GameMaps
{
    internal class MapEmpty : MapElement
    {
        //当作地图的空元素
        public  MapEmpty(int x, int y, char avatar) : base(x, y, avatar)
        {

        }
    }
}
