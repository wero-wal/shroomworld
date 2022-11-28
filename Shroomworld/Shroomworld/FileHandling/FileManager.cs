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
		public static class Level
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
		/// converts an array of <paramref name="items"/> into one line of a csv file.
		/// </summary>
		/// <param name="level">Default: comma. Change this if you don't want it to be a csv (i.e. you want to use a different separator character).</param>
		/// <param name="items"></param>
		/// <returns>the <paramref name="items"/> combined into a string, separated by a chosen separator char.</returns>
		private static string FormatAsCsv(int level = Level.i, params object[] items)
		{
			string separator = Separators[level].ToString();

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
					lines.Add(sr.ReadLine().Split(Separators[Level.i])); // add a line (split by commas into an array)
				}
				sr.Close();
			}
			return lines;
		}

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
					breakableBy: StringToIntArray(SplitAtLevel(line[i++], Level.ii)),
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
		public static List<NpcType> LoadEnemyTypes()
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
					stackable: Convert.ToBoolean(line[i++]),
					));
			}
			return itemTypes;
		}

		private static List<Drop> ParseDrops(string plaintext)
		{
			List<Drop> drops;
			string[] allDrops = SplitAtLevel(plaintext, Level.ii);
			drops = new List<Drop>(allDrops.Length);

			foreach (string drop in allDrops)
			{
				drops.Add(new Drop(StringToIntArray(SplitAtLevel(drop, Level.iii))));
			}
			return drops;
		}
        private static int[] StringToIntArray(string[] strArray)
		{
			int[] intArray = new int[strArray.Length];
			for (int i = 0; i < strArray.Length; i++)
			{
				intArray[i] = Convert.ToInt32(strArray[i]);
			}
			return intArray;
		}
	}
}
