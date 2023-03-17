using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Shroomworld.FileHandling;

namespace Shroomworld;
public class Shroomworld : Game {

    // ----- Enums -----
    public enum GameState {
        CreatingWorld,
        Playing,
        Menu,
        Error,
    }

    // ----- Properties -----
    public static Dictionary<int, TileType> TileTypes => s_tileTypes;
    public static Dictionary<int, ItemType> ItemTypes => s_itemTypes;
    public static Dictionary<int, BiomeType> BiomeTypes => s_biomeTypes;
    public static Dictionary<int, FriendlyType> NpcTypes => s_friendlyTypes;
    public static Dictionary<int, PlayerType> PlayerTypes => s_playerTypes;

    public static ContentManager ContentManager => s_contentManager;
    public static IDisplayHandler DisplayHandler => s_displayHandler;


    // ----- Fields -----

    // Dictionaries:
    private static Dictionary<int, ItemType> s_itemTypes;
    private static Dictionary<int, TileType> s_tileTypes;
    private static Dictionary<int, BiomeType> s_biomeTypes;
    private static Dictionary<int, FriendlyType> s_friendlyTypes;
    private static Dictionary<int, PlayerType> s_playerTypes;

    private static readonly int s_defaultTileTypeCount; // Number of default tile types.
    
    // Monogame:
    private static ContentManager s_contentManager;
    private static IDisplayHandler s_displayHandler;
    private GraphicsDeviceManager _graphicsDeviceManager;

    // State management:
    private GameState _currentGameState;
    private Stack<Menu> _activeMenus;
    private event Action _checkForAttacks; // Subscribe enemy Npcs to this.
    private string _errorMessage;

    // Gameplay:
    //public static User CurrentUser;
    private World _world;
    private KeyBinds _menuKeyBinds;
    private KeyBinds _inventoryKeyBinds;
    private KeyBinds _friendlyInteractionKeyBinds;
    
    private class Menus {
        public static Menu MainMenu;
        public static Menu PauseMenu;
        public static Menu StatisticsMenu;
        // Todo: add the rest
    }


    // ----- Constructors -----
    public Shroomworld() {
        _graphicsDeviceManager = new GraphicsDeviceManager(this);
        s_contentManager = Content;
        s_contentManager.RootDirectory = "Content";
        IsMouseVisible = true;
    }


    // ----- Methods -----
    // Prepare
    protected override void Initialize() {
        // Set up display.
        s_displayHandler = new DisplayHandler(this, _graphicsDeviceManager);
        s_displayHandler.SetTitle("Shroomworld");

        // Set up menus.
        _currentGameState = GameState.Menu;
        _activeMenus = new Stack<Menu>();

        Input.Initialise();
        PhysicsData.SetAcceleration(0.2f);

        base.Initialize();
    }
    
    // Loading
    protected override void LoadContent() {
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
        && FileManager.LoadTypes<FriendlyType>().TryGetValue(out _friendlyTypes)*/
        && FileManager.LoadTypes<PlayerType>().TryGetValue(out s_playerTypes))) {
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
    private Menu GetCurrentMenu => _activeMenus.Peek();

    protected override void Update(GameTime gameTime) {
        Input.Update();
        switch (_currentGameState) {

            case GameState.CreatingWorld:
                _world = new World(new MapGenerator(100, 100, 5, 69_420).Generate(), s_playerTypes[0].CreateNew());
                _currentGameState = GameState.Playing;
                break;

            case GameState.Playing:
                _world.Update();
                s_displayHandler.MoveCamera(_world.Player.Sprite.Hitbox);
                s_displayHandler.UpdateCameraBounds();
                break;

            case GameState.Menu:
                _currentGameState = GameState.CreatingWorld;
                // TODO: if user chooses option to open a saved world, set _updateCurrentState to LoadWorld(id)
                break;

            case GameState.Error:
                break;

            default:
                SetStateToError("Unknown gamestate.");
                break;
        }
        base.Update(gameTime);
    }
    private void SetStateToError(string errorMessage) {
        _errorMessage = errorMessage;
        _currentGameState = GameState.Error;
    }
    private void SetStateToMenu(Menu menu) {
        OpenMenu(menu);
        _currentGameState = GameState.Menu;
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
            _currentGameState = GameState.Playing;
        }
        // (else: go back to previous menu)

        return true; // the current menu was closed successfully
    }
    
    // Drawing
    protected override void Draw(GameTime gameTime) {
        s_displayHandler.Begin();

        switch (_currentGameState) {
            case GameState.CreatingWorld:
                s_displayHandler.SetBackground(Color.Green);
                s_displayHandler.DrawText("Creating world...", new Vector2(500, 500), Color.Black);
                break;

            case GameState.Playing:
                _world.Draw();
                break;

            case GameState.Menu:
                s_displayHandler.SetBackground(Color.White);
                s_displayHandler.DrawText("Shroomworld", Vector2.Zero, Color.Black);
                break;

            case GameState.Error:
                string errorMsg = "An error has occured";
                s_displayHandler.SetBackground(Color.Red);
                s_displayHandler.DrawText(errorMsg, Vector2.Zero, Color.White);
                break;

            default:
                break;
        }
        s_displayHandler.End();
        base.Draw(gameTime);
    }
}