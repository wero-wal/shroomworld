using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
		{ typeof(BiomeType), ParseBiomeType }/*,
		{ typeof(EnemyType), ParseEnemyType },
		{ typeof(FriendlyType), ParseFriendlyType },
		{ typeof(PlayerType), ParsePlayerType }*/
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
		// Load file:
		string path = FilePaths.TypeFiles[FilePaths.ElementForType[typeof(TType)]];
		if (!LoadCsvFile(path).TryGetValue(out string[][] file)) {
			return Maybe.None;
		}

		try {
			return ParseTypes(file);
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
				line = sr.ReadLine().Split(s_separators[Levels.I]);
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
		return Content.Load<Texture2D>(FilePaths.TextureDirectories[element] + itemName);
	}
	
	// Parse types:
	private static Dictionary<int, TType> ParseTypes(string[][] file) where TType : IType {
		Dictionary<int, TType> typeDictionary = new Dictionary<int, TType>(file.Length);;
		Func<IdData, string[], TType> parse = s_parsers[typeof(TType)];
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
			type = parse(idData, line.Skip(p).ToArray());
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
			isSolid: Convert.ToBoolean(plaintext[p++]),
			breakableBy: SplitAtLevel(plaintext[p++], Levels.II).ToInt(),
			friction: (float)Convert.ToDecimal(plaintext[p++])
		);
	}
	private static ItemType ParseItemType(IdData idData, string[] plaintext) {
		int p = 0;
		bool isTool = Convert.ToBoolean(plaintext[p++]);
		if (isTool) {
			return new ItemType(
				idData,
				toolData: new ToolData(type: plaintext[p++].ToInt(), level: plaintext[p++].ToInt())
				);
		}
		return new ItemType(
			idData,
			placeable: Convert.ToBoolean(plaintext[p++]),
			stackable: Convert.ToBoolean(plaintext[p++])
		);
	}
    private static BiomeType ParseBiomeType(IdData idData, string[] plaintext) {
      	int p = 0;
    	return new BiomeType(
    		idData: idData,
    		background: LoadTexture(FilePaths.Elements.Biome, idData.Name),
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
    //private static PlayerType ParsePlayerType(IdData idData, string[] plaintext)
    //{
    //	return new PlayerType(
    //		// todo: add parameters in LoadPlayerTypes()
    //		);
    //}
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
		return SplitAtLevel(plaintext, Levels.II).ToInt();
	}
	private static PhysicsData ParsePhysicsData(string plaintext) {
		return new PhysicsData(plaintext.ToInt());
	}
	private static Drop[] ParseDrops(string plaintext) {
		string[] drops_str = SplitAtLevel(plaintext, Levels.II);
		Drop[] drops = new Drop[drops_str.Length];

		for (int i = 0; i < drops.Length; i++) {
			drops[i] = new Drop(SplitAtLevel(drops_str[i], Levels.III).ToInt());
		}
		return drops;
	}
	private static EntityHealthData ParseEntityHealthData(string plaintext, HealthData typeHealthData) {
		int? currentHealth = String.IsNullOrEmpty(plaintext) ? null : plaintext.ToInt();
		return new EntityHealthData(currentHealth, typeHealthData);
	}
	//private static PowerUp[] ParsePowerUps(string plaintext) {
	//  // TODO: parse powerup
	//  // split into line
	//	// ConvertAll to powerup
	//	// local function to use as converter
	//}

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
	private static string[] SplitAtLevel(string stringToSplit, int level) {
		return stringToSplit.Split(s_separators[level]);
	}
	private static int ToInt(this string str) {
		return Convert.ToInt32(str);
	}
	private static int[] ToInt(this string[] strArray) {
		return Array.ConvertAll(strArray, item => Convert.ToInt32(item));
	}
}
