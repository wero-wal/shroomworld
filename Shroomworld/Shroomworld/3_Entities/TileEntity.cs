using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class TileEntity : StaticSprite
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        public int Id { get => _id; }

        // ---------- Fields ----------
        private readonly int _id;

        private bool _placedByPlayer; // for statistical purposes

        // ---------- Constructors ----------
        public TileEntity(int id, Vector2 position) : base(MyGame.TileDictionary[id].Texture, position)
        {
            _id = id;
        }

        // ---------- Methods ----------
    }
}
