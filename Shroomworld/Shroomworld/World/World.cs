using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Shroomworld;
/// <summary>
/// Contains a tile map and lists of entities.
/// </summary>
public class World {
    // ----- Properties -----
    public Map Map => _map;
    public List<Friendly> Friendlies => _friendlies;
    public List<Enemy> Enemies => _enemies;


    // ----- Fields -----
    private readonly Map _map;
    private readonly List<Friendly> _friendlies;
    private readonly List<Enemy> _enemies;
    private readonly Player _player;
    private KeyBinds _keyBinds;

    private static Action<Texture2D, int, int> _drawTile;
    
    // ----- Constructors -----
    public static void SetDrawFunction(Action<Texture2D, int, int> drawTile) {
        _drawTile = drawTile;
    }
    /// <summary>
    /// Create a new world.
    /// </summary>
    public World(Map map, Player player) {
        _map = map;
        _player = player;
        _friendlies = new List<Friendly>(/*capacity*/);
        _enemies = new List<Enemy>(/*capacity*/);
    }
    /// <summary>
    /// Instantiate a saved / existing world.
    /// </summary>
    public World(Map map, Player player, List<Friendly> friendlies, List<Enemy> enemies) {
        _map = map;
        _player = player;
        _friendlies = friendlies;
        _enemies = enemies;
    }

    // ----- Methods -----
    public void Update(GameTime gameTime) {
        _keyBinds.ProcessInputs();
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
    public void SetKeyBinds() {
        __keyBinds = new KeyBinds();
        __keyBinds.Add(Input.Inputs.W, PlayerJump);
        __keyBinds.Add(Input.Inputs.A, PlayerMoveLeft);
        __keyBinds.Add(Input.Inputs.D, PlayerMoveRight);
        __keyBinds.Add(Input.Inputs.Space, PlayerJump);
        /*_keyBinds.Add(Input.Inputs.Escape, OpenPauseMenu);
        __keyBinds.Add(Input.Inputs.Q, OpenQuestMenu);
        __keyBinds.Add(Input.Inputs.E, OpenInventory);
        __keyBinds.Add(Input.Inputs.N1, SwitchToHotbarSlot1);
        __keyBinds.Add(Input.Inputs.N2, SwitchToHotbarSlot2);
        __keyBinds.Add(Input.Inputs.N3, SwitchToHotbarSlot3);
        __keyBinds.Add(Input.Inputs.N4, SwitchToHotbarSlot4);*/
    }
    private void PlayerJump() {
        _player.Body.AddAcceleration(-Vector2.UnitY);
    }
    private void PlayerMoveLeft() {
        _player.Body.AddAcceleration(-Vector2.UnitX);
    }
    private void PlayerMoveRight() {
        _player.Body.AddAcceleration(Vector2.UnitX);
    }
}