using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class World
    {
        public int[,] Map { get => _map; }
        public int Width { get => _width; }
        public int Height { get => _height; }

        //------------------
        private int[,] _map; // contains the IDs of all the tiles in the world.
        private int
            _width,
            _height;
        private List<Entity> _entities; // all NPC and enemy objects.
        private Dictionary<int, BiomeType> _biomes; // key = startpoint of biome, BiomeType = the biome


        //-------------
        // Constructor
        public World(bool createNew)
        {

        }


        //-----------------------------
        private void Create_New_World()
        {

        }

        // World generation
        private void Generate_World()
        {

        }
        private void Generate_Surface()
        {

        }
        private void Generate_Caves()
        {

        }
        private void Smooth_Caves()
        {

        }
        private List<Point> Get_Von_Neumann_Neighbours()
        {
            return null;
        }

        // File
        private void Load()
        {

        }
        private void Format_To_Save()
        {

        }

        // Update
        private void Update_Map(int x, int y)
        {
            
        }
        private void Update_Map(int x, int y, int tileId)
        {

        }
        private void Update_Entities()
        {

        }

        // Spawning
        private void Attempt_Entity_Spawning()
        {

        }

    }
}
