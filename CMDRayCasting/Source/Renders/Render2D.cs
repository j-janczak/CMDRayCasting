using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting.Renders
{
    class Render2D
    {
        public static int RENDER_WIDTH = 30;
        public static int RENDER_HEIGHT = 20;

        private readonly Map mapFile;
        private Hero hero;
        private readonly char[,] buffor;

        public Render2D(Map m, Hero h)
        {
            mapFile = m;
            hero = h;
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

            for (int y = (int)(hero.y - RENDER_HEIGHT / 2); y <= hero.y + RENDER_HEIGHT / 2; y++)
            {
                for (int x = (int)(hero.x - RENDER_WIDTH / 2); x <= hero.x + RENDER_WIDTH / 2; x++)
                {
                    if (x == (int)hero.x - RENDER_WIDTH / 2 || x == (int)hero.x + RENDER_WIDTH / 2 || y == (int)hero.y - RENDER_HEIGHT / 2 || y == (int)hero.y + RENDER_HEIGHT / 2) tempBuffor += "█";
                    else if (x < 0 || x >= mapFile.mapWidth || y < 0 || y >= mapFile.mapHeight) tempBuffor += " ";
                    else tempBuffor += buffor[x, y];
                }
                tempBuffor += "\n";
            }
            tempBuffor += "x: " + (int)hero.x + " y: " + (int)hero.y + " °: " + hero.direction + "   ";
            Console.SetCursorPosition(0, RayCasting.SCREEN_HEIGHT);
            Console.Write(tempBuffor);
            ResetBuffor();
        }

        public void PutMark(char ch, double x, double y)
        {
            if (x >= 0 && x < mapFile.mapWidth && y >= 0 && y < mapFile.mapHeight) buffor[(int)x, (int)y] = ch;
        }
    }
}
