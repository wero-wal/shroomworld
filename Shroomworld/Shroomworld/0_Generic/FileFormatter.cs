using System;

namespace Shroomworld
{
	internal static class FileFormatter
	{
		// ---------- Enums ----------
		// ---------- Properties ----------
		// ---------- Fields ----------
		public const string PrimarySeparator = ",";
		public const string SecondarySeparator = " ";
		public const string TertiarySeparator = ";";

		public const char PrimarySeparator_Char = ',';
		public const char SecondarySeparator_Char = ' ';
		public const char TertiarySeparator_Char = ';';

		// ---------- Methods ----------
		public static string FormatAsPlainText<T>(params T[] items, string separator)
		{
			string plainText = items[0];
			foreach(T item in items)
			{
				plainText += separator + item.ToString();
			}
			return plainText;
		}
		public static string FormatAsPlainText(params string[] items, string separator)
		{
			string plainText = items[0];
			foreach(string item in items)
			{
				plainText += separator + item;
			}
			return plainText;
		}
	}
}
