using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{ 
    public class PlayerProfile // exists outside of the world.
                               // Stores: custom player settings
                               // username, password
                               // note: store most recent user id in gamelog.txt
    {
        // ---------- Enums ----------


        // ---------- Properties ----------


        // ---------- Fields ----------
        private readonly int _id;
        private readonly string _userName;

        private string _password;
        private Texture2D[] _skins;
        //private Settings _settings;
        //private PlayerProperties _properties;
        private Dictionary<string, int>[] _statistics; // [worldId][statName] => or createee StatsDictionary.cs

        // ---------- Constructors ----------


        // ---------- Methods ----------
        private void CreateFolder()
        {
            System.IO.Directory.CreateDirectory($"Content/user{_id}/");
        }
    }
}
