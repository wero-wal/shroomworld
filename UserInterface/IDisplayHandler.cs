using System;
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
	void DrawSprite(Sprite sprite);
	void DrawTile(Texture2D texture, int x, int y);

	void ClampToScreen(ref Rectangle hitbox, ref Vector2 position);
    int GetHeightInTiles(Texture2D texture);
    Point GetTileCoords(Sprite sprite, Func<float, int> round);
	Vector2 CalculateTilePosition(int x, int y, int heightInTiles);

	Rectangle CalculateTileHitbox(int x, int y, Texture2D texture);

    void SetBounds(int width, int height);
	void SetBackground(Color colour);
	void SetTitle(string title);
}
