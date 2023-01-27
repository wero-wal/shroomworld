using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
	internal static class FileManager
	{
		//-----Enums-----
		// used as an enum to keep track of which level of list (or separation) we are in within a file
		public static struct Levels
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
		public static List<string[]> LoadCsvFile(string path)
		{
			List<string[]> lines = new List<string[]> { };
			using (StreamReader sr = new StreamReader(path))
			{
				while (!sr.EndOfStream)
				{
					lines.Add(sr.ReadLine().Split(Separators[Levels.i])); // add a line (split by commas into an array)
				}
				sr.Close();
			}
			return lines;
		}

		// Load specifics
		public static Texture2D LoadTexture(string directory, string name)
		{
			return Content.Load<Texture2D>(directory + name);
		}
		public static List<TileType> LoadTileTypes()
		{
			List<string[]> file = LoadCsvFile(FilePaths.TileData);
			List<TileType> tiles = new List<TileType>(file.Count);
			int i;
			foreach(string[] line in file) // each line represents a tile
			{
				i = 0;
				tiles.Add(new TileType(
					id: Convert.ToInt32(line[i++]),
					name: line[i++],
					pluralName: line[i++],
					drops: ParseDrops(line[i++]),
					isSolid: Convert.ToBoolean(line[i++]),
					breakableBy: ConvertStringArrayToInt(SplitAtLevel(line[i++], Levels.ii)),
					friction: (float)Convert.ToDecimal(line[i++])));
			}
			return tiles;
		}
		public static List<ItemType> LoadItemTypes()
		{
			List<string[]> file = LoadCsvFile(FilePaths.ItemData);
			List<ItemType> itemTypes = new List<ItemType>(file.Count);

			int i;
			foreach (string[] line in file)
			{
				i = 0;
				itemTypes.Add(new ItemType(
					id: Convert.ToInt32(line[i++]),
					name: line[i++],
					pluralName: line[i++],
					canBePlaced: Convert.ToBoolean(line[i++]),
					stackable: Convert.ToBoolean(line[i++])));
			}
			return itemTypes;
		}
		public static List<EnemyType> LoadEnemyTypes()
		{
			List<string[]> file = LoadCsvFile(FilePaths.ItemData);
			List<ItemType> itemTypes = new List<NpcType>(file.Count);

			int i;
			foreach (string[] line in file)
			{
				i = 0;
				itemTypes.Add(new NpcType(
					id: Convert.ToInt32(line[i++]),
					name: line[i++],
					pluralName: line[i++],
					canBePlaced: Convert.ToBoolean(line[i++]),
					stackable: Convert.ToBoolean(line[i++])
					));
			}
			return itemTypes;
		}
		public static Player LoadPlayer(int id)
		{
			// todo: sort loadplayer out
			string[] parts = LoadCsvFile(FilePaths.AllUsers); // load player file
            int p = 0; // index to keep track of which part we're on
			PlayerTemplate type = Shroomworld.PlayerTypes[Convert.ToInt32(parts[p++])];
			return new Player(
				type: type,
				sprite: new Sprite(LoadTexture(parts[p++])),
            	powerUps: new PowerUp(ParsePowerUps(parts[p++])),
            	healthData: new HealthData(ParseHealthData(parts[p++]), type.ReadonlyHealthData),
				shieldData: new HealthData(ParseHealthData(parts[p++])),
            	attack: new AttackAndBoostInfo(_powerUps.Damage),
            	inventory: ParseInventory(parts[p++]), // doesn't even need to be stored as 2-dimensional if we store inventory height / width in settings
            	quests: ParseQuests(parts[p++]),
            	statistics: ParseStatistics(parts[p++]));
		}
		public static List<PlayerTemplate> LoadPlayerTypes()
		{
			// TODO: LoadPLayerTypes()
		}
		public static List<BiomeType> LoadBiomeTypes()
		{
			// TODO: LoadBiomeTypes()
		}
		public static List<FriendlyTypes> LoadFriendlyTypes()
		{
			// TODO: LoadFriendlyTypes()
		}

		// Parsing
		private static MovementData ParseMovementData(string plaintext, int containingLevel) // i.e. what symbol is around the movement data? commas? semi-colons?
		{
			int[] parts = ConvertStringArrayToInt(SplitAtLevel(plaintext, containingLevel + 1));
			int p = 0;
			return new MovementData(new Vector2(parts[p++], parts[p++]));
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
			// split into parts
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
