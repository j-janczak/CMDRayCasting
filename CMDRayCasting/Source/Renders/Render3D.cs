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

        public Render3D(Map m, Hero h)
        {
            mapFile = m;
            hero = h;
        }

        public void Show()
        {

        }

        private void CastRay(int endx, int endy)
        {
            int d, dx, dy, ai, bi, xi, yi;
            int x = hero.x, y = hero.y;
            if (hero.x < endx)
            {
                xi = 1;
                dx = endx - hero.x;
            }
            else
            {
                xi = -1;
                dx = hero.x - endx;
            }
            if (hero.y < endy)
            {
                yi = 1;
                dy = endy - hero.y;
            }
            else
            {
                yi = -1;
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
                    if (mapFile.segment[x, y] == '#')
                    {
                        //TODO
                    }
                    //PutMark('O', x, y);
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
                    if (mapFile.segment[x, y] == '#')
                    {
                        //TODO
                    }
                    //PutMark('O', x, y);
                }
            }
        }
    }
}
