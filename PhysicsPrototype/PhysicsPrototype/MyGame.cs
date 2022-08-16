using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PhysicsPrototype
{
    public class MyGame : Game
    {
        public static float BufferWidth { get => _graphics.PreferredBackBufferWidth; }
        public static float BufferHeight { get => _graphics.PreferredBackBufferHeight; }
        public static Vector2 TopLeftOfScreen { get => _topLeftOfScreen; }
        public static Vector2 BottomRightOfScreen { get => _bottomRightOfScreen; }
        public static Vector2 Gravity = new Vector2(0f, 9.8f);

        private enum Functions
        {
            PlayerJump,
            PlayerMoveLeft,
            PlayerMoveRight,
            Count,
        }


        private static Vector2
            _topLeftOfScreen = Vector2.Zero,
            _bottomRightOfScreen;

        private static GraphicsDeviceManager _graphics;

        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private Dictionary<Keys, Functions> _functionsDictionary;
        private Player _player;

        public MyGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _bottomRightOfScreen = new Vector2(BufferWidth, BufferHeight);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _functionsDictionary = new Dictionary<Keys, Functions>((int)Functions.Count)
            {
                { Keys.Up, Functions.PlayerJump },
                { Keys.Left, Functions.PlayerMoveLeft },
                { Keys.Right, Functions.PlayerMoveRight },
                { Keys.Space, Functions.PlayerJump },
                { Keys.W, Functions.PlayerJump },
                { Keys.A, Functions.PlayerMoveLeft },
                { Keys.D, Functions.PlayerMoveRight },
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
            var sprite = new Sprite(Content.Load<Texture2D>("ball"), GetCentreOfScreen(), Color.White);
            _player = new Player(sprite, 100f, 1f);
            _player.CentreOnOrigin();

            _font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            Functions action;
            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            { Exit(); }

            // TODO: Add your update logic here
            var pressedKeys = Keyboard.GetState().GetPressedKeys();

            foreach (var key in pressedKeys) // find out what should actually be happening (convert Keys => Actions)
            {
                try
                {
                    action = _functionsDictionary[key];
                    switch (action)
                    {
                        case Functions.PlayerJump:
                            direction.Y -= 10; // maybe just code in a Jump()
                            break;
                        case Functions.PlayerMoveLeft:
                            direction.X--;
                            break;
                        case Functions.PlayerMoveRight:
                            direction.X++;
                            break;
                    }
                }
                catch (KeyNotFoundException)
                {
                }
            }

            //if (direction != Vector2.Zero)
            //{
            //    direction.Normalize();
            //}

            _player.Move(direction, 0.25f, (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player.Sprite.Texture, _player.Sprite.Position, _player.Sprite.Colour);
            _spriteBatch.DrawString(_font, $"x: {_player.Sprite.Position.X}    y: {_player.Sprite.Position.Y}", Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetCentreOfScreen()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        }
    }
}