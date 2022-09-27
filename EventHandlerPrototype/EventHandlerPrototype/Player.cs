using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandlerPrototype
{
    internal class Player
    {
        public int Health { get => _health; }
        public int MaxHealth { get => _maxHealth; }


        public event EventHandler HealthChanged;
        public event EventHandler Defeated;

        private int _health;
        private readonly int _maxHealth;
        private int _skillLevel;


        public Player(int maxHealth, int skillLevel)
        {
            _health = maxHealth;
            _maxHealth = maxHealth;
            _skillLevel = skillLevel;
        }


        public void OnDamageTaken(object source, EnemyEventArgs enemyEventArgs)
        {
            if (new Random().Next(0, _skillLevel) == 0) // simulates player being within the enemy's range
            {
                Console.WriteLine($"You yelp as the {enemyEventArgs.Type} hurts you.");
                DecreaseHealth(enemyEventArgs.AttackStrength);

                if (_health == 0)
                {
                    OnDefeated();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"You dodge the {enemyEventArgs.Type}'s attack.");
            }
            Console.WriteLine();
        }

        protected void OnHealthChanged()
        {
            HealthChanged?.Invoke(this, EventArgs.Empty);
        }
        protected void OnDefeated()
        {
            Defeated?.Invoke(this, EventArgs.Empty);
        }

        private void DecreaseHealth(int decreaseBy)
        {
            _health = Math.Max(0, _health - decreaseBy);

            // set display stuff
            DisplayHelper.SaveCurrentColours();
            Console.ForegroundColor = ConsoleColor.Red;
            DisplayHelper.SaveCurrentCursorPosition();
            Console.SetCursorPosition(0, 1);

            // output
            Console.WriteLine($"-{decreaseBy} health point{((decreaseBy == 1) ? "" : "s")}");

            // reset display stuff
            DisplayHelper.ResetColours();
            DisplayHelper.ResetCursorPosition();

            OnHealthChanged();
        }
    }
}
