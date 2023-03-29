using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class GuiElements {

	// ----- Properties -----
	public Texture2D HotbarSlot => _hotbarSlot;
	public Texture2D SelectedHotbarSlot => _selectedHotbarSlot;
	public Vector2 HotbarPosition => _hotbarPosition;

	// ----- Fields -----
	private readonly Texture2D _hotbarSlot;
	private readonly Texture2D _selectedHotbarSlot;
	private readonly Vector2 _hotbarPosition;

	// ----- Constructors -----
	public GuiElements(Texture2D hotbarSlot, Texture2D selectedHotbarSlot, Vector2 hotbarPosition) {
		_hotbarSlot = hotbarSlot;
		_selectedHotbarSlot = selectedHotbarSlot;
		_hotbarPosition = hotbarPosition;
	}
	
	// ----- Methods -----

}