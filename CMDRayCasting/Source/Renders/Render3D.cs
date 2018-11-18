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

            double ray = hero.direction - fov / 2;
            double rayEnd = hero.direction + fov / 2;
            double rayAngle = 0;
            double rayIncrement = (double)fov / (double)screenWidth;

            for (int screenPosition = 0; screenPosition < screenWidth; screenPosition++)
            {
                double radians = DecToRad(ray);
                int endX = (int)(hero.x + 50 * Math.Sin(radians));
                int endY = (int)(hero.y + 50 * Math.Cos(radians));

                double correction = 1.0;
                correction = Math.Cos(DecToRad2((double)fov / 2 - rayAngle));

                int dis = (int)CastRay(hero.x, hero.y, endX, endY);
                double disC = (double)CastRay(hero.x, hero.y, endX, endY) * correction;

                DrawOnBuffor((int)dis, screenPosition, correction);

                rayAngle += rayIncrement;
                ray += rayIncrement;

                //System.Diagnostics.Debug.WriteLine(rayAngle);
            }
            //System.Environment.Exit(1);

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

            if (height > screenHeight) height = screenHeight - 1;

            int startPosition = screenHeight / 2 - height / 2;
            for (int y = startPosition; y <= height; y++)
            {
                if (y > screenHeight-1) y = screenHeight - 1;
                if(height > screenHeight * 0.7) buffor[position, y] = '█';
                else if(height > screenHeight * 0.5) buffor[position, y] = '▓';
                else if(height > screenHeight * 0.3) buffor[position, y] = '▒';
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

        private double CastRay(int startx, int starty, int endx, int endy)
        {
            int d, dx, dy, ai, bi;
            double xi, yi;
            double x = hero.x, y = hero.y;
            if (hero.x < endx)
            {
                xi = 0.1;
                dx = endx - hero.x;
            }
            else
            {
                xi = -0.1;
                dx = hero.x - endx;
            }
            if (hero.y < endy)
            {
                yi = 0.1;
                dy = endy - hero.y;
            }
            else
            {
                yi = -0.1;
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

        public double DecToRad2(double dec)
        {
            return (dec * Math.PI / 180);
        }
    }
}
