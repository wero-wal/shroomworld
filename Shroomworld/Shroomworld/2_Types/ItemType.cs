using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class ItemType
    {
        private readonly int _id;
        private readonly string _name;
        private readonly string _pluralName;
        private readonly bool _canBePlaced;
        private readonly bool _stackable;

        public ItemType(int id, string name, string pluralName, bool canBePlaced, bool stackable)
        {
            _id = id;
            _name = name;
            _pluralName = pluralName;
            _canBePlaced = canBePlaced;
            _stackable = stackable;
        }
    }
}
