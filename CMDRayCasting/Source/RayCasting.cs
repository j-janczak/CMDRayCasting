using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class RayCasting : GameCore.IGameCore
    {
        public static int SCREEN_WIDTH = 100;
        public static int SCREEN_HEIGHT = 50;
        public static int FOV = 60;

        private Map map;
        private Hero hero;
        private Renders.Render2D render2D;
        private Renders.Render3D render3D;

        public void Create()
        {
            map = new Map();
            hero = new Hero();
            render2D = new Renders.Render2D(map, hero);
            render3D = new Renders.Render3D(map, hero);
            Console.WindowHeight = SCREEN_HEIGHT + Renders.Render2D.RENDER_HEIGHT + 2;
        }

        public void Render()
        {
            render3D.Show();
            render2D.Show();

            char key = Console.ReadKey().KeyChar;

            if (key == 'w') hero.Walk(1, hero.direction);
            if (key == 's') hero.Walk(-1, hero.direction);
            if (key == 'd') hero.Walk(1, hero.direction + 90);
            if (key == 'a') hero.Walk(1, hero.direction - 90);

            if (key == 'k') {
                hero.direction -= 5;
                if (hero.direction == -5) hero.direction = 355;
            }
            if (key == 'l') {
                hero.direction += 5;
                if (hero.direction == 365) hero.direction = 5;
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
