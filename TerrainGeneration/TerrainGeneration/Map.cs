using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGeneration
{
    public class Map
    {
        public bool[,] Tiles => _tiles;

        private bool[,] _tiles;
        private int _width;
        private int _height;

        private int[] _surfaceHeight; // stores top of terrain at each x
        
        private float _seed;
        private float _smoothness; // how smooth or 'janky' the terrain looks
        private const int _smoothAmount = 2;

        private const int _airAtTop = 2; // (min) number of empty tiles at the top of the map
        private const int _tilesAtBottom = 1; // (min) number of solid tiles at the bottom of the map
        private const float _sizeOfSurface = 0.33f; // percentage of the map (vertically) that makes up the surface

        private const ConsoleColor _groundColour = ConsoleColor.Green;
        private const ConsoleColor _backgroundColour = ConsoleColor.Black;

        private const bool _Ground = true;
        private const bool _Air = false;

        public Map(bool[,] tiles, int width, int height, float smoothness, float? chosenSeed = null)
        {
            _tiles = tiles;
            _width = width;
            _height = height;
            _seed = chosenSeed ?? (float)new Random().NextDouble();
            _smoothness = smoothness;
            _surfaceHeight = new int[_width];
        }
        public Map(int width, int height, float smoothness, float? chosenSeed = null)
        {
            _tiles = GetEmptyMap(width, height);
            _width = width;
            _height = height;
            _seed = chosenSeed ?? (float)new Random().NextDouble();
            _smoothness = smoothness;
            _surfaceHeight = new int[_width];
        }

        // Generate surface
        public void GenerateSurfaceTerrain()
        {
            int height = (int)Math.Round(_height * _sizeOfSurface); // the Perlin wave will be <= this height

            Perlin perlin = new Perlin();
            int y;
            for (int x = 0; x < _width; x++)
            {
                y = _airAtTop + (int)Math.Round(perlin.OctavePerlin(x / _smoothness, _seed, 0, 6, _smoothness) * height);
                _surfaceHeight[x] = y;
                _tiles[x, y] = true;
            }
        }


        // Generate caves
        public void GenerateCaves(int smoothAmount = -1)
        {
            int randomFillPercent = 55; // what % of the underground will be ground
            Random random = new Random(_seed.GetHashCode());
            for (int x = 0; x < _width; x++)
            {
                for (int y = _surfaceHeight[x]; y < (_height - _tilesAtBottom); y++)
                {
                    _tiles[x, y] = random.Next(1, 100) < randomFillPercent;
                }
            }
            Smooth_Map();
        }
        private void Smooth_Map() // smooth_amount = how many times to smooth the caves. Smaller amount = bigger caves + vice versa.
        {
            int surroundingGroundCount;
            int max_neighbours = 4; // Moore’s Neighbourhood: max = 8. Von Neumann’s: max = 4.
            for (int s = 0; s < Math.Abs(_smoothAmount); s++)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int y = _surfaceHeight[x]; y < _height; y++)
                    {
                        if ((x == 0) || (x == (_width - 1)) || (y == _surfaceHeight[x]) || (y >= (_height - _tilesAtBottom))) // if on the edge, create a border (place solid tile).
                        {
                            _tiles[x, y] = _Ground;
                        }
                        else
                        {
                            surroundingGroundCount = GetSurroundingGroundCount(x, y);
                            if (surroundingGroundCount > (max_neighbours / 2)) // if surrounded by > 2 ground tiles, become a ground tile.
                            {
                                _tiles[x, y] = _Ground;
                            }
                            else if (surroundingGroundCount < (max_neighbours / 2)) // if surrounded by < 2 ground tiles, become an air tile.
                            {
                                _tiles[x, y] = _Air;
                            }
                            else
                            {
                                // do nothing
                            }
                        }
                    }
                }
            }
        }
        private int GetSurroundingGroundCount(int x, int y)
        {
            /* using the Von Neumann Neighbourhood:
             * 
             * . n .
             * n t n
             * . n .
             * 
             * (n = neighbour    t = current tile    . = other tile)
             */

            int groundCount = 0;
            for (int nebx = (x - 1); nebx <= (x + 1); nebx += 2) // nebx = x-index of neighbour
            {
                AddToGroundCount(nebx, y);
            }
            for (int neby = (y - 1); neby <= (y + 1); neby += 2) // neby = y-index of neighbour
            {
                AddToGroundCount(x, neby);
            }
            return groundCount;

            // local functions
            void AddToGroundCount(int neighbourX, int neighbourY)
            {
                if ((neighbourX >= 0) && (neighbourX < _width) && (neighbourY >= 0) && (neighbourY < _height)) // check if within bounds
                {
                    groundCount += (_tiles[neighbourX, neighbourY] ? 1 : 0); // if neighbour contains a tile, add 1
                }
            }
        }

        // Clearing map
        public void Clear()
        {
            _tiles = GetEmptyMap(_width, _height);
        }
        private bool[,] GetEmptyMap(int width, int height)
        {
            bool[,] map = new bool[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = false;
                }
            }
            return map;
        }
        
        // Other
        public void Display()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Console.BackgroundColor = _tiles[x, y] ? _groundColour : _backgroundColour;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}
