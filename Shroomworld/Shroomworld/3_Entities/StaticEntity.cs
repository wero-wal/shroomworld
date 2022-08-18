using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class StaticEntity : Entity // Uses: backgrounds, tiles, menu boxes
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        // ---------- Fields ----------
        // ---------- Constructors ----------
        public StaticEntity(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        // ---------- Methods ----------
        public void SetPosition(float x, float y)
        {
            _position.X = x;
            _position.Y = y;
        }
        public void SetPosition(Vector2 vector)
        {
            _position = vector;
        }
    }
}
