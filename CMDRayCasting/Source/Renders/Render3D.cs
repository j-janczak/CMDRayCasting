using System;

namespace CMDRayCasting.Renders
{
    class Render3D
    {
        private readonly Map mapFile;
        private Hero hero;
        private readonly int screenWidth = RayCasting.SCREEN_WIDTH - 5;
        private readonly int screenHeight = RayCasting.SCREEN_HEIGHT - 5;
        private readonly char[,] buffor;
        public int fov = RayCasting.FOV;

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
                double radians = RayCasting.DecToRadCorrected(ray);
                int endX = (int)(hero.x + 50 * Math.Sin(radians));
                int endY = (int)(hero.y + 50 * Math.Cos(radians));

                double correction = 1.0;
                correction = Math.Cos(RayCasting.DecToRad((double)fov / 2 - rayAngle));

                int distance = (int)CastRay(hero.x, hero.y, endX, endY);
                int distanceC = (int)(CastRay(hero.x, hero.y, endX, endY) * correction);
                DrawOnBuffor(distanceC, screenPosition, correction);

                rayAngle += rayIncrement;
                ray += rayIncrement;
            }

            string screen = "╔";
            for (int x = 0; x < screenWidth; x++) screen += '═';
            screen += "╗\n";

            for (int y = 0; y < screenHeight; y++)
            {
                screen += "║";
                for (int x = 0; x < screenWidth; x++)
                {
                    screen += buffor[x, y];
                }
                screen += "║\n";
            }
            screen += "╚";
            for (int x = 0; x < screenWidth; x++) screen += '═';
            screen += "╝\n";

            string debug = "X: " + (int)hero.x + " Y: " + (int)hero.y + " Rotation: " + (int)hero.direction + " fov: " + fov + "    ";

            Console.SetCursorPosition(0, 0);
            Console.Write(screen + debug);
        }

        private void DrawOnBuffor(int length, int position, double correction)
        {
            int height;
            if (length > 0) height = (int)Math.Ceiling(screenHeight / (length / 6.2));
            else height = length;

            int startPosition = screenHeight / 2 - height / 2;
            for (int y = startPosition; y <= startPosition + height; y++)
            {
                int tmpY = y;
                if (tmpY < 0) continue;
                if (tmpY > screenHeight - 1) break;

                if (height > screenHeight * 0.6) buffor[position, tmpY] = '█';
                else if (height > screenHeight * 0.4) buffor[position, tmpY] = '▓';
                else if (height > screenHeight * 0.2) buffor[position, tmpY] = '▒';
                else buffor[position, tmpY] = '░';
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
            double add = 0.1;
            if (hero.x < endx)
            {
                xi = add;
                dx = (int)(endx - hero.x);
            }
            else
            {
                xi = -add;
                dx = (int)(hero.x - endx);
            }
            if (hero.y < endy)
            {
                yi = add;
                dy = (int)(endy - hero.y);
            }
            else
            {
                yi = -add;
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
