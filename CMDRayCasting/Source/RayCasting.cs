using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class RayCasting : GameCore.IGameCore
    {
        Map map;
        RenderMap renderMap;

        public void Create()
        {
            map = new Map();
            renderMap = new RenderMap(map);
        }

        public void Render()
        {
            renderMap.Show();
            Console.ReadKey();
        }
    }
}
