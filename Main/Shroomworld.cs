using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shroomworld.FileHandling;

namespace Shroomworld;
public class Shroomworld : Game {

    // ----- Enums -----
    public enum GameStates {
        CreatingWorld,
        Playing,
        Menu,
        Error,
    }

    // ----- Properties -----
    public static ContentManager ContentManager => s_contentManager;
    public static DisplayHandler DisplayHandler => s_displayHandler;
    public static Dictionary<int, PlayerType> PlayerTypes => s_playerTypes;
    public static Settings Settings => s_settings;

    // ----- Fields -----
    private static Dictionary<int, PlayerType> s_playerTypes;


    // Monogame:
    private static ContentManager s_contentManager;
    private static DisplayHandler s_displayHandler;
    private GraphicsDeviceManager _graphicsDeviceManager;

    private GuiElements _guiElements;

    // State management:
    private GameStates _currentGameState;
    private Stack<ButtonMenu> _activeMenus;
    private event Action _checkForAttacks; // Subscribe enemy Npcs to this.
    private string _errorMessage;

    // Gameplay:
    //public static User CurrentUser;
    private World _world;
    private KeyBinds _menuKeyBinds;
    private KeyBinds _inventoryKeyBinds;
    private KeyBinds _friendlyInteractionKeyBinds;
    private static Settings s_settings;
    private GameData _gameData;
    private List<Quest> _defaultQuests;
    
    private class Menus {
        public static ButtonMenu MainMenu;
        public static ButtonMenu PauseMenu;
        public static ButtonMenu StatisticsMenu;
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
        Sprite.SetSizeConverter(s_displayHandler.GetSizeInTiles);

        // Set up menus.
        _currentGameState = GameStates.CreatingWorld;
        _activeMenus = new Stack<ButtonMenu>();

        Input.Initialise();

        base.Initialize();
    }
    
    // Loading
    protected override void LoadContent() {
        const string LoadingErrorMessageSuffix = " loading failed.";
        const string FontErrorMessage = "Font" + LoadingErrorMessageSuffix;
        const string FilePathsErrorMessage = "File paths" + LoadingErrorMessageSuffix;
        const string SettingsErrorMessage = "Settings" + LoadingErrorMessageSuffix;
        const string GameDataErrorMessage = "Game data" + LoadingErrorMessageSuffix;
        const string QuestErrorMessage = "Quest." + LoadingErrorMessageSuffix;
        const string ItemErrorMessage = "Item" + LoadingErrorMessageSuffix;
        const string TileErrorMessage = "Tile" + LoadingErrorMessageSuffix;
        const string BiomeErrorMessage = "Biome" + LoadingErrorMessageSuffix;
        const string PlayerErrorMessage = "Player" + LoadingErrorMessageSuffix;
        const string MenuErrorMessage = "Menu" + LoadingErrorMessageSuffix;
        const string GuiErrorMessage = "Gui" + LoadingErrorMessageSuffix;

        if (FileManager.LoadFont("font").TryGetValue(out SpriteFont font)) {
            s_displayHandler.SetFont(font);
        }
        else {
            SetStateToError(FontErrorMessage); return;
        }

        if (!FileManager.TryLoadFilePaths()) {
            SetStateToError(FilePathsErrorMessage); return;
        }
        LoadGameFiles();
        LoadMenus();
        LoadGuiElements();
        if (!FileManager.LoadSettings().TryGetValue(out s_settings)) {
            SetStateToError(SettingsErrorMessage);
        }
        s_displayHandler.SetTileSize(s_settings.TileSize);
        if (!FileManager.LoadGameData().TryGetValue(out _gameData)) {
            SetStateToError(GameDataErrorMessage);
        }
        if (!FileManager.LoadQuests().TryGetValue(out _defaultQuests)) {
            SetStateToError(QuestErrorMessage); return;
        }

        // --- Local functions ---
        void LoadGameFiles() {
            Dictionary<int, TileType> tileTypes;
            Dictionary<int, ItemType> itemTypes;
            Dictionary<int, BiomeType> biomeTypes;
            if(!FileManager.LoadTypes<ItemType>().TryGetValue(out itemTypes)) {
                SetStateToError(ItemErrorMessage); return;
            }
            else if (!FileManager.LoadTypes<TileType>().TryGetValue(out tileTypes)) {
                SetStateToError(TileErrorMessage); return;
            }
            else if (!FileManager.LoadTypes<BiomeType>().TryGetValue(out biomeTypes)) {
                SetStateToError(BiomeErrorMessage); return;
            }
            /*else if (!FileManager.LoadTypes<EnemyType>().TryGetValue(out var enemyTypes)) {
                SetStateToError(); return;
            }
            else if (!FileManager.LoadTypes<FriendlyType>().TryGetValue(out var friendlyTypes)) {
                SetStateToError(); return;
            }*/
            else if (!FileManager.LoadTypes<PlayerType>().TryGetValue(out s_playerTypes)) {
                SetStateToError(PlayerErrorMessage); return;
            }

            World.SetUp(tileTypes, itemTypes, biomeTypes/*, enemyTypes, friendlyTypes*/);
        }
        void LoadMenus() {
            if (!FileManager.TryLoadMenus(s_displayHandler, out var menuDisplayHandler, out var menus)) {
                SetStateToError(MenuErrorMessage);
                return;
            }
            ButtonMenu.SetDisplayHandler(menuDisplayHandler);
            int m = 0;
            Menus.MainMenu = CreateMenu(menus[m++],
            new Action[] {
                () => _currentGameState = GameStates.Playing
            });
            _activeMenus.Push(Menus.MainMenu);

            ButtonMenu CreateMenu((string name, Sprite title, string[] items, Vector2 location) menuData, Action[] actions) {
                return new ButtonMenu(menuData.name, menuData.title, menuData.items, menuData.location, actions);
            }
        }
        void LoadGuiElements() {
            if (!FileManager.LoadGuiElements().TryGetValue(out _guiElements)) {
                SetStateToError(GuiErrorMessage);
            }
        }
    }
    private ButtonMenu GetCurrentMenu => _activeMenus.Peek();

    protected override void Update(GameTime gameTime) {
        Input.Update();
        switch (_currentGameState) {

            case GameStates.CreatingWorld:
                const int Width = 100;
                const int Height = 50;
                const int NumberOfBiomes = 10;
                Player player = s_playerTypes[0].CreateNew(quests: _defaultQuests);
                const int QuestBoxWidth = 4;
                const int QuestBoxHeight = 10;

                _world = new World(
                    map: new MapGenerator(Width, Height, NumberOfBiomes/*, 69_420*/).Generate(),
                    player,
                    gravity: s_settings.Gravity,
                    acceleration: s_settings.Acceleration,
                    s_displayHandler.CreateTexture(colour: Color.Brown, width: QuestBoxWidth, height: (QuestBoxHeight / 2))
                );
                _currentGameState = GameStates.Playing;
                break;

            case GameStates.Playing:
                _world.Update();
                s_displayHandler.Update(_world.Player.Sprite.GetCentre(), Width, Height);
                break;

            case GameStates.Menu:
                _activeMenus.Peek().Update();
                // TODO: if user chooses option to open a saved world, set _updateCurrentState to LoadWorld(id)
                break;

            case GameStates.Error:
                break;

            default:
                SetStateToError("Unknown gamestate.");
                break;
        }
        base.Update(gameTime);
    }
    private void SetStateToError(string errorMessage) {
        _errorMessage = errorMessage;
        _currentGameState = GameStates.Error;
    }
    private void SetStateToMenu(ButtonMenu menu) {
        OpenMenu(menu);
        _currentGameState = GameStates.Menu;
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
    private void OpenMenu(ButtonMenu menu) {
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
            _currentGameState = GameStates.Playing;
        }
        // (else: go back to previous menu)

        return true; // the current menu was closed successfully
    }
    
    // Drawing
    protected override void Draw(GameTime gameTime) {
        switch (_currentGameState) {

            case GameStates.CreatingWorld:
                s_displayHandler.BeginStatic();
                s_displayHandler.SetBackground(Color.Green);
                s_displayHandler.DrawText("Creating world...", new Vector2(500, 500), Color.Black);
                break;

            case GameStates.Playing:
                s_displayHandler.BeginWithCamera();
                _world.Draw(s_displayHandler, _guiElements);
                break;

            case GameStates.Menu:
                s_displayHandler.BeginStatic();
                _activeMenus.Peek().Draw();
                break;

            case GameStates.Error:
                s_displayHandler.BeginStatic();
                s_displayHandler.SetBackground(Color.Red);
                s_displayHandler.DrawText(_errorMessage, Vector2.Zero, Color.White);
                break;

            default:
                break;
        }
        s_displayHandler.End();
        base.Draw(gameTime);
    }
}