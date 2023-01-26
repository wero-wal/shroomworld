using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class TileType : IType
    {
        // ----- Enums -----
        // ----- Properties -----
        public IdentifyingData IdData => _idData;

        // ----- Fields -----
        private IdentifyingData _idData;
        private List<Drop> _drops; // the IDs and mins and maxs of the items the tile can drop when broken
        private bool _isSolid; // if is solid, entities can't pass through
        private int[] _breakableBy; // IDs of the tools which can break this tile
        private float _friction;

        // ----- Constructors -----
        public TileType(IdentifyingData idData, List<Drop> drops, bool isSolid, int[] breakableBy, float friction)
        {
            _idData = idData;
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
            for(int i = 0; i < _drops.Count; i++)
            {
                yield return _drops[i].DropItem();
            }
        }
    }
}
