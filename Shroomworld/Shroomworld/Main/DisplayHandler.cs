using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public class DisplayHandler : IDisplayHandler {
	public void DrawTexture(Texture2D texture, Vector2 position, Color colour) {
		Shroomworld.SpriteBatch.Draw(texture, position, colour);
	}
	public void DrawText(string text, Vector2 position, Color colour) {
		// todo: draw text
	}
	public void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
		// todo: draw rectangle
	}
	public void DrawSprite(Sprite sprite) {
		Shroomworld.SpriteBatch.Draw(sprite.Texture, sprite.Position, Color.White);
	}
}
