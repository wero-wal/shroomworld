using System;
using System.Collections.Generic;
namespace Shroomworld {
    /// <summary>
    /// Used to generate and store a tilemap.
    /// </summary>
	internal class Map : IEnumerable<int, int> {
		// ----- Properties -----
        public int Width => _width;
		public int Height => _height;
		public BiomeDictionary Biomes => _biomes;
		public int this[int x, int y] {
			get { return _tiles[x, y]; }
			set { _tiles[x, y] = value; }
		}


		// ----- Fields -----
        public const int AirTile = 0;

        private readonly int[,] _tiles;
		private readonly BiomeDictionary _biomes;
        
        private readonly float _seed;


		// ----- Constructor -----
        public Map(int[,] tiles, BiomeDictionary biomes, float seed)
        {
            _tiles = tiles;
            _width = tiles.GetLength(0);
            _height = tiles.GetLength(1);
			_biomes = biomes;
            _seed = seed;
        }


		// ----- Methods -----

	}
}
