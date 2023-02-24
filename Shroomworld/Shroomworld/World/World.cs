namespace Shroomworld
{
    internal class World
    {
        // ---------- Properties ----------
        public Map Map => _map;
        public List<Friendly> Friendlies => _friendlies;
        public List<Enemy> Enemies => _enemies;

        // ---------- Fields ----------
        private Map _map;
        private List<Friendly> _friendlies;
        private List<Enemy> _enemies;
        
        // ---------- Constructors ----------
        public World(Map map, List<Friendly> friendlies, List<Enemy> enemies)
        {
            _map = map;
            _friendlies = friendlies;
            _enemies = enemies;
        }

        // ---------- Methods ----------
        public void Generate()
        {
            _map.Generate();
        }
    }
}
