using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

	// ----- Properties -----
	public Point MousePosition => (Vector2.Transform(Input.MousePosition, Matrix.Invert(_camera.Transform)) / _tileSize).ToPoint();
	public Texture2D BlankTexture { get; private set; }

	// ----- Fields -----
	private const int Scale = 5;

	private readonly Color _defaultColour = Color.White;
	private readonly Color _defaultTextColour = Color.Black;
	private readonly Matrix _transform;
	private readonly Point _windowSize;
	private int _tileSize = 8; // Number of pixels per square tile side length.
	private GraphicsDeviceManager _graphicsDeviceManager;
	private SpriteBatch _spriteBatch;
	private SpriteFont _font;
	private GameWindow _window;
	private Camera _camera;
	private Vector2 _centreOfScreen;

	// ----- Constructor -----
	public DisplayHandler(Game game, GraphicsDeviceManager graphicsDeviceManager) {
		_graphicsDeviceManager = graphicsDeviceManager;
		_spriteBatch = new SpriteBatch(graphicsDeviceManager.GraphicsDevice);

		// Configure window.
		_graphicsDeviceManager.PreferredBackBufferWidth = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Width;
		_graphicsDeviceManager.PreferredBackBufferHeight = _graphicsDeviceManager.GraphicsDevice.DisplayMode.Height;
		_graphicsDeviceManager.ApplyChanges();
		_window = game.Window;
		_window.AllowUserResizing = false;
		_camera = new Camera(Scale);
		_centreOfScreen = _window.ClientBounds.Center.ToVector2();
		BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, _tileSize, _tileSize);
		_transform = Matrix.CreateScale(Scale)/* * Matrix.CreateTranslation(_centreOfScreen.X / Scale, _centreOfScreen.Y / Scale, 0)*/;
		_windowSize = _window.ClientBounds.Size;
	}

	// ----- Methods -----
	// Initialising
	public void SetTileSize(int tileSize) {
		_tileSize = tileSize;
	}
	public void SetFont(SpriteFont font) {
		_font = font;
	}
	public void SetBackground(Color colour)	{
		_graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
	public void SetTitle(string title) {
		_window.Title = title;
	}

	public void Update(Vector2 centreOfPlayer, int width, int height) {
		_camera.Follow(ClampToEdges(centreOfPlayer * _tileSize), _centreOfScreen / Scale);

		Vector2 ClampToEdges(Vector2 target) {
			target.X = Math.Clamp(target.X, _centreOfScreen.X / Scale, (width - 1) * _tileSize - _centreOfScreen.X / Scale);
			target.Y = Math.Clamp(target.Y, _centreOfScreen.Y / Scale, (height - 1) * _tileSize - _centreOfScreen.Y / Scale);
			return target;
		}
	}

	// Drawing
	public void BeginWithCamera() {
		_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Transform);
	}
	public void BeginStatic() {
		_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _transform);
	}
	public void BeginText() {
		_spriteBatch.Begin();
	}
	public void End() {
		_spriteBatch.End();
	}
	public void Draw(Texture2D texture, Vector2 position) {
		_spriteBatch.Draw(texture, position * _tileSize, _defaultColour);
	}
	public void Draw(Texture2D texture, int x, int y) {
		_spriteBatch.Draw(texture, new Vector2(x, y - GetSizeInTiles(texture.Bounds.Size).Y + 1) * _tileSize, _defaultColour);
	}
	public void Draw(Sprite sprite) {
		_spriteBatch.Draw(sprite.Texture, sprite.Position * _tileSize, _defaultColour);
	}
	public void DrawText(string text, Vector2 position, Color? colour = null) {
		_spriteBatch.DrawString(_font, text, position * _tileSize * Scale, colour ?? _defaultTextColour);
	}
	public void DrawHotbar(Inventory inventory, GuiElements gui) {
		Vector2 position = gui.HotbarPosition;
		for (int i = 0; i < Inventory.Width; i++) {
			DrawSlot(gui, position, i == inventory.SelectedSlot);
			if (inventory[i, Inventory.HotbarRow].TryGetValue(out InventoryItem item)) {
				DrawItem(item, position);
			}
			position.X++;
		}
	}
	public void DrawInventory(Inventory inventory, GuiElements gui) {
		Vector2 position;
		for (int y = 0; y < Inventory.Height; y++) {
			for (int x = 0; x < Inventory.Width; x++) {
				if (y != Inventory.HotbarRow) {
					position = new Vector2(x, y);
					DrawSlot(gui, position, false);
					if (inventory[x, y].TryGetValue(out InventoryItem item)) {
						DrawItem(item, position);
					}
				}
			}
		}
	}
	private void DrawSlot(GuiElements gui, Vector2 position, bool selected) {
		End();
		BeginStatic();
		Draw(selected ? gui.SelectedHotbarSlot : gui.HotbarSlot, position);
	}
	private void DrawItem(InventoryItem item, Vector2 position) {
		Draw(World.ItemTypes[item.Id].Texture, position);
		End();
		BeginText();
		DrawText(item.Amount.ToString(), position, Color.White);
	}
	public void DrawQuests(List<Quest> quests, Texture2D questBox) {
		int screenWidthInTiles = _window.ClientBounds.Width / (_tileSize * Scale);
		Vector2 position = new Vector2(screenWidthInTiles - questBox.Width / _tileSize, 0);
		string allQuests = string.Empty;
		int questBoxHeight = 0;
		foreach (Quest quest in quests) {
			allQuests += quest.ToString() + Environment.NewLine;
			questBoxHeight += quest.NumberOfRequirements + 1;
		}
		End();
		BeginStatic();
		Draw(questBox, position);
		End();
		BeginText();
		DrawText(allQuests, position);
	}
	public Texture2D CreateTexture(int width = 1, int height = 1, Color? colour = null) {
		width *= _tileSize;
		height *= _tileSize;
		Texture2D texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, width, height);
		Color[] data = new Color[width * height];
		for (int pixel = 0; pixel < data.Length; pixel++) {
			data[pixel] = colour ?? _defaultColour;
		}
		texture.SetData(data);
		return texture;
	}
	public Point GetSizeInTiles(Point size) {
		return new Point(size.X / _tileSize, size.Y / _tileSize);
	}
}