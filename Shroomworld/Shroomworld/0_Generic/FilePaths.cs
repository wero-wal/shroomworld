using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld._0_Generic
{
    internal static class FilePaths
    {
        public const string PlayerData = Directories.GameData + _player + _txt;
        public const string TileData = Directories.GameData + "tiles" + _txt;
        public const string BiomeData = Directories.GameData + "biomes" + _txt;
        public const string ItemData = Directories.GameData + "items" + _txt;
        public const string EnemyData = Directories.GameData + _enemies + _txt;
        public const string NpcData = Directories.GameData + _npcs + _txt;
        public const string QuestData = Directories.GameData + "quests" + _txt;
        public const string WorldData = Directories.GameData + "world" + _txt;

        public const string AllUsers = "users" + _txt;
        public const string GeneralSettings = _settings + _txt;

        public static string UserSettings => Directories.User + _settings + _txt;
        public static string SavedMap => Directories.World + "map" + _txt;
        public static string SavedPlayer => Directories.World + _player + _txt;
        public static string SavedEnemies => Directories.World + _enemies + _txt;
        public static string SavedNpcs => Directories.World + _npcs + _txt;


        private const string _txt = ".txt";
        private const string _enemies = "enemies";
        private const string _npcs = "npcs";
        private const string _player = "player";
        private const string _settings = "settings";
    }
}
