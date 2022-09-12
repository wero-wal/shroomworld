using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class LocationRequirement : IRequirement
    {
        // ---------- Properties ----------
        public string Description => $"Go to point ({_target.X}, {_target.Y}).";
        public bool Completed => _isCompleted;

        // ---------- Fields ----------
        private Point _target;
        private bool _isCompleted;

        // ---------- Methods ----------
        public void Update(Player player)
        {
            _isCompleted = player.Position == _target;
        }
    }
}
