using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDRayCasting
{
    class GameCore
    {
        private IGameCore rc;

        public GameCore()
        {
            rc = new RayCasting();
            rc.Create();
            while (true) rc.Render();
        }

        public interface IGameCore
        {
            void Create();
            void Render();
        }
    }
}
