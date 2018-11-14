using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class Map
    {
        public readonly int mapHeight;
        public readonly int mapWidth;
        public readonly char[,] segment;

        public Map()
        {
            string[] mapFile = System.IO.File.ReadAllLines(@"map.txt");
            mapHeight = mapFile.Length;
            mapWidth = mapFile[0].Length;

            segment = new char[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    segment[x, y] = mapFile[y][x];
                }
            }
        }
    }
}
