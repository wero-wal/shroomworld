using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandlerPrototype
{
    internal class Npc
    {
        private string _name;
        private int _health;
        private readonly int _maxHealth;
        private readonly int _skillLevel;


        public Npc(string name, int maxHealth, int skillLevel)
        {
            _name = name;
            _health = maxHealth;
            _maxHealth = maxHealth;
            _skillLevel = skillLevel;
        }


        public void OnDamageTaken(object source, EnemyEventArgs enemyEventArgs)
        {
            if (_health == 0)
            {
            }
            else if (new Random().Next(0, _skillLevel) == 0) // simulates npc being within the enemy's range
            {
                Console.Write($"{enemyEventArgs.Type} hurts {_name}.");
                DecreaseHealth(enemyEventArgs.AttackStrength);
            }
            else
            {
                Console.WriteLine($"{_name} dodges the {enemyEventArgs.Type}'s attack.");
            }
            DisplayCurrentHealth();
            Console.WriteLine();
        }

        private void DecreaseHealth(int decreaseBy)
        {
            _health = Math.Max(0, _health - decreaseBy);

            DisplayHelper.SaveCurrentColours();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" (-{decreaseBy})");

            DisplayHelper.ResetColours();
        }
        private void DisplayCurrentHealth()
        {
            DisplayHelper.SaveCurrentColours();

            if (_health > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Health: {_health} / {_maxHealth}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{_name} has been defeated.");
            }

            DisplayHelper.ResetColours();
        }
    }
}
