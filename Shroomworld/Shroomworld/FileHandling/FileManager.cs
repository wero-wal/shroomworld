using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

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
	public static char[] Separators => s_separators;


	// ----- Fields -----
	private readonly static char[] s_separators = { ',', ';', ':' };
	private readonly static string[] s_separators_str = { ",", ";", ":" };

	private delegate Func<IdData, string[], Maybe<IType>> _parser();


	// ----- Methods -----
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
	private static string[] SplitAtLevel(string stringToSplit, int level)
	{
		return stringToSplit.Split(s_separators[level]);
	}

	// Loading
	// Note: exceptions will be handled in LoadTypes and other such high level, public methods.
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
	public static Maybe<Dictionary<int, TType>> LoadTypes<TType>(Func<IdData, string[], TType> parser) where TType : IType {
		// Load file:
		string path = FilePaths.TypeFiles[FilePaths.ElementForType[typeof(TType)]];
		if (!LoadCsvFile(path).TryGetValue(out string[][] file)) {
			return Maybe.None;
		}

		// Parse file:
		Dictionary<int, TType> typeDictionary = new Dictionary<int, TType>(file.Length);;
		TType type = default;

		// The index of the variable being parsed within the line.
		int p;
		IdData idData;
		try {
			// Each line represents one instance of a TType.
			foreach (string[] line in file) {
				p = 0;
				// Parse ID data.
				idData = new IdData(id: line[p++].ToInt(), name: line[p++], pluralName: line[p++]);
				// Try to parse the TType. If it doesn't work, TryGetValue will be false.
				if (!parser(idData, line.Skip(p).ToArray()).TryGetValue(out type)) {
					return Maybe.None;
				}
				typeDictionary.Add(idData.Id, type);
			}
			return typeDictionary;
		}
		catch (Exception) {
			return Maybe.None;
		}			
	}

	// Parse types
	public static TileType ParseTileType(IdData idData, string[] plaintext)
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
	public static ItemType ParseItemType(IdData idData, string[] plaintext) {
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
	//public static EnemyType ParseEnemyType(IdData idData, string[] plaintext)
	//{
	//	int p = 0;
	//	return new EnemyType(
	//			idData,
	//			texture: LoadTexture(FilePaths.Elements.Enemy, idData.Name),
	//			physicsData: ParsePhysicsData(plaintext[p++]),
	//			healthData: ParseHealthData(plaintext[p++]),
	//			attackData: ParseAttackData(plaintext[p++])
	//	);
	//}
	//public static PlayerType ParsePlayerType(IdData idData, string[] plaintext)
	//{
	//	return new PlayerType(
	//		// todo: add parameters in LoadPlayerTypes()
	//		);
	//}
	//public static BiomeType ParseBiomeType(IdData idData, string[] plaintext)
	//{
	//	IdData idData;
	//	Texture background;
	//	biomeType = null;
	//	try
	//	{
	//		idData = new IdData(id, plaintext[p++], plaintext[p++]);
	//		background = TryLoadTexture(FileHandling.FilePaths.TextureDirectories[FilePaths.Elements.Biome]);
	//		biomeType = new BiomeType(
	//			idData: idData,
	//			background: background,
	//			layers: ParseLayers(plaintext[p++]),
	//			treeType: Convert.ToInt32(plaintext[p++]),
	//			flowerTypes: ParseLayers(plaintext[p++]),
	//			treeAmount: Convert.ToInt32(plaintext[p++]),
	//			chestAmount: Convert.ToInt32(plaintext[p++])
	//		);
	//		return true;
	//	}
	//	catch (System.Exception)
	//	{
	//		return false;				
	//	}
	//}
	//public static FriendlyType ParseFriendlyType(IdData idData, string[] plaintext)
	//{
	//	friendlyType = null;
	//	try
	//	{
	//		friendlyType = new FriendlyType(
	//			// todo: add parameters
	//		);
	//		return true;
	//	}
	//	catch (System.Exception)
	//	{
	//		return false;				
	//	}
	//}
	private static int ToInt(this string str) {
		return Convert.ToInt32(str);
	}
	private static int[] ToInt(this string[] strArray)
	{
		return Array.ConvertAll(strArray, item => Convert.ToInt32(item));
	}
		
	// Parse components
	private static IEnumerable<int> ParseLayers(string plaintext)
	{
		foreach (string tileType in SplitAtLevel(plaintext, Levels.II))
		{
			yield return Convert.ToInt32(tileType);
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
	private static PhysicsData ParsePhysicsData(string plaintext)
	{
		return new PhysicsData(plaintext.ToInt());
	}
	private static Drop[] ParseDrops(string plaintext)
	{
		string[] drops_str = SplitAtLevel(plaintext, Levels.II);
		Drop[] drops = new Drop[drops_str.Length];

		for (int i = 0; i < drops.Length; i++)
		{
			drops[i] = new Drop(ToInt(SplitAtLevel(drops_str[i], Levels.III)));
		}
		return drops;
	}
	private static HealthData ParseEntityHealthData(string plaintext, ReadonlyHealthData readonlyHealthData)
	{
		int? currentHealth = String.IsNullOrEmpty(plaintext) ? null : Convert.ToInt32(plaintext);
		return new HealthData(currentHealth, readonlyHealthData);
	}
	//private static PowerUp[] ParsePowerUps(string plaintext) // TODO: parse powerups
	//{
	//	// split into line
	//	// ConvertAll to powerup

	//	// local function to use as converter
	//}

	// Save
	//public static void SavePlayer(Player player)
	//{

	//}
	//public static void SaveWorld(Dictionary<int, Npc> npcs, int[,] tileMap, int width, int height, int difficulty)
	//{
			
	//}
}
