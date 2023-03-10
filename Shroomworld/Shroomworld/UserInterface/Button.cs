using System;
using Microsoft.Xna.Framework;

namespace Shroomworld;

public class Button {
	// ----- Properties -----
	public string Text => _text;
	public Sprite Sprite => _sprite;

	// ----- Fields -----
	private static Action<string, Vector2, Color> _drawText;

	private readonly string _text;
	private readonly Sprite _sprite;

	// ----- Constructors -----
	public Button(string text, Sprite sprite) {
		_text = text;
		_sprite = sprite;
	}

	// ----- Methods -----
	public static void InjectDependencies(Action<string, Vector2, Color> drawTextFunction) {
		_drawText = drawTextFunction;
	}

	public void Draw(Color buttonColour, Color textColour) {
		_sprite.Draw(buttonColour);
		_drawText(_text, _sprite.Position, textColour);
	}
	// public bool CheckIfPressed(MouseState mouseState)
	// {
	// 	// todo: write code to check if mouse is pressed and is on the button
	// }
	// public bool CheckIfBeingHoveredOver()
	// {

	// }
}
