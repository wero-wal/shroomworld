using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Type
    {
        public string Name { get => _name; }
        public string PluralName { get => _pluralName; }

        protected readonly int _id;
        protected readonly string _name;
        protected readonly Texture _texture;
        protected readonly string _pluralName;

        public void Display()
        {
            //TODO
        }

        public string GetKey()
        {
            return $"{typeof(this)}:{_id}";
        }
    }
}
