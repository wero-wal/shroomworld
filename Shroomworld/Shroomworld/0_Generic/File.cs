using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
	internal static class File
	{
		// ----- Fields -----
		public static char[] Separators = { ',', ';', ':' };

		public static class Level
		{
			public const int i = 0;
			public const int ii = 1;
			public const int iii = 2;
		}

		// ----- Methods -----
		public static string FormatAsCsv(int level, params object[] items)
		{
			string separator = Separators[level].ToString();

			string plainText = items[0].ToString();
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i].ToString();
			}
			return plainText;
		}
		public static List<string[]> LoadCsvFile(string path)
		{
			List<string> lines = new List<string> { };
			List<string[]> linesSplitIntoParts = new List<string[]> { };
			using (StreamReader sr = new StreamReader(path))
			{
				while (!sr.EndOfStream)
				{
					lines.Add(sr.ReadLine());
					linesSplitIntoParts.Add(lines[^1].Split(Separators[Level.i]));
				}
				sr.Close();
			}
			return linesSplitIntoParts;
		}

		public static Texture2D LoadTexture(string directory, string name)
		{
			return Content.Load<Texture2D>(directory + name);
		}
	}
}
