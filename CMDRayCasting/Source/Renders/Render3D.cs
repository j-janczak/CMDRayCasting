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
        private readonly int fov = 80;
        private readonly int bufforHeight = 46;
        private char[,] buffor;

        public Render3D(Map m, Hero h)
        {
            mapFile = m;
            hero = h;
            buffor = new char[(int)fov, bufforHeight];
        }

        public void Show()
        {
            ResetBuffor();

            int ray = hero.direction - fov / 2;
            int rayEnd = hero.direction + fov / 2;
            int loop = 1;

            while (ray < rayEnd)
            {
                double radians = DecToRad(ray);
                int endX = (int)(hero.x + 50 * Math.Sin(radians));
                int endY = (int)(hero.y + 50 * Math.Cos(radians));

                double correction = 1.0;
                correction = Math.Cos(DecToRad2(fov / 2 - loop));

                int dis = (int)CastRay(hero.x, hero.y, endX, endY);
                int disC = (int)(CastRay(hero.x, hero.y, endX, endY) * correction);

                //System.Diagnostics.Debug.WriteLine(correction + " = " + loop);

                DrawOnBuffor(disC, loop-1);

                loop++;
                ray++;
            }
            //System.Environment.Exit(0);

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
                if (height > bufforHeight - 1) height = bufforHeight - 1;
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

        private double CastRay(int startx, int starty, int endx, int endy)
        {
            int d, dx, dy, ai, bi;
            double xi, yi;
            double x = hero.x, y = hero.y;
            if (hero.x < endx)
            {
                xi = 0.2;
                dx = endx - hero.x;
            }
            else
            {
                xi = -0.2;
                dx = hero.x - endx;
            }
            if (hero.y < endy)
            {
                yi = 0.2;
                dy = endy - hero.y;
            }
            else
            {
                yi = -0.2;
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
                    if (mapFile.segment[(int)x, (int)y] == '#') return Math.Sqrt(Math.Pow(hero.x - x, 2) + Math.Pow(hero.y - y, 2));
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
                    if (mapFile.segment[(int)x, (int)y] == '#') return Math.Sqrt(Math.Pow(hero.x - x, 2) + Math.Pow(hero.y - y, 2));
                }
            }
            return -1;
        }

        public double DecToRad(int dec)
        {
            dec += 180;
            return -(dec * Math.PI / 180);
        }

        public double DecToRad(double dec)
        {
            dec += 180;
            return -(dec * Math.PI / 180);
        }

        public double DecToRad2(int dec)
        {
            return (dec * Math.PI / 180);
        }
    }
}
