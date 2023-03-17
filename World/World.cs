using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
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
    private readonly Map _map;
    private readonly List<Friendly> _friendlies;
    private readonly List<Enemy> _enemies;
    private readonly Player _player;
    private KeyBinds _keyBinds;

    
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
        _player.Body.ApplyPhysics();
        _player.Body.ResetAcceleration();
    }
    public void Draw() {
        Shroomworld.DisplayHandler.SetBackground(Color.CornflowerBlue);

        for (int x = 0; x < _map.Width; x++) {
            for (int y = 0; y < _map.Height; y++) {
                if (_map[x, y] == TileType.AirId) {
                    continue;
                }
                Shroomworld.DisplayHandler.Draw(((TileType)_map[x, y]).Texture, x, y);
            }
        }

        Shroomworld.DisplayHandler.Draw(_player.Sprite);
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
        _player.Body.AddAcceleration(-Vector2.UnitY);
    }
    private void PlayerMoveLeft() {
        _player.Body.AddAcceleration(-Vector2.UnitX);
    }
    private void PlayerMoveRight() {
        _player.Body.AddAcceleration(Vector2.UnitX);
    }
    private void PlayerMoveDown() {
        _player.Body.AddAcceleration(Vector2.UnitY);
    }
}