using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler /*: IDisplayHandler*/ {

    // ----- Properties -----
    public GraphicsDeviceManager GraphicsDeviceManager => _graphicsDeviceManager;
    public SpriteBatch SpriteBatch => _spriteBatch;
    public Texture2D BlankTexture { get; private set; }

    // ----- Fields -----
    private const int TileSize = 8; // Number of pixels per square tile side length.
    private const int ScaleFactor = 20;

    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _spriteBatch;
    private GameWindow _window;
    private Camera _camera;

    private Rectangle _movementBounds; // World bounds in world scale.

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
    // Initialising
    public void SetBounds(int width, int height) {
        _movementBounds = new Rectangle(Point.Zero, new Point(width * TileSize, height * TileSize));
    }
    public void SetBackground(Color colour) {
        _graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
    public void SetTitle(string title) {
        _window.Title = title;
    }

    // Camera management
    public void MoveCamera(Vector2 playerPosition) {
        _camera.MoveToPlayer(playerPosition);
    }
    public void UpdateCentreOfScreen() {
        _camera.UpdateCentreOfScreen(_movementBounds/*_window.ClientBounds*/);
    }

    // Drawing
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
            destinationRectangle: new Rectangle(ScalePoint(position.ToPoint(), ScaleFactor), ScalePoint(texture.Bounds.Size, ScaleFactor)),
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
    public void DrawTile(int x, int y, Texture2D texture) {
        Draw(texture,
            position: CalculateTilePosition(x, y, texture),
            colour: Color.White);
    }
    /// <summary>
    /// Display a sprite on the screen using its <see cref="Sprite.Texture"/> and <see cref="Sprite.Position"/> properties.
    /// </summary>
    /// <param name="sprite">The sprite to be displayed.</param>
    public void DrawSprite(Sprite sprite) {
		Draw(sprite.Texture, sprite.Position, sprite.Colour);
	}
	public void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
        Draw(BlankTexture, position, colour);
	}
    public void DrawText(string text, Vector2 position, Color colour) {
		// todo: draw text
	}

    // Vector conversions
    public int GetHeightInTiles(Texture2D texture) {
        return texture.Height / TileSize;
    }
    public Point GetSizeInTiles(Texture2D texture) {
        return new Point(texture.Width / TileSize, texture.Height / TileSize);
    }
    public Point GetTileCoords(Sprite sprite, Func<float, int> round) {
        Vector2 worldScalePosition = sprite.Position;
        worldScalePosition.X /= TileSize;
        worldScalePosition.Y /= TileSize;
        worldScalePosition.Y += 1 - GetHeightInTiles(sprite.Texture);
        return new Point(round(worldScalePosition.X), round(worldScalePosition.Y));
    }
    public Vector2 CalculateTilePosition(int x, int y, Texture2D texture) {
        return new Vector2(x * TileSize, (y - GetHeightInTiles(texture) + 1) * TileSize);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">The x-coordinate of the tile in the tilemap.</param>
    /// <param name="y">The y-coordinate of the tile in the tilemap.</param>
    /// <param name="texture">The texture of the tile.</param>
    /// <returns>
    ///     A <see cref="Rectangle"/> representing the hitbox of the tile at coordinates
    ///     (<paramref name="x"/>, <paramref name="y"/>) in world scale.
    /// </returns>
    public Rectangle CalculateTileHitbox(int x, int y, Texture2D texture) {
        return new Rectangle(CalculateTilePosition(x, y, texture).ToPoint(), texture.Bounds.Size);
    }

    // Other
    private Point ScalePoint(Point p, int scaleFactor) {
        p.X *= scaleFactor;
        p.Y *= scaleFactor;
        return p;
    }
    public void ClampToScreen(ref Rectangle hitbox, ref Vector2 position) {
        hitbox.X = Math.Clamp(hitbox.X, _movementBounds.Left, _movementBounds.Right - hitbox.Width);
        hitbox.Y = Math.Clamp(hitbox.Y, _movementBounds.Top, _movementBounds.Right - hitbox.Height);
        position.X = Math.Clamp(position.X, _movementBounds.Left, _movementBounds.Right - hitbox.Width);
        position.Y = Math.Clamp(position.Y, _movementBounds.Top, _movementBounds.Right - hitbox.Height);
    }
    public void ClampToScreen(Rectangle hitbox, ref Vector2 position) {
        position.X = Math.Clamp(position.X, _movementBounds.Left, _movementBounds.Right - hitbox.Width);
        position.Y = Math.Clamp(position.Y, _movementBounds.Top, _movementBounds.Right - hitbox.Height * 2);
    }
}