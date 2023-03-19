using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

    // ----- Properties -----
    public GraphicsDeviceManager GraphicsDeviceManager => _graphicsDeviceManager;
    public SpriteBatch SpriteBatch => _spriteBatch;
    public Texture2D BlankTexture { get; private set; }

    // ----- Fields -----
    private const int TileSize = 8; // Number of pixels per tile side length.
    private const int ScaleFactor = 20;

    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;
    private GameWindow _window;
    private Camera _camera;

    private Rectangle _worldBounds; // its the world bounds but in screen scale

    // ----- Constructor -----
    public DisplayHandler(Game game, GraphicsDeviceManager graphicsDeviceManager) {
        _graphicsDeviceManager = graphicsDeviceManager;
        _spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);

        // Configure window.
        _graphicsDeviceManager.PreferredBackBufferWidth = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
        _graphicsDeviceManager.PreferredBackBufferHeight = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
        _graphicsDeviceManager.ApplyChanges();
        _window = game.Window;
        _window.AllowUserResizing = true;
        _camera = new Camera(game.Window.ClientBounds);

        BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
    }

    // ----- Methods -----
    public void MoveCamera(Rectangle playerHitbox) {
        _camera.MoveToPlayer(playerHitbox);
    }
    public void UpdateCentreOfScreen() {
        _camera.UpdateCentreOfScreen(_window.ClientBounds);
    }

    public void Begin() {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    }
    public void End() {
        _spriteBatch.End();
    }
    /// <summary>
    /// Main drawing method.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="position">scaled-up on-screen position</param>
    /// <param name="colour"></param>
    public void Draw(Texture2D texture, Vector2 position, Color colour) {
        position = _camera.CalibratePosition(position);
		_spriteBatch.Draw(texture,
            destinationRectangle: new Rectangle((int)position.X, (int)position.Y, ScaleFactor, ScaleFactor * GetHeightInTiles(texture)),
            colour);
    }
    /// <summary>
    /// Displays a texture on the screen based on a position in the tile map.
    /// </summary>
    /// <param name="texture">texture of the tile</param>
    /// <param name="x">x-coordinate of the object in the tile map</param>
    /// <param name="y">y-coordinate of the object in the tile map</param>
    /// <summary>
    /// Display a texture on the screen at a given position.
    /// </summary>
    /// <param name="texture">The texture to be displayed.</param>
    /// <param name="position">The position (destination coordinates) in pixels,
    /// of the top left corner of the texture when it is displayed on-screen.</param>
    public void Draw(Texture2D texture, int x, int y) {
        Draw(texture,
            position: ToScreenScale(x, y, GetHeightInTiles(texture)),
            colour: Color.White);
    }
    public void DrawText(string text, Vector2 position, Color colour) {
		// todo: draw text
	}
	public void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
        Draw(BlankTexture, position, colour);
	}
    /// <summary>
    /// Display a sprite on the screen using its <see cref="Sprite.Texture"/> and <see cref="Sprite.Position"/> properties.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    public void Draw(Sprite sprite) {
		Draw(sprite.Texture, sprite.Position, Color.White);
	}
    public Vector2 ToScreenScale(int x, int y, int heightInTiles) {
        return new Vector2(x * ScaleFactor, (y - heightInTiles + 1) * ScaleFactor);
    }

    public void ClampToScreen(ref Rectangle hitbox, ref Vector2 position) {
        hitbox.X = Math.Clamp(hitbox.X, _worldBounds.Left, _worldBounds.Right - hitbox.Width);
        hitbox.Y = Math.Clamp(hitbox.Y, _worldBounds.Top, _worldBounds.Right - hitbox.Height);
        position.X = Math.Clamp(position.X, _worldBounds.Left, _worldBounds.Right - hitbox.Width);
        position.Y = Math.Clamp(position.Y, _worldBounds.Top, _worldBounds.Right - hitbox.Height);
    }
    public void ClampToScreen(Rectangle hitbox, ref Vector2 position) {
        position.X = Math.Clamp(position.X, _worldBounds.Left, _worldBounds.Right - hitbox.Width);
        position.Y = Math.Clamp(position.Y, _worldBounds.Top, _worldBounds.Right - hitbox.Height);
    }

    public int GetHeightInTiles(Texture2D texture) {
        return texture.Height / TileSize;
    }
    public Point GetSizeInTiles(Texture2D texture) {
        return new Point(texture.Width / TileSize, texture.Height / TileSize);
    }

    public Point ToWorldScale(Sprite sprite, Func<float, int> round) {
        Vector2 worldScalePosition = sprite.Position;
        worldScalePosition.X /= TileSize;
        worldScalePosition.Y /= TileSize;
        worldScalePosition.Y += 1 - GetHeightInTiles(sprite.Texture);
        return new Point(round(worldScalePosition.X), round(worldScalePosition.Y));
    }

    public void SetBounds(int width, int height) {
        _worldBounds = new Rectangle(Point.Zero, ToScreenScale(width, height, 1).ToPoint());
    }
    public void SetBackground(Color colour) {
        _graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
    public void SetTitle(string title) {
        _window.Title = title;
    }

    public Rectangle GetHitboxOfTile(int x, int y, Texture2D texture) {
        return new Rectangle(ToScreenScale(x, y, GetHeightInTiles(texture)).ToPoint(), ScalePoint(texture.Bounds.Size, ScaleFactor));
    }
    private void ScalePoint(ref Point p, int scaleFactor) {
        p.X *= scaleFactor;
        p.Y *= scaleFactor;
    }
    private Point ScalePoint(Point p, int scaleFactor) {
        p.X *= scaleFactor;
        p.Y *= scaleFactor;
        return p;
    }
}