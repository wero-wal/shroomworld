namespace Shroomworld
{
	internal interface IDisplayHandler
	{
		void DrawTexture(Texture2 texture, Vector2 position, Color colour = Color.White);
		void DrawText(string text, Vector2 position, Color colour);
		void DrawRectangle(Vector2 size, Vector2 position, Color colour);
	}
}
