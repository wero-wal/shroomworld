using System;
using System.Runtime.Serialization.Formatters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

	// ----- Properties -----
	public Vector2 CentreOfScreen => _centreOfScreen;
	public Texture2D BlankTexture { get; private set; }

	// ----- Fields -----
	private const int TileSize = 8; // Number of pixels per square tile side length.
	private const int ScaleFactor = 5;

	private GraphicsDeviceManager _graphicsDeviceManager;
	private SpriteBatch _spriteBatch;
	public static SpriteFont Font;
	private GameWindow _window;
	private Camera _camera;

	private Vector2 _centreOfScreen;

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
		_camera = new Camera();

		BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
	}

	// ----- Methods -----
	// Initialising
	public void SetBounds(int width, int height) {
		_movementBounds = new Rectangle(Point.Zero, new Point(width * TileSize, height * TileSize));
	}
	public void SetBackground(Color colour)	{
		_graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
	public void SetTitle(string title) {
		_window.Title = title;
	}

	public void Update(Vector2 playerPosition) {
		_centreOfScreen = _window.ClientBounds.Size.ToVector2() / 2;
		_camera.Follow(playerPosition);
	}

	// Drawing
	public void Begin() {
		_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Transform);
	}
	public void BeginText() {
		_spriteBatch.Begin();
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
			destinationRectangle: new Rectangle(position.ToPoint().ScaleBy(ScaleFactor), texture.Bounds.Size.ScaleBy(ScaleFactor)),
			Color.White);
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
			position: GetTilePosition(x, y, texture.Bounds.Size),
			colour: Color.White);
	}
	/// <summary>
	/// Display a sprite on the screen using its <see cref="Sprite.Texture"/> and <see cref="Sprite.Position"/> properties.
	/// </summary>
	/// <param name="sprite">The sprite to be displayed.</param>
	public void DrawSprite(Sprite sprite) {
		Draw(sprite.Texture, sprite.Position, Color.White);
	}
	public void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
		Draw(BlankTexture, position, colour);
	}
	public void DrawText(string text, Vector2 position, Color colour) {
		_spriteBatch.DrawString(Font, text, position, colour);
	}

	// Vector conversions
	/// <summary>
	///     Converts world-scale coordinates to coordinates in the tile map.
	/// </summary>
    /// <returns>
    ///     A tuple containing the tile map coordinates of the tiles in which the top left and
    ///     bottom right corners of the hitbox lie.
    /// </returns>
	public (Point TopLeft, Point BottomRight) GetTileCoords(Rectangle hitbox) {
		Point position = hitbox.Location;
		position.X /= TileSize;
		position.Y /= TileSize;
		position.Y += 1 - hitbox.Height;
		return (position, position + new Point(hitbox.Width / TileSize, hitbox.Height / TileSize));
	}
	/// <summary>
	/// Converts tile map coordinates to a world-scale position.
	/// </summary>
	/// <param name="x">x-coordinate of the tile in the tile map.</param>
	/// <param name="y">y-coordinate of the tile in the tile map.</param>
	/// <param name="size">The size of the tile's texture.</param>
	/// <returns>The position of the tile in world-scale.</returns>
	private Vector2 GetTilePosition(int x, int y, Point size) {
		return new Vector2(x * TileSize, (y + 1) * TileSize - size.Y);
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
	public Rectangle GetTileHitbox(int x, int y, Point textureSize) {
		return new Rectangle(GetTilePosition(x, y, textureSize).ToPoint(), textureSize);
	}

	// Other
	public void ClampToWorld(ref Rectangle hitbox, ref Vector2 position) {
		hitbox.X = Math.Clamp((int)position.X, _movementBounds.Left, _movementBounds.Right - hitbox.Width);
		hitbox.Y = Math.Clamp((int)position.Y, _movementBounds.Top, _movementBounds.Bottom - hitbox.Height);
		position.X = Math.Clamp(position.X, _movementBounds.Left, _movementBounds.Right - hitbox.Width);
		position.Y = Math.Clamp(position.Y, _movementBounds.Top, _movementBounds.Bottom - hitbox.Height);
	}
}