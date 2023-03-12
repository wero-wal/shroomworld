using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public interface IDisplayHandler {
	Vector2 TopLeftOfScreen { get; }
	Vector2 BottomRightOfScreen { get; }
	Texture2D GetBlankTexture();
	void DrawText(string text, Vector2 position, Color colour);
	void DrawRectangle(Vector2 size, Vector2 position, Color colour);
	void Draw(Texture2D texture, Vector2 position, Color colour);
	void Draw(Sprite sprite);
	void Draw(Texture2D texture, int x, int y);
	void SetBackground(Color colour);
	void SetTitle(string title);
	void Begin();
	void End();
}
