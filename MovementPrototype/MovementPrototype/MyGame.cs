using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MovementPrototype
{
    public class MyGame : Game
    {
        public static float BufferWidth { get => _graphics.PreferredBackBufferWidth; }
        public static float BufferHeight { get => _graphics.PreferredBackBufferHeight; }

        private enum Actions
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Count,
        }


        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<Actions, Action> _actionsToMethods;
        private Dictionary<Keys, Actions> _keyToActions;
        private Sprite _ball;
        

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _keyToActions = new Dictionary<Keys, Actions>((int)Actions.Count)
            {
                { Keys.Up, Actions.MoveUp },
                { Keys.Down, Actions.MoveDown },
                { Keys.Left, Actions.MoveLeft },
                { Keys.Right, Actions.MoveRight },
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

           _actionsToMethods = new Dictionary<Actions, Action>
            {
                { Actions.MoveUp, _ball.MoveUp },
                { Actions.MoveDown, _ball.MoveDown },
                { Actions.MoveLeft, _ball.MoveLeft },
                { Actions.MoveRight, _ball.MoveRight },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Actions action;
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
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetCentreOfScreen()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        }
    }
}