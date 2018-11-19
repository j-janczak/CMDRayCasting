using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class Hero
    {
        public double x = 25;
        public double y = 25;
        public int direction = 0;
        public char mark = '@';

        public Hero() { }

        public void Walk(int distance, int direction)
        {
            double radians = RayCasting.DecToRadCorrected(direction);
            x = x + distance * Math.Sin(radians);
            y = y + distance * Math.Cos(radians);
        }
    }
}
