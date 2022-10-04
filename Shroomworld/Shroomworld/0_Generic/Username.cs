using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLibrary;

namespace Shroomworld
{
    public struct Username
    {
        // ----- Enums -----


        // ----- Properties -----
        public string Name { get => _name; set => _name = value; }

        // ----- Fields -----
        private static Range _allowedlength;

        private string _name;

        // ----- Constructors -----
        public Username()
        {
            _name = string.Empty;
        }

        // ----- Methods -----
        public static void SetMinAndMaxLength(byte minLength, byte maxLength)
        {
            if (_allowedlength == null) // This is true by default
            {
                _allowedlength = new Range(minLength, maxLength, true);
            }
            else
            {
                throw new Exception("Minimum and maximum username lengths have already been set."); // useful for testing purposes
            }
        }

        public bool Update(string name)
        {
            if (_allowedlength.CheckIfInRange((byte)name.Length))
            {
                _name = name;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
