using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public interface IDisplayHandler {
	Vector2 CentreOfScreen { get; }
	Texture2D BlankTexture { get; }

	void Begin();
	void BeginText();
	Rectangle GetTileHitbox(int x, int y, Point textureSize);
	void ClampToWorld(ref Rectangle hitbox, ref Vector2 position);
	void Draw(Texture2D texture, Vector2 position, Color colour);
	void DrawRectangle(Vector2 size, Vector2 position, Color colour);
	void DrawSprite(Sprite sprite);
	void DrawText(string text, Vector2 position, Color colour);
	void DrawTile(int x, int y, Texture2D texture);
	void End();
	(Point TopLeft, Point BottomRight) GetTileCoords(Rectangle hitbox);
	void Draw(Texture2D texture, Vector2 position);
	void SetBackground(Color colour);
	void SetBounds(int width, int height);
	void SetTitle(string title);
	void Update(Vector2 playerPosition);
}