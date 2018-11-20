using System;

namespace CMDRayCasting
{
    public class Hero
    {
        public double x = 41;
        public double y = 41;
        public int direction = 180;
        public char mark = '@';

        public void Walk(int distance, int direction, Map map)
        {
            double radians = RayCasting.DecToRadCorrected(direction);
            double newX = x + distance * Math.Sin(radians);
            double newY = y + distance * Math.Cos(radians);

            if (newX > 0 && newY > 0 && newX < map.mapWidth && newY < map.mapHeight)
            {
                if (map.segment[(int)newX, (int)newY] == ' ')
                {
                    x = newX;
                    y = newY;
                }
            }
        }
    }
}
