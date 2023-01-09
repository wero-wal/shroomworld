using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shroomworld
{
    public class Shroomworld : Game
    {
        // ----- Enums -----
        private enum SpecialChestTypes
        {
            Death, // created upon death to store the user's items
            User // when a user places a chest
        }

        // ----- Properties -----
        internal static Dictionary<int, TileType> TileTypes => _tileTypes;
        internal static Dictionary<int, ItemType> ItemTypes => _itemTypes;
        internal static Dictionary<int, BiomeType> BiomeTypes => _biomeTypes;
        internal static Dictionary<int, NpcType> NpcTypes => _npcTypes;
        internal static Dictionary<int, PlayerTemplate> PlayerTypes => _playerTypes;

        
        // ----- Fields -----
        public static Vector2 TopLeftOfScreen;
        public static Vector2 BottomRightOfScreen;

        public static User CurrentUser;
        public static int CurrentWorldID;

        
        private const int _WIDTH = 0;
        private const int _HEIGHT = 1;

        private static Dictionary<int, TileType> _tileTypes;
        private static Dictionary<int, ItemType> _itemTypes;
        private static Dictionary<int, BiomeType> _biomeTypes;
        private static Dictionary<int, NpcType> _npcTypes;
        private static Dictionary<int, PlayerTemplate> _playerTypes;

        private static int _universalTileTypeCount; // number of default tile types

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private event Action<> _checkForAttacks; // subscribe enemy Npcs to this

        private Action<GameTime> _updateCurrentState; // The update method to call in the Update method. I have opted for this
                                                      // instead of a GameState enum and an if statement in the Update function
                                                      // because it uses less processing time.
        private Stack<Menu> _activeMenus;
        private Menu _currentMenu => _activeMenus.Peek();
        private List<Npc> _npcs;
        private List<Entity> _friendlyEntities; // contains references to all friendly entities in current world
        


		private class Menus
		{
            public Menu MainMenu;
            public Menu PauseMenu;
            public Menu StatisticsMenu;
            // todo: add the rest
		}

        // ----- Constructors -----
		public Shroomworld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // ----- Methods -----
        // Prepare
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _updateCurrentState = UpdateMenu;
            _activeMenus = new Stack<Menu> { Menus.MainMenu };
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = "Content";

            // FILE FORMAT for biomes: name,rarity,top-layer-tile,2nd-layer,3rd-layer,tree-texture,flower1;flower2;flower3;...,amount-of-trees,amount-of-chests,amount-of-NPCs,amount-of-enemies
            // 	context	| 0		 | 1		| 2				| 3
            // 	amount	| none	 | a few	| a fair amount	| a lot
            //	rarity	| common | uncommon	| cool			| rare

            // TODO: use this.Content to load your game content here
        }
        private void LoadFiles()
		{
            /* TODO: Things to load:
             * - Types
             * - World names / ids
             * - User settings
             * - Game settings
             * - Menu button text (so, functionality will always be the same [will be hard-coded], but the button text can change).
             */
		}
        
        // Update cycle
        protected override void Update(GameTime gameTime)
        {
            _updateCurrentState(gameTime);
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        private void SetStateToStage()
		{
            _updateCurrentState = UpdateStage;
		}
        private void SetStateToMenu(Menu menu)
		{
            OpenMenu(menu);
            _updateCurrentState = UpdateMenu;
		}
        private void UpdateStage(GameTime gameTime)
		{
            _checkForAttacks?.Invoke(); // all subcribed npcs will now attempt to initiate an attack
		}
        
        // Gameplay
        /// <summary>
        /// This will be called whenever an enemy is able and attempts to initiate an attack.
        /// It will check whether any of its opponents are in range. If so, an attack will be initiated.
        /// </summary>
        /// <returns></returns>
        private void TryApplyAttack(Npc source, ReadonlyAttackData attackData)
        {
            // (from npc in _npcs where npc.Type.IsFriendly select npc)
            foreach (Entity opponent in _friendlyEntities) // loop through opponents
            {
                if (source.IsInRange(opponent)) // check if in range
                {
                    source.InitiateAttack();
                }
            }
        }
        private void CalculateNpcPaths()
        {
            // this will be called upon edits to the world or changes in the player's position
            // Perhaps check if the interacted-with tile lies between them.
            // recalculate paths for npcs
        }
        private void CreateChest(SpecialChestTypes chestType, List<Item> items)
        {
            const string CHEST = "chest";
            string name = chestType.ToString().ToLower() + CHEST;
            List<Drop> drops = items.ForEach(item => new Drop(item.Id, item.Amount));
            _tileTypes.Add(_tileTypes.Count, new TileType(new IdentifyingData(_tileTypes.Count, name, name + "s"), drops, false, ));
            // todo: use the chest item type to do this
        }

        // Menu
        private void UpdateMenu(GameTime gameTime)
		{

		}
        /// <summary>
        /// Adds <paramref name="menu"/> to the stack (<see cref="_activeMenus"/>).
        /// </summary>
        /// <param name="menu">The menu you would like to open (add to the <see cref="_activeMenus"/> stack).</param>
        private void OpenMenu(Menu menu)
		{
            _activeMenus.Push(menu);
		}
        /// <summary>
        /// Closes a menu, so the user will either return to the previous one, or will return to the stage (if this was the only menu in <see cref="_activeMenus"/>).
        /// </summary>
        /// <returns>TRUE if a menu was closed successfully, FALSE if there were no menus to close.</returns>
        private bool CloseCurrentMenu()
		{
			if (_activeMenus.Count == 0) // There's no menu to close
			{
                return false; // no menus were closed
			}

            _activeMenus.Pop(); // Closes current menu
            
            // If there are no more open menus, go back to playing the game
			if (_activeMenus.Count == 0)
			{
                SetStateToStage();
			}
            // (else: go back to previous menu)

            return true; // the current menu was closed successfully
		}
        
        // Saving
        /// <summary>
        /// idk
        /// </summary>
        private void SaveSettings()
		{
            // Save user settings
		}
        /// <summary>
        /// save a world
        /// </summary>
        private void SaveWorld()
		{
            // Save world
		}

        // Drawing
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static Vector2 ClampToScreen(float x, float y)
        {
            x = Math.Clamp(x, Shroomworld.TopLeftOfScreen.X, Shroomworld.BottomRightOfScreen.X);
            y = Math.Clamp(y, Shroomworld.TopLeftOfScreen.Y, Shroomworld.BottomRightOfScreen.Y);
            return new Vector2(x, y);
        }
        public static Vector2 ClampToScreen(Vector2 vector)
        {
            var x = Math.Clamp(vector.X, Shroomworld.TopLeftOfScreen.X, Shroomworld.BottomRightOfScreen.X);
            var y = Math.Clamp(vector.Y, Shroomworld.TopLeftOfScreen.Y, Shroomworld.BottomRightOfScreen.Y);
            return new Vector2(x, y);
        }

        // Initialisation details
        private Player CreatePlayer()
        {
            Player player = new Player();
            player.Moved += CalculateNpcPaths;
            player.PlacedOrRemovedTile += CalculateNpcPaths;
        }
        private Npc CreateNewEnemy()
		{
            Npc npc = new Npc();
            _checkForEnemyAttacks += npc.TryToInitiateAttack(); // todo: sort this out
            npc.AttackAttemptInitiated += TryApplyAttack;
            return npc;
		}

    }
}