using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting.Renders
{
    class Render3D
    {
        private readonly Map mapFile;
        private Hero hero;
        private readonly int fov = 60;
        private readonly int bufforHeight = 45;
        private char[,] buffor;

        public Render3D(Map m, Hero h)
        {
            mapFile = m;
            hero = h;
            buffor = new char[fov, bufforHeight];
        }

        public void Show()
        {
            ResetBuffor();

            int ray = hero.direction;
            int rayEnd = hero.direction - fov;
            int loop = 0;

            while (ray > rayEnd)
            {
                double radians = ray * Math.PI / 180;
                int endX = (int)(hero.x + 50 * Math.Sin(radians));
                int endY = (int)(hero.y + 50 * Math.Cos(radians));

                DrawOnBuffor((int)(CastRay(endX, endY)), loop);

                loop++;
                ray--;
            }

            string screen = "";
            for (int y = 0; y < bufforHeight; y++)
            {
                for (int x = 0; x < fov; x++)
                {
                    screen += buffor[x, y];
                }
                screen += "\n";
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(screen);
        }

        private void DrawOnBuffor(int length, int position)
        {
            int height = bufforHeight - length;
            if (height < 0) height = 0;

            if (height > bufforHeight) height = bufforHeight - 1;

            int startPosition = bufforHeight / 2 - height / 2;
            for (int y = startPosition; y <= height; y++)
            {
                if (height > 35) buffor[position, y] = '█';
                else if (height > 28) buffor[position, y] = '▓';
                else if (height > 22) buffor[position, y] = '▒';
                else buffor[position, y] = '░';
            }
        }

        private void ResetBuffor()
        {
            for (int y = 0; y < bufforHeight; y++)
            {
                for (int x = 0; x < fov; x++)
                {
                    buffor[x, y] = ' ';
                }
            }
        }

        private int CastRay(int endx, int endy)
        {
            int d, dx, dy, ai, bi, xi, yi;
            int x = hero.x, y = hero.y;
            int distance = 0;
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
                    distance++;
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
                    if (mapFile.segment[x, y] == '#') return distance;
                }
            }
            else
            {
                ai = (dx - dy) * 2;
                bi = dx * 2;
                d = bi - dy;
                while (y != endy)
                {
                    distance++;
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
                    if (mapFile.segment[x, y] == '#') return distance;
                }
            }
            return -1;
        }

        private double DecToRad(int dec)
        {
            return dec * Math.PI / 180;
        }
    }
}
