using System;
using System.Collections.Generic;

namespace QuestPrototype
{
    internal class Game
    {
        // ----- Enums -----
		internal enum Key
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
		}

        // ----- Fields -----
        private Dictionary<ConsoleKey, Input> _keyBinds;
        private Player _player;
        private List<Npc> _npcs;

        private event EventHandler[] _keyPressed;

        // ----- Constructors -----
        internal Game()
        {
        }

        // ----- Methods -----
		internal static void Play()
		{
            SetKeyBinds();
            Console.CursorVisible = false;
            _player = new Player(GetString("Name: "), Settings.CentreOfScreen);
			Input input;
			do
			{
                input = GetInput();
                _keyPressed[(int)input]?.Invoke(this, EventArgs.Empty);
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

            _keyPressed[Input.MoveUp] += player.OnMoveUpKeyPressed;
			_keyPressed[Input.MoveDown] += player.OnMoveDownKeyPressed;
			_keyPressed[Input.MoveLeft] += player.OnMoveLeftKeyPressed;
			_keyPressed[Input.MoveRight] += player.OnMoveRightKeyPressed;

			//_keyPressed[Input.Interact] += ...;

			_keyPressed[Input.ToggleQuestMenu] += player.QuestMenu.OnToggleVisibilityKeyPressed;

			//_keyPressed[Input.Previous,
			//_keyPressed[Input.Next,
			//_keyPressed[Input.Enter,]
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
