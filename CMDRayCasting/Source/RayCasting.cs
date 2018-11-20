using System;
using CMDRayCasting.Renders;

namespace CMDRayCasting
{
    class RayCasting : GameCore.IGameCore
    {
        public static int SCREEN_WIDTH = 89;
        public static int SCREEN_HEIGHT = 50;
        public static int FOV = 70;

        private Map map;
        private Hero hero;
        private Render2D render2D;
        private Render3D render3D;

        private bool toogleRender = true;

        public void Create()
        {
            map = new Map();
            hero = new Hero();
            render2D = new Render2D(map, hero);
            render3D = new Render3D(map, hero);
            Console.WindowHeight = SCREEN_HEIGHT;
            Console.CursorVisible = false;
        }

        public void Render()
        {
            if(toogleRender) render3D.Show();
            else render2D.Show();

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.W) hero.Walk(1, hero.direction);
                if (key == ConsoleKey.S) hero.Walk(-1, hero.direction);
                if (key == ConsoleKey.D) hero.Walk(1, hero.direction + 90);
                if (key == ConsoleKey.A) hero.Walk(1, hero.direction - 90);

                if (key == ConsoleKey.R)
                {
                    toogleRender = !toogleRender;
                    hero.direction = 0;
                    Console.Clear();
                }

                if (key == ConsoleKey.UpArrow) render3D.fov += 1;
                if (key == ConsoleKey.DownArrow) render3D.fov -= 1;

                if (key == ConsoleKey.LeftArrow)
                {
                    hero.direction -= 5;
                    if (hero.direction == -5) hero.direction = 355;
                }
                if (key == ConsoleKey.RightArrow)
                {
                    hero.direction += 5;
                    if (hero.direction == 360) hero.direction = 0;
                }
            }
        }

        public static double DecToRadCorrected(int dec)
        {
            dec += 180;
            return -(dec * Math.PI / 180);
        }

        public static double DecToRadCorrected(double dec)
        {
            dec += 180;
            return -(dec * Math.PI / 180);
        }

        public static double DecToRad(double dec)
        {
            return (dec * Math.PI / 180);
        }
    }
}
