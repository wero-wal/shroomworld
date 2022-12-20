using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MyLibrary
{
	class MonogameDisplayHandler : IDisplayHandler<Color, Vector2, float>
	{
		// ---------- Properties ---------- 
		// ---------- Fields ---------- 
		private SpriteBatch _spriteBatch;

		// ---------- Methods ---------- 
		public void DisplayBox(string text, Vector2 position, Vector2 size, Color texColor, Color backgroundColour, bool wordWrapEnabled = false) {
			DisplayBox(position, size, backgroundColour);
			_spriteBatch.DrawText(text, position);
			
			// todo: somehow find the size of the font

			float fontWidth, fontHeight, // in pixels
                        lineSpacing; // as a scalar multiple of font height
			int widthCharacters = size.X / fontWidth;

			Vector2 textStartPosition;



			if (!wordWrapEnabled) {
				textStartPosition = position + new Vector2(CenterTextHorizontally(text.Length), CenterTextVertically(1));
			}
			else { // word wrap enabled
				string[] lines = TextHelper.WrapText(text, widthCharacters); // text split into lines
				int[] xOffsets = new int[lines.Length]; // the x offset of each line from the start of the x thingy
				
				for (int i = 0; i < lines.Length; i++) {
					xOffsets[i] = CenterTextHorizontally(lines[i].Length);
				}
				textStartPosition = position + new Vector2(position.X, CenterTextVertically(lines.Length));
			}
			
			// local functions
			float CenterTextHorizontally(int textLength) {
				return (size.X - textLength * fontWidth) / 2;
			}
                  float CenterTextVertically(int numberOfLines) {
                        return (size.Y - (numberOfLines + lineSpacing) * fontHeight) / 2;
                  }
		}
		public void DisplayBox(Vector2 position, Vector2 size, Color backgroundColour) {
			_spriteBatch.Draw(CreateRectangle(size, backgroundColour), position, backgroundColour);
		}
		public void DisplayText(string text, Vector2 position, Color texColor, Color backgroundColour = null);
		public void DisplayText(string text, Color texColor, Color backgroundColour = null);

		private Sprite CreateRectangle(Vector2 size, Color colour) {
			return new Sprite(size.X, size.Y, colour);
		}
	}
}
