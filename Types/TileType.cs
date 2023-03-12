using System;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld; 
public class TileType : IType {

    // ----- Properties -----
	public static int AirId => _airId;
	public static int WaterId => _waterId;
	public static int ChestId => _chestId;

    public IdData IdData => _idData;
    public Texture2D Texture => _texture;


	// ----- Fields -----
	private const int _airId = 0;
    private const int _waterId = 11;
    private const int _chestId = 23;

    private readonly IdData _idData;
    private readonly Maybe<Drop[]> _drops; // the IDs and mins and maxs of the items the tile can drop when broken
    private readonly bool _isSolid; // if is solid, entities can't pass through
    private readonly int[] _breakableBy; // IDs of the tools which can break this tile
    private readonly float _friction;
    private readonly Texture2D _texture;


    // ----- Constructors -----
    public TileType(IdData idData, Texture2D texture, Maybe<Drop[]> drops, bool isSolid, int[] breakableBy, float friction)
    {
        _idData = idData;
        _texture = texture;
        _drops = drops;
        _isSolid = isSolid;
        _breakableBy = breakableBy;
        _friction = friction;
    }

    // ----- Methods -----
    public static implicit operator TileType(int id) {
        return Shroomworld.TileTypes[id];
    }
    public InventoryItem[] GetDrops()
    {
        if (!_drops.TryGetValue(out Drop[] drops)) {
            return Array.Empty<InventoryItem>();
        }
        return Array.ConvertAll(drops, drop => drop.DropItem());
    }
}
