using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class TileType : Type
    {
        // ----- Enums -----
        // ----- Properties -----
        // ----- Fields -----
        private bool _isSolid; // if is solid, entities can't pass through
        private List<Drop> _drops;

        // ----- Constructors -----
        public TileType(string plainText)
        {
            string[] parts = plainText.Split(File.Separators[File.Level.i]);
            int i = 0;
            _id = Convert.ToInt32(parts[i++]);
            _name = parts[i++];
            _pluralName = parts[i++];
            ParseDrops(parts[i++], ++File.Primary);
        }

        // ----- Methods -----
        public Sprite GetSprite()
        {
            return new StaticSprite(File.LoadTexture(File.TileDirectory + _id), _isSolid);
        }

        private void ParseDrops(string plainText, int separatorLevel)
        {
            string[] parts = plainText.Split(File.Separators[separatorLevel]);
            foreach(string part in parts)
            {
                _drops.Add(new Drop(part, ++separatorLevel));
            }
        }
    }
}
