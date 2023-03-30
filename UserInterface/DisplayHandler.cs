using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {

	// ----- Properties -----
	public Point MousePosition => (Input.MousePosition / (Scale * TileSize)).ToPoint();
	public Texture2D BlankTexture { get; private set; }

	// ----- Fields -----
	public const int TileSize = 8; // Number of pixels per square tile side length.
	private const int Scale = 5;

	public static SpriteFont Font;

	private readonly Color _defaultColour = Color.White;
	private readonly Color _defaultTextColour = Color.Black;
	private readonly Matrix _transform;
	private GraphicsDeviceManager _graphicsDeviceManager;
	private SpriteBatch _spriteBatch;
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
		BlankTexture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
		_transform = Matrix.CreateScale(Scale)/* * Matrix.CreateTranslation(_centreOfScreen.X / Scale, _centreOfScreen.Y / Scale, 0)*/;
	
	}

	// ----- Methods -----
	// Initialising
	public void SetBackground(Color colour)	{
		_graphicsDeviceManager.GraphicsDevice.Clear(colour);
	}
	public void SetTitle(string title) {
		_window.Title = title;
	}

	public void Update(Vector2 centreOfPlayer, int width, int height) {
		_camera.Follow(ClampToEdges(centreOfPlayer * TileSize), _centreOfScreen / Scale);

		Vector2 ClampToEdges(Vector2 target) {
			target.X = Math.Clamp(target.X, _centreOfScreen.X / Scale, (width - 1) * TileSize - _centreOfScreen.X / Scale);
			target.Y = Math.Clamp(target.Y, _centreOfScreen.Y / Scale, (height - 1) * TileSize - _centreOfScreen.Y / Scale);
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
		_spriteBatch.Draw(texture, position * TileSize, _defaultColour);
	}
	public void Draw(Texture2D texture, int x, int y) {
		_spriteBatch.Draw(texture, new Vector2(x, y - GetSizeInTiles(texture.Bounds.Size).Y + 1) * TileSize, _defaultColour);
	}
	public void Draw(Sprite sprite) {
		_spriteBatch.Draw(sprite.Texture, sprite.Position * TileSize, _defaultColour);
	}
	public void DrawText(string text, Vector2 position, Color? colour = null) {
		_spriteBatch.DrawString(Font, text, position, colour ?? _defaultTextColour);
	}
	public void DrawHotbar(Inventory inventory, Dictionary<int, ItemType> itemTypes, GuiElements gui) {
		Vector2 position = gui.HotbarPosition;
		for (int i = 0; i < inventory.SelectedSlot; i++) {
			DrawNext(gui.HotbarSlot, inventory[i, Inventory.HotbarRow]);
		}
		DrawNext(gui.SelectedHotbarSlot, inventory[inventory.SelectedSlot, Inventory.HotbarRow]);
		for (int i = inventory.SelectedSlot + 1; i < Inventory.Width; i++) {
			DrawNext(gui.HotbarSlot, inventory[i, Inventory.HotbarRow]);
		}

		void DrawNext(Texture2D texture, Maybe<InventoryItem> maybeItem) {
			if (!maybeItem.TryGetValue(out var item)) {
				return;
			}
			End();
			BeginStatic();
			_spriteBatch.Draw(texture, position * TileSize, _defaultColour);
			_spriteBatch.Draw(itemTypes[item.Id].Texture, position * TileSize, _defaultColour);
			End();
			BeginText();
			_spriteBatch.DrawString(Font, item.Amount.ToString(), position * TileSize * Scale, Color.White);
			position.X++;
		}
	}
	public void DrawInventory(Inventory inventory, Dictionary<int, ItemType> itemTypes, GuiElements gui) {
		Vector2 position;
		for (int y = 0; y < Inventory.Height; y++) {
			for (int x = 0; x < Inventory.Width; x++) {
				position = new Vector2(y * TileSize, x * TileSize);
				End();
				BeginStatic();
				_spriteBatch.Draw(gui.HotbarSlot, position, _defaultColour);
				if (!inventory[x, y].TryGetValue(out var item)) {
					continue;
				}
				_spriteBatch.Draw(itemTypes[item.Id].Texture, position, _defaultColour);
				End();
				BeginText();
				_spriteBatch.DrawString(Font, item.Amount.ToString(), position * Scale, Color.White);
			}
		}
	}
	public Texture2D CreateTexture(Color? colour = null) {
		Texture2D texture = new Texture2D(_graphicsDeviceManager.GraphicsDevice, TileSize, TileSize);
		texture.SetData(new Color[] { colour ?? _defaultColour });
		return texture;
	}
	public Point GetSizeInTiles(Point size) {
		return new Point(size.X / TileSize, size.Y / TileSize);
	}
}