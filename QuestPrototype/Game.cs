using System;
using System.Collections.Generic;

namespace QuestPrototype
{
    internal class Game
    {
        // ----- Enums -----
		internal enum Input
		{
			MoveUp,
			MoveDown,
			MoveLeft,
			MoveRight,

			Interact,

			ToggleQuestMenu,

			Previous,
			Next,
			Enter,

            DoNothing,
		}

        // ----- Fields -----
        private Dictionary<ConsoleKey, Input> _keyBinds;
        private Player _player;
        private List<Npc> _npcs;

        // ----- Constructors -----
        internal Game()
        {
        }

        // ----- Methods -----
		internal static void Play()
		{
            SetKeyBinds();
            Console.CursorVisible = false;
            _player = new Player(GetString("Name: "));
			Input Input;
			do
			{
                Input = GetInput();
				player.Update(Input);
			} while (true);
		}

        // Setup
		private void SetKeyBinds()
		{
            _keyBinds = new Dictionary<ConsoleKey, Input>
            {
				// Movement
                { ConsoleKey.W, Input.MoveUp },
                { ConsoleKey.UpArrow, Input.MoveUp },
                { ConsoleKey.Spacebar, Input.MoveUp },

                { ConsoleKey.S, Input.MoveDown },
                { ConsoleKey.DownArrow, Input.MoveDown },

                { ConsoleKey.A, Input.MoveLeft },
                { ConsoleKey.LeftArrow, Input.MoveLeft },

                { ConsoleKey.D, Input.MoveRight },
                { ConsoleKey.RightArrow, Input.MoveRight },

                // Interaction
                { ConsoleKey.C, Input.Interact },

                // Accessing menus
                { ConsoleKey.E, Input.ToggleQuestMenu },
                { ConsoleKey.Tab, Input.ToggleQuestMenu },

                // Navigating menus
                { ConsoleKey.B, Input.Previous },
                { ConsoleKey.N, Input.Next },
                { ConsoleKey.Enter, Input.Enter },
            };
		}
        private void CreateNpcs(int amount)
        {
            List<Point> usedPoints;
            for(int i = 0; i < amount; i++)
            {
                
            }
        }

        // Inputs
        private Input GetInput()
        {
            try
            {
                return _keyBinds[Console.ReadKey(true).Key];
            }
            catch(KeyNotFoundException)
            {
                return Input.DoNothing;
            }
        }
        private string GetString(string prompt)
        {
            // prompt
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt);

            // get input
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool cursorVisible = Console.CursorVisible;
            Console.CursorVisible = true;
            string input = Console.ReadLine();

            // un-change state
            Console.CursorVisible = cursorVisible;
            Console.ResetColor();

            return input;
        }
    }
}
