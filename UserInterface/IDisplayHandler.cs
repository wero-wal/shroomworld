using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld;
public interface IDisplayHandler {
	Texture2D BlankTexture { get; }

	void MoveCamera(Rectangle playerHitbox);
	void UpdateCentreOfScreen();

    void Begin();
	void End();
	void DrawText(string text, Vector2 position, Color colour);
	void DrawRectangle(Vector2 size, Vector2 position, Color colour);
	void Draw(Texture2D texture, Vector2 position, Color colour);
	void Draw(Sprite sprite);
	void Draw(Texture2D texture, int x, int y);

	Vector2 ClampToScreen(Rectangle hitbox);

	int GetHeightInTiles(Texture2D texture);

	void SetBounds(int width, int height);
	void SetBackground(Color colour);
	void SetTitle(string title);
}
