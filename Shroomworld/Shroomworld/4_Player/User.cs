using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{ 
    public class User
    {
        // ----- Enums -----


        // ----- Properties -----


        // ----- Fields -----
        private readonly int _id;
        private readonly string _username;

        private Texture2D[] _skins;
        //private Settings _settings;
        //private PlayerProperties _properties;
        private Dictionary<string, int>[] _statistics; // [worldId][statName]

        // ----- Constructors -----


        // ----- Methods -----
        private void CreateFolder()
        {
            System.IO.Directory.CreateDirectory($"Content/user{_id}/");
        }
    }
}
