using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting.Renders
{
    class Render2D
    {
        private readonly Map mapFile;
        private Hero hero;
        private readonly char[,] buffor;

        static int RENDER_WIDTH = 80;
        static int RENDER_HEIGHT = 28;

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
            tempBuffor += "x: " + hero.x + " y: " + hero.y + "   ";
            Console.SetCursorPosition(0, 0);
            Console.Write(tempBuffor);
            ResetBuffor();
        }

        public void PutMark(char ch, int x, int y)
        {
            if (x >= 0 && x < mapFile.mapWidth && y >= 0 && y < mapFile.mapHeight) buffor[x, y] = ch;
        }

        public void DrawLine(int endx, int endy)
        {
            int d, dx, dy, ai, bi, xi, yi;
            int x = hero.x, y = hero.y;
            if (hero.x < endx)
            {
                xi = 1;
                dx = endx - hero.x;
            }
            else
            {
                xi = -1;
                dx = hero.x - endx;
            }
            if (hero.y < endy)
            {
                yi = 1;
                dy = endy - hero.y;
            }
            else
            {
                yi = -1;
                dy = hero.y - endy;
            }
            if (dx > dy)
            {
                ai = (dy - dx) * 2;
                bi = dy * 2;
                d = bi - dx;
                while (x != endx)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        x += xi;
                    }
                    if (mapFile.segment[x, y] == '#') break;
                    else PutMark('O', x, y);
                }
            }
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
                while (y != endy)
                {
                    if (d >= 0)
                    {
                        x += xi;
                        y += yi;
                        d += ai;
                    }
                    else
                    {
                        d += bi;
                        y += yi;
                    }
                    if (mapFile.segment[x, y] == '#') break;
                    else PutMark('O', x, y);
                }
            }
        }
    }
}
