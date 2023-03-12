using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

    // ----- Properties -----
    public SpriteBatch SpriteBatch => _spriteBatch;
    public Vector2 TopLeftOfScreen => _topLeftOfScreen;
    public Vector2 BottomRightOfScreen => _bottomRightOfScreen;

    public GraphicsDeviceManager GraphicsDeviceManager => _graphicsDeviceManager;

    // ----- Fields -----
    private const int PixelsPerTile = 8;
    private const int TileSize = 20;

    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;
    private GameWindow _window;
    private static Vector2 _topLeftOfScreen;
    private static Vector2 _bottomRightOfScreen;

    // ----- Constructor -----
    public DisplayHandler(Game game, GraphicsDeviceManager graphicsDeviceManager) {
        _graphicsDeviceManager = graphicsDeviceManager;
        _spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);
        // Increase window size.
        _graphicsDeviceManager.PreferredBackBufferWidth = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
        _graphicsDeviceManager.PreferredBackBufferHeight = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
        _graphicsDeviceManager.ApplyChanges();

        _window = game.Window;
        _window.AllowUserResizing = true;
        _topLeftOfScreen = Vector2.Zero;
        _bottomRightOfScreen = new Vector2(_window.ClientBounds.Right, _window.ClientBounds.Bottom);
    }

    // ----- Methods -----
    public void Begin() {
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
    }
    public void End() {
        _spriteBatch.End();
    }
    public void Draw(Texture2D texture, Vector2 position, Color colour) {
		_spriteBatch.Draw(texture, position, colour);
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
        _spriteBatch.Draw(texture, new Rectangle(x * TileSize, y * TileSize * texture.Height / PixelsPerTile, TileSize, TileSize * texture.Height / PixelsPerTile), Color.White);
    }
    public void DrawText(string text, Vector2 position, Color colour) {
		// todo: draw text
	}
	public void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
		// todo: draw rectangle
	}
    /// <summary>
    /// Display a sprite on the screen using its <see cref="Sprite.Texture"/> and <see cref="Sprite.Position"/> properties.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    public void Draw(Sprite sprite) {
		_spriteBatch.Draw(sprite.Texture, sprite.Position, Color.White);
	}
    public Texture2D GetBlankTexture() {
        return new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
    }
    public void SetBackground(Color colour) {
        _graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
    public void SetTitle(string title) {
        _window.Title = title;
    }
}
