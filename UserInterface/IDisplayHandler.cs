using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public interface IDisplayHandler {
	Texture2D BlankTexture { get; }
	Point MousePosition { get; }

	void Begin();
	void BeginText();
	void Draw(Texture2D texture, Vector2 position);
	void Draw(Texture2D texture, int x, int y);
	void Draw(Sprite sprite);
	void DrawText(string text, Vector2 position, Color? colour = null);
	void End();
	void SetBackground(Color colour);
	void SetTitle(string title);
	void Update(Vector2 playerPosition);
	Point GetSizeInTiles(Texture2D texture);
}