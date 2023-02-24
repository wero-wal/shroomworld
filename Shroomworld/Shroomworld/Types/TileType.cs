using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    internal class TileType : IType
    {
        // ----- Enums -----
        // ----- Properties -----
        public IdentifyingData IdData => _idData;
        public Texture2D Texture => _texture;

        // ----- Fields -----
        private readonly IdentifyingData _idData;
        private readonly Drop[] _drops; // the IDs and mins and maxs of the items the tile can drop when broken
        private readonly bool _isSolid; // if is solid, entities can't pass through
        private readonly int[] _breakableBy; // IDs of the tools which can break this tile
        private readonly float _friction;
        private readonly Texture2D _texture;

        // ----- Constructors -----
        public TileType(IdentifyingData idData, Texture2D texture, Drop[] drops, bool isSolid, int[] breakableBy, float friction)
        {
            _idData = idData;
            _texture = texture;
            _drops = drops;
            _isSolid = isSolid;
            _breakableBy = breakableBy;
            _friction = friction;
        }

        // ----- Methods -----
        public InventoryItem[] GetDrops()
        {
            for(int i = 0; i < _drops.Length; i++)
            {
                yield return _drops[i].DropItem();
            }
        }
    }
}
