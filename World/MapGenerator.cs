using System;
namespace Shroomworld {
	internal class MapGenerator {

		// ----- Fields -----
        private const bool Ground = true;
        private const bool Air = false;

        private readonly int _minCaveDepth = 5;
        private readonly int _topOffset = 0; // (min) number of empty tiles at the top of the map
        private readonly int _numberOfTilesAtBottom = 1; // (min) number of solid tiles at the bottom of the map
        private readonly int _surfacePercent = 27; // percentage of the map (vertically) that makes up the surface (height of the perlin wave)
        private readonly int _smoothAmount = 2; // how many times to smooth the cave
    	private readonly int _randomFillPercent = 54; // what % of the underground will be ground (the rest will be air)

        private readonly int[,] _tiles;
		private readonly BiomeDictionary _biomes;
        private readonly int _width;
        private readonly int _height;

		private readonly bool[,] _groundMap;
        private readonly int[] _surfaceHeights; // stores top of terrain at each x
        
        private readonly int _seed;
        private readonly int _numberOfBiomes;
        private readonly int[] _layerWidths = { 1, 0, 0 };

        private readonly Random _random;

        private int _surfaceWaveHeight;

		// ----- Constructor -----
		/// <summary>
		/// Use this when generating a new map
		/// </summary>
        public MapGenerator(int width, int height, int numberOfBiomes, int? seed = null) {
            _width = width;
            _height = height;
			_numberOfBiomes = numberOfBiomes;
			_biomes = new BiomeDictionary(numberOfBiomes, width);

            _seed = seed ?? new Random().Next();

            _tiles = GetEmptyTileMap(width, height);
            _groundMap = GetEmptyMap(width, height);
            _surfaceHeights = new int[width];
            _layerWidths[1] = height / 10;
            _layerWidths[^1] = height;

            _random = new Random(_seed);
        }

		// ----- Methods -----
        // Generate surface
        public Map Generate() {
            // Main terrain generation
            GenerateSurfaceTerrain();
            FillWithGround();
            GenerateCaves();
            SmoothCaves();

            // Adding variation
            SetBiomes();
            PaintTiles();

            // Adding details
            AddWater();
            AddFlowers();
            AddTrees();
            //AddChests();

			return new Map(_tiles, _biomes, _seed);
        }
		
		// Generate surface
        private void GenerateSurfaceTerrain() {
            Perlin perlin = new Perlin();
            int PerlinConstant = 33;
            int y;
            _surfaceWaveHeight = _height * _surfacePercent / 100;
            for (int x = 0; x < _width; x++) {
                y = _topOffset + (int)(perlin.OctavePerlin((double)x / PerlinConstant, (double)_surfaceWaveHeight/ PerlinConstant, z: 1d / PerlinConstant, octaves: 3, persistence: 3) * _surfaceWaveHeight);
                _surfaceHeights[x] = y;
                _groundMap[x, y] = Ground;
            }
        }

        // Generate caves
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smoothAmount">How many times to smooth the caves. Smaller amount = bigger caves + vice versa.</param>
        private void SmoothCaves() {
            for (int i = 0; i < _smoothAmount; i++) {
                SmoothMap();
            }
        }

        private void GenerateCaves() {
            for (int x = 0; x < _width; x++) {
                // Go from the surface to the bottom of the map.
                for (int y = _surfaceHeights[x] + _minCaveDepth; y < (_height - _numberOfTilesAtBottom); y++) {
                    _groundMap[x, y] = (RandomPercentage() < _randomFillPercent) ? Ground : Air;
                }
            }
        }

        private void FillWithGround() {
            for (int x = 0; x < _width; x++) {
                for (int y = _surfaceHeights[x] + 1; y < (_height - _numberOfTilesAtBottom); y++) {
                    _groundMap[x, y] = Ground;
                }
            }
        }

        private void SmoothMap() {
            // Using Moore’s neighbourhood.
            const int MaxNeighbours = 8;
            int surroundingGroundCount;

			for (int x = 0; x < _width; x++) {
				for (int y = _surfaceHeights[x] + _minCaveDepth; y < _height; y++) {
					// Create border on the edges.
					if ((x == 0) || (x == (_width - 1)) || (y >= (_height - _numberOfTilesAtBottom))) {
						_groundMap[x, y] = Ground;
					}
					else {
                        // Smooth based on neighbouring tiles.
						surroundingGroundCount = GetSurroundingGroundCount(x, y);
						if (surroundingGroundCount > (MaxNeighbours / 2)) { // if surrounded by > 4 ground tiles, become a ground tile.
							_groundMap[x, y] = Ground;
						}
						else if (surroundingGroundCount < (MaxNeighbours / 2)) { // if surrounded by < 4 ground tiles, become an air tile.
							_groundMap[x, y] = Air;
						}
					}
				}
			}
        }
        private bool CheckIfWithinBounds(int x, int y) {
            return (x >= 0) && (x < _width) && (y >= 0) && (y < _height);
        }
        private int GetSurroundingGroundCount(int x, int y) {
            int surroundingGroundCount = 0;
            for (int neighbourX = x - 1; neighbourX <= (x + 1); neighbourX++) {
                for (int neighbourY = y - 1; neighbourY <= (y + 1); neighbourY++) {
                    if ((!((neighbourX == x) && (neighbourY == y)))
                        && CheckIfWithinBounds(neighbourX, neighbourY)
                        && (_groundMap[neighbourX, neighbourY] == Ground)) {
                        surroundingGroundCount++;
                    }
                }
            }
            return surroundingGroundCount;
        }
        // Texturing
        /// <summary>
        /// Randomly assign biomes of set width to the map, horizontally.
        /// </summary>
        private void SetBiomes() {
            int biomeSize = _width / _numberOfBiomes;
            BiomeType currentBiome;
            BiomeType previousBiome = null;
            for (int i = 0; i < _numberOfBiomes; i++) {
                // Ensure that adjacent biomes are different.
                do {
                    currentBiome = GetRandomBiome();
                } while (currentBiome.Equals(previousBiome));

                _biomes.Add(i * biomeSize, currentBiome);
                previousBiome = currentBiome;
            }
            BiomeType GetRandomBiome() {
                return Shroomworld.BiomeTypes[_random.Next(1, Shroomworld.BiomeTypes.Count + 1)];
            }
        }
        /// <summary>
        /// Convert the untextured tile map to a tile map containing tile id
        /// values chosen based on the height and biome of the tile.
        /// </summary>
        private void PaintTiles() {
            int currentLayer;
            for (int x = 0; x < _width; x++) {
                currentLayer = 0;
                for (int y = _surfaceHeights[x]; y < _height; y++) {
                    if (y >= (_surfaceHeights[x] + _layerWidths[currentLayer])) {
                        currentLayer++;
                    }
                    if (_groundMap[x, y] == Ground) {
                        _tiles[x, y] = _biomes[x].Layers[currentLayer];
                    }
                }
            }
        }

        // Adding details
        private void AddWater() {
            const int LeastWideWaterWidth = 5;
            int deepestWaterDepth = _surfaceWaveHeight / 2;
            int lowestSurfacePoint = _topOffset + _surfaceWaveHeight;
            int waterLevel = lowestSurfacePoint - deepestWaterDepth;

            int dipWidth = 0;
            for (int x = 0; x < _width; x++) {
                if (_tiles[x, waterLevel] == TileType.AirId) {
                    dipWidth++;
                }
                else if (dipWidth >= LeastWideWaterWidth) {
                    FillWithWater(x - dipWidth, x);
                    dipWidth = 0;
                }
            }

            // --- Local Functions ---
            void FillWithWater(int startX, int endX) {
                for (int x = startX; x < endX; x++) {
                    for (int y = waterLevel; y < _surfaceHeights[x]; y++) {
                        _tiles[x, y] = TileType.WaterId;
                    }
                }
            }
        }
        private void AddFlowers() {
            int y;
            // Iterate over surface layer.
            for (int x = 0; x < _width; x++) {
                // We want the tile directly above the surface.
                y = _surfaceHeights[x] - 1;
                if ((RandomPercentage() < _biomes[x].FlowerAmount) // chance to plant flower
                && (_tiles[x, y] != TileType.WaterId)) { // flowers can't be planted in water
                    // Place random type of flower.
                    _tiles[x, y] = RandomFrom(_biomes[x].FlowerTypes);
                }
            }
        }
        private void AddTrees() {
            const int ChanceToReplaceFlower = 100;
            int y;
            // Iterate over the surface of the map.
            for (int x = 0; x < _width; x++) {
                // We want the tile directly above the surface.
                y = _surfaceHeights[x] - 1;
                if ((RandomPercentage() < _biomes[x].TreeAmount) // chance to plant tree
                && (_tiles[x, y] != TileType.WaterId) // trees can't be planted in water
                && ((_tiles[x, y] == TileType.AirId) // will always replace air
                || (RandomPercentage() < ChanceToReplaceFlower))) { // will sometimes replace flower
                    // Place tree.
                    _tiles[x, y] = _biomes[x].TreeType;
                }
            }
        }
        //private void AddChests() {
        //    int airTileCount = 0;
        //    for (int x = 0; x < _width; x++) {
        //        for (int y = _surfaceHeights[x]; y < _height; y++) { // go through all the ground below the surface
        //            if(_tiles[x, y] == TileType.AirId) {
        //                airTileCount++;
        //            }
        //            else {
        //                if ((airTileCount >= 3) && (GetSurroundingGroundCount(x, y - 1) > 1) // the chest should be exposed (the only neighbouring tile should be the one it's standing on)
        //                && (RandomPercentage() < _biomes[x].ChestAmount)) { // chance to place chest
        //                    _tiles[x, y - 1] = TileType.ChestId; // y - 1 because the current tile is not an air tile. we want to place the chest in the air tile directly above the ground
        //                }
        //                airTileCount = 0;
        //            }
        //        }
        //    }
        //}
        private int RandomPercentage() {
            return _random.Next(0, 101);
        }
        private int RandomFrom(int[] array) {
            return array[_random.Next(array.Length)];
        }

        // Clearing map
        /// <summary>
        /// Create and return a tile array of size <see cref="_width"/>
		/// * <see cref="_height"/> consisting of only air tiles.
        /// </summary>
        /// <returns>A 2D tile array filled with the air tile id (<see cref="TileType.AirId"/>)</returns>
		private bool[,] GetEmptyMap(int width, int height) {
            bool[,] map = new bool[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map[x, y] = Air;
                }
            }
            return map;
        }
        private int[,] GetEmptyTileMap(int width, int height) {
            int[,] map = new int[width, height];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map[x, y] = TileType.AirId;
                }
            }
            return map;
        }
    }
}
