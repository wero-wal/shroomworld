using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace Shroomworld.FileHandling;

// Naming note: In this class, Dir = Directory
public static class FilePaths {

    // ----- Enums -----
    public enum Elements {
        Menu,
        Gui,
        Item,
        Tile,
        Biome,
        Map,
        Enemy,
        Friendly,
        Player,
    }


    // ----- Properties -----
    /// <value>
    /// Path for the file containing names, ids, and users' settings.
    /// </value>
    public static string UsersFile => s_users;
    /// <value>
    /// Path for the file containing general (non-user-specific) game settings.
    /// </value>
    public static string GeneralSettingsFile => s_generalSettings;
    public static string GameData => s_gameData;
    /// <value>
    /// Path for the file containing text for all menu buttons and headings for all menus.
    /// </value>
    public static string MenuTextFile => s_menuText;
    public static string GuiData => s_guiData;
    public static string Quests => s_quests;
    /// <value>
    /// Paths for the files containing data about the types of different <see cref="Elements"/>.
    /// </value>
    public static Dictionary<Elements, string> TypeFiles => s_types;
    /// <value>
    /// Names of the files in directories of saved worlds.
    /// </value>
    public static Dictionary<Elements, string> WorldSaveFileNames => s_worldSaveFileNames;
    /// <value>
    /// Paths for the directories containing textures for different <see cref="Elements"/>.
    /// </value>
    public static Dictionary<Elements, string> TextureDirectories => s_textureDirectories;
    public static Dictionary<Type, Elements> ElementForType => s_elementForType;
    public static Elements[] TextureElements => s_textureElements;

	public static string HotbarSlotTexture => s_hotbarSlotTexture;
	public static string SelectedHotbarSlotTexture => s_selectedHotbarSlotTexture;
	public static string TitleTexture => s_titleTexture;
	public static string ButtonTexture => s_buttonTexture;
	public static string HighlightedButtonTexture => s_highlightedButtonTexture;
	public static string PressedButtonTexture => s_pressedButtonTexture;



	// ----- Fields -----
	public const string FilePathFile = "file-paths.txt";

    // Elements for which there are types / templates.
    private static Elements[] s_typeElements = new Elements[] {
        Elements.Item,
        Elements.Tile,
        Elements.Biome,
        Elements.Map,
        Elements.Enemy,
        Elements.Friendly,
        Elements.Player
    };
    // Elements for which there are files in folders of saved worlds.
    private static Elements[] s_worldSaveElements = new Elements[] {
        Elements.Map,
        Elements.Enemy,
        Elements.Friendly,
        Elements.Player
    };
    // Elements which have texture files.
    private static Elements[] s_textureElements = new Elements[] {
        Elements.Menu,
        Elements.Gui,
        Elements.Item,
        Elements.Tile,
        Elements.Biome,
        Elements.Enemy,
        Elements.Friendly,
        Elements.Player
    };
    private static string s_users;
    private static string s_generalSettings;
    private static string s_menuText;
    private static string s_gameData;
    public static string s_quests;
    // Path for the directory containing all world save directories.
    private static string s_worldSavesDirectory;
    
    // Gui texture file names
    private static string s_hotbarSlotTexture;
    private static string s_selectedHotbarSlotTexture;

    // Gui file name
    private static string s_guiData; // Path to file containing data about the in-game GUI.

    // Menu texture file names
    private static string s_titleTexture;
    private static string s_buttonTexture;
    private static string s_highlightedButtonTexture;
    private static string s_pressedButtonTexture;

    // Dictionaries
    private static Dictionary<Elements, string> s_types;
    private static Dictionary<Elements, string> s_worldSaveFileNames;
    private static Dictionary<Elements, string> s_textureDirectories;
    private static Dictionary<Type, Elements> s_elementForType = new Dictionary<Type, Elements> {
        { typeof(ItemType), Elements.Item },
        { typeof(TileType), Elements.Tile },
        { typeof(BiomeType), Elements.Biome },
        { typeof(EnemyType), Elements.Enemy },
        { typeof(FriendlyType), Elements.Friendly },
        { typeof(PlayerType), Elements.Player }
    };


    // ----- Methods -----
    /// <summary>
    /// Takes a list of filepaths and saves them to the appropriate dictionaries.
    /// </summary>
    /// <param name="paths"></param>
    /// <exceptions>Throws ArgumentOutOfRangeException if not enough paths were passed.</exceptions>
    public static void SetFilePaths(string[] paths) {
        int p = 0;

        // General settings
        s_generalSettings = paths[p++];
        s_gameData = paths[p++];
        s_menuText = paths[p++];
        s_guiData = paths[p++];
        s_quests = paths[p++];

        // Types
        s_types = new Dictionary<Elements, string>(s_typeElements.Length);
        for (int i = 0; i < s_typeElements.Length; i++) {
            s_types.Add(s_typeElements[i], paths[p++]);
        }
        // World files
        s_worldSavesDirectory = paths[p++];
        s_worldSaveFileNames = new Dictionary<Elements, string>(s_worldSaveElements.Length);
        for (int i = 0; i < s_worldSaveElements.Length; i++) {
            s_worldSaveFileNames.Add(s_worldSaveElements[i], paths[p++]);
        }
        // Textures
        s_textureDirectories = new Dictionary<Elements, string>(s_textureElements.Length);
        for (int i = 0; i < s_textureElements.Length; i++) {
            s_textureDirectories.Add(s_textureElements[i], paths[p++]);
        }
        // Gui texture file names
        s_hotbarSlotTexture = paths[p++];
        s_selectedHotbarSlotTexture = paths[p++];

        // Menu texture file names
        s_titleTexture = paths[p++];
        s_buttonTexture = paths[p++];
        s_highlightedButtonTexture = paths[p++];
        s_pressedButtonTexture = paths[p++];
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="worldId">The ID of the world whose file's path you want.</param>
    /// <param name="file">Specify the file whose path you want.
    /// (options: <see cref="Elements.Map"/>, <see cref="Elements.Enemy"/>, <see cref="Elements.Friendly"/>, <see cref="Elements.Player"/>).</param>
    /// <returns>Path for the specified <paramref name="file"/> in the directory of a specific saved world.</returns>
    public static string GetPathForWorldFile(int worldId, Elements file) {
        if (!s_worldSaveElements.Contains(file)) {
            throw new ArgumentException("This Element is not a world save file element.");
        }
        return $"{s_worldSavesDirectory}{worldId}\\{s_worldSaveFileNames[file]}";
    }
}
