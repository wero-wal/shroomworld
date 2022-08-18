using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public abstract class XType
    {
        // ---------- Enums ----------
        // ---------- Properties ----------
        public static Dictionary<int, XType> Dictionary { get => _dictionary; }
        public static string[] FullIds { get => _fullIds; }


        public string Name { get => _name; }
        public string PluralName { get => _pluralName; }

        // ---------- Fields ----------
        protected static Dictionary<int, XType> _dictionary;
        protected static string[] _fullIds;


        protected readonly string
            _name,
            _type,
            _pluralName;
        protected readonly int
            _id;
        protected readonly Texture2D
            _texture;

        // ---------- Constructors ----------
        public XType()
        {

        }
        public XType(string fileText)
        {
            ParseFileText(fileText);
            _dictionary.Add(_id, this);
            _fullIds.Add(GetFullId());
        }

        // ---------- Methods ----------
        public string GetFullId()
        {
            return $"{_type}:{_name}";
        }
        public Entity GenerateEntity()
        {
            return new Entity(); // customise for each type
        }
        protected XType ParseFileText(string fileText)
        {

        }
    }
}
