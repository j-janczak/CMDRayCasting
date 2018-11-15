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
        int start = 120;
        int range = 75;
        int a;

        public void Create()
        {
            a = start;
            map = new Map();
            hero = new Hero();
            renderMap = new Renders.Render2D(map, hero);
            render3D = new Renders.Render3D(map, hero);
        }

        public void Render()
        {
            double angle = (a * Math.PI / 180);
            int endX = (int)(hero.x + 50 * Math.Sin(angle));
            int endY = (int)(hero.y + 50 * Math.Cos(angle));
            a--;
            if (a == start - range) a = start;

            renderMap.DrawLine(endX, endY);

            renderMap.Show();
            System.Threading.Thread.Sleep(10);
            /*char key = Console.ReadKey().KeyChar;

            if (key == 'd') hero.x++;
            if (key == 'a') hero.x--;
            if (key == 'w') hero.y--;
            if (key == 's') hero.y++;*/
        }
    }
}
