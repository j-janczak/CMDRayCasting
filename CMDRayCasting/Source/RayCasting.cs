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
        Renders.RenderMap renderMap;

        public void Create()
        {
            map = new Map();
            hero = new Hero();
            renderMap = new Renders.RenderMap(map);
            renderMap.ConnectWithHero(hero);
        }

        public void Render()
        {
        
            renderMap.Show();
            char key = Console.ReadKey().KeyChar;

            if (key == 'd') hero.x++;
            if (key == 'a') hero.x--;
            if (key == 'w') hero.y--;
            if (key == 's') hero.y++;
        }
    }
}
