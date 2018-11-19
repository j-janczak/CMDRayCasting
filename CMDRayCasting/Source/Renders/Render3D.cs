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
        private readonly int screenWidth = RayCasting.SCREEN_WIDTH - 5;
        private readonly int screenHeight = RayCasting.SCREEN_HEIGHT - 5;
        private char[,] buffor;

        public Render3D(Map m, Hero h)
        {
            mapFile = m;
            hero = h;
            buffor = new char[screenWidth, screenHeight];
        }

        public void Show()
        {
            ResetBuffor();

            double ray = hero.direction - RayCasting.FOV / 2;
            double rayEnd = hero.direction + RayCasting.FOV / 2;
            double rayAngle = 0;
            double rayIncrement = (double)RayCasting.FOV / (double)screenWidth;

            for (int screenPosition = 0; screenPosition < screenWidth; screenPosition++)
            {
                double radians = RayCasting.DecToRadCorrected(ray);
                int endX = (int)(hero.x + 50 * Math.Sin(radians));
                int endY = (int)(hero.y + 50 * Math.Cos(radians));

                double correction = 1.0;
                correction = Math.Cos(RayCasting.DecToRad((double)RayCasting.FOV / 2 - rayAngle));

                int distance = (int)CastRay(hero.x, hero.y, endX, endY);
                DrawOnBuffor(distance, screenPosition, correction);

                rayAngle += rayIncrement;
                ray += rayIncrement;
            }

            string screen = "";
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    screen += buffor[x, y];
                }
                screen += "\n";
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(screen);
        }

        private void DrawOnBuffor(int length, int position, double correction)
        {
            int height = (int)(screenHeight - length);
            if (height < 0) height = 0;

            height = (int)((double)height / correction);

            if (height > screenHeight-1) height = screenHeight - 2;

            int startPosition = screenHeight / 2 - height / 2;
            for (int y = startPosition; y <= height; y++)
            {
                if (y < 0) y = 0;
                if (y > screenHeight) y = screenHeight - 1;
                if(height > screenHeight * 0.8) buffor[position, y] = '█';
                else if(height > screenHeight * 0.7) buffor[position, y] = '▓';
                else if(height > screenHeight * 0.6) buffor[position, y] = '▒';
                else buffor[position, y] = '░';
            }
        }

        private void ResetBuffor()
        {
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    buffor[x, y] = ' ';
                }
            }
        }

        //----------------------Bresenham's line algorithm----------------------
        private double CastRay(double startx, double starty, int endx, int endy)
        {
            int d, dx, dy, ai, bi;
            double xi, yi;
            double x = hero.x, y = hero.y;
            if (hero.x < endx)
            {
                xi = 0.1;
                dx = (int)(endx - hero.x);
            }
            else
            {
                xi = -0.1;
                dx = (int)(hero.x - endx);
            }
            if (hero.y < endy)
            {
                yi = 0.1;
                dy = (int)(endy - hero.y);
            }
            else
            {
                yi = -0.1;
                dy = (int)(hero.y - endy);
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
    }
}
