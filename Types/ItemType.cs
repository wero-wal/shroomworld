namespace Shroomworld;

public class ItemType : IType {

    // ----- Properties -----
    public IdData IdData => _idData;

    // ----- FIelds -----
    private readonly IdData _idData;
    private readonly Maybe<ToolData> _toolData;
    private readonly bool _stackable;
    private readonly bool _placeable;

    // ----- Constructors -----
    /// <summary>
    /// Constructor for non-tool items
    /// </summary>
    public ItemType(IdData idData, bool stackable, bool placeable) {
        _idData = idData;
        _stackable = stackable;
        _placeable = placeable;
        _toolData = Maybe.None;
    }
    /// <summary>
    /// Constructor for tool items
    /// </summary>
    public ItemType(IdData idData, ToolData toolData) {
        _idData = idData;
        _toolData = toolData;
        _stackable = false;
        _placeable = false;
    }
}
