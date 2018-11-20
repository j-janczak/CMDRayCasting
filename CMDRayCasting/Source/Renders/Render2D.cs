using System;

namespace CMDRayCasting.Renders
{
    class Render2D
    {
        public int screenWidth = 50;
        public int screenHeight = 35;

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
            PutMark(hero.mark, hero.x, hero.y);

            string screen = "╔";
            for (int x = 0; x < screenWidth+1; x++) screen += '═';
            screen += "╗\n";

            for (int y = (int)(hero.y - screenHeight / 2); y <= hero.y + screenHeight / 2; y++)
            {
                screen += "║";
                for (int x = (int)(hero.x - screenWidth / 2); x <= hero.x + screenWidth / 2; x++)
                {
                    if (x < 0 || x >= mapFile.mapWidth || y < 0 || y >= mapFile.mapHeight) screen += " ";
                    else screen += buffor[x, y];
                }
                screen += "║\n";
            }
            screen += "╚";
            for (int x = 0; x < screenWidth+1; x++) screen += '═';
            screen += "╝\n";
            string debug = "X: " + (int)hero.x + " Y: " + (int)hero.y + " Rotation: " + (int)hero.direction + "    ";

            Console.SetCursorPosition(0, 0);
            Console.Write(screen + debug);
            ResetBuffor();
        }

        public void PutMark(char ch, double x, double y)
        {
            if (x >= 0 && x < mapFile.mapWidth && y >= 0 && y < mapFile.mapHeight) buffor[(int)x, (int)y] = ch;
        }
    }
}
