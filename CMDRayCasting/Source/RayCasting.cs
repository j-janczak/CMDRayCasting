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
        }

        public void Render()
        {


            render3D.Show();

            /*renderMap.DrawLine(endX, endY);
            renderMap.Show();*/


            char key = Console.ReadKey().KeyChar;

            if (key == 'w') hero.x++;
            if (key == 's') hero.x--;
            if (key == 'a')
            {
                if (hero.y > 2) hero.y--;
            }
            if (key == 'd')
            {
                if (hero.y < map.mapHeight - 3) hero.y++;
            }

            if (key == 'k') hero.direction += 5;
            if (key == 'l') hero.direction -= 5;
        }
    }
}
