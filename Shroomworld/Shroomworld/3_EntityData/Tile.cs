using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
    public class Tile
    {
        // ----- Enums -----
        // ----- Properties -----
        public Sprite Sprite { get => _sprite; }

        // ----- Fields -----
        private readonly int _tileType;

        private Sprite _sprite;
        private bool _placedByPlayer; // for statistical purposes. If not placed by player, the resources it drops will count towards stats when broken. otherwise, no.

        // ----- Constructors -----
        public Tile(string plainText)
        {
            string[] parts = plainText.Split(FileManager.Separators[FileManager.Level.ii]);
            int i = 0;
            _tileType = TileType.Dictionary[Convert.ToInt32(parts[i++])];
            _sprite = _tileType.GenerateSprite();
            _placedByPlayer = Convert.ToBoolean(Convert.ToInt32(parts[i++]));
        }
        private Tile(int id)
        {
            _tileType = TileType.Dictionary[id];
            _sprite = _tileType.GenerateSprite();
            _placedByPlayer = false;
        }

        // ----- Methods -----
        public static Tile CreateNew(int id)
        {
            return new Tile(id);
        }

        public string ToString()
        {
            return FileManager.FormatAsPlainText(_tileType.Id, Convert.ToInt32(_placedByPlayer), separator: FileManager.Level.ii);
        }
    }
}
