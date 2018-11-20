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
