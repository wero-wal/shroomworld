using System;
using System.Threading;
using System.Collections.Generic;

namespace EventHandlerPrototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var enemies = new List<Enemy>
            {
                new Enemy("dragon", 6, "breathes a scathing flame"),
                new Enemy("bear", 9, "swipes"),
                new Enemy("goblin", 4, "lashes out with its freakishly long fingernails"),
                new Enemy("boss", 20, "smashes its fists into the ground"),
            };
            var npcs = new List<Npc>
            {
                new Npc("Billy", 100, 0),
                new Npc("Venus", 150, 3),
                new Npc("Jinx", 300, 2),
                new Npc("Lola", 300, 1),
            };
            int health = 100 * new Random().Next(2, 5);
            int skill = new Random().Next(0, 4);
            Player player = new Player(health, skill);

            // Subscribe to events
            foreach (Enemy enemy in enemies)
            {
                foreach (Npc npc in npcs)
                {
                    enemy.PerformedAttack += npc.OnDamageTaken;
                }
                enemy.PerformedAttack += player.OnDamageTaken;
            }
            player.HealthChanged += OnPlayerHealthChanged;
            player.Defeated += OnPlayerDefeated;

            // Game loop
            ConsoleKey input;
            do
            {
                Console.Clear();
                UpdatePlayerHealthDisplay(player);

                Console.SetCursorPosition(0, 3);
                Console.ForegroundColor = ConsoleColor.Gray;
                enemies[new Random().Next(0, enemies.Count)].Attack();
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press any key to continue, or [Esc] to exit.\n");
                input = Console.ReadKey(true).Key;

            } while ((input != ConsoleKey.Escape) && (player.Health > 0));

            Console.Clear();
            Console.ForegroundColor = (player.Health == 0) ? ConsoleColor.Red : ConsoleColor.Blue;
            Console.WriteLine("Game over!");
            Thread.Sleep(1000);
        }

        private static void UpdatePlayerHealthDisplay(Player player)
        {
            DisplayHelper.SaveCurrentCursorPosition();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Player health: {player.Health} / {player.MaxHealth}\n");

            DisplayHelper.ResetCursorPosition();
        }

        private static void OnPlayerHealthChanged(object source, EventArgs eventArgs)
        {
            UpdatePlayerHealthDisplay((Player)source);
        }
        private static void OnPlayerDefeated(object source, EventArgs eventArgs)
        {
            Console.ReadKey(true);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have been defeated!");
        }
    }
}
