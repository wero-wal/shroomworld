using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shroomworld
{
	internal static class File
	{
		// ---------- Fields ----------
		// Directory names
		public const string TextureDirectory = "textures/";
		public const string TileDirectory = "tiles/";
		public const string EnemyDirectory = "enemies/";
		public const string NpcDirectory = "npcs/";


		// File names
		public const string TileFile = "tiles.txt";

		// Separators
		public static char[] Separators = { ',', ' ', ';' };

		// an enum would make the statement way too long ( e.g. File.Level.Primary ).
		public const int Primary = 0;
		public const int Secondary = 1;
		public const int Tertiary = 2;


		private static string[] Separator_Strings = { ",", " ", ";" };

		// ---------- Methods ----------
		public static string FormatAsPlainText(params object[] items, int separatorLevel)
		{
			string separator = Separator_Strings[separatorLevel];

			string plainText = items[0];
			for(int i = 1; i < items.Length; i++)
			{
				plainText += separator + items[i].ToString();
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
