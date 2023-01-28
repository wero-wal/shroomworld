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
		// used as an enum to keep track of which level of list (or separation) we are in within a file
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
		public static bool LoadCsvFile(string path, out Queue<string>[] file)
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

		// Load specifics
		public static bool LoadTexture(string directory, string name, out Texture2D texture)
		{
			try
			{
				texture = Content.Load<Texture2D>(directory + name);
				return true;
			}
			catch (Exception)
			{
				texture = null;
				return false;
			}
		}
		public static bool LoadTileTypes(out List<TileType> tileTypes)
		{
			List<Queue<string>> file;
			if (!LoadCsvFile(FilePaths.TileData, out file))
			{
				tileTypes = null;
				return false;
			}

			tileTypes = new List<TileType>(file.Count);
			int i;
			try
			{
				foreach(Queue<string> line in file) // each line represents a tile
				{
					i = 0;
					tiles.Add(new TileType(
						id: Convert.ToInt32(line.Dequeue()),
						name: line.Dequeue(),
						pluralName: line.Dequeue(),
						drops: ParseDrops(line.Dequeue()),
						isSolid: Convert.ToBoolean(line.Dequeue()),
						breakableBy: ConvertStringArrayToInt(SplitAtLevel(line.Dequeue(), Levels.ii)),
						friction: (float)Convert.ToDecimal(line.Dequeue())));
				}
				return true;
			}
			catch (Exception)
			{
				tileTypes = null;
				return false;
			}
		}
		public static bool LoadItemTypes(out List<ItemType> itemTypes)
		{
			Queue<string>[] file;
			if (!LoadCsvFile(FilePaths.ItemData, out file))
			{
				itemTypes = null;
				return false;
			}

			int i;
			itemTypes = new List<ItemType>(file.Count);
			try
			{
				foreach (Queue<string> line in file)
				{
					i = 0;
					itemTypes.Add(new ItemType(
						id: Convert.ToInt32(line.Dequeue()),
						name: line.Dequeue(),
						pluralName: line.Dequeue(),
						canBePlaced: Convert.ToBoolean(line.Dequeue()),
						stackable: Convert.ToBoolean(line.Dequeue())));
				}
				return true;
			}
			catch(Exception)
			{
				itemTypes = null;
				return false;
			}
		}
		public static bool LoadEnemyTypes(out List<EnemyType> enemyTypes)
		{
			Queue<string>[] file;
			if (!LoadCsvFile(FilePaths.EnemyData, out file))
			{
				enemyTypes = null;
				return false;
			}

			int i;
			enemyTypes = new List<EnemyType>(file.Count);
			try
			{
				foreach (Queue<string> line in file)
				{
					i = 0;
					enemyTypes.Add(new EnemyType(
						// parameters here
					));
				}
				return true;
			}
			catch(Exception)
			{
				enemyTypes = null;
				return false;
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
		public static bool LoadPlayerTypes(out List<PlayerTemplate> playerTemplates)
		{
			// TODO: LoadPLayerTypes()
			playerTemplates = null;
		}
		public static bool LoadBiomeTypes(out List<BiomeType> biomeTypes)
		{
			// TODO: LoadBiomeTypes()
			Queue<string>[] file;
			if (!LoadCsvFile(FilePaths.BiomeData, out file))
			{
				biomeTypes = null;
				return false;
			}

			biomeTypes = new List<BiomeType>(file.Count);
			int i;
			try
			{
				foreach (Queue<string> line in file)
				{
					i = 0;
					biomeTypes.Add(new BiomeType(
						// parameters here
					));
				}
				return true;
			}
			catch(Exception)
			{
				return false;
			}

		}
		public static bool LoadFriendlyTypes(out List<FriendlyType> friendlyTypes)
		{
			// TODO: LoadFriendlyTypes()
			friendlyTypes = null;
		}

		// Parsing
		private static MovementData ParseMovementData(string plaintext, int containingLevel) // i.e. what symbol is around the movement data? commas? semi-colons?
		{
			int[] line = ConvertStringArrayToInt(SplitAtLevel(plaintext, containingLevel + 1));
			int p = 0;
			return new MovementData(new Vector2(line.Dequeue(), line.Dequeue()));
		}
		private static List<Drop> ParseDrops(string plaintext)
		{
			List<Drop> drops;
			string[] allDrops = SplitAtLevel(plaintext, Levels.ii);
			drops = new List<Drop>(allDrops.Length);

			foreach (string drop in allDrops)
			{
				drops.Add(new Drop(ConvertStringArrayToInt(SplitAtLevel(drop, Levels.iii))));
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

		// Saving
		public static void SavePlayer(Player player)
		{

		}
		public static void SaveWorld(List<Npc> npcs, int[,] tileMap, int width, int height, int difficulty)
		{
			
		}
		private static int[] ConvertStringArrayToInt(string[] strArray)
		{
			return Array.ConvertAll(strArray, item => Convert.ToInt32(item));
		}
	}
}
