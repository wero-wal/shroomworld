using System;
using System.Collections.Generic;
namespace Shroomworld
{
	internal class Map : IEnumerable<int, int>
	{
		// ----- Properties -----
        public int Width => _width;
		public int Height => _height;
		public BiomeDictionary Biomes => _biomes;
		public int this[int x, int y]
		{
			get
			{
				return _tiles[x, y];
			}
			set
			{
				_tiles[x, y] = value;
			}
		}

		// ----- Fields -----
        private const bool _GROUND = true;
        private const bool _AIR = false;

        private const int _numberOfAirAtTop = 2; // (min) number of empty tiles at the top of the map
        private const int _numberOfTilesAtBottom = 1; // (min) number of solid tiles at the bottom of the map
        private const float _surfacePercent = 0.33f; // percentage of the map (vertically) that makes up the surface (height of the perlin wave)
        private const int _smoothAmount = 2;
    	private const int _randomFillPercent = 55; // what % of the underground will be ground (the rest will be air)


        private readonly int[,] _tiles;
		private readonly BiomeDictionary _biomes;
        private readonly int _width;
        private readonly int _height;

		private readonly bool[,] _untexturedMap;
        private readonly int[] _surfaceHeights; // stores top of terrain at each x
        
        private readonly float _seed;
        private readonly float _smoothness; // how smooth or 'janky' the terrain looks
        private readonly int _numberOfBiomes;
        private readonly int[] _layerBoundaries = { 0, 1, 3, 0 };

		// ----- Constructor -----
		/// <summary>
		/// Use this when generating a new map
		/// </summary>
        public Map(int width, int height, int numberOfBiomes, float smoothness, float? seed = null)
        {
			_tiles = new int[width, height];
            _untexturedMap = GetEmptyMap(width, height);
            _surfaceHeights = new int[width];
            _layerBoundaries[^1] = height;

            _width = width;
            _height = height;
			_numberOfBiomes = numberOfBiomes;
			_biomes = new BiomeDictionary(numberOfBiomes);

            _smoothness = smoothness;
            _seed = seed ?? (float)new Random().NextDouble();
        }
		/// <summary>
		/// Use this for instantiating an existing map
		/// </summary>
        public Map(int[,] tiles, BiomeDictionary biomes, float seed)
        {
            _tiles = tiles;
            _width = tiles.GetLength(0);
            _height = tiles.GetLength(1);
			_biomes = biomes;

            _seed = seed;
            _surfaceHeights = new int[width]; // todo: get surface heights
        }


		// ----- Methods -----
        // Generate surface
        public void Generate()
        {
            GenerateSurfaceTerrain();
            GenerateCaves();
            AddBiomes();
            PaintTiles();
        }
		
		// Generate surface
        private void GenerateSurfaceTerrain()
        {
            int waveHeight = (int)Math.Round(_height * _surfacePercent);

            Perlin perlin = new Perlin();
            int y;
            for (int x = 0; x < _width; x++)
            {
                y = _numberOfAirAtTop + (int)Math.Round(perlin.OctavePerlin(x / _smoothness, _seed, 0, 6, _smoothness) * waveHeight);
                _surfaceHeights[x] = y;
                _untexturedMap[x, y] = _GROUND;
            }
        }

        // Generate caves
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smoothAmount">How many times to smooth the caves. Smaller amount = bigger caves + vice versa.</param>
        private void GenerateCaves()
        {
            Random random = new Random(seed.GetHashCode());

            // Generate noise
            for (int x = 0; x < _width; x++)
            {
                for (int y = _surfaceHeights[x]; y < (_height - _numberOfTilesAtBottom); y++)
                {
                    _untexturedMap[x, y] = random.Next(0, 100) < _randomFillPercent;
                }
            }

            // Smooth
            for (int i = 0; i < _smoothAmount; i++)
            {
                SmoothMap();
            }
        }
        private void SmoothMap()
        {
            int surroundingGroundCount;
            const int MAX_NEIGHBOURS = 4; // Moore’s Neighbourhood: max = 8. Von Neumann’s: max = 4.

			for (int x = 0; x < _width; x++)
			{
				for (int y = _surfaceHeights[x]; y < _height; y++)
				{
					// create border on the edges
					if ((x == 0) || (x == (_width - 1)) || (y == _surfaceHeights[x]) || (y >= (_height - _numberOfTilesAtBottom)))
					{
						_untexturedMap[x, y] = _GROUND;
					}
					else // smooth based on neighbouring tiles
					{
						surroundingGroundCount = GetSurroundingGroundCount(x, y);
						if (surroundingGroundCount > (MAX_NEIGHBOURS / 2)) // if surrounded by > 2 ground tiles, become a ground tile.
						{
							_untexturedMap[x, y] = _GROUND;
						}
						else if (surroundingGroundCount < (MAX_NEIGHBOURS / 2)) // if surrounded by < 2 ground tiles, become an air tile.
						{
							_untexturedMap[x, y] = _AIR;
						}
					}
				}
			}
        }
        private int GetSurroundingGroundCount(int x, int y)
        {
            /* using the Von Neumann Neighbourhood:
            * . n .
            * n t n
            * . n .
            * (n = neighbour    t = current tile    . = other tile)
            */

            int surroundingGroundCount = 0;

            // check to the left and right of the tile
            for (int nebx = (x - 1); nebx <= (x + 1); nebx += 2) // nebx = x-index of neighbour
            {
                AddToGroundCount(nebx, y);
            }

            // check above and below the tile
            for (int neby = (y - 1); neby <= (y + 1); neby += 2) // neby = y-index of neighbour
            {
                AddToGroundCount(x, neby);
            }

            return surroundingGroundCount;

            // --- Local functions ---
            void AddToGroundCount(int neighbourX, int neighbourY)
            {
                if (CheckWithinBounds(neighbourX, neighbourY))
                {
                    surroundingGroundCount += (_untexturedMap[neighbourX, neighbourY] ? 1 : 0); // if neighbour contains a tile, add 1
                }
            }
            bool CheckWithinBounds(int x, int y)
            {
                return ((x >= 0) && (x < _width) && (y >= 0) && (y < _height));
            }
        }

        // Texturing
        private void AddBiomes()
        {
            int biomeSize = _width / numberOfBiomes;
            Random random = new Random(seed.GetHashCode());
            for (int i = 0; i < _numberOfBiomes; i++)
            {
                _biomes.Add(i * biomeSize, Shroomworld.BiomeTypes[random.Next(0, Shroomworld.BiomeTypes.Count)]);
            }
        }
        private void PaintTiles()
        {
            int startOfcurrentLayer;
            int endOfcurrentLayer;
            for (int x = 0; x < _width; x++)
            {
                startOfcurrentLayer = _surfaceHeights[x];
                for (int i = 1; i < _layerBoundaries.Length; i++)
                {
                    startOfcurrentLayer = _surfaceHeights[x] + _layerBoundaries[i - 1];
                    endOfcurrentLayer = Math.Min(_surfaceHeights[x] + _layerBoundaries[i], _height);
                    for (int y = startOfcurrentLayer; y < endOfcurrentLayer; y++)
                    {
                        if (_untexturedMap[x, y] == _GROUND)
                        {
                            _tiles[x, y] = _biomes[x].Tiles[i - 1];
                        }
                    }                        
                }
            }
        }

        // Clearing map
        private bool[,] GetEmptyMap(int width, int height)
        {
            bool[,] map = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = AIR;
                }
            }
            return map;
        }
	}
}
