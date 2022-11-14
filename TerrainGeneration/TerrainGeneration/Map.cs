using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrainGeneration
{
    internal class Map
    {
        public bool[,] Tiles => _tiles;

        private bool[,] _tiles;
        private int _width;
        private int _height;

        private int[] _surfaceHeight; // stores top of terrain at each x
        
        private float _seed;

        private const int _airAtTop = 2; // (min) number of empty tiles at the top of the map
        private const int _tileAtBottom = 1; // (min) number of solid tiles at the bottom of the map
        private const float _sizeOfSurface = 0.33f; // percentage of the map (vertically) that makes up the surface

        public Map(bool[,] tiles, int width, int height, float? chosenSeed = null)
        {
            _tiles = tiles;
            _width = width;
            _height = height;
            _seed = chosenSeed ?? (float)new Random().NextDouble();
        }
        public Map(int width, int height, float? chosenSeed = null)
        {
            _tiles = GetEmptyMap(width, height);
            _width = width;
            _height = height;
            _seed = chosenSeed ?? (float)new Random().NextDouble();
        }

        // Coordinate stuff
        public int GetX(int index)
        {
            return (index % _width);
        }
        public int GetY(int index)
        {
            return (index / _width);
        }
        public int GetIndex(int x, int y)
        {
            return (y * _width + x);
        }

        // Map stuff
        public void GenerateSurfaceTerrain()
        {
            float smoothness = 0.3f; // how smooth or “janky” the terrain looks
            int height = (int)Math.Round(_height * _sizeOfSurface); // the Perlin wave will be <= this height

            Perlin perlin = new Perlin();
            int y;
            for (int x = 0; x < _width; x++)
            {
                y = _airAtTop + (int)Math.Round(perlin.OctavePerlin(x / smoothness, _seed, 0, 6, smoothness) * height);
                _surfaceHeight[x] = y;
                _tiles[x, y] = true;
            }
        }

        int randomFillPercent = 45, // what % of the underground will be caves
            max_neighbours; // Moore’s Neighbourhood: max = 8. Von Neumann’s: max = 4.

        // generate terrain
        // 

        public void GenerateCaves()
        {
            int perlin_height;
            int height = _height - _airAtTop - _tileAtBottom;
            float smoothness = 0.3f;
            Perlin perlin = new();
            Random random = new Random(_seed.GetHashCode());
            for (int x = 0; x < _width; x++)
            {
                perlin_height = (int)Math.Round(perlin.OctavePerlin(x / smoothness, _seed, 0, 4, smoothness) * height);
                for (int y = _airAtTop; y < (_airAtTop + perlin_height); y++)
                {
                    // the && bit at the end is to ensure that any air tiles above the surface don't get converted to solid tiles
                    _tiles[x, y] = (random.Next(1, 100) < randomFillPercent) && (!_tiles[x, y]);
                }
            }
            Smooth_Map(smoothness * 10);
        }
        private void Smooth_Map(int smoothAmount) // smooth_amount = how many times to smooth the caves. Smaller amount = bigger caves + vice versa.
        {
            int surroundingGroundCount;
            for (int s = 0; s < Math.Abs(smoothAmount); s++)
            {
                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _surfaceHeight[x]; y++)
                    {
                        if (x == 0 || y == 0 || x == _width - 1 || y == _surfaceHeight[x] - 1) // if on the edge, create a border.
                        {
                            _tiles[x, y] = true;
                        }
                        else
                        {
                            surroundingGroundCount = GetSurroundingGroundCount(x, y);
                            if (surroundingGroundCount > (max_neighbours / 2)) // if surrounded by > 4 ground tiles, become a ground tile.
                            {
                                _tiles[x, y] = true;
                            }
                            else if (surroundingGroundCount < (max_neighbours / 2)) // if surrounded by < 4 ground tiles, become an air tile.
                            {
                                _tiles[x, y] = false;
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
            int groundCount = 0;
            for (int nebx = x - 1; nebx <= x + 1; nebx++) // nebx = x-index of neighbour
            {
                for (int neby = y - 1; neby <= y + 1; neby++) // neby = y-index of neighbour
                {
                    if (nebx >= 0 && nebx < _width && neby >= 0 && neby < _height) // within bounds
                    {
                        groundCount += (_tiles[nebx, neby] ? 1 : 0); // if neighbour contains a tile, add 1
                    }
                }
            }
            return groundCount;
        }


        public void Clear()
        {
            _tiles = GetEmptyMap(_width, _height);
        }
        public void Display()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_tiles[x, y])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
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
    }
}
