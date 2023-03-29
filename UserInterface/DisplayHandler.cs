using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

	// ----- Properties -----
	public Point MousePosition => (Input.MousePosition / (Scale * TileSize)).ToPoint();
	public Texture2D BlankTexture { get; private set; }

	// ----- Fields -----
	public const int TileSize = 8; // Number of pixels per square tile side length.
	private const int Scale = 10;

	private GraphicsDeviceManager _graphicsDeviceManager;
	private SpriteBatch _spriteBatch;
	public static SpriteFont Font;
	private GameWindow _window;
	private Camera _camera;
	private Vector2 _centreOfScreen;
	private readonly Color _defaultColour = Color.White;
	private readonly Color _defaultTextColour = Color.Black;

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
		_camera = new Camera(Scale, ref _centreOfScreen);

		BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
	}

	// ----- Methods -----
	// Initialising
	public void SetBackground(Color colour)	{
		_graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
	public void SetTitle(string title) {
		_window.Title = title;
	}

	public void Update(Vector2 playerPosition) {
		_centreOfScreen = _window.ClientBounds.Center.ToVector2();
		_camera.Follow(playerPosition * TileSize);
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
	public void Draw(Texture2D texture, Vector2 position) {
		_spriteBatch.Draw(texture, position * TileSize, _defaultColour);
	}
	public void Draw(Texture2D texture, int x, int y) {
		_spriteBatch.Draw(texture, new Vector2(x, y - GetSizeInTiles(texture).Y + 1) * TileSize, _defaultColour);
	}
	public void Draw(Sprite sprite) {
		_spriteBatch.Draw(sprite.Texture, sprite.Position * TileSize, _defaultColour);
	}
	public void DrawText(string text, Vector2 position, Color? colour = null) {
		_spriteBatch.DrawString(Font, text, position, colour ?? _defaultTextColour);
	}
	public Point GetSizeInTiles(Texture2D texture) {
		return new Point(texture.Width / TileSize, texture.Height / TileSize);
	}
}