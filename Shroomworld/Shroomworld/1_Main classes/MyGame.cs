using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shroomworld
{
    public class MyGame : Game
    {
        public static Vector2 TopLeftOfScreen;
        public static Vector2 BottomRightOfScreen;

        public static User CurrentUser;
        public static int CurrentWorldID;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private event Func<bool> _doEnemyAttacks;
        private Action<GameTime> _updateCurrentState; // The update method to call in the Update method. I have opted for this
                                                      // instead of a GameState enum and an if statement in the Update function
                                                      // because it uses less processing time.
        private Stack<Menu> _activeMenus;
        private Menu _currentMenu => _activeMenus.Peek();
        // todo: menu class

		private class Menus
		{
            public Menu MainMenu;
            public Menu PauseMenu;
            public Menu StatisticsMenu;
            // todo: add the rest
		}

		public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

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

            // TODO: use this.Content to load your game content here
        }

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

		}
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static Vector2 ClampToScreen(float x, float y)
        {
            x = Math.Clamp(x, MyGame.TopLeftOfScreen.X, MyGame.BottomRightOfScreen.X);
            y = Math.Clamp(y, MyGame.TopLeftOfScreen.Y, MyGame.BottomRightOfScreen.Y);
            return new Vector2(x, y);
        }
        public static Vector2 ClampToScreen(Vector2 vector)
        {
            var x = Math.Clamp(vector.X, MyGame.TopLeftOfScreen.X, MyGame.BottomRightOfScreen.X);
            var y = Math.Clamp(vector.Y, MyGame.TopLeftOfScreen.Y, MyGame.BottomRightOfScreen.Y);
            return new Vector2(x, y);
        }


        private Npc CreateNewEnemy()
		{
            Npc npc = new Npc();
            _doEnemyAttacks += npc.InitiateAttack(); // todo: sort this out
            return npc;
		}
    }
}