﻿using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld;

public class ItemType : IType {

    // ----- Properties -----
    public IdData IdData => _idData;
    public Maybe<ToolData> ToolData => _toolData;
    public Maybe<int> Tile => _tile;
    public Texture2D Texture => _texture;

    // ----- FIelds -----
    private readonly IdData _idData;
    private readonly Texture2D _texture;
    private readonly Maybe<ToolData> _toolData;
    private readonly Maybe<int> _tile;
    private readonly bool _stackable;

    // ----- Constructors -----
    /// <summary>
    /// Constructor for non-tool items
    /// </summary>
    public ItemType(IdData idData, Texture2D texture, bool stackable, Maybe<int> tileType) {
        _idData = idData;
        _stackable = stackable;
        _toolData = Maybe.None;
        _tile = tileType;
    }
    /// <summary>
    /// Constructor for tool items
    /// </summary>
    public ItemType(IdData idData, Texture2D texture, ToolData toolData) {
        _idData = idData;
        _toolData = toolData;
        _stackable = false;
    }
}
