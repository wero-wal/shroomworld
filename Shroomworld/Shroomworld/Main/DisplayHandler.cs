using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public static class DisplayHandler : IDisplayHandler {
	public static void DrawTexture(Texture2D texture, Vector2 position, Color colour = Color.White) {
		Shroomworld.SpriteBatch.Draw(texture, position, colour);
	}
	public static void DrawText(string text, Vector2 position, Color colour) {
		// todo: draw text
	}
	public static void DrawRectangle(Vector2 size, Vector2 position, Color colour) {
		// todo: draw rectangle
	}
	public static void DrawSprite(Sprite sprite) {
		Shroomworld.SpriteBatch.Draw(sprite.Texture, sprite.Position, Color.White);
	}
}
