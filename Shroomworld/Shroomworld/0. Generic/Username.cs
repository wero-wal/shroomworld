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
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        private static MyLibrary.Range _allowedlength;

        private string _name;

        // ---------- Constructors ----------
        public Username()
        {
            _name = string.Empty;
        }

        // ---------- Methods ----------
        public static void SetMinAndMaxLength(int minLength, int maxLength)
        {
            if (minLength == maxLength)
            {
                throw new ArgumentException("Minimum and maximum username lengths must be different.");
            }
            if (minLength > maxLength)
            {
                (minLength, maxLength) = (maxLength, minLength);
            }
            if (_allowedlength == null) // This is true by default
            {
                _allowedlength = new MyLibrary.Range(minLength, maxLength, true);
            }
            else
            {
                throw new Exception("Minimum and maximum username lengths have already been set."); // useful for testing purposes
            }
        }


        public bool Update(string name)
        {
            if (_allowedlength.CheckIfInRange(name.Length))
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
