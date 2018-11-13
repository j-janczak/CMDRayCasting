using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class RenderMap
    {
        private readonly Map map;

        public RenderMap(Map m) => map = m;

        public void Show()
        {
            string buffor = "";

            for (int y = 0; y < map.mapHeight; y++)
            {
                for (int x = 0; x < map.mapWidth; x++)
                {
                    buffor += map.mapSegment[x, y].mark;
                }

                buffor += "\n";
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(buffor);
        }
    }
}
