using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class RayCasting : GameCore.IGameCore
    {
        Map map;
        Hero hero;
        Renders.Render2D renderMap;
        Renders.Render3D render3D;
        int a = 0;

        public void Create()
        {
            map = new Map();
            hero = new Hero();
            renderMap = new Renders.Render2D(map, hero);
            render3D = new Renders.Render3D(map, hero);
            Console.WindowHeight = 50;
            //Console.WindowWidth = 250;
        }

        public void Render()
        {
            render3D.Show();

            /*double radians = render3D.DecToRad(a + hero.direction);
            int endX = (int)(hero.x + 50 * Math.Sin(radians));
            int endY = (int)(hero.y + 50 * Math.Cos(radians));
            renderMap.DrawLine(endX, endY);

            double radians1 = render3D.DecToRad(a+90 + hero.direction);
            int endX1 = (int)(hero.x + 50 * Math.Sin(radians1));
            int endY1 = (int)(hero.y + 50 * Math.Cos(radians1));
            renderMap.DrawLine(endX1, endY1);

            renderMap.Show();*/

            char key = Console.ReadKey().KeyChar;

            if (key == 'w')
            {
                double radians = render3D.DecToRad(hero.direction);
                int endX = (int)(hero.x + 2 * Math.Sin(radians));
                int endY = (int)(hero.y + 2 * Math.Cos(radians));
                hero.x = endX;
                hero.y = endY;
            }

            if (key == 's')
            {
                double radians = render3D.DecToRad(hero.direction);
                int endX = (int)(hero.x - 2 * Math.Sin(radians));
                int endY = (int)(hero.y - 2 * Math.Cos(radians));
                hero.x = endX;
                hero.y = endY;
            }

            if (key == 'd')
            {
                double radians = render3D.DecToRad(hero.direction + 90);
                int endX = (int)(hero.x + 2 * Math.Sin(radians));
                int endY = (int)(hero.y + 2 * Math.Cos(radians));
                hero.x = endX;
                hero.y = endY;
            }

            if (key == 'a')
            {
                double radians = render3D.DecToRad(hero.direction - 90);
                int endX = (int)(hero.x + 2 * Math.Sin(radians));
                int endY = (int)(hero.y + 2 * Math.Cos(radians));
                hero.x = endX;
                hero.y = endY;
            }

            if (key == 'k') {
                hero.direction -= 5;
                if (hero.direction == -5) hero.direction = 355;
                System.Diagnostics.Debug.WriteLine(hero.direction);
            }
            if (key == 'l') {
                hero.direction += 5;
                if (hero.direction == 365) hero.direction = 5;
                System.Diagnostics.Debug.WriteLine(hero.direction);
            }
        }
    }
}
