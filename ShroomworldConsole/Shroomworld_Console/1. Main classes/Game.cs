using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Game
    {
        //- - - - - enums ----------
        public enum Input
        {
            // world interaction
            MoveLeft,
            MoveRight,
            Jump,
            Sprint,
            InteractOrPlaceTile,
            AttackOrBreakTile,

            // hotbar
            HotbarSlot1,
            HotbarSlot2,
            HotbarSlot3,
            HotbarSlot4,

            // opening menus
            OpenInventory,
            OpenQuestMenu,
            OpenPauseMenu,

            // navigating menus
            NavigateUpInMenu,
            NavigateDownInMenu,
            NavigateLeftInMenu,
            NavigateRightInMenu,
            ConfirmSelection,
            ExitCurrentMenu,

            // mouse controls (?)
            LeftMouseDown,
            RightMouseDown,
            LeftMouseUp,
            RightMouseUp,
            ScrollUp,
            ScrollDown,
        }

        //- - - - - public properties ----------
        public static Dictionary<int, TileType> TileDictionary { get; private init; }; // ID --> TileType
        public static Dictionary<int, BiomeType> BiomeDictionary { get; private init; }; // ID --> BiomeType
        public static Dictionary<int, ItemType> ItemDictionary { get; private init; }; // ID --> ItemType
        public static Dictionary<int, EnemyType> EnemyDictionary { get; private init; }; // ID --> EnemyType
        
        // - - - - private properties ---------- -
        private const int numOfLayers = 3; // will be determined by biome IRL
        
        private bool
            _world_exists,
            _player_exists;
        private int _frame; // which frame we are currently on

        // file management
        // menu management

        //- - - - - constructor ----------
        public Game()
        {
            
        }

        //- - - - - methods ----------
        // Input
        public static Input Get_Input() // TODO
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            return Input.MoveLeft;
        }

        // File loading
        private void Load_Files()
        {
            Load_Game_Log();
            Load_Tiles();
            if (_world_exists)
            {
                Load_World();
            }
        }
        private void Load_Game_Log()
        {
            // format:

            // world-exists?
            // player-exists?
            List<string> game_log = new List<string> { };
            int index = 0;

            File.Load(ref game_log, gameLogFile);
            _world_exists = Convert.ToBoolean(game_log[index++]);
            _player_exists = Convert.ToBoolean(game_log[index++]);
        }
        private void Load_Game_Settings() // TODO
        {
            // format:

            // setting-name:setting-value(s)

            // num-of-difficulty-settings:x
            // resolution:x,y
            // tile-size:x,y
            // move-up:W        [use the numeric key value to store ConsoleKeys]
            // move-down:S
            // move-left:A
            // move-right:D
            // jump:Space
            // interact-place:Enter
            // attack-break:Backspace
            // sprint:LeftShift
            // target-block-above:UpArrow
            // target-block-below:DownArrow
            // target-block-left:LeftArrow
            // target-block-right:RightArrow
            // pause-game:Escape
            // menu-confirm:Enter
            // menu-exit:Escape
            // menu-scroll:arrows
        }
        private Dictionary<int, TileType> Load_Tiles() // TODO
        {
            // format:

            // tile-name,texture,size,colour,solid?,resource-id:min-amount:max-amount;next-resource: ...

            List<string> tile_file = new List<string> { };
            Dictionary<int, TileType> tile_dictionary = new Dictionary<int, TileType> { };
            string[] tile_parts;

            int id;
            string name;
            string texture;
            Point size;
            int colour;
            bool solid;

            string[] str_drops;
            string[] drop;
            int[] drop_info;
            List<Drop> drops;

            int p; // indexer to keep track of tile parts

            File.Load(ref tile_file, typesFolder + tileFile);

            foreach (string tile in tile_file)
            {
                tile_parts = tile.Split(',');
                p = 0;
                id = Convert.ToInt32(tile_parts[p++]);
                name = tile_parts[p++];
                texture = tile_parts[p++];
                size = new Point(Convert.ToInt32(tile_parts[p++]), Convert.ToInt32(tile_parts[p++]));
                colour = Convert.ToInt32(tile_parts[p++]);
                solid = Convert.ToBoolean(tile_parts[p++]);
                drops = new List<Drop> { };
                str_drops = tile_parts[p++].Split(';');
                for (int i = 0; i < str_drops.Length; i++) // drops
                {
                    drop = str_drops[i].Split(':');
                    drop_info = new int[drop.Length];
                    for (int j = 0; j < drop.Length; j++)
                    {
                        drop_info[j] = Convert.ToInt32(drop[i]);
                    }
                    drops.Add(new Drop(drop_info[0], drop_info[1], drop_info[2]));
                }
                tile_dictionary.Add(id, new TileType(name, texture, size, colour, solid, drops)); // add tile
            }
            return tile_dictionary;
        }
        private Dictionary<int, ItemType> Load_Items() // TODO
        {
            return null;
        }
        private Dictionary<int, BiomeType> Load_Biome_Types() // TODO 
        {
            return null;
        }
        private Dictionary<int, EnemyType> Load_Enemy_Types() // TODO
        {
            return new Dictionary<int, EnemyType>();
        }
        private Dictionary<int, NpcType> Load_NPC_Types() // TODO
        {
            return new Dictionary<int, NpcType>();
        }


        // File saving

        // New world
    }
}
