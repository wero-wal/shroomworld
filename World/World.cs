using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Shroomworld;

/// <summary>
/// Contains a tile map and lists of entities.
/// </summary>
public class World {

    // ----- Properties -----
    public Player Player => _player;
    public Map Map => _map;
    public List<Friendly> Friendlies => _friendlies;
    public List<Enemy> Enemies => _enemies;


    // ----- Fields -----
    public delegate Vector2 Clamper(Vector2 position, Point size);

    private static Dictionary<int, TileType> s_tileTypes;
    private static Dictionary<int, ItemType> s_itemTypes;
    private static Dictionary<int, BiomeType> s_biomeTypes;
    private static Dictionary<int, EnemyType> s_enemyTypes;
    private static Dictionary<int, FriendlyType> s_friendlyTypes;

    private static readonly int s_defaultTileTypeCount; // Number of default tile types.

    private readonly Map _map;
    private readonly List<Friendly> _friendlies;
    private readonly List<Enemy> _enemies;
    private readonly Player _player;
    private KeyBinds _keyBinds;
    private Physics.Physics _physics;

    
    // ----- Constructors -----
    /// <summary>
    /// Create a new world.
    /// </summary>
    public World(Map map, Player player) {
        _map = map;
        _player = player;
        _friendlies = new List<Friendly>(/*capacity*/);
        _enemies = new List<Enemy>(/*capacity*/);
        SetKeyBinds();
        _physics = new Physics.Physics(acceleration: 0.2f, gravity: 0.1f);
    }
    /// <summary>
    /// Instantiate a saved / existing world.
    /// </summary>
    public World(Map map/*, Player player, List<Friendly> friendlies, List<Enemy> enemies*/) {
        _map = map;
        //_player = player;
        //_friendlies = friendlies;
        //_enemies = enemies;
    }

    // ----- Methods -----
    public static void SetUp(Dictionary<int, TileType> tileTypes, Dictionary<int, ItemType> itemTypes,
        Dictionary<int, BiomeType> biomeTypes/*, Dictionary<int, EnemyType> enemyTypes, Dictionary<int, FriendlyType> friendlyTypes*/) {
        s_tileTypes = tileTypes;
        s_itemTypes = itemTypes;
        s_biomeTypes = biomeTypes;
    }
    public static BiomeType GetRandomBiome(Random random = null) {
        return s_biomeTypes[(random ?? new Random()).Next(1, s_biomeTypes.Count + 1)];
    }

    public void Update() {
        _keyBinds.ProcessInputs(Input.CurrentInputs);
        ApplyPhysics();
        _player.Update();
        _player.Sprite.Position = Clamp(_player.Sprite.Position, _player.Sprite.Size);
    }
    public void Draw(IDisplayHandler displayHandler, GuiElements guiElements) {
        displayHandler.SetBackground(Color.CornflowerBlue);

        for (int x = 0; x < _map.Width; x++) {
            for (int y = 0; y < _map.Height; y++) {
                if (_map[x, y] == TileType.AirId) {
                    continue;
                }
                displayHandler.Draw(s_tileTypes[_map[x, y]].Texture, x, y);
            }
        }
        displayHandler.Draw(_player.Sprite);
        displayHandler.End();
        displayHandler.BeginStatic();
        displayHandler.DrawHotbar(_player.Inventory, s_itemTypes, guiElements);
    }

    private void SetKeyBinds() {
        _keyBinds = new KeyBinds();
        _keyBinds.Add(Input.Inputs.W, PlayerJump);
        _keyBinds.Add(Input.Inputs.A, PlayerMoveLeft);
        _keyBinds.Add(Input.Inputs.D, PlayerMoveRight);
        _keyBinds.Add(Input.Inputs.Space, PlayerJump);
        _keyBinds.Add(Input.Inputs.S, PlayerMoveDown);
        _keyBinds.Add(Input.Inputs.LeftMouseButton, PlaceTile);
        _keyBinds.Add(Input.Inputs.RightMouseButton, BreakTile);
        /*_keyBinds.Add(Input.Inputs.Escape, OpenPauseMenu);
        _keyBinds.Add(Input.Inputs.Q, OpenQuestMenu);
        _keyBinds.Add(Input.Inputs.E, OpenInventory);
        _keyBinds.Add(Input.Inputs.N1, SwitchToHotbarSlot1);
        _keyBinds.Add(Input.Inputs.N2, SwitchToHotbarSlot2);
        _keyBinds.Add(Input.Inputs.N3, SwitchToHotbarSlot3);
        _keyBinds.Add(Input.Inputs.N4, SwitchToHotbarSlot4);*/
    }
    private void PlayerJump() => _player.Body.AddAcceleration(_physics.AccelerationUp);
    private void PlayerMoveLeft() => _player.Body.AddAcceleration(_physics.AccelerationLeft);
    private void PlayerMoveRight() => _player.Body.AddAcceleration(_physics.AccelerationRight);
    private void PlayerMoveDown() => _player.Body.AddAcceleration(_physics.AccelerationDown);
    private void ApplyPhysics() {
        _player.Body.ApplyPhysics(_physics.Gravity, GetSolidIntersectingTiles);
    }
    /// <summary>
    /// </summary>
    /// <param name="hitbox">The hitbox of the entity for which you are checking collisions.</param>
    /// <returns>
    ///     A collection of the solid tiles with which the entity with the given <paramref name="point"/> and <paramref name="size"> intersects.
    /// </returns>
    private IEnumerable<Point> GetSolidIntersectingTiles(Vector2 position, Point size) {
        Clamp(ref position, size);
        for (int x = (int)position.X; x <= Math.Ceiling(position.X + size.X); x++) {
            for (int y = (int)position.Y; y <= Math.Ceiling(position.Y + size.Y); y++) {
                if (s_tileTypes[_map[x, y]].IsSolid) {
                    yield return new Point(x, y);
                }
            }
        }
	}
    private void BreakTile() {
        Point mouse = Shroomworld.DisplayHandler.MousePosition;

        if (!TileIsInRange(mouse, _player)) { return; }

        // Check if player is holding the appropriate tool.
        if (!s_itemTypes[_player.Inventory.SelectedItem.Id].ToolData.TryGetValue(out ToolData toolData)) { return; }
        if (!s_tileTypes[_map[mouse.X, mouse.Y]].BreakableBy.Contains(toolData.Type)) { return; }

        // Place the tile.
        s_tileTypes[_map[mouse.X, mouse.Y]].InsertDrops(_player.Inventory);
        _map[mouse.X, mouse.Y] = TileType.AirId;
    }
    private void PlaceTile() {
        Point mouse = Shroomworld.DisplayHandler.MousePosition;

        if (!TileIsInRange(mouse, _player)) { return; }

        if ((_map[mouse.X, mouse.Y] == TileType.AirId)
            && (s_itemTypes[_player.Inventory.SelectedItem].Tile.TryGetValue(out int tileToPlace))) {
            _map[mouse.X, mouse.Y] = tileToPlace;
        }
    }
    private bool TileIsInRange(Point tilePosition, Player player) {
        float distanceBetweenCentres = Vector2.Distance(player.Sprite.GetCentre(), tilePosition.ToVector2() + Vector2.One * 0.5f);
        return (distanceBetweenCentres <= Shroomworld.PlayerTypes[player.Id].PhysicsData.Range);
    }
    private void Clamp(ref Vector2 position, Point size) {
        position.X = Math.Clamp(position.X, 0, _map.Width - 1 - size.X);
        position.Y = Math.Clamp(position.Y, 0, _map.Height - 1 - size.Y);
    }
    private Vector2 Clamp(Vector2 position, Point size) {
        position.X = Math.Clamp(position.X, 0, _map.Width - 1 - size.X);
        position.Y = Math.Clamp(position.Y, 0, _map.Height - 1 - size.Y);
        return position;
    }
}