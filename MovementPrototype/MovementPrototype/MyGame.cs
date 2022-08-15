using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementPrototype
{
    public class MyGame : Game
    {
        public static float BufferWidth { get => _graphics.PreferredBackBufferWidth; }
        public static float BufferHeight { get => _graphics.PreferredBackBufferHeight; }

        private enum Functions
        {
            PlayerMoveUp,
            PlayerMoveDown,
            PlayerMoveLeft,
            PlayerMoveRight,
            Count,
        }


        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<Functions, Action> _actionsToMethods;
        private Dictionary<Keys, Functions> _keyToActions;
        private Sprite _ball;
        private SpriteFont _font;
        

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _keyToActions = new Dictionary<Keys, Functions>((int)Functions.Count)
            {
                { Keys.Up, Functions.PlayerMoveUp },
                { Keys.Down, Functions.PlayerMoveDown },
                { Keys.Left, Functions.PlayerMoveLeft },
                { Keys.Right, Functions.PlayerMoveRight },
            };

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
            _ball = new Sprite(Content.Load<Texture2D>("ball"), GetCentreOfScreen(), 10f);
            _ball.Centre();

           _actionsToMethods = new Dictionary<Functions, Action>
            {
                { Functions.PlayerMoveUp, _ball.MoveUp },
                { Functions.PlayerMoveDown, _ball.MoveDown },
                { Functions.PlayerMoveLeft, _ball.MoveLeft },
                { Functions.PlayerMoveRight, _ball.MoveRight },
            };

            _font = Content.Load<SpriteFont>("Fonts/Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Functions action;
            var pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (var key in pressedKeys) // find out what should actually be happening (convert Keys => Actions)
            {
                action = _keyToActions[key];
                _actionsToMethods[action]();// call the appropriate method for each action
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_ball.Texture, _ball.Position, _ball.Color);
            _spriteBatch.DrawString(_font, $"x: {_ball.Position.X}    y: {_ball.Position.Y}", new(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetCentreOfScreen()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        }
    }
}