using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Stage // where the action takes place.// Playing the actual game
    {
        

        //private Point _origin; // position of the upper left tile. Acts as a reference point for the positions of all the other tiles.
        private Point _camera; // position of the camera. Acts as a reference point to determine which tiles to display and where.
        private List<PhysicalEntity> _visibleEntities; // contains all visible NPC and enemy objects (i.e. those within update-range).
        private Player _player;


        //-------------
        public Stage(bool createNewWorld, bool createNewPlayer)
        {
            
        }


        //------------------

        // Updating
        private void Update_Player()
        {

        }
        private void Update_Camera()
        {

        }

        // Displaying
        private void Display_Player()
        {

        }
        private void Display_Entities()
        {

        }
        private void Display_World()
        {

        }

        // Create new...
        private void New_Player()
        {

        }
        private void New_World()
        {

        }

        // File loading
        private void Load_Files()
        {

        }
        private void Load_World()
        {
            // format:

            // difficulty
            // width
            // 0 1 1 0 0
            // 0 1 1 1 0
            // 1 1 1 1 1
            List<string> world_file = new List<string> { };

            int difficulty,
                width,
                height;
            int[,] map;

            int index = 0;

            File.Load(ref world_file, saveFolder + worldFile);
            difficulty = Convert.ToInt32(world_file[index++]);
            width = Convert.ToInt32(world_file[index++]);
            height = world_file.Count - index;
            map = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map[x, y] = Convert.ToInt32(world_file[y][x].ToString());
                }
            }
            index++;
        }
        private void Load_Player_Data()
        {

        }
        private void Load_World_Template()
        {
            // format:

            // width,height,chunk-width,enemy-cap,npc-cap,npc-spawn-chance,enemy-spawn-chance,chest-chance,tree-chance
            List<string> world_template_file = new List<string> { };
            int[] as_ints;
            int width, height, chunk_width, enemy_cap, npc_cap, npc_chance, enemy_chance, chest_chance, tree_chance;
            int index = 0;

            File.Load(ref world_template_file, templatesFolder + worldFile);
            as_ints = new int[world_template_file.Count];
            for (int i = 0; i < world_template_file.Count; i++)
            {
                as_ints[i] = Convert.ToInt32(world_template_file[i]);
            }
            width = as_ints[index++];
            height = as_ints[index++];
            chunk_width = as_ints[index++];
            enemy_cap = as_ints[index++];
            npc_cap = as_ints[index++];
            npc_chance = as_ints[index++];
            enemy_chance = as_ints[index++];
            chest_chance = as_ints[index++];
            tree_chance = as_ints[index++];
        }
        private void Load_Player_Template()
        {

        }

        // File saving
        private void Save_World() // TODO
        {

        }

    }
}
