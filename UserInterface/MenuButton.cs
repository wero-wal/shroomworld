using System.ComponentModel;
using Microsoft.Xna.Framework;

namespace Shroomworld;
public class MenuButton {

	// ----- Enums -----
	[DefaultValue(Default)]
	public enum States
	{
		Default, // The button is in its default, untouched state.
		Highlighted, // The mouse is hovering over the button.
		Pressed, // The mouse is hovering over the button and the left mouse button is down.
	}

	// ----- Properties -----
	public string Text => _text;
	public Vector2 Position => _position;
	public States State { get => _state; set => _state = value; }

	// ----- Fields -----
	private readonly string _text;
	private readonly Vector2 _position;
	private States _state;

	// ----- Constructors -----
	public MenuButton(string text, Vector2 position) {
		_text = text;
		_position = position;
		_state = default;
	}
}