using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NpcPrototype2
{
    internal class Npc
    {
        public string Name => _name;
        public bool Dead => _health == 0;

        public event UpdateBehaviourHandler Updated;
        public delegate void UpdateBehaviourHandler(Npc npc);

        private string _name;
        private int _health;
        private int _maxHealth;

        public void OnUpdate()
        {
            Updated?.Invoke(this);
        }
        public void IncreaseHealth(int amount)
        {
            _health = Math.Min(_health + amount, _maxHealth);
        }
        public void DecreaseHealth(int amount)
        {
            _health = Math.Max(_health - amount, 0);
        }
        public void SetVelocity(int xv, int yv)
        {
            Console.WriteLine($"velocity: x: {xv}  y: {yv}");
        }
        public void SetPosition()
        {
            Console.WriteLine("Set position based on velocity.");
        }
        public void SetPosition(int x, int y)
        {
            Console.WriteLine($"Position: x: {x}  y: {y}");
        }
    }
}
