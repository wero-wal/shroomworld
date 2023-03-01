using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld.FileHandling {
	public static class FileManager {

		//----- Enums -----
		/// <summary>
		/// Used as an enum to keep track of which level of list (or separation) we are in within a file.
		/// </summary>
		public struct Levels {
			public const int i = 0;
			public const int ii = 1;
			public const int iii = 2;
		}


		// ----- Properties -----
		public static char[] Separators => s_separators;


		// ----- Fields -----
		private readonly static char[] s_separators = { ',', ';', ':' };
		private readonly static string[] s_separators_str = { ",", ";", ":" };


		// ----- Methods -----
		/// <summary>
		/// Converts an array of <paramref name="items"/> into one line of a csv file.
		/// </summary>
		/// <param name="level">Default: Level 1 (comma). Change this if you don't want it to be a csv (i.e. you want to use a different separator character).</param>
		/// <param name="items">The array you want to convert into strings</param>
		/// <returns>the <paramref name="items"/> combined into a string, separated by a chosen separator char.</returns>
		private static Maybe<string> ConvertToCsv(int level = Levels.i, params object[] items) {
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
		public static Maybe<Queue<string>[]> LoadCsvFile(string path) {
			List<Queue<string>> file = new List<Queue<string>>();
			string line;
			try {
				using (StreamReader sr = new StreamReader(path)) {
					while (!sr.EndOfStream) {
						// Split the line using commas as separators.
						line = sr.ReadLine().Split(s_separators[Levels.i]);
						file.Add(new Queue<string>(line));
					}
					sr.Close();
				}
				return file.ToArray();
			}
			catch (Exception) {
				// The file was not found or was formatted incorrectly.
				return Maybe.None;
			}
		}
		public static Maybe<Texture2D> LoadTexture(string path) {
			try {
				return Content.Load<Texture2D>(path);
			}
			catch (Exception) {
				return Maybe.None;
			}
		}
		public static Maybe<Dictionary<int, T>> LoadTypes<T>() where T : IType {
			// Load file.
			if (!LoadCsvFile(GetPathForType(typeof(T)))
				.TryGetValue(out Queue<string>[] file)) {
				return Maybe.None;
			}

			// Parse file.
			Dictionary<int, T> typeDictionary;
			string id;
			int id_int;
			T type;

			typeDictionary = new Dictionary<int, T>(file.Count);

			// Each line represents a type of x.
			foreach (Queue<string> line in file) {
				// Try to parse this line.
				if (!(line.TryDequeue(out id) && int.TryParse(id, out id_int)
				&& Parse<T>(id_int, line).TryGetValue(out type))) {
					// Parsing failed.
					return Maybe.None;
				}
				typeDictionary.Add(id, type);
			}
			return typeDictionary;
		}

		// Parse types
		public static bool TryParse<T>(int id, Queue<string> plaintext, out T? type) where T : IType
		{
			throw new NotImplementedException();
		}
		public static bool TryParse<TileType>(int id, Queue<string> plaintext, out TileType tileType)
		{
			tileType = null;
			IdentifyingData idData;
			Texture texture;
			try
			{
				idData = new IdentifyingData(id, name: plaintext.Dequeue(), pluralName: plaintext.Dequeue());
				TryLoadTexture(FilePaths.TextureDirectories[FilePaths.Elements.Tile], idData.Name, out texture);
				tileType = new TileType
				(
					idData: idData,
					texture: texture,
					drops: ParseDrops(plaintext.Dequeue()),
					isSolid: Convert.ToBoolean(plaintext.Dequeue()),
					breakableBy: ConvertStringArrayToInt(SplitAtLevel(plaintext.Dequeue(), Levels.ii)),
					friction: (float)Convert.ToDecimal(plaintext.Dequeue())
				);
				return true;
			}
			catch (Exception e)
			{
				/* Check for expected exceptions
				   ("expected" because I know the way they will arise will be if the files are corrupted /
				   incorrectly formatted, and this should be the only reason for an exception to be thrown)
				   */ 
				//if ((e is IndexOutOfRangeException) // not enough items in queue
				//|| (e is FormatException)) // format error in one/more of: isSolid, breakableBy, friction
				//{
					return false;
				//}
				//throw;
			}
		}
		public static bool TryParse<ItemType>(int id, Queue<string> plaintext, out ItemType? itemType)
		{
			string name;
			string pluralName;
			bool isTool;
			itemType = null;
			try
			{
				name = plaintext.Dequeue();
				pluralName = plaintext.Dequeue();
				isTool = Convert.ToBoolean(plaintext.Dequeue());
				if (isTool)
				{
					itemType = new ItemType(
						id, name, pluralName,
						toolData: new ToolData(
							type: Convert.ToInt32(plaintext.Dequeue()),
							level: Convert.ToInt32(plaintext.Dequeue()))
					);
				}
				else
				{
					itemType = new ItemType(
						id, name, pluralName,
						canBePlaced: Convert.ToBoolean(plaintext.Dequeue()),
						stackable: Convert.ToBoolean(plaintext.Dequeue())
					);
				}
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
		public static bool TryParse<EnemyType>(int id, Queue<string> plaintext, out EnemyType? enemyType)
		{
			enemyTypes = null;
			try
			{
				new EnemyType(
					id: id,
					// todo: add parameters in LoadEnemyTypes()
				);
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}
		public static bool TryParse<PlayerTemplate>(int id, Queue<string> plaintext, out PlayerTemplate? playerTemplate)
		{
			playerTemplate = null;
			try
			{
				playerTemplate = new PlayerTemplate(
					// todo: add parameters in LoadPlayerTypes()	
				);
				return true;
			}
			catch (System.Exception)
			{
				return false;				
			}
		}
		public static bool TryParse<BiomeType>(int id, Queue<string> plaintext, out BiomeType? biomeType)
		{
			IdentifyingData idData;
			Texture background;
			biomeType = null;
			try
			{
				idData = new IdentifyingData(id, plaintext.Dequeue(), plaintext.Dequeue());
				background = TryLoadTexture(FileHandling.FilePaths.TextureDirectories[FilePaths.Elements.Biome]);
				biomeType = new BiomeType(
					idData: idData,
					background: background,
					layers: ParseLayers(plaintext.Dequeue()),
					treeType: Convert.ToInt32(plaintext.Dequeue()),
					flowerTypes: ParseLayers(plaintext.Dequeue()),
					treeAmount: Convert.ToInt32(plaintext.Dequeue()),
					chestAmount: Convert.ToInt32(plaintext.Dequeue())
				);
				return true;
			}
			catch (System.Exception)
			{
				return false;				
			}
		}
		public static bool TryParse<FriendlyType>(int id, Queue<string> plaintext, out FriendlyType? friendlyType)
		{
			friendlyType = null;
			try
			{
				friendlyType = new FriendlyType(
					// todo: add parameters
				);
				return true;
			}
			catch (System.Exception)
			{
				return false;				
			}
		}
		
		// Parse components
		private static int[] ParseLayers(string plaintext)
		{
			foreach (string tileType in SplitAtLevel(plaintext, Levels.ii))
			{
				yield return Convert.ToInt32(tileType);
			}
		}
		public static bool LoadPlayer(int id, out Player player)
			{
				// todo: sort loadplayer out
				Queue<string>[] file;
				player = null;
	
				if (!LoadCsvFile(FilePaths.AllUsers, out file))
				{
					return false;
				}
	
				Textutre2D texture;
				try
				{
					Queue<string> line = file[0];
					PlayerTemplate type = Shroomworld.PlayerTypes[Convert.ToInt32(line.Dequeue())];
					if (!LoadTexture(line.Dequeue(), out texture))
					{
						return false;
					}
					player = new Player(
						type: type,
						sprite: new Sprite(texture),
						powerUps: new PowerUp(ParsePowerUps(line.Dequeue())),
						healthData: new HealthData(ParseHealthData(line.Dequeue()), type.ReadonlyHealthData),
						shieldData: new HealthData(ParseHealthData(line.Dequeue())),
						attack: new AttackAndBoostInfo(_powerUps.Damage),
						inventory: ParseInventory(line.Dequeue()), // doesn't even need to be stored as 2-dimensional if we store inventory height / width in settings
						quests: ParseQuests(line.Dequeue()),
						statistics: ParseStatistics(line.Dequeue()));
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
		private static MovementData ParseMovementData(string plaintext, int containingLevel) // i.e. what symbol is around the movement data? commas? semi-colons?
		{
			int[] line = ConvertStringArrayToInt(SplitAtLevel(plaintext, containingLevel + 1));
			int p = 0;
			return new MovementData(new Vector2(line.Dequeue(), line.Dequeue()));
		}
		private static Drop[] ParseDrops(string plaintext)
		{
			string[] drops_str = SplitAtLevel(plaintext, Levels.ii);
			Drop[] drops = new Drop[drops_str.Length];

			for (int i = 0; i < drops.Length; i++)
			{
				drops[i] = new Drop(ConvertStringArrayToInt(SplitAtLevel(drops_str[i], Levels.iii)));
			}
			return drops;
		}
		private static HealthData ParseHealthData(string plaintext, ReadonlyHealthData readonlyHealthData)
		{
			int? currentHealth = String.IsNullOrEmpty(plaintext) ? null : Convert.ToInt32(plaintext);
			return new HealthData(currentHealth, readonlyHealthData);
		}
		private static PowerUp[] ParsePowerUps(string plaintext) // TODO: parse powerups
		{
			// split into line
			// ConvertAll to powerup

			// local function to use as converter
		}

		// Save
		public static void SavePlayer(Player player)
		{

		}
		public static void SaveWorld(Dictionary<int, Npc> npcs, int[,] tileMap, int width, int height, int difficulty)
		{
			
		}
		private static int[] ConvertStringArrayToInt(string[] strArray)
		{
			return Array.ConvertAll(strArray, item => Convert.ToInt32(item));
		}
	}
}
