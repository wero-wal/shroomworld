using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
	internal static class File
	{
		// ---------- Fields ----------
		public const string[] Separator_Strings = { ",", " ", ";" };
		public const char[] Separator_Chars = { ',', ' ', ';' };
		public const int Primary = 0;
		public const int Secondary = 1;
		public const int Tertiary = 2;

		// Directory names
		public const string TextureDirectory = "textures/";
		public const string TileDirectory = "tiles/";
		public const string EnemyDirectory = "enemies/";


		// File names
		public const string TileFile = "tiles.txt";

		// ---------- Methods ----------
		public static string FormatAsPlainText(params object[] items, string separator)
		{
			string plainText = items[0];
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i].ToString();
			}
			return plainText;
		}
		public static string FormatAsPlainText(params object[] items, int separatorLevel)
		{
			string separator = Separator_Strings[separator];

			string plainText = items[0];
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i].ToString();
			}
			return plainText;
		}
		public static string FormatAsPlainText(params string[] items, string separator) // duplicate method for string becaue the ToString method is not needed
		{
			string plainText = items[0];
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i];
			}
			return plainText;
		}
		public static string FormatAsPlainText(params string[] items, int separatorLevel) // duplicate method for string becaue the ToString method is not needed
		{
			string separator = Separator_Strings[separator];
			string plainText = items[0];
			for (int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i];
			}
			return plainText;
		}
		
		public static Texture LoadTexture(string typeDirectory, string textureFileName)
		{
			return Content.Load<Texture>(TextureDirectory + type + textureFileName);
		}
	}
}
