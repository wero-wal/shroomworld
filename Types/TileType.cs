using System;
using Microsoft.Xna.Framework.Graphics;
using Shroomworld.Drops;

namespace Shroomworld; 
public class TileType : IType {

    // ----- Properties -----
    public IdData IdData => _idData;
    public Texture2D Texture => _texture;
    public bool IsSolid => _isSolid;
    public int[] BreakableBy => _breakableBy;


	// ----- Fields -----
	public const int AirId = 0;
    public const int WaterId = 11;
    public const int ChestId = 23;

    private readonly IdData _idData;
    private readonly Maybe<IDroppable[]> _drops; // the IDs and mins and maxs of the items the tile can drop when broken
    private readonly bool _isSolid; // if is solid, entities can't pass through
    private readonly int[] _breakableBy; // IDs of the tools which can break this tile
    private readonly Texture2D _texture;


    // ----- Constructors -----
    public TileType(IdData idData, Texture2D texture, Maybe<IDroppable[]> drops, bool isSolid, int[] breakableBy) {
        _idData = idData;
        _texture = texture;
        _drops = drops;
        _isSolid = isSolid;
        _breakableBy = breakableBy;
    }

    // ----- Methods -----
    public InventoryItem[] GetDrops() {
        if (!_drops.TryGetValue(out IDroppable[] drops)) {
            return Array.Empty<InventoryItem>();
        }
        InventoryItem[] items = new InventoryItem[drops.Length];
        for (int i = 0; i < drops.Length; i++) {
            items[i] = drops[i].Drop();
        }
        return items;
    }
}
