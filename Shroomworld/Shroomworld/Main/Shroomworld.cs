using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Shroomworld.FileHandling;
using Microsoft.Xna.Framework.Content;

namespace Shroomworld;
public class Shroomworld : Game {

    // ----- Properties -----
    public static Dictionary<int, TileType> TileTypes => s_tileTypes;
    public static Dictionary<int, ItemType> ItemTypes => s_itemTypes;
    public static Dictionary<int, BiomeType> BiomeTypes => s_biomeTypes;
    public static Dictionary<int, FriendlyType> NpcTypes => s_friendlyTypes;
    public static Dictionary<int, PlayerType> PlayerTypes => s_playerTypes;

    public static SpriteBatch SpriteBatch => s_spriteBatch;
    public static float TileSize => s_tileSize;

    public static Vector2 TopLeftOfScreen => _topLeftOfScreen;
    public static Vector2 BottomRightOfScreen => _bottomRightOfScreen;

    public static GraphicsDeviceManager GraphicsDeviceManager => s_graphics;
    public static ContentManager ContentManager => s_contentManager;


    // ----- Fields -----
    private static Vector2 _topLeftOfScreen;
    private static Vector2 _bottomRightOfScreen;

    //public static User CurrentUser;

    private static Dictionary<int, ItemType> s_itemTypes;
    private static Dictionary<int, TileType> s_tileTypes;
    private static Dictionary<int, BiomeType> s_biomeTypes;
    private static Dictionary<int, FriendlyType> s_friendlyTypes;
    private static Dictionary<int, PlayerType> s_playerTypes;

    // Number of default tile types.
    private static int s_defaultTileTypeCount;
    private static float s_tileSize;

    private static ContentManager s_contentManager;
    
    private static GraphicsDeviceManager s_graphics;
    private static SpriteBatch s_spriteBatch;
    // subscribe enemy Npcs to this
    private event Action _checkForAttacks;

    /* The method to call in the main Update method. I have opted for this instead of a GameState
        enum and if statements in the Update function because it uses less processing time. */
    private Action<GameTime> _updateCurrentState;
    private Action _drawCurrentState;
    private Stack<Menu> _activeMenus;
    private Menu _currentMenu => _activeMenus.Peek();


    private World _world;
    private KeyBinds _menuKeyBinds;
    private KeyBinds _inventoryKeyBinds;
    private KeyBinds _friendlyInteractionKeyBinds;
    
    private class Menus {
        public Menu MainMenu;
        public Menu PauseMenu;
        public Menu StatisticsMenu;
        // Todo: add the rest
    }


    // ----- Constructors -----
    public Shroomworld() {
        s_graphics = new GraphicsDeviceManager(this);
        s_contentManager = Content;
        s_contentManager.RootDirectory = "Content";
        IsMouseVisible = true;
    }


    // ----- Methods -----
    // Prepare
    protected override void Initialize() {
        // TODO: Add your initialization logic here
        SetStateToCreatingWorld();
        //_updateCurrentState = UpdateMenu;
        //_activeMenus = new Stack<Menu> { Menus.MainMenu };
        base.Initialize();
    }
    
    // Loading
    protected override void LoadContent() {
        s_spriteBatch = new SpriteBatch(GraphicsDevice);
        s_contentManager.RootDirectory = "Content";

        if (!FileManager.TryLoadFilePaths()) {
            SetStateToError("Couldn't load file paths.");
            return;
        }
        if (!TryLoadGameFiles()) {
            SetStateToError("Couldn't load game files.");
        }
    }
    /// <summary>
    /// Loads all generic game files.
    /// </summary>
    /// <returns><see langword="true"/> if all files are successfully loaded, <see langword="false"/></returns>
    private bool TryLoadGameFiles() {
        // Load Types
        if(!(FileManager.LoadTypes<ItemType>().TryGetValue(out s_itemTypes)
        && FileManager.LoadTypes<TileType>().TryGetValue(out s_tileTypes)
        && FileManager.LoadTypes<BiomeType>().TryGetValue(out s_biomeTypes)/*
        && FileManager.LoadTypes<EnemyType>().TryGetValue(out _enemyTypes)
        && FileManager.LoadTypes<FriendlyType>().TryGetValue(out _friendlyTypes)
        && FileManager.LoadTypes<PlayerType>().TryGetValue(out _playerTypes)*/)) {
            return false;
        }

        /* TODO: Things to load:
           - World names / ids
           - User settings --> different method
           - Game settings
           - Menu button text (so, functionality will always be the same [will be hard-coded], but the button text can change).*/
        return true;
    }

    // Instantiation
    /*private void CreateMenus() {
        Menus.MainMenu = new Menu<>(new string[]{"1. New", "2. Quit"}, bgColour, textColour, null, null, new Vector2(100, 100), new Vector2(200, 200), null, MonogameDisplayHandler.DisplayBox, GetCursorLocation, GetInput)
    }*/
    private void SetKeyBinds() {
        _world.SetKeyBinds();
    }
    
    // Update cycle
    protected override void Update(GameTime gameTime) {
        _updateCurrentState(gameTime);
        //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        //    Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }
    private void SetStateToCreatingWorld() {
        _updateCurrentState = CreateWorld;
        _drawCurrentState = DrawCreatingWorld;
    }
    private void SetStateToStage() {
        _updateCurrentState = UpdateStage;
        _drawCurrentState = _world.Draw;
    }
    private void SetStateToMenu(Menu menu) {
        OpenMenu(menu);
        _updateCurrentState = UpdateMenu;
    }
    private void SetStateToError(string message = "An error has occured.") {
        _updateCurrentState = UpdateError;
        _drawCurrentState = DrawError;
    }
    private void DrawError() {
        GraphicsDevice.Clear(Color.Red);
        // todo: display error message
    }
    private void UpdateError(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Red);
    }
    private void UpdateStage(GameTime gameTime) {
        //_checkForAttacks?.Invoke(); // all subcribed npcs will now attempt to initiate an attack
    }
    private void CreateWorld(GameTime gameTime) {
        MapGenerator mapGenerator = new(100, 50, 5, 0.2f);
        _world = new World(mapGenerator.Generate(), s_playerTypes[0].CreateNew());
        SetStateToStage();
    }
    //// Gameplay
    ///// <summary>
    ///// This will be called whenever an enemy is able and attempts to initiate an attack.
    ///// It will check whether any of its opponents are in range. If so, an attack will be initiated.
    ///// </summary>
    ///// <returns></returns>
    //private void TryApplyAttack(Npc source, ReadonlyAttackData attackData) {
    //    // (from npc in _npcs where npc.Type.IsFriendly select npc)
    //    // loop through opponents
    //    foreach (Entity opponent in _friendlyEntities) {
    //        // check if in range
    //        if (source.IsInRange(opponent)) {
    //            source.InitiateAttack();
    //        }
    //    }
    //}
    private void CalculateNpcPaths() { 
        // this will be called upon edits to the world or changes in the player's position
        // Perhaps check if the interacted-with tile lies between them.
        // recalculate paths for npcs
    }
    /*private void CreateChest(SpecialChestTypes chestType, List<Item> items) {
        const string CHEST = "chest";
        string name = chestType.ToString().ToLower() + CHEST;
        List<Drop> drops = items.ForEach(item => new Drop(item.Id, item.Amount));
        _tileTypes.Add(_tileTypes.Count, new TileType(new IdData(_tileTypes.Count, name, name + "s"), drops, false, ));
        // todo: use the chest item type to do this
    }*/

    // Menu
    private void UpdateMenu(GameTime gameTime) {
        // todo: if user chooses option to open a saved world, set _updateCurrentState to LoadWorld(id)
    }
    /// <summary>
    /// Adds <paramref name="menu"/> to the stack (<see cref="_activeMenus"/>).
    /// </summary>
    /// <param name="menu">The menu you would like to open (add to the <see cref="_activeMenus"/> stack).</param>
    private void OpenMenu(Menu menu) {
        _activeMenus.Push(menu);
    }
    /// <summary>
    /// Closes a menu, so the user will either return to the previous one, or will return to the stage (if this was the only menu in <see cref="_activeMenus"/>).
    /// </summary>
    /// <returns>TRUE if a menu was closed successfully, FALSE if there were no menus to close.</returns>
    private bool CloseCurrentMenu() {
        // There's no menu to close
        if (_activeMenus.Count == 0) {
            return false; // no menus were closed
        }

        _activeMenus.Pop(); // Closes current menu
        
        // If there are no more open menus, go back to playing the game
        if (_activeMenus.Count == 0) {
            SetStateToStage();
        }
        // (else: go back to previous menu)

        return true; // the current menu was closed successfully
    }
    
    // Saving
    /// <summary>
    /// idk
    /// </summary>
    private void SaveSettings() {
        // Save user settings
    }
    /// <summary>
    /// save a world
    /// </summary>
    private void SaveWorld() {
        // Save world
    }

    // Drawing
    protected override void Draw(GameTime gameTime) {

        s_spriteBatch.Begin();
        _drawCurrentState();
        s_spriteBatch.End();

        base.Draw(gameTime);
    }
    private void DrawCreatingWorld() {
        s_graphics.GraphicsDevice.Clear(Color.Green);
    }
    /// <summary>
    /// Displays a texture on the screen based on a position in the tile map.
    /// </summary>
    /// <param name="texture">texture of the tile</param>
    /// <param name="x">x-coordinate of the object in the tile map</param>
    /// <param name="y">y-coordinate of the object in the tile map</param>
    private void Draw(Texture2D texture, int x, int y) {
        s_spriteBatch.Draw(texture, new Vector2(x * TileSize, y * TileSize), Color.White);
    }
    /// <summary>
    /// Display a texture on the screen at a given position.
    /// </summary>
    /// <param name="texture">The texture to be displayed.</param>
    /// <param name="position">The position (destination coordinates) in pixels,
    /// of the top left corner of the texture when it is displayed on-screen.</param>
    private void Draw(Texture2D texture, Vector2 position) {
        s_spriteBatch.Draw(texture, position, Color.White);
    }
    /// <summary>
    /// Display a sprite on the screen using its <see cref="Sprite.Texture"/> and <see cref="Sprite.Position"/> properties.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    private void Draw(Sprite sprite) {
        s_spriteBatch.Draw(sprite.Texture, sprite.Position, Color.White);
    }

    //
    public static Vector2 ClampToScreen(float x, float y) {
        x = Math.Clamp(x, Shroomworld.TopLeftOfScreen.X, Shroomworld.BottomRightOfScreen.X);
        y = Math.Clamp(y, Shroomworld.TopLeftOfScreen.Y, Shroomworld.BottomRightOfScreen.Y);
        return new Vector2(x, y);
    }
    public static Vector2 ClampToScreen(Vector2 vector) {
        var x = Math.Clamp(vector.X, Shroomworld.TopLeftOfScreen.X, Shroomworld.BottomRightOfScreen.X);
        var y = Math.Clamp(vector.Y, Shroomworld.TopLeftOfScreen.Y, Shroomworld.BottomRightOfScreen.Y);
        return new Vector2(x, y);
    }
}