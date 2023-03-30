using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;
public class ButtonMenuDisplayHandler {

	// ----- Properties -----

	// ----- Fields -----
	private readonly float _distanceBetweenEachButton = 1;
	private readonly Dictionary<MenuButton.States, Texture2D> _buttonTextures;
	private readonly Dictionary<MenuButton.States, Color> _textColours;
	private readonly Color _backgroundColour;
	private readonly IDisplayHandler _displayHandler;
	private readonly Point _buttonSize;

	// ----- Constructors -----
	public ButtonMenuDisplayHandler(float distanceBetweenEachButton, IDisplayHandler displayHandler,
	Texture2D normalButton, Texture2D highlightedButton, Texture2D pressedButton,
	Color normalTextColour, Color highlightedTextColour, Color pressedTextColour, Color backgroundColour) {

		_displayHandler = displayHandler;

		// Textures:
		_buttonTextures = new Dictionary<MenuButton.States, Texture2D> {
			{ MenuButton.States.Default, normalButton },
			{ MenuButton.States.Highlighted, highlightedButton },
			{ MenuButton.States.Pressed, pressedButton }
		};
		// Colours:
		_textColours = new Dictionary<MenuButton.States, Color> {
			{ MenuButton.States.Default, normalTextColour },
			{ MenuButton.States.Highlighted, highlightedTextColour },
			{ MenuButton.States.Pressed, pressedTextColour }
		};
		_backgroundColour = backgroundColour;

		// Bounds, locations, and sizes:
		_distanceBetweenEachButton = distanceBetweenEachButton;
		_buttonSize = displayHandler.GetSizeInTiles(_buttonTextures[MenuButton.States.Default]);
	}
	

	// ----- Methods -----
	/// <summary>
	/// 	Create a <see cref="MenuButton"/> for each item in <paramref name="items"/>, calculating
	/// 	a position for each.
	/// </summary>
	public IEnumerable<MenuButton> ConvertToButtons(IEnumerable<string> items, Vector2 location) {
		Vector2 position = location;
		foreach (string item in items) {
			yield return new MenuButton(item, position);
			position.Y += _buttonSize.Y + _distanceBetweenEachButton;
		}
	}
	public int GetIndexOfButtonContainingMouse(int defaultIndex, Rectangle menuBounds) {
	  	// Check if mouse is outside the bounds of the menu.
		if (!menuBounds.Contains(_displayHandler.MousePosition)) {
			return defaultIndex;
		}
		// Calculate index
		int index = (int)((Input.MousePosition.Y - menuBounds.Location.Y) / (_buttonSize.Y + _distanceBetweenEachButton));

		// Check mouse is in the gap between two buttons.
		float bottomOfButton = location.Y + (_buttonSize.Y * index) + (_distanceBetweenEachButton * (index - 1));
		if (_displayHandler.MousePosition.Y > bottomOfButton) {
			return defaultIndex;
		}
		return index;
	}
	public void Draw(MenuButton[] buttons, Sprite title) {
		_displayHandler.SetBackground(_backgroundColour);
		_displayHandler.DrawSprite(title);
		foreach (MenuButton button in buttons) {
			DrawButton(button);
		}
	}
	public Rectangle CalculateMenuBounds(Point location, int numberOfButtons) {
		int height = (int)((_buttonSize.Y + _distanceBetweenEachButton) * numberOfButtons);
		return new Rectangle(location, new Point(_buttonSize.X, height));
	}
	private void DrawButton(MenuButton button) {
		_displayHandler.Draw(_buttonTextures[button.State], button.Position);
		_displayHandler.DrawText(button.Text, button.Position, _textColours[button.State]);
	}
}