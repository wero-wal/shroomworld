using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Shroomworld.FileHandling;
public static class FileManager {

	//----- Enums -----
	/// <summary>
	/// Used as an enum to keep track of which level of list (or separation) we are in within a file.
	/// </summary>
	public struct Levels {
		public const int I = 0;
		public const int II = 1;
		public const int III = 2;
	}

	// ----- Properties -----
	// ----- Fields -----
	private readonly static char[] s_separators = { ',', ';', ':' };
	private readonly static string[] s_separators_str = { ",", ";", ":" };
	private static Dictionary<Type, Func<IdData, string[], IType>> s_parsers = new Dictionary<Type, Func<IdData, string[], IType>> {
		{ typeof(ItemType), ParseItemType },
		{ typeof(TileType), ParseTileType },
		{ typeof(BiomeType), ParseBiomeType },/*
		{ typeof(EnemyType), ParseEnemyType },
		{ typeof(FriendlyType), ParseFriendlyType },*/
		{ typeof(PlayerType), ParsePlayerType }
	};

	// ----- Methods -----

	// Load:
	public static bool TryLoadFilePaths() {
		try {
			string[] filePaths = LoadCsvFile(FilePaths.FilePathFile)[0];
			FilePaths.SetFilePaths(filePaths);
			return true;
		}
		catch (System.Exception) {
			return false;
		}
	}
	// Note: exceptions will be handled in LoadTypes and other such high-level, public methods.
	public static Maybe<Dictionary<int, TType>> LoadTypes<TType>() where TType : IType {
		string path = FilePaths.TypeFiles[FilePaths.ElementForType[typeof(TType)]];
		try {
			return ParseTypes<TType>(LoadCsvFile(path));
		}
		catch (Exception) {
			return Maybe.None;
		}
	}
	//public static Maybe<Player> LoadPlayer(int id)
	//{
	//	// todo: sort loadplayer out
	//	string[][] file;
	
	//	if (!LoadCsvFile(FilePaths.AllUsers, out file))
	//	{
	//		return Maybe.None;
	//	}
	//	int p = 0;
	//	Texture2D texture;
	//	try
	//	{
	//		string[] line = file[0];
	//		PlayerType type = Shroomworld.PlayerTypes[line[p++].ToInt()];
	//		texture = LoadTexture(FilePaths.Elements.Player, idData.Name);
	//		return new Player(
	//			type: type,
	//			sprite: new Sprite(texture),
	//			powerUps: new PowerUp(ParsePowerUps(line[p++])),
	//			healthData: new HealthData(ParseEntityHealthData(line[p++]), type.HealthData),
	//			shieldData: new HealthData(ParseHealthData(line[p++])),
	//			attack: new AttackAndBoostInfo(_powerUps.Damage),
	//			inventory: ParseInventory(line[p++]), // doesn't even need to be stored as 2-dimensional if we store inventory height / width in settings
	//			quests: ParseQuests(line[p++]),
	//			statistics: ParseStatistics(line[p++]));
	//		return true;
	//	}
	//	catch (Exception)
	//	{
	//		return Maybe.None;
	//	}
	//}
	
	private static string[][] LoadCsvFile(string path) {
		List<string[]> file = new List<string[]>();
		string[] line;
		using (StreamReader sr = new StreamReader(path)) {
			while (!sr.EndOfStream) {
				// Split the line using commas as separators.
				line = sr.ReadLine().Split(Levels.I);
				file.Add(line);
			}
			sr.Close();
		}
		return file.ToArray();
	}
	private static Texture2D LoadTexture(FilePaths.Elements element, string itemName) {
		if (!FilePaths.TextureElements.Contains(element)) {
			throw new ArgumentException($"There's no texture folder for this element ({element}).");
		}
		try {
			return Shroomworld.ContentManager.Load<Texture2D>(FilePaths.TextureDirectories[element] + itemName);
		}
		catch (Exception) {
			// Return a default texture if it couldn't be loaded.
			return Shroomworld.DisplayHandler.BlankTexture;
		}
	}
	
	// Parse types:
	private static Dictionary<int, TType> ParseTypes<TType>(string[][] file) where TType : IType {
		Dictionary<int, TType> typeDictionary = new Dictionary<int, TType>(file.Length);;
		Func<IdData, string[], IType> parse = s_parsers[typeof(TType)];
		TType type = default;
		IdData idData;
		// The index of the variable being parsed within the line.
		int p;
		// Each line represents one instance of a TType.
		foreach (string[] line in file) {
			p = 0;
			// Parse ID data.
			idData = new IdData(
				id: line[p++].ToInt(),
				name: line[p++],
				pluralName: line[p++]);
			type = (TType) parse(idData, line.Skip(p).ToArray());
			typeDictionary.Add(idData.Id, type);
		}
		return typeDictionary;
	}
	private static TileType ParseTileType(IdData idData, string[] plaintext)
	{
		int p = 0;
		return new TileType(
			idData: idData,
			texture: LoadTexture(FilePaths.Elements.Tile, idData.Name),
			drops: ParseDrops(plaintext[p++]),
			isSolid: plaintext[p++].ToBoolean(),
			breakableBy: ParseBreakableBy(plaintext[p++]),
			friction: (float)Convert.ToDecimal(plaintext[p++])
		);
	}
	private static ItemType ParseItemType(IdData idData, string[] plaintext) {
		int p = 0;
		Texture2D texture = LoadTexture(FilePaths.Elements.Item, idData.Name);
		bool isTool = plaintext[p++].ToBoolean();
		if (isTool) {
			return new ItemType(
				idData, texture,
				toolData: new ToolData(type: plaintext[p++].ToInt(), level: plaintext[p++].ToInt())
				);
		}
		return new ItemType(
			idData, texture,
			tileType: ParseTile(plaintext[p++]),
			stackable: plaintext[p++].ToBoolean()
		);

		// Local functions:
		Maybe<int> ParseTile(string plaintext) {
			if (string.IsNullOrEmpty(plaintext)) {
				return Maybe.None;
			}
			return plaintext.ToInt();
		}
	}
    private static BiomeType ParseBiomeType(IdData idData, string[] plaintext) {
      	int p = 0;
    	return new BiomeType(
    		idData: idData,
    		background: ParseColour(plaintext[p++].Split(Levels.II)),
    		layers: ParseLayers(plaintext[p++]),
    		treeType: plaintext[p++].ToInt(),
    		flowerTypes: ParseLayers(plaintext[p++]),
    		treeAmount: plaintext[p++].ToInt(),
    		chestAmount: plaintext[p++].ToInt()
    	);
    }
    //private static EnemyType ParseEnemyType(IdData idData, string[] plaintext) {
    //	int p = 0;
    //	return new EnemyType(
    //			idData,
    //			texture: LoadTexture(FilePaths.Elements.Enemy, idData.Name),
    //			physicsData: ParsePhysicsData(plaintext[p++]),
    //			healthData: ParseHealthData(plaintext[p++]),
    //			attackData: ParseAttackData(plaintext[p++])
    //	);
    //}
    private static PlayerType ParsePlayerType(IdData idData, string[] plaintext) {
		int p = 0;
    	return new PlayerType(
    		idData,
			texture: LoadTexture(FilePaths.Elements.Player, idData.Name),
			healthData: ParseHealthData(plaintext[p++].Split(Levels.II)),
			physicsData: ParsePhysicsData(plaintext[p++].Split(Levels.II))
		);
    }
    //private static FriendlyType ParseFriendlyType(IdData idData, string[] plaintext) {
    //  var texture = LoadTexture(FilePaths.Elements.Friendly, idData.Name);
    //  int p = 0;
    //	return new FriendlyType(
    //		idData,
    //		texture,
    //		// todo: add parameters
    //	);
    //}

    // Parse components:
    private static int[] ParseLayers(string plaintext) {
		return plaintext.Split(Levels.II).ToInt();
	}
	private static PhysicsData ParsePhysicsData(string[] plaintext) {
		int p = 0;
		return new PhysicsData(
			maxSpeed: Convert.ToSingle(plaintext[p++]),
			range: Convert.ToSingle(plaintext[p++])
		);
	}
	private static Maybe<Drop[]> ParseDrops(string plaintext) {
		if (string.IsNullOrEmpty(plaintext)) {
			return Maybe.None;
		}
		string[] drops_str = plaintext.Split(Levels.II);
		Drop[] drops = new Drop[drops_str.Length];
		for (int i = 0; i < drops.Length; i++) {
			drops[i] = new Drop(drops_str[i].Split(Levels.III).ToInt());
		}
		return drops;
	}
	private static int[] ParseBreakableBy(string plaintext) {
		if (string.IsNullOrEmpty(plaintext)) {
			return Array.Empty<int>();
		}
		return plaintext.Split(Levels.II).ToInt();
    }
	private static HealthData ParseHealthData(string[] plaintext) {
		int p = 0;
		return new HealthData(
			maxHealth: plaintext[p++].ToInt(),
			regenerationAmountPerSecond: plaintext[p++].ToInt()
		);
	}
	private static EntityHealthData ParseEntityHealthData(string plaintext, HealthData typeHealthData) {
		int? currentHealth = String.IsNullOrEmpty(plaintext) ? null : plaintext.ToInt();
		return new EntityHealthData(typeHealthData, currentHealth);
	}
	//private static PowerUp[] ParsePowerUps(string plaintext) {
	//  // TODO: parse powerup
	//  // split into line
	//	// ConvertAll to powerup
	//	// local function to use as converter
	//}

	// Parse Menus
	public static void LoadMenus(IDisplayHandler displayHandler, out ButtonMenuDisplayHandler menuDisplayHandler,
	out List<(string name, Sprite title, string[] items, Vector2 location)> menus) {
		
		string[][] file = LoadCsvFile(FilePaths.MenuTextFile);

		// Load menu display handler:
		int p = 0;
		menuDisplayHandler = new ButtonMenuDisplayHandler(
			distanceBetweenEachButton: Convert.ToSingle(file[0][p++]),
			displayHandler: displayHandler,
			normalButton: LoadTexture(FilePaths.Elements.Menu, FilePaths.ButtonTexture),
			highlightedButton: LoadTexture(FilePaths.Elements.Menu, FilePaths.HighlightedButtonTexture),
			pressedButton: LoadTexture(FilePaths.Elements.Menu, FilePaths.PressedButtonTexture),
			normalTextColour: ParseColour(file[0][p++].Split(Levels.II)),
			highlightedTextColour: ParseColour(file[0][p++].Split(Levels.II)),
			pressedTextColour: ParseColour(file[0][p++].Split(Levels.II)),
			backgroundColour: ParseColour(file[0][p++].Split(Levels.II))
		);
		// Load menus:
		menus = new();
		for(int i = 1; i < file.Length; i++) {
			string[] menu_str = file[i];
			(string name, Sprite title, string[] items, Vector2 location) menu;
			menu.name = menu_str[p++];
			menu.title = new Sprite(LoadTexture(FilePaths.Elements.Menu, FilePaths.TitleTexture), ParseVector(menu_str[p++].Split(Levels.II)), displayHandler);
			menu.items = menu_str[p++].Split(Levels.II);
			menu.location = ParseVector(menu_str[p++].Split(Levels.II));
			menus.Add(menu);
		}
	}
	public static GuiElements LoadGuiElements() {
		string[][] file = LoadCsvFile(FilePaths.GuiData);
		return new GuiElements(
			hotbarSlot: LoadTexture(FilePaths.Elements.Gui, FilePaths.HotbarSlotTexture),
			selectedHotbarSlot: LoadTexture(FilePaths.Elements.Gui, FilePaths.SelectedHotbarSlotTexture),
			hotbarPosition: ParseVector(file[0][0].Split(Levels.II))
		);
	}
	private static Shroomworld.GameStates[] ParseGameStates(string[] plaintext) {
		return Array.ConvertAll(plaintext, gamestate => (Shroomworld.GameStates)(gamestate.ToInt()));
	}
	private static Vector2 ParseVector(string[] plaintext) {
		return new Vector2(Convert.ToSingle(plaintext[0]), Convert.ToSingle(plaintext[1]));
	}
	private static Color ParseColour(string[] plaintext) {
		return new Color(plaintext[0].ToInt(), plaintext[1].ToInt(), plaintext[2].ToInt());
    }

	// Save:
	//public static void SavePlayer(Player player) {
	//}
	//public static void SaveWorld() {
	//	Dictionary<int, Npc> npcs, int[,] tileMap, int width, int height, int difficulty
	//}

	// Helper methods:
	/// <summary>
	/// Converts an array of <paramref name="items"/> into one line of a csv file.
	/// </summary>
	/// <param name="level">Default: Level 1 (comma). Change this if you don't want it to be a csv (i.e. you want to use a different separator character).</param>
	/// <param name="items">The array you want to convert into strings</param>
	/// <returns>the <paramref name="items"/> combined into a string, separated by a chosen separator char.</returns>
	private static Maybe<string> ConvertToCsv(int level = Levels.I, params object[] items) {
		if ((items is null) || (items.Length == 0)) {
			return Maybe.None;
		}
		StringBuilder csv = new StringBuilder(items[0].ToString());
		for(int i = 1; i < items.Length; i++) {
			csv.Append(s_separators_str[level]).Append(items[i].ToString());
		}
		return csv.ToString();
	}
	private static string[] Split(this string stringToSplit, int level) {
		return stringToSplit.Split(s_separators[level]);
	}
	private static int ToInt(this string str) {
		return Convert.ToInt32(str);
	}
	private static bool ToBoolean(this string str) {
		return Convert.ToBoolean(Convert.ToInt32(str));
	}
	private static int[] ToInt(this string[] strArray) {
		return Array.ConvertAll(strArray, item => Convert.ToInt32(item));
	}
}
