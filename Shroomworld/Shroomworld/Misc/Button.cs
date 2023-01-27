using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shroomworld
{
	internal class Button
	{
		public string Text => _text;
		public string Sprite => _sprite;

		private readonly string _text;
		private readonly Sprite _sprite;


		public void Draw(Color buttonColour, Color textColour)
		{
			_sprite.Draw(buttonColour);
			Shroomworld.SpriteFont.Draw(textColour); // check: is this correct
		}
		// public bool CheckIfPressed(MouseState mouseState)
		// {
		// 	// todo: write code to check if mouse is pressed and is on the button
		// }
		// public bool CheckIfBeingHoveredOver()
		// {

		// }
	}
}
