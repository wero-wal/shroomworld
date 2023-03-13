using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

    // ----- Properties -----
    public GraphicsDeviceManager GraphicsDeviceManager => _graphicsDeviceManager;
    public SpriteBatch SpriteBatch => _spriteBatch;
    public Vector2 TopLeftOfScreen => new(_window.ClientBounds.Left, _window.ClientBounds.Top);
    public Vector2 BottomRightOfScreen => new(_window.ClientBounds.Right, _window.ClientBounds.Bottom);
    public Texture2D BlankTexture { get; private set; }

    // ----- Fields -----
    private const int PixelsPerTile = 8;
    private const int TileSize = 20;

    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;
    private GameWindow _window;

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

        BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
    }

    // ----- Methods -----
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
		_spriteBatch.Draw(texture,
            destinationRectangle: new Rectangle((int)position.X, (int)position.Y, TileSize, TileSize * GetHeightInTiles(texture)),
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
            position: new Vector2(x * TileSize, (y - GetHeightInTiles(texture) + 1) * TileSize),
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
    
    public int GetHeightInTiles(Texture2D texture) {
        return texture.Height / PixelsPerTile;
    }
    public void SetBackground(Color colour) {
        _graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
    public void SetTitle(string title) {
        _window.Title = title;
    }
}
