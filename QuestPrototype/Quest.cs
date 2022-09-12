using System;
using System.Collections.Generic;

namespace QuestPrototype
{
    internal class Quest
    {
        // ----- Properties -----
        internal string Name => _name; // rename to title
        internal bool Complete => _complete;

        // ----- Fields -----
        private readonly string _name;
        private bool _complete;
        private List<IRequirement> _requirements;

        // ----- Constructors -----
        internal Quest(string name, params IRequirement[] requirements)
        {
            _name = name;
            _requirements = new List<IRequirement>(requirements);
            _complete = false;
        }

        // ----- Methods -----
        internal void Update(Player player)
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
