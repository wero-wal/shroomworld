using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    internal class TileType : IType
    {
        // ----- Enums -----
        // ----- Properties -----
        public IdentifyingData IdData => _idData;

        // ----- Fields -----
        private readonly IdentifyingData _idData;
        private readonly Drop[] _drops; // the IDs and mins and maxs of the items the tile can drop when broken
        private readonly bool _isSolid; // if is solid, entities can't pass through
        private readonly int[] _breakableBy; // IDs of the tools which can break this tile
        private readonly float _friction;
        private readonly Texture _texture;

        // ----- Constructors -----
        public TileType(IdentifyingData idData, Texture texture, Drop[] drops, bool isSolid, int[] breakableBy, float friction)
        {
            _idData = idData;
            _texture = texture;
            _drops = drops;
            _isSolid = isSolid;
            _breakableBy = breakableBy;
            _friction = friction;
        }

        // ----- Methods -----
        public Sprite GetSprite()
        {
            return new StaticSprite(FileManager.LoadTexture(Directories.Textures.Tiles, _id.ToString()), _isSolid);
        }
        public InventoryItem[] GetDrops()
        {
            for(int i = 0; i < _drops.Length; i++)
            {
                yield return _drops[i].DropItem();
            }
        }
    }
}
