using System.Linq;

namespace Shroomworld;
public class ButtonMenu {

    // ----- Fields -----
	private const int DefaultIndex = -1;
    private readonly string _name;
    private readonly MenuButton[] _buttons;
    private readonly Shroomworld.GameStates[] _outcomes;
    private readonly ButtonMenuDisplayHandler _menuDisplayHandler;
	private int _indexOfPreviouslyHighlightedButton;
	private int _indexOfHighlightedButton = DefaultIndex;


	// ----- Constructors -----
	/// <summary>
	/// Create new instance of <see cref="ButtonMenu"/> and calculate button positions.
	/// </summary>
	public ButtonMenu(string name, string[] items, Shroomworld.GameStates[] outcomes, ButtonMenuDisplayHandler menuDisplayHandler) {
        _name = name;
        _menuDisplayHandler = menuDisplayHandler;
        _buttons = _menuDisplayHandler.ConvertToButtons(items).ToArray();
        _outcomes = outcomes;
    }


    // ----- Methods -----
    /// <summary>
    /// Display all the buttons in the menu. They cannot be interacted with yet.
    /// </summary>
    public void Draw() {
        _menuDisplayHandler.Draw(_buttons);
    }
	/// <summary>
	///     Allows the user to select a button by releasing the left mouse button on it.
	/// </summary>
    /// <param name="defaultGameState">The <see cref="Shroomworld.GameStates"/> to return if no button is selected.</param>
	/// <returns>
    ///     The item in <see cref="_outcomes"/> corresponding to the button pressed if a button has
    ///     been selected; otherwise <paramref name="defaultGameState"/>.
    /// </returns>
	public Shroomworld.GameStates Update(Shroomworld.GameStates defaultGameState) {
        // Update user inputs.
        _indexOfPreviouslyHighlightedButton = _indexOfHighlightedButton;
        bool mouseHasBeenReleased = Input.HasBeenReleased(Input.Inputs.LeftMouseButton);
        bool leftMouseDown = Input.CurrentInputs.Contains(Input.Inputs.LeftMouseButton);

        // Get index of the button containing the mouse.
        _indexOfHighlightedButton = _menuDisplayHandler.GetIndexOfButtonContainingMouse(DefaultIndex);

        // Update button states accordingly.
        if (_indexOfPreviouslyHighlightedButton != DefaultIndex) {
            _buttons[_indexOfPreviouslyHighlightedButton].State = MenuButton.States.Default;
        }
        if (_indexOfHighlightedButton == DefaultIndex) {
            // No button has been highlighted.
            return defaultGameState;
        }
        if(mouseHasBeenReleased) {
            // User has selected a menu item.
            return _outcomes[_indexOfHighlightedButton];
        }
        if (leftMouseDown) {
            // Button is being pressed.
            _buttons[_indexOfHighlightedButton].State = MenuButton.States.Pressed;
            return defaultGameState;
        }
        // Button is being highlighted.
        _buttons[_indexOfHighlightedButton].State = MenuButton.States.Highlighted;
        return defaultGameState;
    }
}