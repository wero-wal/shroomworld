using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class TileEntity : StaticEntity
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        public int Id { get => _id; }

        // ---------- Fields ----------
        private readonly int _id;

        // ---------- Constructors ----------
        public TileEntity(int id, Vector2 position) : base(MyGame.TileDictionary[id].Texture, position)
        {
        }

        // ---------- Methods ----------
    }
}
