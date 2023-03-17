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

    public Vector2 ClampToScreen(Rectangle hitbox) {
        return new Vector2(
            x: Math.Clamp(hitbox.X, _worldBounds.Left, _worldBounds.Right - hitbox.Width),
            y: Math.Clamp(hitbox.Y, _worldBounds.Top, _worldBounds.Right - hitbox.Height)
        );
    }

    public int GetHeightInTiles(Texture2D texture) {
        return texture.Height / TileSize;
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
}
