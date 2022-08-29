using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public abstract class Type : IType
    {
        // ---------- Properties ----------
        public static Dictionary<int, Type> Dictionary { get => _dictionary; }

        public int Id { get => _id; }
        public sealed string FullId { get => $"{this.GetType().ToString().Split('.')[1]}{_id}"; }
        public string Name { get => _name; }
        public string PluralName { get => _pluralName; }

        // ---------- Fields ----------
        protected static Dictionary<int, Type> _dictionary;

        protected readonly int _id;
        protected readonly string _name;
        protected readonly string _type;
        protected readonly string _pluralName;


        // ---------- Constructors ----------
        public abstract Type(string plainText); // must add to dictionary here

        // ---------- Methods ----------
        public abstract Sprite GetSprite();

        protected sealed void ParseNamesAndId(ref int index, string[] parts)
        {
            _id = Convert.ToInt32(parts[index++]);
            _name = parts[index++];
            _pluralName = parts[index++];
        }
        protected sealed void AddToDictionary()
        {
            _dictionary.Add(_id, this);
        }
    }
}
