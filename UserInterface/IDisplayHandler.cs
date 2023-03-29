using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public interface IDisplayHandler {
	Texture2D BlankTexture { get; }
	Point MousePosition { get; }

	void Update(Vector2 playerPosition);
	void BeginWithCamera();
	void BeginStatic();
	void End();
	void Draw(Texture2D texture, Vector2 position);
	void Draw(Texture2D texture, int x, int y);
	void Draw(Sprite sprite);
	void DrawText(string text, Vector2 position, Color? colour = null);
	void DrawHotbar(Inventory inventory, Dictionary<int, ItemType> itemTypes, GuiElements gui);
	void SetTitle(string title);
	void SetBackground(Color colour);
	Point GetSizeInTiles(Texture2D texture);
}