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
    public void Update() {
        _keyBinds.ProcessInputs(Input.CurrentInputs);
        ApplyPhysics();
    }
    public void Draw() {
        Shroomworld.DisplayHandler.SetBackground(Color.CornflowerBlue);

        for (int x = 0; x < _map.Width; x++) {
            for (int y = 0; y < _map.Height; y++) {
                if (_map[x, y] == TileType.AirId) {
                    continue;
                }
                Shroomworld.DisplayHandler.DrawTile(x, y, ((TileType)_map[x, y]).Texture);
            }
        }

        Shroomworld.DisplayHandler.DrawSprite(_player.Sprite);
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
        _player.Body.AddAcceleration(-Vector2.UnitY * _physics.Acceleration);
    }
    private void PlayerMoveLeft() {
        _player.Body.AddAcceleration(-Vector2.UnitX * _physics.Acceleration);
    }
    private void PlayerMoveRight() {
        _player.Body.AddAcceleration(Vector2.UnitX * _physics.Acceleration);
    }
    private void PlayerMoveDown() {
        _player.Body.AddAcceleration(Vector2.UnitY * _physics.Acceleration);
    }
    private void ApplyPhysics() {
        _player.Body.ApplyKinematics();
        _player.Body.ResetAcceleration();
        ResolveCollisions(_player);
    }
    private void ResolveCollisions(Entity entity) {
        // Get the coordinates of the tiles at the top left and bottom right of the entity's hitbox.
        Point topLeft = Shroomworld.DisplayHandler.GetTileCoords(entity.Sprite, i => (int)Math.Floor(i)); // tile pos in map
        Point bottomRight = topLeft + Shroomworld.DisplayHandler.GetSizeInTiles(entity.Sprite.Texture); //tile coords
        ClampToMap(ref topLeft);
        ClampToMap(ref bottomRight);
        DebugText = $"top left: {topLeft.X}, {topLeft.Y}     bottom right: {bottomRight.X}, {bottomRight.Y}";

        // Find all the tiles within that range.
        int maxIntersectingTiles = (Shroomworld.DisplayHandler.GetSizeInTiles(entity.Sprite.Texture).Y + 1) * (entity.Sprite.Texture.Width + 1);
        List<Rectangle> tiles = new List<Rectangle>(capacity: maxIntersectingTiles);
        for (int x = topLeft.X; x <= bottomRight.X; x++) {
            for (int y = topLeft.Y; y <= bottomRight.Y; y++) {
                if (_map[x, y] != TileType.AirId) {
                    tiles.Add(Shroomworld.DisplayHandler.CalculateTileHitbox(x, y, Shroomworld.TileTypes[_map[x, y]].Texture));
                }
            }
        }
        _physics.ResolveCollisions(entity.Body, tiles);

        void ClampToMap(ref Point p) {
            p.X = Math.Clamp(p.X, 0, _map.Width - 1);
            p.Y = Math.Clamp(p.Y, 0, _map.Height - 1);
        }
    }
}