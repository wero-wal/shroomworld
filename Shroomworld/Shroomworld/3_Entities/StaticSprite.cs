using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class StaticSprite : Sprite // Uses: backgrounds, tiles, menu boxes
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        public bool Solid { get => _solid; }

        // ---------- Fields ----------
        private readonly bool _solid;

        // ---------- Constructors ----------
        public StaticSprite(Texture2D texture, bool solid) : base(texture)
        {
            _solid = solid;
        }
        public StaticSprite(Texture2D texture, Vector2 position, bool solid) : base(texture, position)
        {
            _solid = solid;
        }

        // ---------- Methods ----------
    }
}
