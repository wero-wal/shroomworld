using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Shroomworld
{
    /// <summary>
    /// Contains a tile map and lists of entities.
    /// </summary>
    internal class World
    {
        // ----- Properties -----
        public Map Map => _map;
        public List<Friendly> Friendlies => _friendlies;
        public List<Enemy> Enemies => _enemies;

        // ----- Fields -----
        private readonly Map _map;
        private readonly List<Friendly> _friendlies;
        private readonly List<Enemy> _enemies;

        private static Action<Texture2D, int, int> _drawTile;
        
        // ----- Constructors -----
        public static void SetDrawFunction(Action<Texture2D, int, int> drawTile) {
            _drawTile = drawTile;
        }
        public World(Map map, List<Friendly> friendlies, List<Enemy> enemies) {
            _map = map;
            _friendlies = friendlies;
            _enemies = enemies;
        }

        // ----- Methods -----
        public void Update(GameTime gameTime) {
            // move entities
        }
        public void Draw() {
            for (int x = 0; x < _map.Width; x++) {
                for (int y = 0; y < _map.Height; y++) {
                    if (_map[x, y] == Map.AirTile) {
                        continue;
                    }
                    _drawTile(Shroomworld.TileTypes[_map[x, y]].Texture, x, y);
                }
            }
        }
    }
}
