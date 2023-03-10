using System;
namespace Shroomworld {
	internal class MapGenerator {

		// ----- Fields -----
        private const bool Ground = true;
        private const bool Air = false;

        private readonly int _topOffset = 2; // (min) number of empty tiles at the top of the map
        private readonly int _numberOfTilesAtBottom = 1; // (min) number of solid tiles at the bottom of the map
        private readonly int _surfacePercent = 33; // percentage of the map (vertically) that makes up the surface (height of the perlin wave)
        private readonly int _smoothAmount = 2;
    	private readonly int _randomFillPercent = 55; // what % of the underground will be ground (the rest will be air)

        private readonly int[,] _tiles;
		private readonly BiomeDictionary _biomes;
        private readonly int _width;
        private readonly int _height;

		private readonly bool[,] _groundMap;
        private readonly int[] _surfaceHeights; // stores top of terrain at each x
        
        private readonly int _seed;
        private readonly float _smoothness; // how smooth or 'janky' the terrain looks
        private readonly int _numberOfBiomes;
        private readonly int[] _layerBoundaries = { 0, 1, 3, 0 };

        private readonly Random _random;

        private int _surfaceWaveHeight;

		// ----- Constructor -----
		/// <summary>
		/// Use this when generating a new map
		/// </summary>
        public MapGenerator(int width, int height, int numberOfBiomes, float smoothness, int? seed = null) {
			_tiles = new int[width, height];
            _groundMap = GetEmptyMap();
            _surfaceHeights = new int[width];
            _layerBoundaries[^1] = height;

            _width = width;
            _height = height;
			_numberOfBiomes = numberOfBiomes;
			_biomes = new BiomeDictionary(numberOfBiomes);

            _smoothness = smoothness;
            _seed = seed ?? new Random().Next();

            _random = new Random(_seed);
        }

		// ----- Methods -----
        // Generate surface
        public Map Generate() {
            // Main terrain generation
            GenerateSurfaceTerrain();
            GenerateCaves();

            // Adding variation
            AddBiomes();
            PaintTiles();

            // Adding details
            AddWater();
            AddFlowers();
            AddTrees();
            AddChests();

			return new Map(_tiles, _biomes, _seed);
        }
		
		// Generate surface
        private void GenerateSurfaceTerrain() {
            _surfaceWaveHeight = _height * _surfacePercent / 100;

            Perlin perlin = new Perlin();
            int y;
            for (int x = 0; x < _width; x++) {
                y = _topOffset + (int)Math.Round(perlin.OctavePerlin(x / _smoothness, _seed, 0, 6, _smoothness) * _surfaceWaveHeight);
                _surfaceHeights[x] = y;
                _groundMap[x, y] = Ground;
            }
        }

        // Generate caves
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smoothAmount">How many times to smooth the caves. Smaller amount = bigger caves + vice versa.</param>
        private void GenerateCaves() {
            // Generate noise
            for (int x = 0; x < _width; x++) {
                for (int y = _surfaceHeights[x]; y < (_height - _numberOfTilesAtBottom); y++) { // go from the surface to almost the bottom of the map
                    _groundMap[x, y] = (RandomPercentage() < _randomFillPercent) ? Ground : Air;
                }
            }

            // Smooth
            for (int i = 0; i < _smoothAmount; i++) {
                SmoothMap();
            }
        }
        private void SmoothMap() {
            int surroundingGroundCount;
            const int MaxNeighbours = 4; // using Von Neumann’s neighbourhood

			for (int x = 0; x < _width; x++) {
				for (int y = _surfaceHeights[x]; y < _height; y++) {
					// create border on the edges
					if ((x == 0) || (x == (_width - 1)) || (y >= (_height - _numberOfTilesAtBottom))) {
						_groundMap[x, y] = Ground;
					}
					else { // smooth based on neighbouring tiles
						surroundingGroundCount = GetSurroundingGroundCount(x, y);
						if (surroundingGroundCount > (MaxNeighbours / 2)) { // if surrounded by > 2 ground tiles, become a ground tile.
							_groundMap[x, y] = Ground;
						}
						else if (surroundingGroundCount < (MaxNeighbours / 2)) { // if surrounded by < 2 ground tiles, become an air tile.
							_groundMap[x, y] = Air;
						}
					}
				}
			}
        }
        private int GetSurroundingGroundCount(int x, int y) {
            /* using the Von Neumann Neighbourhood:
               . n .		n = neighbour
               n t n		t = current tile
               . n .		. = other tile
            */
            int surroundingGroundCount = 0;

            // check to the left and right of the tile
            for (int nebx = (x - 1); nebx <= (x + 1); nebx += 2) { // nebx = x-coord of neighbour
                AddToGroundCount(nebx, y);
            }
            // check above and below the tile
            for (int neby = (y - 1); neby <= (y + 1); neby += 2) { // neby = y-coord of neighbour
                AddToGroundCount(x, neby);
            }
            return surroundingGroundCount;

            // --- Local functions ---
            void AddToGroundCount(int neighbourX, int neighbourY) {
                if (CheckWithinBounds(neighbourX, neighbourY) && (_groundMap[neighbourX, neighbourY] == Ground)) {
                    surroundingGroundCount ++;
                }
            }
            bool CheckWithinBounds(int x, int y) {
                return ((x >= 0) && (x < _width) && (y >= 0) && (y < _height));
            }
        }

        // Texturing
        /// <summary>
        /// Randomly assign biomes of set width to the map, horizontally.
        /// </summary>
        private void AddBiomes() {
            int biomeSize = _width / _numberOfBiomes;
            for (int i = 0; i < _numberOfBiomes; i++) {
                _biomes.Add(i * biomeSize, Shroomworld.BiomeTypes[_random.Next(Shroomworld.BiomeTypes.Count)]);
            }
        }
        /// <summary>
        /// Convert the untextured tile map to a tile map containing tile id
        /// values chosen based on the height and biome of the tile.
        /// </summary>
        private void PaintTiles() {
            int startY;
            int endY;
            for (int x = 0; x < _width; x++) { // go across the map
                startY = _surfaceHeights[x];
				// the map is made up of three layers (in each column). This iterates through the layers.
                for (int i = 0; i < (_layerBoundaries.Length - 1); i++) {
                    startY = _surfaceHeights[x] + _layerBoundaries[i]; // start of current layer
                    endY = Math.Min(_surfaceHeights[x] + _layerBoundaries[(i + 1) % _layerBoundaries.Length], _height);
                    for (int y = startY; y < endY; y++) { // go down the current column
                        if (_groundMap[x, y] == Ground) {
                            _tiles[x, y] = _biomes[x].Layers[i];
                        }
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
            for (int x = 0; x < _width; x++) { // go over surface layer
                y = _surfaceHeights[x] - 1; // we want the tile directly above the surface
                if ((RandomPercentage() < _biomes[x].FlowerAmount) // chance to plant flower
                && (_tiles[x, y] != TileType.WaterId)) { // flowers can't be planted in water
                    _tiles[x, y] = RandomFrom(_biomes[x].FlowerTypes); // place random type of flower
                }
            }
        }
        private void AddTrees() {
            const int ChanceToReplaceFlower = 25;
            int y;
            for (int x = 0; x < _width; x++) { // go over the surface of the map
                y = _surfaceHeights[x] - 1; // we want the tile directly above the surface
                if ((RandomPercentage() < _biomes[x].TreeAmount) // chance to plant tree
                && (_tiles[x, y] != TileType.WaterId) // trees can't be planted in water
                && ((_tiles[x, y] == TileType.AirId) // will always replace air
                || (RandomPercentage() < ChanceToReplaceFlower))) { // will sometimes replace flower
                    _tiles[x, y] = _biomes[x].TreeType;
                }
            }
        }
        private void AddChests() {
            int airTileCount = 0;
            for (int x = 0; x < _width; x++) {
                for (int y = _surfaceHeights[x]; y < _height; y++) { // go through all the ground below the surface
                    if(_tiles[x, y] == TileType.AirId) {
                        airTileCount++;
                    }
                    else {
                        if ((airTileCount >= 3) && (GetSurroundingGroundCount(x, y - 1) > 1) // the chest should be exposed (the only neighbouring tile should be the one it's standing on)
                        && (RandomPercentage() < _biomes[x].ChestAmount)) { // chance to place chest
                            _tiles[x, y - 1] = TileType.ChestId; // y - 1 because the current tile is not an air tile. we want to place the chest in the air tile directly above the ground
                        }
                        airTileCount = 0;
                    }
                }
            }
        }
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
		private bool[,] GetEmptyMap() {
            bool[,] map = new bool[_width, _height];
            for (int y = 0; y < _height; y++) {
                for (int x = 0; x < _width; x++) {
                    map[x, y] = Air;
                }
            }
            return map;
        }
	}
}
