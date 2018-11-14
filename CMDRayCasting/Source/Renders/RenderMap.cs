using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting.Renders
{
    class RenderMap
    {
        private readonly Map mapFile;
        private Hero hero;
        private readonly char[,] buffor;

        static int RENDER_WIDTH = 17;
        static int RENDER_HEIGHT = 11;

        public RenderMap(Map m)
        {
            mapFile = m;
            buffor = new char[mapFile.mapWidth, mapFile.mapHeight];
            ResetBuffor();
        }

        private void ResetBuffor()
        {
            for (int y = 0; y < mapFile.mapHeight; y++)
            {
                for (int x = 0; x < mapFile.mapWidth; x++)
                {
                    buffor[x, y] = mapFile.segment[x, y];
                }
            }
        }

        public void Show()
        {
            string tempBuffor = "";

            PutMark(hero.mark, hero.x, hero.y);

            for (int y = hero.y - RENDER_HEIGHT / 2; y <= hero.y + RENDER_HEIGHT / 2; y++)
            {
                for (int x = hero.x - RENDER_WIDTH / 2; x <= hero.x + RENDER_WIDTH / 2; x++)
                {
                    if (x == hero.x - RENDER_WIDTH / 2 || x == hero.x + RENDER_WIDTH / 2 || y == hero.y - RENDER_HEIGHT / 2 || y == hero.y + RENDER_HEIGHT / 2) tempBuffor += "█";
                    else if (x < 0 || x >= mapFile.mapWidth || y < 0 || y >= mapFile.mapHeight) tempBuffor += " ";
                    else tempBuffor += buffor[x, y];
                }
                tempBuffor += "\n";
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(tempBuffor);
            ResetBuffor();
        }

        public void PutMark(char ch, int x, int y)
        {
            buffor[x, y] = ch;
        }

        public void ConnectWithHero(Hero h)
        {
            hero = h;
        }
    }
}
