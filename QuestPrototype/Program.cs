using System;

namespace QuestPrototype
{
	class Program
	{
		public enum UserInput
		{
			MoveUp,
			MoveDown,
			MoveLeft,
			MoveRight,

			Interact,

			ToggleQuestMenu,

			Previous,
			Next,
			Okay,
		}

		public static void Main(string args[])
		{
			Player player = new Player();
			UserInput userInput;

			do
			{
				userInput = KeyToInput(Console.ReadKey(true).Key);
				//Console.WriteLine(userInput.ToString()); // testing
				player.Update(userInput);
			} while (true);
		}
		private static UserInput KeyToInput(ConsoleKey key)
		{
			switch (key)
			{
				// Movement
				case ConsoleKey.W:
				case ConsoleKey.UpArrow:
				case ConsoleKey.Spacebar:
					return UserInput.MoveUp;
				case ConsoleKey.S:
				case ConsoleKey.DownArrow:
					return UserInput.MoveDown;
				case ConsoleKey.A:
				case ConsoleKey.LeftArrow:
					return UserInput.MoveLeft;
				case ConsoleKey.D:
				case ConsoleKey.RightArrow:
					return UserInput.MoveRight;

					// Interaction
				case ConsoleKey.C:
					return UserInput.Interact;

					// Accessing menus
				case ConsoleKey.E:
				case ConsoleKey.Tab:
					return UserInput.ToggleQuestMenu;

					// Navugating menus
				case ConsoleKey.B:
					return UserInput.Previous;
				case ConsoleKey.N:
					return UserInput.Next;
				case ConsoleKey.Enter:
					return UserInput.Okay;

					// Exceptions
				default:
					throw new ArgumentException("Key not bound to an action.");
			}
		}
	}
}
