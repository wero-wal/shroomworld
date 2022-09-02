using Shroomworld._2_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class NpcType : EntityType
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        // ---------- Fields ----------
        private Quest _quest;

        // ---------- Constructors ----------
        // ---------- Methods ----------
        public override Sprite GetSprite()
        {
            return new MoveableSprite(File.LoadTexture(File.NpcDirectory, _id), _movementSpeed, _constantOfRestitution);
        }
    }
}
