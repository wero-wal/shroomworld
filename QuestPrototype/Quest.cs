using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestPrototype
{
    internal class Quest
    {
        // ---------- Properties ----------
        public string Name => _name;
        public bool Complete => _complete;

        // ---------- Fields ----------
        private readonly string _name;
        private bool _complete;
        private List<IRequirement> _requirements;

        // ---------- Constructors ----------
        internal Quest(string name, params IRequirement[] requirements)
        {
            _name = name;
            _requirements = new List<IRequirement>(requirements);
            _complete = false;
        }

        // ---------- Methods ----------
        public void Update(Player player)
        {
            if (!_complete) // no need to update if we know it's complete
            {
                _complete = true;
                foreach (IRequirement requirement in _requirements)
                {
                    if (!requirement.Completed) // no need to update if we know it's complete
                    {
                        requirement.Update(player);
                    }
                    if (!requirement.Completed)
                    {
                        _complete = false;
                    }
                }
            }
        }
    }
}
