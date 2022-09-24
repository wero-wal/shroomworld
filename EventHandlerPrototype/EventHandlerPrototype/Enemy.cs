using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandlerPrototype
{
    internal class Enemy
    {
        public event EventHandler<EnemyEventArgs> PerformedAttack;

        private string _type;
        private int _attackStrength;
        private string _attackMessage;


        public Enemy(string type, int attackStrength, string attackMessage = "attacks")
        {
            _type = type;
            _attackStrength = attackStrength;
            _attackMessage = attackMessage;
        }

        public void Attack()
        {
            DisplayHelper.SaveCurrentColours();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{_type} {_attackMessage}.\n");

            DisplayHelper.ResetColours();
            OnPerformedAttack();
        }

        protected void OnPerformedAttack()
        {
            PerformedAttack?.Invoke(this, new EnemyEventArgs() { Type = _type, AttackStrength = _attackStrength });
        }
    }
}
