using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class Map
    {
        public MapSegment[,] mapSegment;
        public readonly int mapHeight;
        public readonly int mapWidth;

        public class MapSegment
        {
            public readonly char mark;

            public MapSegment(char defaultMark) => mark = defaultMark;
        }

        public Map()
        {
            string[] mapFile = System.IO.File.ReadAllLines(@"map.txt");
            mapHeight = mapFile.Length;
            mapWidth = mapFile[0].Length;

            mapSegment = new MapSegment[mapWidth, mapHeight];

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    mapSegment[x, y] = new MapSegment(mapFile[y][x]);
                }
            }
        }
    }
}
