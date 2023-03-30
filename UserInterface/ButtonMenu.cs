using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Shroomworld;
public class ButtonMenu {

    // ----- Fields -----
	private const int DefaultIndex = -1;

    private static ButtonMenuDisplayHandler _menuDisplayHandler;

    private readonly string _name;
    private readonly Sprite _title;
    private readonly MenuButton[] _buttons;
    private readonly Action[] _actions;
	private readonly Vector2 _location; // Position of the top left corner of the first button in the menu.
	private readonly Rectangle _menuBounds;

	private int _indexOfPreviouslyHighlightedButton = DefaultIndex;
	private int _indexOfHighlightedButton = DefaultIndex;


	// ----- Constructors -----
	/// <summary>
	/// Create new instance of <see cref="ButtonMenu"/> and calculate button positions.
	/// </summary>
	public ButtonMenu(string name, Sprite title, string[] items, Vector2 location, Action[] actions) {
        _name = name;
        _buttons = _menuDisplayHandler.ConvertToButtons(items, location).ToArray();
        _actions = actions;
        _menuBounds = _menuDisplayHandler.CalculateMenuBounds(location.ToPoint(), items.Length);
    }


    // ----- Methods -----
    public static void SetDisplayHandler(ButtonMenuDisplayHandler menuDisplayHandler) {
        _menuDisplayHandler = menuDisplayHandler;
    }

    /// <summary>
    /// Display all the buttons in the menu. They cannot be interacted with yet.
    /// </summary>
    public void Draw() {
        _menuDisplayHandler.Draw(_buttons, _title);
    }
	/// <summary>
	///     Allows the user to select a button by releasing the left mouse button on it.
    ///     <para>
    ///         Calls the item in <see cref="_actions"/> corresponding to the button pressed if a button has
    ///         been selected; otherwise calls <paramref name="defaultAction"/>.
    ///     </para>
	/// </summary>
    /// <param name="defaultAction">The action to perform if no button is selected.</param>
	public void Update() {
        // Update inputs.
        _indexOfPreviouslyHighlightedButton = _indexOfHighlightedButton;
        bool leftMouseButtonHasBeenReleased = Input.HasBeenReleased(Input.Inputs.LeftMouseButton);
        bool leftMouseDown = Input.CurrentInputs.Contains(Input.Inputs.LeftMouseButton);

        _indexOfHighlightedButton = _menuDisplayHandler.GetIndexOfButtonContainingMouse(DefaultIndex, _menuBounds);

        // Update button states accordingly.
        // 1. Un-press previously pressed button.
        if (_indexOfPreviouslyHighlightedButton != DefaultIndex) {
            _buttons[_indexOfPreviouslyHighlightedButton].State = MenuButton.States.Default;
        }
        // 2. Check if a button is highlighted.
        if (_indexOfHighlightedButton == DefaultIndex) { return; }
        else {
            _buttons[_indexOfHighlightedButton].State = MenuButton.States.Highlighted;
        }
        // 3. Check if user has selected a button (button is being released).
        if(leftMouseButtonHasBeenReleased) {
            _actions[_indexOfHighlightedButton]();
            return;
        }
        // 4. Check if button is pressed.
        if (leftMouseDown) {
            _buttons[_indexOfHighlightedButton].State = MenuButton.States.Pressed;
        }
    }
}