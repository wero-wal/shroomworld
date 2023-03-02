using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public interface IDisplayHandler {
	void DrawTexture(Texture2D texture, Vector2 position, Color colour = Color.White);
	void DrawText(string text, Vector2 position, Color colour = Color.Black);
	void DrawRectangle(Vector2 size, Vector2 position, Color colour);
	void DrawSprite(Sprite sprite);
}
