using System;
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

        public string FullId { get => _fullId; }
        public string Name { get => _name; }
        public string PluralName { get => _pluralName; }

        // ---------- Fields ----------
        protected static Dictionary<int, XType> _dictionary;

        protected readonly string _name;
        protected readonly string _type;
        protected readonly string _pluralName;
        protected readonly int _id;
        protected readonly Texture2D _texture;

        protected string _fullId;

        // ---------- Constructors ----------
        public XType()
        {

        }
        public XType(string fileText)
        {
            ParseFileText(fileText);
            _dictionary.Add(_id, this);
            _fullId = GetFullId();
        }

        // ---------- Methods ----------
        public string GetFullId()
        {
            return $"{_type}:{_name}";
        }
        protected void ParseFileText(string fileText)
        {

        }
    }
}
