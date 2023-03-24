using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class ButtonMenuDisplayHandler {

	// ----- Properties -----

	// ----- Fields -----
	private readonly Sprite _title;
	private readonly Vector2 _location; // Position of the top left corner of the first button in the menu.
	private Rectangle _menuBounds;
	private readonly float _distanceBetweenEachButton = 10;
	private readonly Dictionary<MenuButton.States, Texture2D> _buttonTextures;
	private readonly Dictionary<MenuButton.States, Color> _textColours;
	private readonly Color _backgroundColour;
	private readonly IDisplayHandler _displayHandler;


	// ----- Constructors -----
	public ButtonMenuDisplayHandler(Sprite title, Vector2 location, float distanceBetweenEachButton, int numberOfButtons,
	IDisplayHandler displayHandler, Texture2D normalButton, Texture2D highlightedButton, Texture2D pressedButton,
	Color normalTextColour, Color highlightedTextColour, Color pressedTextColour, Color backgroundColour) {
		_title = title;
		_location = location;
		_distanceBetweenEachButton = distanceBetweenEachButton;
		_displayHandler = displayHandler;
		_buttonTextures = new Dictionary<MenuButton.States, Texture2D> {
			{ MenuButton.States.Default, normalButton },
			{ MenuButton.States.Highlighted, highlightedButton },
			{ MenuButton.States.Pressed, pressedButton }
		};
		_textColours = new Dictionary<MenuButton.States, Color> {
			{ MenuButton.States.Default, normalTextColour },
			{ MenuButton.States.Highlighted, highlightedTextColour },
			{ MenuButton.States.Pressed, pressedTextColour }
		};
		_backgroundColour = backgroundColour;
		int width = _buttonTextures[MenuButton.States.Default].Width;
		int height = (int)((_buttonTextures[MenuButton.States.Default].Height + _distanceBetweenEachButton) * numberOfButtons);
		_menuBounds = new Rectangle(_location.ToPoint(), new Point(width, height));
	}
	

	// ----- Methods -----
	/// <summary>
	/// 	Create a <see cref="MenuButton"/> for each item in <paramref name="items"/>, calculating
	/// 	a position for each.
	/// </summary>
	public IEnumerable<MenuButton> ConvertToButtons(IEnumerable<string> items) {
		Vector2 position = _location;
		float buttonHeight = _buttonTextures[MenuButton.States.Default].Height;
		foreach (string item in items) {
			yield return new MenuButton(item, position);
			position.Y += buttonHeight + _distanceBetweenEachButton;
		}
	}
	public int GetIndexOfButtonContainingMouse(int defaultIndex) {
	  	// Check if mouse is outside the bounds of the menu.
		if (!MouseIsWithinMenuBounds()) {
			return defaultIndex;
		}
		// Calculate index
		int index = (int)(Input.MousePosition.Y / (_buttonTextures[MenuButton.States.Default].Height + _distanceBetweenEachButton));

		// Check mouse is in the gap between two buttons.
		float bottomOfButton = _location.Y + (_buttonTextures[MenuButton.States.Default].Height * index) + (_distanceBetweenEachButton * (index - 1));
		if (Input.MousePosition.Y > bottomOfButton) {
			return defaultIndex;
		}
		return index;
	}
	private bool MouseIsWithinMenuBounds() {
		return _menuBounds.Contains(Input.MousePosition);
	}
	public void Draw(MenuButton[] buttons) {
		_displayHandler.SetBackground(_backgroundColour);
		_displayHandler.Draw(_title);
		foreach (MenuButton button in buttons) {
			DrawButton(button);
		}
	}
	private void DrawButton(MenuButton button) {
		_displayHandler.Draw(_buttonTextures[button.State], button.Position);
		_displayHandler.DrawText(button.Text, button.Position, _textColours[button.State]);
	}
}