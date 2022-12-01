using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld.FileHandling
{
    internal static class FilePaths
    {
        // Style note: In this class, Dir = Directory
        // todo: make a Create() method here
        // ----- Enums -----
        public enum WorldFiles
        {
            Map,
            Enemies,
            Friendlies,
            Player,
        }

        // ----- Properties -----
        // todo: add properties to the fields below

        // ----- Fields -----
        // UI and settings
        private static string _users; // contains names, ids, and settings of users
        private static string _generalSettings; // contains 
        private static string _menuText; // contains text for all menu buttons and headings for all menus

        // Types and templates
        private static string _itemTypes;
        private static string _tileTypes;
        private static string _biomeTypes;
        private static string _mapTemplate;
        private static string _enemyTypes;
        private static string _friendlyTypes;
        private static string _playerTemplate;

        // World saves
        private static string _worldSavesDir;
        private static string _savedMapFileName;
        private static string _savedEnemiesFileName;
        private static string _savedFriendliesFileName;
        private static string _savedPlayerFileName;

        // Textures
        private static string _itemTextureDir;
        private static string _tileTextureDir;
        private static string _biomeTextureDir;
        private static string _enemyTextureDir;
        private static string _friendlyTextureDir;
        private static string _playerTextureDir;
        private static string _menuTextureDir;
        private static string _guiTextureDir;


        // ----- Methods -----
        public static bool SetFilePaths(params string[] paths)
        {
            int p = 0;
            try
            {
                _generalSettings = paths[p++];
                _menuText = paths[p++];

                _itemTypes = paths[p++];
                _tileTypes = paths[p++];
                _biomeTypes = paths[p++];
                _mapTemplate = paths[p++];
                _enemyTypes = paths[p++];
                _friendlyTypes = paths[p++];
                _playerTemplate = paths[p++];

                _worldSavesDir = paths[p++];
                _savedMapFileName = paths[p++];
                _savedEnemiesFileName = paths[p++];
                _savedFriendliesFileName = paths[p++];
                _savedPlayerFileName = paths[p++];

                _itemTextureDir = paths[p++];
                _tileTextureDir = paths[p++];
                _biomeTextureDir = paths[p++];
                _enemyTextureDir = paths[p++];
                _friendlyTextureDir = paths[p++];
                _playerTextureDir = paths[p++];
                _menuTextureDir = paths[p++];
                _guiTextureDir = paths[p++];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new ArgumentException("Not enough paths specified.");
            }
        }
        public static string GetPathForWorldFile(int worldId, WorldFiles worldFile)
        {
            string prefix = $"{_worldSavesDir}{worldId}\\";
            switch (worldFile)
            {
                case WorldFiles.Map:
                    return prefix + _savedMapFileName;
                case WorldFiles.Enemies:
                    return prefix + _savedEnemiesFileName;
                case WorldFiles.Friendlies:
                    return prefix + _savedFriendliesFileName;
                case WorldFiles.Player:
                    return prefix + _savedPlayerFileName;
                default:
                    return String.Empty;
            }
        }
    }
}
