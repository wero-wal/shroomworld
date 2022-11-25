namespace Shroomworld
{
    internal static class Directories
    {
        public const string GameData = "gamedata/";
        public const string UserData = "userdata/";

        public static string User => UserData + $"user{MyGame.CurrentUser.ID}/";
        public static string World => User + $"world{MyGame.CurrentWorldID}/";

        public static class Textures
        {
            public static string MainDirectory { get => _textures; }

            public const string Tiles = _textures + "tiles/";
            public const string Biomes = _textures + "biome/";
            public const string Items = _textures + "item/";
            public const string Enemies = _textures + "enemy/";
            public const string Npcs = _textures + "npcs/";
            public const string Player = _textures + "player/";
            public const string Menu = _textures + "menu/";
            public const string Gui = _textures + "gui/";

            private const string _textures = "textures/";
        }
    }
}
