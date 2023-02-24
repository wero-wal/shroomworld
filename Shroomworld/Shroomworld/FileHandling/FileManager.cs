using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shroomworld.FileHandling;

namespace Shroomworld
{
	internal static class FileManager
	{
		//-----Enums-----
		/// <summary>
		/// used as an enum to keep track of which level of list (or separation) we are in within a file
		/// </summary>
		public struct Levels
		{
			public const int i = 0;
			public const int ii = 1;
			public const int iii = 2;
		}

		// ----- Fields -----
		public static char[] Separators => _separators;

		private static char[] _separators = { ',', ';', ':' };

		// ----- Methods -----
		/// <summary>
		/// Converts an array of <paramref name="items"/> into one line of a csv file.
		/// </summary>
		/// <param name="level">Default: Level 1 (comma). Change this if you don't want it to be a csv (i.e. you want to use a different separator character).</param>
		/// <param name="items">The array you want to convert into strings</param>
		/// <returns>the <paramref name="items"/> combined into a string, separated by a chosen separator char.</returns>
		private static string FormatAsCsv(int level = Levels.i, params object[] items)
		{
			string separator = Separators[level].ToString();

			//Array.ConvertAll(items, item => item.ToString());
			string plainText = items[0].ToString();
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i].ToString();
			}
			return plainText;
		}
		private static string[] SplitAtLevel(string stringToSplit, int level)
		{
			return stringToSplit.Split(_separators[level]);
		}

		// Load
		public static bool TryLoadCsvFile(string path, out Queue<string>[] file)
		{
			List<Queue<string>> file_AsList = new List<Queue<string>>();
			file = null;
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					while (!sr.EndOfStream)
					{
						Queue<string> line = new Queue<string>(sr.ReadLine().Split(Separators[Levels.i]));
						file_AsList.Add(line); // add a line (split by commas into an array)
					}
					sr.Close();
				}
				file = file_AsList.ToArray();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool TryLoadTexture(string directory, string name, out Texture2D texture)
		{
			texture = null;
			try
			{
				texture = Content.Load<Texture2D>(directory + name);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static bool TryLoadTypes<T>(out Dictionary<int, T> typeDictionary) where T : IType
		{
			// Loading
			Queue<string>[] file;
			if (!TryLoadCsvFile(GetPathForType(typeof(T)), out file))
			{
				typeDictionary = null;
				return false;
			}

			// Parsing
			string id;
			int id_int;
			T type;

			typeDictionary = new Dictionary<int, T>(file.Count);

			
			foreach(Queue<string> line in file) // each line represents a type
			{
				if (!( line.TryDequeue(out id) && int.TryParse(id, out id_int) && TryParse<T>(id_int, line, out type))) // try to parse this line
				{
					typeDictionary = null;
					return false; // parsing failed
				}
				typeDictionary.Add(id, type); // parsing succeeded
			}
			return true;
		}
		private static string GetPathForType(Type type)
		{
			switch (type)
			{
				case typeof(TileType):
					return FilePaths.Types[FilePaths.Elements.Tile];
				default:
					return "Not found";
			}
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
				TryLoadTexture(FilePaths.TextureDirs[FilePaths.Elements.Tile], idData.Name, out texture);
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
				background = TryLoadTexture(FileHandling.FilePaths.TextureDirs[FilePaths.Elements.Biome]);
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
