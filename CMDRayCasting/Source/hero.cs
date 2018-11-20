using System;

namespace CMDRayCasting
{
    public class Hero
    {
        public double x = 41;
        public double y = 41;
        public int direction = 180;
        public char mark = '@';

        public void Walk(int distance, int direction)
        {
            double radians = RayCasting.DecToRadCorrected(direction);
            x = x + distance * Math.Sin(radians);
            y = y + distance * Math.Cos(radians);
        }
    }
}
