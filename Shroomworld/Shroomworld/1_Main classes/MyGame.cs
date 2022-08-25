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

        public static Dictionary<int, XType>
            TileDictionary,
            ItemDictionary,
            BiomeDictionary,
            EnemyDictionary,
            NpcDictionary;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
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
    }
}