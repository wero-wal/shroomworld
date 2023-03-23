using System;
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

    public static string DebugText = string.Empty;


    // ----- Fields -----
    private static IDisplayHandler _display;
    private static Dictionary<int, TileType> _tileTypes;

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
        _physics = new Physics.Physics(acceleration: 0.2f, gravity: 6f);
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
    public static void SetUp(IDisplayHandler displayHandler, Dictionary<int, TileType> tileTypes) {
        _display = displayHandler;
        _tileTypes = tileTypes;
    }

    public void Update() {
        _keyBinds.ProcessInputs(Input.CurrentInputs);
        ApplyPhysics();
    }
    public void Draw() {
        _display.SetBackground(Color.CornflowerBlue);

        for (int x = 0; x < _map.Width; x++) {
            for (int y = 0; y < _map.Height; y++) {
                if (_map[x, y] == TileType.AirId) {
                    continue;
                }
                _display.DrawTile(x, y, _tileTypes[_map[x, y]].Texture);
            }
        }

        _display.DrawSprite(_player.Sprite);
    }

    private void SetKeyBinds() {
        _keyBinds = new KeyBinds();
        _keyBinds.Add(Input.Inputs.W, PlayerJump);
        _keyBinds.Add(Input.Inputs.A, PlayerMoveLeft);
        _keyBinds.Add(Input.Inputs.D, PlayerMoveRight);
        _keyBinds.Add(Input.Inputs.Space, PlayerJump);
        _keyBinds.Add(Input.Inputs.S, PlayerMoveDown);
        /*_keyBinds.Add(Input.Inputs.Escape, OpenPauseMenu);
        _keyBinds.Add(Input.Inputs.Q, OpenQuestMenu);
        _keyBinds.Add(Input.Inputs.E, OpenInventory);
        _keyBinds.Add(Input.Inputs.N1, SwitchToHotbarSlot1);
        _keyBinds.Add(Input.Inputs.N2, SwitchToHotbarSlot2);
        _keyBinds.Add(Input.Inputs.N3, SwitchToHotbarSlot3);
        _keyBinds.Add(Input.Inputs.N4, SwitchToHotbarSlot4);*/
    }
    private void PlayerJump() {
        _player.Body.AddAcceleration(_physics.AccelerationUp);
    }
    private void PlayerMoveLeft() {
        _player.Body.AddAcceleration(_physics.AccelerationLeft);
    }
    private void PlayerMoveRight() {
        _player.Body.AddAcceleration(_physics.AccelerationRight);
    }
    private void PlayerMoveDown() {
        _player.Body.AddAcceleration(_physics.AccelerationDown);
    }
    private void ApplyPhysics() {
        _player.Body.ApplyPhysics(CheckForCollisions);
    }
    /// <summary>
    /// Checks whether the given <paramref name="hitbox"/> intersects with any solid tiles.
    /// </summary>
    /// <param name="hitbox">The hitbox of the entity for which you are checking collisions.</param>
    /// <returns></returns>
    private bool CheckForCollisions(Rectangle hitbox) {
        // Get the tilemap coordinates of the tiles at the top left and bottom right of the entity's hitbox.
        (Point topLeft, Point bottomRight) corners = _display.GetTileCoords(hitbox);
        ClampToMap(ref corners.topLeft);
        ClampToMap(ref corners.bottomRight);
DebugText += $"top left: {corners.topLeft}\nbottom right: {corners.bottomRight}\n";
DebugText+=$"Player hitbox position: {_player.Sprite.Hitbox.Location}\n";
DebugText+=$"Player position: {_player.Sprite.Position}\n";
        // Check whether the tiles that intersect with the hitbox are solid.
        TileType tile;
        for (int x = corners.topLeft.X; x <= corners.bottomRight.X; x++) {
            for (int y = corners.topLeft.Y; y <= corners.bottomRight.Y; y++) {
                tile = _tileTypes[_map[x, y]];
                if (tile.IsSolid && _display.GetTileHitbox(x, y, tile.Texture.Bounds.Size).Intersects(hitbox)) {
                    return true;
                }
            }
        }
        return false;

        /// <summary>
        /// Clamps the given <see cref="Point"/> to the tile map.
        /// </summary>
        /// <param name="point">Tile map coordinates.</param>
        void ClampToMap(ref Point point) {
            point.X = Math.Clamp(point.X, 0, _map.Width - 1);
            point.Y = Math.Clamp(point.Y, 0, _map.Height - 1);
        }
    }
}